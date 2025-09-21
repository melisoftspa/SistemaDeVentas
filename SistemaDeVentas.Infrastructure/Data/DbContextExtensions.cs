using System.ComponentModel;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SistemaDeVentas.Infrastructure.Data
{
    public partial class SalesSystemDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales del modelo si son necesarias

            // Configuraciones para campos DTE en Sale
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.DteFolio).HasColumnName("dte_folio");
                entity.Property(e => e.DteStatus).HasColumnName("dte_status").HasMaxLength(50);
                entity.Property(e => e.DteType).HasColumnName("dte_type").HasMaxLength(10);
                entity.Property(e => e.CafId).HasColumnName("caf_id");
                entity.Property(e => e.DteSentDate).HasColumnName("dte_sent_date");
            });

            // Configuración para tabla Caf si se usa
            modelBuilder.Entity<Caf>(entity =>
            {
                entity.ToTable("caf");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.RutEmisor).HasColumnName("rut_emisor").HasMaxLength(12);
                entity.Property(e => e.TipoDocumento).HasColumnName("tipo_dte");
                entity.Property(e => e.FolioDesde).HasColumnName("folio_desde");
                entity.Property(e => e.FolioHasta).HasColumnName("folio_hasta");
                entity.Property(e => e.FechaAutorizacion).HasColumnName("fecha_autorizacion");
                entity.Property(e => e.XmlContent).HasColumnName("xml_caf");
                entity.Property(e => e.Activo).HasColumnName("estado");
                entity.Property(e => e.FolioActual).HasColumnName("folio_actual");
                entity.Property(e => e.Ambiente).HasColumnName("ambiente");
                entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            });
        }

        public async Task<DataTable> ExecReturnQuery(FormattableString query)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                if (command.Connection.State.Equals(ConnectionState.Closed)) { command.Connection.Open(); }
                command.CommandText = query.ToString();
                using var result = command.ExecuteReaderAsync().Result;
                var table = new DataTable();
                table.Load(result);
                // returning DataTable (instead of DbDataReader), cause can't use DbDataReader after CloseConnection().
                if (command.Connection.State.Equals(ConnectionState.Open)) { command.Connection.Close(); }
                return table;
            }
        }

        public IList<T> ReturnQuery<T>(FormattableString query)
        {
            List<T> dataList = new List<T>();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                if (command.Connection.State.Equals(ConnectionState.Closed)) { command.Connection.Open(); }
                command.CommandText = query.ToString();
                using var reader = command.ExecuteReaderAsync().Result;
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dataList.Add((T)reader[i]);
                    }
                }
                if (command.Connection.State.Equals(ConnectionState.Open)) { command.Connection.Close(); }
            }
            return dataList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> RunQuery<T>(string query, SqlParameter[] parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            var entities = new List<T>();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                if (command.Connection.State.Equals(ConnectionState.Closed)) { command.Connection.Open(); }
                using var reader = command.ExecuteReader();
                var properties = typeof(T).GetProperties();
                var param = parameters.Where(i => i.Direction == ParameterDirection.Output);
                if (param.Count() > 0)
                {
                    foreach (var data in param)
                    {
                        // output handling later
                    }
                }
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        if (properties.Length > 0)
                        {
                            // try to map simple types
                            if (properties.Length == 1)
                            {
                                entities.Add((T)Convert.ChangeType(value, typeof(T)));
                            }
                            else
                            {
                                // complex mapping skipped
                            }
                        }
                    }
                }
                if (command.Connection.State.Equals(ConnectionState.Open)) { command.Connection.Close(); }
            }
            return entities;
        }
    }
}
