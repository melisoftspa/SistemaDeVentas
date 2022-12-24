using LibPrintTicket;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Data;
using SistemaDeVentas.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System.Windows.Shapes;
using static System.Windows.Forms.LinkLabel;
using Newtonsoft.Json.Linq;
using static Azure.Core.HttpHeader;
using System.Text;

namespace SistemaDeVentas
{
    public static class ConDB
    {
        internal static Form mainForm;

        internal static string cadena;

        //private static SqlConnection conDB = new SqlConnection();

        private static readonly SalesSystemDbContext Context = new SalesSystemDbContext();

        internal static int userId = -1;

        internal static int userRole = 0;

        internal static List<Role> roles { get; set; }

        internal static bool userNameInvoice = false;

        internal static string userName = string.Empty;

        internal static DataTable current_invoice_detailsDataTable;

        internal static List<string> data { get; set; }

        public static bool OpenDB { get; set; }

        internal static bool openConnection()
        {
            try
            {
                Context.Database.SetConnectionString(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al consegurir configurarcion o coneccion esta ya iniciada: " + ex.ToString());
            }
            try
            {
                OpenDB = Context.Database.CanConnect();
                Console.WriteLine("coneccion a base de datos iniciada");
                return true;
            }
            catch (Exception ex2)
            {
                Console.WriteLine("fallo conneccion a base de datos: " + ex2.ToString());
                OpenDB = false;
                return false;
            }
        }

        internal static float getTotalPaymentCash(string start, string end)
        {
            try
            {
                string sql = $@"SELECT (isnull(sum(payment_cash),0) - isnull(sum(change),0)) + (select isnull(sum(total),0) from dbo.sale with(nolock) where state = 1 and payment_cash = 0 and payment_other = 0 and date >='{start}T00:00:00' and date <='{end}T23:59:59') as Value from dbo.sale with(nolock) where state = 1 and date >='{start}T00:00:00' and date <='{end}T23:59:59'";
                FormattableString text = $"{sql}";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Payment Cash", ex.ToString());
                return 0f;
            }
        }

        internal static DataTable getInvoices(string start, string end)
        {
            try
            {
                FormattableString text = $@"select * from dbo.invoice with(nolock) where state = 1 and date >='{start}T00:00:00' and date <='{end}T23:59:59';";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Invoices", ex.ToString());
                return new DataTable();
            }
        }

        internal static float getTotalPaymentOtherCash(string start, string end)
        {
            try
            {
                string sql = $@"select isnull(sum(payment_other),0) from dbo.sale with(nolock) where state = 1 and date >='{start}T00:00:00' and date <='{end}T23:59:59'";
                FormattableString text = $"{sql}";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Payment Other Cash", ex.ToString());
                return 0f;
            }
        }

        internal static CultureInfo getCultureInfo()
        {
            openConnection();
            return CultureInfo.GetCultureInfo(getParameters("POS", "GetCultureInfo"));
        }

        internal static DataTable getListOfDecrease(string start, string end)
        {
            try
            {
                FormattableString text = $@"select date, line from decrease with(nolock) where date >='{start}T00:00:00' and date <='{end}T23:59:59';";
                DataTable dataTable = Context.ExecReturnQuery(text).Result;

                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("Fecha");
                dataTable2.Columns.Add("Nombre");
                dataTable2.Columns.Add("Cantidad");
                dataTable2.Columns.Add("IVA");
                dataTable2.Columns.Add("Total");
                dataTable2.Columns.Add("Nota");
                foreach (DataRow row in dataTable.Rows)
                {
                    string line = (string)row["line"];
                    if (line.Contains(","))
                    {
                        string[] array = line.Split(',');
                        DataRow dataRow2 = dataTable2.NewRow();
                        dataRow2.ItemArray = new object[6]
                        {
                        row["date"],
                        array[0].Split(':')[1],
                        array[1].Split(':')[1],
                        array[2].Split(':')[1],
                        array[3].Split(':')[1],
                        array[4].Split(':')[1]
                        };
                        dataTable2.Rows.Add(dataRow2);
                    }
                    else
                    {
                        dataTable2.Rows.Add(row.ItemArray);
                    }
                }
                return dataTable2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new DataTable();
            }
        }

        internal static DataTable getCategoryList(bool subcategory = false)
        {
            try
            {
                FormattableString text = $@"select id, Text from category with(nolock) order by Value asc;";
                if (subcategory)
                {
                    text = $@"select id, Text, id_category from subcategory with(nolock) order by Value asc;";
                }
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new DataTable();
            }
        }

        internal static DataTable getProductsListPack(string idpack_product)
        {
            try
            {
                FormattableString text = $@"select pp.id_product as id, pp.name, p.price, pp.amount, pp.bar_code AS barcode from pack pp inner join product p on pp.id_pack = p.id_pack where pp.id_pack = '{idpack_product}'";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new DataTable();
            }
        }

        internal static float Round(string price)
        {
            int num = int.Parse(price.Substring(price.Length - 1));
            if (num < 5)
            {
                return int.Parse(price) - num;
            }
            return int.Parse(price) + (10 - num);
        }

        internal static float getTotalSalesExenta(string start, string end)
        {
            try
            {
                string sql = $@"select isnull(sum(d.total),0) as Value from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and p.exenta = 1 and s.date >='{start}T00:00:00' and s.date <='{end}T23:59:59'";
                FormattableString text = $"{sql}";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Sales Exenta", ex.ToString());
                return 0f;
            }
        }

        internal static float getTotalSalesAfecta(string start, string end, bool profit = false)
        {
            try
            {
                string sql = $@"select isnull(sum(d.total),0) + (select isnull(sum(d.total),0) from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and p.exenta is null and s.date >='{start}T00:00:00' and s.date <='{end}T23:59:59') as Value from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and p.exenta = 0 and s.date >='{start}T00:00:00' and s.date <='{end}T23:59:59'";
                FormattableString text = $"{sql}";
                float num = float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
                if (profit)
                {
                    num -= getTotalSalesExenta(start, end);
                }
                return num;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total sales", ex.ToString());
                return 0f;
            }
        }

        internal static float getTotalSalesCategory(string start, string end, string id)
        {
            try
            {
                string sql = $@"select isnull(sum(d.total),0) from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.date >='{start}T00:00:00' and s.date <='{end}T23:59:59' and p.id_category = '{id}'";
                FormattableString text = $"{sql}";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total sales", ex.ToString());
                return 0f;
            }
        }

        internal static DataTable getTotalSalesSubCategory(string start, string end, string id)
        {
            try
            {
                FormattableString text = $@"select sc.text, isnull(SUM(d.total),0) as total from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product left join subcategory sc on sc.id = p.id_subcategory where s.date >='{start}T00:00:00' and s.date <='{end}T23:59:59' and sc.id_category = '{id}' group by sc.text, sc.id_category";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Sales SubCategory", ex.ToString());
                return new DataTable();
            }
        }

        internal static DataTable getTaxList()
        {
            try
            {
                FormattableString text = $@"select id, Value from tax with(nolock);";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new DataTable();
            }
        }

        internal static string getTax(Guid id)
        {
            try
            {
                return Context.Taxes.Where(i => i.Id == id).Select(i => i.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        internal static bool getTaxIsExenta(Guid id)
        {
            try
            {
                return Context.Taxes.Where(i => i.Id == id).Select(i => i.Exenta).FirstOrDefault().Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        internal static void UpdatePackProduct(string idpack_product, string id_product, string name, string amount, string bar_code)
        {
            CreatePackProduct(idpack_product, id_product, name, amount, bar_code);
        }

        internal static void closeConnection()
        {
            try
            {
                Context.Database.CloseConnection();
                Console.WriteLine("coneccion a base de datos terminada");
            }
            catch (Exception ex)
            {
                Console.WriteLine("fallo al cerrar la coneccion: " + ex.ToString());
            }
        }

        /// <summary>
        /// Agrega nuevo usuario al sistema con perfil de vendedor.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pasword"></param>
        /// <param name="name"></param>
        /// <param name="inInvoice"></param>
        /// <returns></returns>
        internal static bool newUser(string username, string pasword, string name, bool inInvoice)
        {
            try
            {
                //text = $"INSERT INTO users ([username],[password],[role],[name],[in_invoice]) VALUES ('{username}',PWDENCRYPT('{pasword}'),0,'{name}','{inInvoice}')";
                User user = new User()
                {
                    Username = username,
                    Password = pasword,
                    Name = name,
                    Role = "3",
                    InInvoice = inInvoice
                };
                Context.Users.Add(user);
                Context.SaveChanges();
                Context.Database.ExecuteSqlInterpolated($@"UPDATE users SET password = PWDENCRYPT({pasword}) WHERE id = {user.Id}");
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get user id", ex.ToString());
                return false;
            }
        }

        internal static bool login(string username, string pasword)
        {
            try
            {
                userId = getUserID(username, pasword);
                if (userId == -1)
                {
                    return false;
                }
                userRole = getUserRole();
                userName = getUserName().ToUpper();
                userNameInvoice = bool.Parse(getUserName(inVoince: true));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        internal static DataTable getProductsList(string word = "", bool isSales = false, bool isInventory = false, bool isPurchase = false)
        {
            try
            {
                string text = "select dbo.product.* from dbo.product with(nolock) INNER JOIN dbo.entries with(nolock) ON dbo.product.id = dbo.entries.id_product where dbo.entries.state = 1 and dbo.product.amount > 0 ";
                if (isInventory)
                {
                    text = "select dbo.product.* from dbo.product with(nolock) where 1=1 ";
                }
                if (isPurchase)
                {
                    text = "select * from product where id not in (select id_product from entries) ";
                }
                if (string.IsNullOrEmpty(word))
                {
                    if (isSales)
                    {
                        text += "and dbo.product.sale_price is not null and dbo.product.sale_price > 0 ";
                    }
                    text = ((!isInventory) ? (text + "and dbo.product.state = 1 and dbo.product.amount >= dbo.product.minimum;") : (text + "and dbo.product.state = 1 "));
                }
                else
                {
                    if (isSales)
                    {
                        text += "and dbo.product.sale_price is not null and dbo.product.sale_price > 0 ";
                    }
                    if (!isInventory)
                    {
                        text += "and dbo.product.amount >= dbo.product.minimum ";
                    }
                    text += text + $"and dbo.product.state = 1 and (coalesce(convert(nvarchar(max), dbo.product.name), '') + coalesce(convert(varchar(max), dbo.product.amount), '') + coalesce(convert(varchar(max), dbo.product.sale_price), '') + coalesce(convert(varchar(max), dbo.product.minimum), '') + coalesce(convert(varchar(max), dbo.product.bar_code), '') + coalesce(convert(varchar(max), dbo.product.stock), '') + coalesce(convert(varchar(max), dbo.product.price), '') + coalesce(convert(varchar(max), dbo.product.line), '') ) like '%{word}%';";
                }
                FormattableString query = $@"{text}";
                DataTable dataTable = Context.ExecReturnQuery(query).Result;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("name", "asc");
                dataTable.DefaultView.Sort = string.Join(",", dictionary.Select((KeyValuePair<string, string> x) => x.Key + " " + x.Value).ToArray());
                dataTable = dataTable.DefaultView.ToTable();
                
                DataColumn dcRowString = dataTable.Columns.Add("_RowString", typeof(string));
                foreach (DataRow row in dataTable.Rows)
                {
                    row["name"] = row["name"].ToString().ToUpper();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dataTable.Columns.Count - 1; i++)
                    {
                        if(dataTable.Columns[i].ColumnName == "name" || dataTable.Columns[i].ColumnName == "sale_price" || dataTable.Columns[i].ColumnName == "bar_code")
                        {
                            sb.Append(row[i].ToString());
                            sb.Append("\t");
                        }
                    }
                    row[dcRowString] = sb.ToString();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new DataTable();
            }
        }

        internal static void CancelSale(int nroticket)
        {
            try
            {
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "nroticket", Value = nroticket, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                };
                Context.RunQuery<int>("dbo.cancel_sale", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("insert decrease", ex.ToString());
            }
        }

        internal static bool getDecreaseCode(string decreaseCode, DataTable products, string note)
        {
            if (getParameters("VENTA", "MERMA").Equals(decreaseCode))
            {
                try
                {
                    IEnumerator enumerator = products.Rows.GetEnumerator();
                    try
                    {
                        if (enumerator.MoveNext())
                        {
                            DataRow dataRow = (DataRow)enumerator.Current;
                            string text = dataRow["id"].ToString();
                            string text2 = dataRow["amount"].ToString();
                            float num = float.Parse(dataRow["tax"].ToString()) / 100f;
                            float num2 = float.Parse(validNumber(dataRow["total"].ToString()));
                            float num3 = float.Parse(Math.Ceiling(num2 * num).ToString());
                            string value = string.Format("nombre: {0}, cantidad: {1}, IVA: {2}, total: {3}, nota: {4}, id product: {5}", dataRow["name"], text2, num3, num2, note, text);
                            Context.Decreases.FromSqlInterpolated($@"dbo.create_decrease {text}, {float.Parse(text2.Replace("$", "").Replace(".", ""))}, {num3}, {num2}, {note}, {value}, {userId}");
                            return true;
                        }
                    }
                    finally
                    {
                        IDisposable disposable = enumerator as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    log("insert decrease", ex.ToString());
                    return false;
                }
            }
            return false;
        }

        internal static bool CreateProduct(string name, string amount, string price, string minimum, string barcode, string tax, bool exenta, DateTime expiration, string category, bool isPack, string idpack, string idsubcategory)
        {
            string query = "procedure";
            try
            {
                string value = $"nombre: {name}, cantidad: {amount}, precio compra: {price}, stock: {minimum}, codigo: {barcode}, id IVA: {tax}, Exenta: {exenta}, fecha Vencimiento: {expiration.ToShortDateString()}, id Categoria: {category}, Es Pack: {isPack}, id pack: {idpack}, id SubCategoria: {idsubcategory}";
                int num = 0;
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "name", Value = name, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "amount", Value = amount, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "minimum", Value = minimum, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "price", Value = price, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "barcode", Value = barcode, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "line", Value = value, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "tax", Value = tax, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "exenta", Value = exenta, DbType = DbType.Boolean},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "expiration", Value = expiration.ToShortDateString(), DbType = DbType.DateTime },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "category", Value = category, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "isPack", Value = isPack, DbType = DbType.Boolean },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "idPack", Value = idpack, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "idsubcategory", Value = idsubcategory, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Output, ParameterName = "res", Value = num },
                };
                num = int.Parse(Context.RunQuery<string>("dbo.create_product", parameters).FirstOrDefault());

                return num == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("create product " + name, ex.ToString(), query);
                return false;
            }
        }

        internal static void CreatePackProduct(string id, string idproducto, string name, string amount, string barcode)
        {
            try
            {
                string value = "nombre: " + name + ", cantidad: " + amount + ", codigo: " + barcode + ", id producto: " + idproducto + ", id pack: " + id;
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "id", Value = id, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "id", Value = idproducto, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "name", Value = name, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "amount", Value = amount, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "barcode", Value = barcode, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "line", Value = value, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                };
                Context.RunQuery<int>("dbo.create_pack", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("Create Pack Product", ex.ToString());
            }
        }

        internal static bool UpdateProduct(string id, string name, string amount, string price, string minimum, string barcode, string tax, bool exenta, DateTime expiration, string category, bool isPack, string idpack_product, string idsubcategory)
        {
            try
            {
                string line = $"nombre: {name}, cantidad: {amount}, precio compra: {price}, stock: {minimum}, codigo: {barcode}, id IVA: {tax}, Exenta: {exenta}, fecha Vencimiento: {expiration.ToShortDateString()}, Es Pack: {isPack} , id Categoria: {category}, id pack: {idpack_product}, id SubCategoria: {idsubcategory}";
                int num = 0;
                var parameters = new SqlParameter[] { 
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "id", Value = id, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "name", Value = name, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "amount", Value = amount, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "minimum", Value = minimum, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "price", Value = price, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "barcode", Value = barcode, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "line", Value = line, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "tax", Value = tax, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "exenta", Value = exenta, DbType = DbType.Boolean},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "expiration", Value = expiration, DbType = DbType.DateTime },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "category", Value = category, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "isPack", Value = isPack, DbType = DbType.Boolean },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "id_pack", Value = idpack_product, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "idsubcategory", Value = idsubcategory, DbType = DbType.String},
                    new SqlParameter() { Direction = ParameterDirection.Output, ParameterName = "res", Value = num },
                };

                num = int.Parse(Context.RunQuery<string>("dbo.update_product", parameters).FirstOrDefault());
                return num == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("update product", ex.ToString());
                return false;
            }
        }

