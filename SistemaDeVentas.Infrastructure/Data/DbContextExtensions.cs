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
            // Configuraciones para User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.InInvoice).HasColumnName("in_invoice");
            });

            // Configuraciones para Sale
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.IdUser).HasColumnName("id_user");
                entity.Property(e => e.PaymentCash).HasColumnName("payment_cash");
                entity.Property(e => e.PaymentOther).HasColumnName("payment_other");
                entity.Property(e => e.DteGenerated).HasColumnName("dte_generated");
                entity.Property(e => e.DteXml).HasColumnName("dte_xml");
                entity.Property(e => e.DteFolio).HasColumnName("dte_folio");
                entity.Property(e => e.DteStatus).HasColumnName("dte_status");
                entity.Property(e => e.DteType).HasColumnName("dte_type");
                entity.Property(e => e.CafId).HasColumnName("caf_id");
                entity.Property(e => e.DteSentDate).HasColumnName("dte_sent_date");
                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
                entity.Property(e => e.PaymentTransactionId).HasColumnName("payment_transaction_id");
                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            });

            // Configuraciones para Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnName("date");
                entity.Property(e => e.IsActive).HasColumnName("state");
                entity.Property(e => e.SalePrice).HasColumnName("sale_price");
                entity.Property(e => e.Barcode).HasColumnName("bar_code");
                entity.Property(e => e.Exenta).HasColumnName("exenta");
                entity.Property(e => e.IdTax).HasColumnName("id_tax");
                entity.Property(e => e.IdCategory).HasColumnName("id_category");
                entity.Property(e => e.IsPack).HasColumnName("isPack");
                entity.Property(e => e.IdPack).HasColumnName("id_pack");
                entity.Property(e => e.IdSubcategory).HasColumnName("id_subcategory");
            });

            // Configuraciones para Detail
            modelBuilder.Entity<Detail>(entity =>
            {
                entity.Property(e => e.IdSale).HasColumnName("id_sale");
                entity.Property(e => e.IdProduct).HasColumnName("id_product");
                entity.Property(e => e.ProductName).HasColumnName("product_name");
                entity.Property(e => e.TotalTax).HasColumnName("total_tax");
            });

            // Configuraciones para Tax
            modelBuilder.Entity<Tax>(entity =>
            {
                entity.Property(e => e.Exenta).HasColumnName("exenta");
            });

            // Configuraciones para Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.IsActive).HasColumnName("state");
            });

            // Configuraciones para Decrease
            modelBuilder.Entity<Decrease>(entity =>
            {
                entity.Property(e => e.IdProduct).HasColumnName("id_product");
            });

            // Configuraciones para Entry
            modelBuilder.Entity<Entry>(entity =>
            {
                entity.Property(e => e.IdProduct).HasColumnName("id_product");
                entity.Property(e => e.NameProduct).HasColumnName("name_product");
                entity.Property(e => e.IdUser).HasColumnName("id_user");
            });

            // Configuraciones para Historical
            modelBuilder.Entity<Historical>(entity =>
            {
                entity.Property(e => e.IdUser).HasColumnName("id_user");
                entity.Property(e => e.IdProduct).HasColumnName("id_product");
            });

            // Configuraciones para Invoice
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.NameProvider).HasColumnName("name_provider");
                entity.Property(e => e.StatePayment).HasColumnName("state_payment");
                entity.Property(e => e.PaymentCheck).HasColumnName("payment_check");
                entity.Property(e => e.PaymentCheckNumber).HasColumnName("payment_check_number");
                entity.Property(e => e.PaymentCheckDate).HasColumnName("payment_check_date");
                entity.Property(e => e.PaymentCheckTotal).HasColumnName("payment_check_total");
                entity.Property(e => e.PaymentCash).HasColumnName("payment_cash");
                entity.Property(e => e.PaymentCashTotal).HasColumnName("payment_cash_total");
            });

            // Configuraciones para Pack
            modelBuilder.Entity<Pack>(entity =>
            {
                entity.Property(e => e.IdPack).HasColumnName("id_pack");
                entity.Property(e => e.IdProduct).HasColumnName("id_product");
                entity.Property(e => e.BarCode).HasColumnName("bar_code");
            });

            // Configuraciones para Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.ActionAdd).HasColumnName("actionAdd");
                entity.Property(e => e.ActionEdit).HasColumnName("actionEdit");
                entity.Property(e => e.ActionDelete).HasColumnName("actionDelete");
            });

            // Configuraciones para Subcategory
            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.Property(e => e.IdCategory).HasColumnName("id_category");
            });

            // Configuración para tabla Caf
            modelBuilder.Entity<Caf>(entity =>
            {
                entity.ToTable("caf");
                entity.Property(e => e.TipoDocumento).HasColumnName("tipo_documento");
                entity.Property(e => e.FolioDesde).HasColumnName("folio_desde");
                entity.Property(e => e.FolioHasta).HasColumnName("folio_hasta");
                entity.Property(e => e.FechaAutorizacion).HasColumnName("fecha_autorizacion");
                entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
                entity.Property(e => e.XmlContent).HasColumnName("xml_content");
                entity.Property(e => e.Ambiente).HasColumnName("ambiente");
                entity.Property(e => e.RutEmisor).HasColumnName("rut_emisor");
                entity.Property(e => e.FolioActual).HasColumnName("folio_actual");
                entity.Property(e => e.Activo).HasColumnName("activo");
            });

            // Configuración para tabla DteLog
            modelBuilder.Entity<DteLog>(entity =>
            {
                entity.ToTable("dte_log");
                entity.Property(e => e.SaleId).HasColumnName("sale_id");
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(50);
                entity.Property(e => e.Message).HasColumnName("message").HasMaxLength(1000);
                entity.Property(e => e.ErrorCode).HasColumnName("error_code").HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.DteFolio).HasColumnName("dte_folio");
                entity.Property(e => e.DteType).HasColumnName("dte_type").HasMaxLength(10);

                entity.HasOne(d => d.Sale)
                    .WithMany(p => p.DteLogs)
                    .HasForeignKey(d => d.SaleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para tabla CertificateData
            modelBuilder.Entity<CertificateData>(entity =>
            {
                entity.ToTable("certificate_data");
                entity.Property(e => e.Nombre).HasColumnName("certificate_name").HasMaxLength(255);
                entity.Property(e => e.RutEmisor).HasColumnName("rut_emisor").HasMaxLength(12);
                entity.Property(e => e.Ambiente).HasColumnName("ambiente");
                entity.Property(e => e.DatosCertificado).HasColumnName("certificate_content");
                entity.Property(e => e.PasswordEncriptado).HasColumnName("password").HasMaxLength(255);
                entity.Property(e => e.Activo).HasColumnName("is_active");
                entity.Property(e => e.FechaCreacion).HasColumnName("created_at");
                entity.Property(e => e.FechaVencimiento).HasColumnName("expires_at");
            });

            // Relaciones adicionales para Sale
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasOne(d => d.Caf)
                    .WithMany()
                    .HasForeignKey(d => d.CafId)
                    .OnDelete(DeleteBehavior.SetNull);
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
