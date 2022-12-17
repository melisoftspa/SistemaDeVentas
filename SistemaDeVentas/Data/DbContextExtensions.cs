using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SistemaDeVentas.Data
{
    public partial class SalesSystemDbContext
    {
        public virtual DbSet<UpdateProduct> UpdateProducts { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UpdateProduct>().HasNoKey();
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
                //this.Database.CloseConnection();
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
        public List<T> RunQuery<T>(string query, SqlParameter[] parameters)
        {
            var entities = new List<T>();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                //this.Database.OpenConnection();
                command.CommandText = query;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                if (command.Connection.State.Equals(ConnectionState.Closed)) { command.Connection.Open(); }
                command.ExecuteNonQuery();                
                using var reader = command.ExecuteReaderAsync().Result;
                var properties = typeof(T).GetProperties();
                while (reader.Read())
                {
                    var t = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(t, Convert.ChangeType(reader[property.Name], convertTo), null);
                        }
                    }
                }
                var param = parameters.Where(i => i.Direction == ParameterDirection.Output);
                if(param.Count() > 0)
                {
                    foreach(var data in param)
                    {
                        entities.Add((T)data.Value);
                    }
                }
                if (command.Connection.State.Equals(ConnectionState.Open)) { command.Connection.Close(); }
            }
            return entities;
        }
    }
}