        internal static bool DeleteProduct(string id, string name)
        {
            try
            {
                int num = 0;
                name = "nombre: " + name;
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "id", Value = id, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "line", Value = name, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Output, ParameterName = "res", Value = num },
                };
                num = int.Parse(Context.RunQuery<string>("dbo.delete_product", parameters).FirstOrDefault());
                return num == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("delete procuct", ex.ToString());
                return false;
            }
        }

        internal static bool RegisterWareHouseEntry(string id_product, string name, string price, string amount)
        {
            try
            {
                string value = price.Replace(",", ".");
                string value2 = amount.Replace(",", ".");
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "productID", Value = id_product, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "productName", Value = name, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "amount", Value = value2, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "total", Value = value, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                };
                Context.RunQuery<string>("dbo.create_entry", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("Register WareHous Entry", ex.ToString());
                return false;
            }
        }

        internal static bool DeleteWareHouseEntry(string id_product)
        {
            try
            {
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "productID", Value = id_product, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                };
                Context.RunQuery<string>("dbo.delete_entry", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("Register WareHous Entry", ex.ToString());
                return false;
            }
        }

        internal static bool makeBackup(string dir)
        {
            try
            {
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "folder", Value = dir, DbType = DbType.String },
                };
                Context.RunQuery<string>("dbo.backupInventoryDB", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("make backup", ex.ToString());
                return false;
            }
        }

        internal static Guid insertSale(float amount, float tax, float total, string notes, string payment_cash, string payment_other, string change, ref int ticket)
        {
            try
            {
                var _rs = new Guid();
                var rs = new Guid();
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "amount", Value = amount, DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "tax", Value = tax, DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "total", Value = total, DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "notes", Value = notes, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "payment_cash", Value = float.Parse(validNumber(payment_cash)), DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "payment_other", Value = float.Parse(validNumber(payment_other)), DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "change", Value = float.Parse(validNumber(change)), DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.Int16 },
                    new SqlParameter() { Direction = ParameterDirection.Output, ParameterName = "saleID", Value = rs, DbType = DbType.Guid },
                    new SqlParameter() { Direction = ParameterDirection.Output, ParameterName = "ticket", Value = ticket, DbType = DbType.Int64 }
                };

                var result = Context.RunQuery<string>("dbo.create_sale", parameters, CommandType.StoredProcedure).ToList();
                foreach(var data in result)
                {
                    if(data != null)
                    {
                        if(Guid.TryParse(data, out _rs)){ rs = _rs; } 
                        else if (int.TryParse(data, out ticket)) { }
                    }
                }
                return rs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("insert sale", ex.ToString());
                return default(Guid);
            }
        }

        internal static bool insertDetailSale(Guid id_sale, Guid id_product, string product_name, string price, string amount, string total, string tax, string total_tax)
        {
            try
            {
                var parameters = new SqlParameter[] {
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "idSale", Value = id_sale, DbType = DbType.Guid },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "idProduct", Value = id_product, DbType = DbType.Guid },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "name", Value = product_name, DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "price", Value = float.Parse(validNumber(price)), DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "amount", Value = float.Parse(validNumber(amount)), DbType = DbType.Double },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "tax", Value = float.Parse(validNumber(tax)), DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "total", Value = float.Parse(validNumber(total)), DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "total_tax", Value = float.Parse(validNumber(total_tax)), DbType = DbType.String },
                    new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "userID", Value = userId, DbType = DbType.String },
                };

                Context.RunQuery<string>("dbo.create_detail", parameters);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("insert detail sale", ex.ToString());
                return false;
            }
        }

        internal static string validString(string word)
        {
            return Regex.Replace(word.Trim().ToLower(), "[^0-9a-zA-ZñÑ.,:\\/'& ]", "", RegexOptions.None);
        }

        internal static string validNumber(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return "0";
            }
            try
            {
                return float.Parse(word.Trim().Replace("¤", "").Replace(".", "")
                    .Replace("$", "")
                    .Replace(",00", "")).ToString().Replace(",", ".");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("valid number", ex.ToString(), "");
                return "error";
            }
        }

        private static int getUserID(string username, string pwd)
        {
            try
            {
                FormattableString text = $"SELECT id FROM dbo.users WHERE username={username} AND PWDCOMPARE({pwd}, password) = 1";
                return Context.Users.FromSqlInterpolated(text).Select(i => i.Id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get user id", ex.ToString());
                return -1;
            }
        }

        private static int getUserRole()
        {
            try
            {
                int rol = int.Parse(Context.Users.Where(i => i.Id == userId).FirstOrDefault().Role);
                roles = Context.Roles.Where(i => i.RoleId== rol).ToList();
                return rol;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get user role", ex.ToString());
                return 0;
            }
        }

        private static string getUserName(bool inVoince = false)
        {
            try
            {
                return inVoince ? Context.Users.Where(i => i.Id == userId).FirstOrDefault().InInvoice.ToString() : Context.Users.Where(i => i.Id == userId).FirstOrDefault().Name.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get User Name", ex.ToString());
                return string.Empty;
            }
        }

        internal static bool changePassword(string new_pwd, string name, bool inInvoice)
        {
            try
            {
                FormattableString text = $"update dbo.users set password=PWDENCRYPT({new_pwd}), name={name},in_invoice={inInvoice} where id={userId}";
                return bool.Parse(Context.Database.ExecuteSqlInterpolated(text).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("chenge password", ex.ToString());
                return false;
            }
        }

        internal static DataTable Current_invoice_details(int current_invoice_nroTicket)
        {
            try
            {
                FormattableString text = $"select d.amount, d.product_name as name, d.total, d.price from sale s inner join detail d on s.id = d.id_sale where s.ticket = {current_invoice_nroTicket}";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Current_invoice_details", ex.ToString());
                return new DataTable();
            }
        }

        private static void log(string type, string error, string? query = "")
        {
            string path = $"{Environment.CurrentDirectory}\\log_{DateTime.Now.ToShortDateString()}";
            try
            {
                if (!File.Exists(path))
                {
                    using StreamWriter streamWriter = File.CreateText(path);
                    streamWriter.WriteLine("Log de Errores del sistema");
                }
                using StreamWriter streamWriter2 = File.AppendText(path);
                streamWriter2.WriteLine(type);
                if (query != "")
                {
                    streamWriter2.WriteLine(query);
                }
                streamWriter2.WriteLine(error);
                streamWriter2.WriteLine(DateTime.Now.ToString());
                streamWriter2.WriteLine("");
            }
            catch (Exception)
            {
            }
        }

        internal static object getVendorName()
        {
            throw new NotImplementedException();
        }

        internal static List<string> getDataHeadPos()
        {
            List<string> result = new List<string>();
            try
            {
                return getParameters("BOLETA", "CABECERA").Split('|').ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get by code bar", ex.ToString());
                return result;
            }
        }

        internal static string getParameters(string module, string name)
        {
            try
            {
                return Context.Parameters.Where(i => i.Module == module && i.Name == name).Select(i => i.Value).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Parameters", ex.ToString());
                return string.Empty;
            }
        }

        internal static List<string> getProductByBarcode(string code, bool isSales = false)
        {
            try
            {
                FormattableString text = $@"select REPLACE(REPLACE(REPLACE(p.id, Char(9), ''), Char(10), ''), Char(13), '') AS id, 
                                            REPLACE(REPLACE(REPLACE(p.name, Char(9), ''), Char(10), ''), Char(13), '') AS name, 
                                            REPLACE(REPLACE(REPLACE(p.sale_price, Char(9), ''), Char(10), ''), Char(13), '') AS prise, 
                                            REPLACE(REPLACE(REPLACE(p.amount, Char(9), ''), Char(10), ''), Char(13), '') AS amount, 
                                            REPLACE(REPLACE(REPLACE(p.minimum, Char(9), ''), Char(10), ''), Char(13), '') AS minimum, 
                                            REPLACE(REPLACE(REPLACE(replace(t.Text, '.',','), Char(9), ''), Char(10), ''), Char(13), '') AS tax, 
                                            REPLACE(REPLACE(REPLACE(p.exenta, Char(9), ''), Char(10), ''), Char(13), '') AS exenta 
                                            from dbo.product p with(nolock) inner join dbo.tax t with(nolock) on p.id_tax = t.id where p.state = 1 
                                            and p.bar_code='{code}'";
                if (isSales)
                {
                    text = $@"select REPLACE(REPLACE(REPLACE(p.id, Char(9), ''), Char(10), ''), Char(13), '') AS id, 
                                            REPLACE(REPLACE(REPLACE(p.name, Char(9), ''), Char(10), ''), Char(13), '') AS name, 
                                            REPLACE(REPLACE(REPLACE(p.sale_price, Char(9), ''), Char(10), ''), Char(13), '') AS prise, 
                                            REPLACE(REPLACE(REPLACE(p.amount, Char(9), ''), Char(10), ''), Char(13), '') AS amount, 
                                            REPLACE(REPLACE(REPLACE(p.minimum, Char(9), ''), Char(10), ''), Char(13), '') AS minimum, 
                                            REPLACE(REPLACE(REPLACE(replace(t.Text, '.',','), Char(9), ''), Char(10), ''), Char(13), '') AS tax, 
                                            REPLACE(REPLACE(REPLACE(p.exenta, Char(9), ''), Char(10), ''), Char(13), '') AS exenta
                                            from dbo.product p with(nolock) inner join dbo.tax t with(nolock) on p.id_tax = t.id inner join dbo.entries e with(nolock) ON e.id_product = p.id where p.state = 1 and e.state = 1
                                            and p.bar_code='{code}'";
                }
                return Context.ReturnQuery<string>(text).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get by code bar", ex.ToString());
                return new List<string>();
            }
        }

        internal static DataTable getListOfSales(string start, string end)
        {
            try
            {
                FormattableString text = $@"select id, date, name, amount, tax, total, id_user, change, payment_cash, payment_other, ticket, note from dbo.sale with(nolock) where state = 1 and date>='{start}T00:00:00' and date<='{end}T23:59:59' order by date desc";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get list of sales", ex.ToString());
                return new DataTable();
            }
        }

        internal static DataTable getListOfDetails(string idSale)
        {
            try
            {
                FormattableString text = $"select * from dbo.detail with(nolock) where id_sale='{idSale}'";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get list od details", ex.ToString());
                return new DataTable();
            }
        }

        internal static float getTotalSales(string start = "", string end = "", int userid = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(start))
                {
                    start = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(end))
                {
                    end = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                FormattableString text = ((userid <= 0) ? ((FormattableString)$"select isnull(sum(d.total),0) as Value from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and s.date>='{start}T00:00:00' and s.date<='{end}T23:59:59'") : ($"select isnull(sum(d.total),0) as Value from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and s.date>='{start}T00:00:00' and s.date<='{end}T23:59:59' and id_user = {userid}"));
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total sales", ex.ToString());
                return 0f;
            }
        }

        internal static float getTotalTaxSales(string start, string end)
        {
            try
            {
                FormattableString text = $@"select isnull(sum(d.total_tax),0) as Value from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and p.exenta = 0 and s.date>='{start}T00:00:00' and s.date<='{end}T23:59:59'";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total tax sales", ex.ToString());
                return 0f;
            }
        }

        internal static float getTotalInvoiceSales(string start = "", string end = "", int userid = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(start))
                {
                    start = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(end))
                {
                    end = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                string sql = ((userid <= 0) ?  ($"select count(1) as Value from dbo.sale with(nolock) where state = 1 and date >='{start}T00:00:00' and date <='{end}T23:59:59'") : $"select count(1) as Value from dbo.sale with(nolock) where state = 1 and date >='{start}T00:00:00' and date <='{end}T23:59:59' and id_user = {userid}");
                FormattableString text = $"{sql}";
                return float.Parse(Context.ReturnQuery<int>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total invoice sales", ex.ToString());
                return 0f;
            }
        }

        internal static DataTable getListOfEntries(string start, string end)
        {
            try
            {
                FormattableString text = $"select * from dbo.entries with(nolock) where date>='{start}T00:00:00' and date<='{end}T23:59:59'";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get list entries", ex.ToString());
                return new DataTable();
            }
        }

        internal static DataTable getHistoricalProducts()
        {
            try
            {
                FormattableString text = $@"select name [Nombre Producto], stock [Stock Inicial], amount [Stock Actual], price [Precio Compra], 
                                        sale_price [Precio Venta], (stock * sale_price) [Total Venta Proyectada], (amount * sale_price) [Total Pendiente por Vender],
                                        ((stock - amount) * sale_price) [Total Vendido] from product with(nolock) where 1 = 1 and state = 1";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get historical products", ex.ToString());
                return new DataTable();
            }
        }

        internal static float getTotalOutlay()
        {
            try
            {
                FormattableString text = $"SELECT isnull(SUM(amount * price),0) as total from product with(nolock) where 1 = 1 and state = 1";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Outlay", ex.ToString());
                return 0f;
            }
        }

        internal static float getTotalHopedSales()
        {
            try
            {
                FormattableString text = $"SELECT isnull(SUM(amount * sale_price),0) as total from product with(nolock) where 1 = 1 and state = 1";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Hoped Sales", ex.ToString());
                return 0f;
            }
        }

        internal static float getTotalSoldOut()
        {
            try
            {
                FormattableString text = $"SELECT isnull(SUM((stock - amount) * sale_price),0) as total from product with(nolock) where 1 = 1 and state = 1";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Sold Out", ex.ToString());
                return 0f;
            }
        }

        internal static DataTable getHistorical(string start, string end)
        {
            try
            {
                FormattableString text = $"select * from dbo.historical with(nolock) where date>='{start}T00:00:00' and date<='{end}T23:59:59'";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Historical", ex.ToString());
                return new DataTable();
            }
        }

        internal static float getTotalEntries(string start, string end)
        {
            try
            {
                FormattableString text = $"select isnull(sum(total),0) as total from dbo.entries with(nolock) where date>='{start}T00:00:00' and date<='{end}T23:59:59'";
                return float.Parse(Context.ReturnQuery<double>(text).FirstOrDefault().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total entries", ex.ToString());
                return 0f;
            }
        }

        internal static DataTable getStatistic()
        {
            try
            {
                FormattableString text = $"select p.id as id, p.name, sum(ISNULL(d.amount,0)) as amount, sum(ISNULL(d.total,0)) as total from dbo.product p left join dbo.detail d on p.id = d.id_product where 1=1 and p.state = 1 group by p.id, p.name";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get stadistics", ex.ToString());
                return new DataTable();
            }
        }

        internal static DataTable getDetailStatistic(string id)
        {
            try
            {
                FormattableString text = $"select d.product_name, d.amount, d.total, s.date from dbo.detail d, dbo.sale s where d.id_product = '{id}' and d.id_sale = s.id";
                return Context.ExecReturnQuery(text).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get detail stadistics", ex.ToString());
                return new DataTable();
            }
        }
    }
}
