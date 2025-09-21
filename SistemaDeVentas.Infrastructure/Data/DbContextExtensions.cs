using System.ComponentModel;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SistemaDeVentas.Core.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SistemaDeVentas.Infrastructure.Data
{
    public partial class SalesSystemDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales del modelo si son necesarias
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
