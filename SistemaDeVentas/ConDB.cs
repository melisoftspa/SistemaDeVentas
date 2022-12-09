using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SistemaDeVentas
{
    public static class ConDB
    {
        internal static Form mainForm;

        internal static string cadena;

        private static SqlConnection conDB = new SqlConnection();

        internal static int userId = -1;

        internal static int userRole = 0;

        internal static bool userNameInvoice = false;

        internal static string userName = string.Empty;

        internal static DataTable current_invoice_detailsDataTable;

        internal static List<string> data { get; set; }

        public static bool OpenDB { get; set; }

        internal static bool openConnection()
        {
            try
            {
                conDB.ConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al consegurir configurarcion o coneccion esta ya iniciada: " + ex.ToString());
            }
            try
            {
                conDB.Open();
                OpenDB = true;
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
            string text = "";
            try
            {
                text = "select (isnull(sum(payment_cash),0) - isnull(sum(change),0)) + (select isnull(sum(total),0) from dbo.sale with(nolock) where state = 1 and payment_cash = 0 and payment_other = 0 and date >='" + start + "T00:00:00' and date <='" + end + "T23:59:59') from dbo.sale with(nolock) where state = 1 and date >='" + start + "T00:00:00' and date <='" + end + "T23:59:59';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Payment Cash", ex.ToString(), text);
                return 0f;
            }
        }

        internal static DataTable getInvoices(string start, string end)
        {
            string text = "";
            try
            {
                text = "select * from dbo.invoice with(nolock) where state = 1 and date >='" + start + "T00:00:00' and date <='" + end + "T23:59:59';";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Invoices", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static float getTotalPaymentOtherCash(string start, string end)
        {
            string text = "";
            try
            {
                text = "select isnull(sum(payment_other),0) from dbo.sale with(nolock) where state = 1 and date >='" + start + "T00:00:00' and date <='" + end + "T23:59:59';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Payment Other Cash", ex.ToString(), text);
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
                using SqlCommand selectCommand = new SqlCommand("select date, line from decrease with(nolock) where date >='" + start + "T00:00:00' and date <='" + end + "T23:59:59';", conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("Fecha");
                dataTable2.Columns.Add("Nombre");
                dataTable2.Columns.Add("Cantidad");
                dataTable2.Columns.Add("IVA");
                dataTable2.Columns.Add("Total");
                dataTable2.Columns.Add("Nota");
                foreach (DataRow row in dataTable.Rows)
                {
                    string text = (string)row["line"];
                    if (text.Contains(","))
                    {
                        string[] array = text.Split(',');
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
                string cmdText = ((!subcategory) ? "select id, Text from category with(nolock) order by Value asc;" : "select id, Text, id_category from subcategory with(nolock) order by Value asc;");
                using SqlCommand selectCommand = new SqlCommand(cmdText, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
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
                using SqlCommand selectCommand = new SqlCommand("select pp.id_product as id, pp.name, p.price, pp.amount, pp.bar_code AS barcode from pack pp inner join product p on pp.id_pack = p.id_pack where pp.id_pack = '" + idpack_product + "'", conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
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
            string text = "";
            try
            {
                text = "select isnull(sum(d.total),0) from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and p.exenta = 1 and s.date >='" + start + "T00:00:00' and s.date <='" + end + "T23:59:59';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Sales Exenta", ex.ToString(), text);
                return 0f;
            }
        }

        internal static float getTotalSalesAfecta(string start, string end, bool profit = false)
        {
            string text = "";
            try
            {
                text = "select isnull(sum(d.total),0) + (select isnull(sum(d.total),0) from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and p.exenta is null and s.date >='" + start + "T00:00:00' and s.date <='" + end + "T23:59:59') from sale s inner join detail d on s.id = d.id_sale left join product p on p.id = d.id_product where s.state = 1 and d.state = 1 and p.exenta = 0 and s.date >='" + start + "T00:00:00' and s.date <='" + end + "T23:59:59';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                float num = float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
                if (profit)
                {
                    num -= getTotalSalesExenta(start, end);
                }
                return num;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total sales", ex.ToString(), text);
                return 0f;
            }
        }

        internal static float getTotalSalesCategory(string start, string end, string id)
        {
            string text = "";
            try
            {
                text = "select isnull(sum(d.total),0) from sale s \r\ninner join detail d on s.id = d.id_sale \r\nleft join product p on p.id = d.id_product\r\nwhere s.date >='" + start + "T00:00:00' and s.date <='" + end + "T23:59:59' and p.id_category = '" + id + "';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total sales", ex.ToString(), text);
                return 0f;
            }
        }

        internal static DataTable getTotalSalesSubCategory(string start, string end, string id)
        {
            string text = "";
            try
            {
                text = "select sc.text, isnull(SUM(d.total),0) as total from sale s \r\ninner join detail d on s.id = d.id_sale \r\nleft join product p on p.id = d.id_product\r\nleft join subcategory sc on sc.id = p.id_subcategory\r\nwhere s.date >='" + start + "T00:00:00' and s.date <='" + end + "T23:59:59' and sc.id_category = '" + id + "' group by sc.text, sc.id_category;";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Sales SubCategory", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static DataTable getTaxList()
        {
            try
            {
                using SqlCommand selectCommand = new SqlCommand("select id, Value from tax with(nolock);", conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
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
                using SqlCommand sqlCommand = new SqlCommand($"select Text from tax with(nolock) where id ='{id}';", conDB);
                return sqlCommand.ExecuteScalar().ToString().Trim();
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
                using SqlCommand sqlCommand = new SqlCommand($"select exenta from tax with(nolock) where id ='{id}';", conDB);
                return bool.Parse(sqlCommand.ExecuteScalar().ToString().Trim());
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
                conDB.Close();
                Console.WriteLine("coneccion a base de datos terminada");
            }
            catch (Exception ex)
            {
                Console.WriteLine("fallo al cerrar la coneccion: " + ex.ToString());
            }
        }

        internal static bool newUser(string username, string pasword, string name, bool inInvoice)
        {
            string text = "";
            try
            {
                text = $"INSERT INTO users ([username],[password],[role],[name],[in_invoice]) VALUES ('{username}',PWDENCRYPT('{pasword}'),0,'{name}','{inInvoice}')";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                sqlCommand.ExecuteScalar();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get user id", ex.ToString(), text);
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
                string text;
                if (string.IsNullOrEmpty(word))
                {
                    text = "select * from dbo.product with(nolock) where 1=1 ";
                    if (isSales)
                    {
                        text += "and sale_price is not null and sale_price > 0 ";
                    }
                    text = ((!isInventory) ? (text + "and state = 1 and amount >= minimum;") : (text + "and state = 1 "));
                }
                else
                {
                    text = "select * from dbo.product with(nolock) where 1=1 ";
                    if (isSales)
                    {
                        text += "and sale_price is not null and sale_price > 0 ";
                    }
                    if (!isInventory)
                    {
                        text += "and amount >= minimum ";
                    }
                    text = text + "and state = 1 and (coalesce(convert(nvarchar(max), name), '') +\r\n                        coalesce(convert(varchar(max), amount), '') +\r\n                        coalesce(convert(varchar(max), sale_price), '') +\r\n                        coalesce(convert(varchar(max), minimum), '') +\r\n                        coalesce(convert(varchar(max), bar_code), '') +\r\n                        coalesce(convert(varchar(max), stock), '') +\r\n                        coalesce(convert(varchar(max), price), '') +\r\n                        coalesce(convert(varchar(max), line), '')\r\n                        ) like '%" + word + "%';";
                }
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("name", "asc");
                dataTable.DefaultView.Sort = string.Join(",", dictionary.Select((KeyValuePair<string, string> x) => x.Key + " " + x.Value).ToArray());
                dataTable = dataTable.DefaultView.ToTable();
                foreach (DataRow row in dataTable.Rows)
                {
                    row["name"] = row["name"].ToString().ToUpper();
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
            string query = "procedure";
            try
            {
                using SqlCommand sqlCommand = new SqlCommand("dbo.cancel_sale", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ticket", nroticket);
                sqlCommand.Parameters.AddWithValue("@userid", userId);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("insert decrease", ex.ToString(), query);
            }
        }

        internal static bool getDecreaseCode(string decreaseCode, DataTable products, string note)
        {
            if (getParameters("VENTA", "MERMA").Equals(decreaseCode))
            {
                string query = "procedure";
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
                            using SqlCommand sqlCommand = new SqlCommand("dbo.create_decrease", conDB);
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.AddWithValue("@idProduct", text);
                            sqlCommand.Parameters.AddWithValue("@amount", float.Parse(text2.Replace("$", "").Replace(".", "")));
                            sqlCommand.Parameters.AddWithValue("@tax", num3);
                            sqlCommand.Parameters.AddWithValue("@total", num2);
                            sqlCommand.Parameters.AddWithValue("@note", note);
                            sqlCommand.Parameters.AddWithValue("@line", value);
                            sqlCommand.Parameters.AddWithValue("@userID", userId);
                            sqlCommand.ExecuteNonQuery();
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
                    log("insert decrease", ex.ToString(), query);
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
                string text = ((expiration < DateTime.Today) ? null : expiration.ToShortDateString());
                string value = $"nombre: {name}, cantidad: {amount}, precio compra: {price}, stock: {minimum}, codigo: {barcode}, id IVA: {tax}, Exenta: {exenta}, fecha Vencimiento: {text}, id Categoria: {category}, Es Pack: {isPack}, id pack: {idpack}, id SubCategoria: {idsubcategory}";
                using SqlCommand sqlCommand = new SqlCommand("dbo.create_product", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name", name);
                sqlCommand.Parameters.AddWithValue("@amount", amount);
                sqlCommand.Parameters.AddWithValue("@stock", minimum);
                sqlCommand.Parameters.AddWithValue("@price", price);
                sqlCommand.Parameters.AddWithValue("@barcode", barcode);
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                sqlCommand.Parameters.AddWithValue("@line", value);
                sqlCommand.Parameters.AddWithValue("@tax", tax);
                sqlCommand.Parameters.AddWithValue("@expiration", text);
                sqlCommand.Parameters.AddWithValue("@exenta", exenta);
                sqlCommand.Parameters.AddWithValue("@category", category);
                sqlCommand.Parameters.AddWithValue("@isPack", isPack);
                sqlCommand.Parameters.AddWithValue("@idPack", idpack);
                sqlCommand.Parameters.AddWithValue("@idsubcategory", idsubcategory);
                SqlParameter sqlParameter = new SqlParameter("@res", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();
                int num = 0;
                if (sqlParameter.Value != DBNull.Value)
                {
                    num = Convert.ToInt32(sqlParameter.Value);
                }
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
            string query = "procedure";
            try
            {
                string value = "nombre: " + name + ", cantidad: " + amount + ", codigo: " + barcode + ", id producto: " + idproducto + ", id pack: " + id;
                using SqlCommand sqlCommand = new SqlCommand("dbo.create_pack", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@id_product", idproducto);
                sqlCommand.Parameters.AddWithValue("@barcode", barcode);
                sqlCommand.Parameters.AddWithValue("@name", name);
                sqlCommand.Parameters.AddWithValue("@amount", amount);
                sqlCommand.Parameters.AddWithValue("@line", value);
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("Create Pack Product", ex.ToString(), query);
            }
        }

        internal static bool UpdateProduct(string id, string name, string amount, string price, string minimum, string barcode, string tax, bool exenta, DateTime expiration, string category, bool isPack, string idpack_product, string idsubcategory)
        {
            string query = "procedure";
            try
            {
                string text = ((expiration < DateTime.Today) ? null : expiration.ToShortDateString());
                string value = $"nombre: {name}, cantidad: {amount}, precio compra: {price}, stock: {minimum}, codigo: {barcode}, id IVA: {tax}, Exenta: {exenta}, fecha Vencimiento: {text}, Es Pack: {isPack} , id Categoria: {category}, id pack: {idpack_product}, id SubCategoria: {idsubcategory}";
                using SqlCommand sqlCommand = new SqlCommand("dbo.update_product", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@name", name);
                sqlCommand.Parameters.AddWithValue("@amount", amount);
                sqlCommand.Parameters.AddWithValue("@minimum", minimum);
                sqlCommand.Parameters.AddWithValue("@price", price);
                sqlCommand.Parameters.AddWithValue("@barcode", barcode);
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                sqlCommand.Parameters.AddWithValue("@line", value);
                sqlCommand.Parameters.AddWithValue("@tax", tax);
                sqlCommand.Parameters.AddWithValue("@expiration", text);
                sqlCommand.Parameters.AddWithValue("@exenta", exenta);
                sqlCommand.Parameters.AddWithValue("@category", category);
                sqlCommand.Parameters.AddWithValue("@isPack", isPack);
                sqlCommand.Parameters.AddWithValue("@id_pack", idpack_product);
                sqlCommand.Parameters.AddWithValue("@idsubcategory", idsubcategory);
                SqlParameter sqlParameter = new SqlParameter("@res", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();
                int num = 0;
                if (sqlParameter.Value != DBNull.Value)
                {
                    num = Convert.ToInt32(sqlParameter.Value);
                }
                return num == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("update product", ex.ToString(), query);
                return false;
            }
        }

        internal static bool DeleteProduct(string id, string name)
        {
            string query = "procedure";
            try
            {
                using SqlCommand sqlCommand = new SqlCommand("dbo.delete_product", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                sqlCommand.Parameters.AddWithValue("@line", "nombre:" + name);
                SqlParameter sqlParameter = new SqlParameter("@res", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();
                int num = 0;
                if (sqlParameter.Value != DBNull.Value)
                {
                    num = Convert.ToInt32(sqlParameter.Value);
                }
                return num == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("delete procuct", ex.ToString(), query);
                return false;
            }
        }

        internal static bool RegisterWareHouseEntry(string id_product, string name, string price, string amount)
        {
            string query = "";
            try
            {
                string value = price.Replace(",", ".");
                string value2 = amount.Replace(",", ".");
                using SqlCommand sqlCommand = new SqlCommand("dbo.create_entry", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@productID", id_product);
                sqlCommand.Parameters.AddWithValue("@productName", name);
                sqlCommand.Parameters.AddWithValue("@amount", value2);
                sqlCommand.Parameters.AddWithValue("@total", value);
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("Register WareHous Entry", ex.ToString(), query);
                return false;
            }
        }

        internal static bool DeleteWareHouseEntry(string id_product)
        {
            string query = "";
            try
            {
                using SqlCommand sqlCommand = new SqlCommand("dbo.delete_entry", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@productID", id_product);
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("Register WareHous Entry", ex.ToString(), query);
                return false;
            }
        }

        internal static bool makeBackup(string dir)
        {
            try
            {
                using SqlCommand sqlCommand = new SqlCommand("dbo.backupInventoryDB", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@folderDir", dir);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("make backup", ex.ToString(), "");
                return false;
            }
        }

        internal static Guid insertSale(float amount, float tax, float total, string notes, string payment_cash, string payment_other, string change, ref int ticket)
        {
            string query = "";
            try
            {
                using SqlCommand sqlCommand = new SqlCommand("dbo.create_sale", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@amount", amount);
                sqlCommand.Parameters.AddWithValue("@tax", tax);
                sqlCommand.Parameters.AddWithValue("@total", total);
                sqlCommand.Parameters.AddWithValue("@notes", notes);
                sqlCommand.Parameters.AddWithValue("@payment_cash", float.Parse(validNumber(payment_cash)));
                sqlCommand.Parameters.AddWithValue("@payment_other", float.Parse(validNumber(payment_other)));
                sqlCommand.Parameters.AddWithValue("@change", float.Parse(validNumber(change)));
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                SqlParameter sqlParameter = new SqlParameter("@saleID", SqlDbType.UniqueIdentifier);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                SqlParameter sqlParameter2 = new SqlParameter("@ticket", SqlDbType.BigInt);
                sqlParameter2.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter2);
                sqlCommand.ExecuteNonQuery();
                Guid result = default(Guid);
                if (sqlParameter.Value != DBNull.Value)
                {
                    result = Guid.Parse(sqlParameter.Value.ToString());
                }
                if (sqlParameter2.Value != DBNull.Value)
                {
                    ticket = int.Parse(sqlParameter2.Value.ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("insert sale", ex.ToString(), query);
                return default(Guid);
            }
        }

        internal static bool insertDetailSale(Guid id_sale, Guid id_product, string product_name, string price, string amount, string total, string tax, string total_tax)
        {
            string query = "procedure";
            try
            {
                string value = id_sale.ToString();
                string s = price.Replace(".", "").Replace("$", "");
                string s2 = amount.Replace(".", "").Replace("$", "");
                string s3 = total.Replace(".", "").Replace("$", "");
                string s4 = tax.Replace(".", "").Replace("$", "");
                string s5 = total_tax.Replace(".", "").Replace("$", "");
                using SqlCommand sqlCommand = new SqlCommand("dbo.create_detail", conDB);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idSale", value);
                sqlCommand.Parameters.AddWithValue("@idProduct", id_product);
                sqlCommand.Parameters.AddWithValue("@name", product_name);
                sqlCommand.Parameters.AddWithValue("@price", float.Parse(s));
                sqlCommand.Parameters.AddWithValue("@amount", float.Parse(s2));
                sqlCommand.Parameters.AddWithValue("@tax", float.Parse(s4));
                sqlCommand.Parameters.AddWithValue("@total", float.Parse(s3));
                sqlCommand.Parameters.AddWithValue("@total_tax", float.Parse(s5));
                sqlCommand.Parameters.AddWithValue("@userID", userId);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("insert detail sale", ex.ToString(), query);
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
            string text = "";
            try
            {
                text = "select id from dbo.users with(nolock) where username='" + username + "' and PWDCOMPARE('" + pwd + "', password)= 1";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return (int)sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get user id", ex.ToString(), text);
                return -1;
            }
        }

        private static int getUserRole()
        {
            string text = "";
            try
            {
                text = $"select role from dbo.users with(nolock) where id={userId};";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return int.Parse(sqlCommand.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get user role", ex.ToString(), text);
                return 0;
            }
        }

        private static string getUserName(bool inVoince = false)
        {
            string text = "";
            try
            {
                text = (inVoince ? $"select in_invoice from dbo.users with(nolock) where id={userId};" : $"select name from dbo.users with(nolock) where id={userId};");
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return sqlCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get User Name", ex.ToString(), text);
                return string.Empty;
            }
        }

        internal static bool changePassword(string new_pwd, string name, bool inInvoice)
        {
            string text = "";
            try
            {
                text = $"update dbo.users set password=PWDENCRYPT('{new_pwd}'), name='{name}',in_invoice='{inInvoice}' where id={userId};";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("chenge password", ex.ToString(), text);
                return false;
            }
        }

        internal static DataTable Current_invoice_details(int current_invoice_nroTicket)
        {
            string text = "";
            try
            {
                text = $"select d.amount, d.product_name as name, d.total, d.price from sale s inner join detail d on s.id = d.id_sale where s.ticket = {current_invoice_nroTicket};";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Current_invoice_details", ex.ToString(), text);
                return new DataTable();
            }
        }

        private static void log(string type, string error, string query)
        {
            string path = "C:\\ErrorLog\\log.txt";
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
            string query = "";
            try
            {
                return getParameters("BOLETA", "CABECERA").Split('|').ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get by code bar", ex.ToString(), query);
                return result;
            }
        }

        internal static string getParameters(string module, string name)
        {
            string text = "";
            try
            {
                text = "select isnull(value,'') from dbo.parameter with(nolock) where module = '" + module + "' and name = '" + name + "';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return sqlCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Parameters", ex.ToString(), text);
                return string.Empty;
            }
        }

        internal static List<string> getProductByBarcode(string code)
        {
            List<string> list = new List<string>();
            string text = "";
            try
            {
                text = "select p.id, p.name, p.sale_price, p.amount, p.minimum, replace(t.Text, '.',',') as tax, p.exenta from dbo.product p with(nolock) inner join dbo.tax t with(nolock) on p.id_tax = t.id where p.state = 1 and p.bar_code='" + code + "';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                sqlCommand.ExecuteNonQuery();
                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                if (sqlDataReader.HasRows)
                {
                    list.Add(sqlDataReader[0].ToString().Replace("\r\n", "").Replace("\n", "")
                        .Replace("\r", ""));
                    list.Add(sqlDataReader[1].ToString().Replace("\r\n", "").Replace("\n", "")
                        .Replace("\r", ""));
                    list.Add(sqlDataReader[2].ToString().Replace("\r\n", "").Replace("\n", "")
                        .Replace("\r", ""));
                    list.Add(sqlDataReader[3].ToString().Replace("\r\n", "").Replace("\n", "")
                        .Replace("\r", ""));
                    list.Add(sqlDataReader[4].ToString().Replace("\r\n", "").Replace("\n", "")
                        .Replace("\r", ""));
                    list.Add(sqlDataReader[5].ToString().Replace("\r\n", "").Replace("\n", "")
                        .Replace("\r", ""));
                    list.Add(sqlDataReader[6].ToString().Replace("\r\n", "").Replace("\n", "")
                        .Replace("\r", ""));
                }
                sqlDataReader.Close();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get by code bar", ex.ToString(), text);
                return list;
            }
        }

        internal static DataTable getListOfSales(string start, string end)
        {
            string text = "";
            try
            {
                text = "select id, date, name, amount, tax, total, id_user, change, payment_cash, payment_other, ticket, note from dbo.sale with(nolock) where state = 1 and date>='" + start + "T00:00:00' and date<='" + end + "T23:59:59';";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get list of sales", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static DataTable getListOfDetails(string idSale)
        {
            string text = "";
            try
            {
                text = "select * from dbo.detail with(nolock) where id_sale='" + idSale + "';";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get list od details", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static float getTotalSales(string start = "", string end = "", int userid = 0)
        {
            string text = "";
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
                text = ((userid <= 0) ? ("select isnull(sum(d.total),0) from sale s \r\ninner join detail d on s.id = d.id_sale \r\nleft join product p on p.id = d.id_product\r\nwhere s.state = 1 and d.state = 1 and s.date>='" + start + "T00:00:00' and s.date<='" + end + "T23:59:59';") : ("select isnull(sum(d.total),0) from sale s \r\ninner join detail d on s.id = d.id_sale \r\nleft join product p on p.id = d.id_product\r\nwhere s.state = 1 and d.state = 1 and s.date>='" + start + "T00:00:00' and s.date<='" + end + "T23:59:59' and id_user = " + userid + ";"));
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total sales", ex.ToString(), text);
                return 0f;
            }
        }

        internal static float getTotalTaxSales(string start, string end)
        {
            string text = "";
            try
            {
                text = "select isnull(sum(d.total_tax),0) from sale s \r\ninner join detail d on s.id = d.id_sale \r\nleft join product p on p.id = d.id_product\r\nwhere s.state = 1 and d.state = 1 and p.exenta = 0 and s.date>='" + start + "T00:00:00' and s.date<='" + end + "T23:59:59';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total tax sales", ex.ToString(), text);
                return 0f;
            }
        }

        internal static float getTotalInvoiceSales(string start = "", string end = "", int userid = 0)
        {
            string text = "";
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
                text = ((userid <= 0) ? ("select count(1) from dbo.sale with(nolock) where state = 1 and date >='" + start + "T00:00:00' and date <='" + end + "T23:59:59';") : $"select count(1) from dbo.sale with(nolock) where state = 1 and date >='{start}T00:00:00' and date <='{end}T23:59:59' and id_user = {userid};");
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total invoice sales", ex.ToString(), text);
                return 0f;
            }
        }

        internal static DataTable getListOfEntries(string start, string end)
        {
            string text = "";
            try
            {
                text = "select * from dbo.entries with(nolock) where date>='" + start + "T00:00:00' and date<='" + end + "T23:59:59';";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get list entries", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static DataTable getHistoricalProducts()
        {
            string text = "";
            try
            {
                text = "select name [Nombre Producto], stock [Stock Inicial], \r\n                        amount [Stock Actual], price [Precio Compra], \r\n                        sale_price [Precio Venta], \r\n                        (stock * sale_price) [Total Venta Proyectada], \r\n                        (amount * sale_price) [Total Pendiente por Vender], \r\n\t                    ((stock - amount) * sale_price) [Total Vendido]\r\n                    from product with(nolock) where 1 = 1 and state = 1";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get historical products", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static float getTotalOutlay()
        {
            string text = "";
            try
            {
                text = "SELECT isnull(SUM(amount * price),0)\n                        from product with(nolock) where 1 = 1 and state = 1";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Outlay", ex.ToString(), text);
                return 0f;
            }
        }

        internal static float getTotalHopedSales()
        {
            string text = "";
            try
            {
                text = "SELECT isnull(SUM(amount * sale_price),0)\n                        from product with(nolock) where 1 = 1 and state = 1";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Hoped Sales", ex.ToString(), text);
                return 0f;
            }
        }

        internal static float getTotalSoldOut()
        {
            string text = "";
            try
            {
                text = "SELECT isnull(SUM((stock - amount) * sale_price),0)\n                        from product with(nolock) where 1 = 1 and state = 1";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Total Sold Out", ex.ToString(), text);
                return 0f;
            }
        }

        internal static DataTable getHistorical(string start, string end)
        {
            string text = "";
            try
            {
                text = "select * from dbo.historical with(nolock) where date>='" + start + "T00:00:00' and date<='" + end + "T23:59:59';";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get Historical", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static float getTotalEntries(string start, string end)
        {
            string text = "";
            try
            {
                text = "select isnull(sum(total),0) from dbo.entries with(nolock) where date>='" + start + "T00:00:00' and date<='" + end + "T23:59:59';";
                using SqlCommand sqlCommand = new SqlCommand(text, conDB);
                return float.Parse(sqlCommand.ExecuteScalar().ToString().Replace('.', ','));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get total entries", ex.ToString(), text);
                return 0f;
            }
        }

        internal static DataTable getStatistic()
        {
            string text = "";
            try
            {
                text = "select p.id as id, p.name, sum(ISNULL(d.amount,0)) as amount,    sum(ISNULL(d.total,0)) as total from dbo.product p left join dbo.detail d on p.id = d.id_product where 1=1 and p.state = 1 group by p.id, p.name;";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get stadistics", ex.ToString(), text);
                return new DataTable();
            }
        }

        internal static DataTable getDetailStatistic(string id)
        {
            string text = "";
            try
            {
                text = "select d.product_name, d.amount, d.total, s.date from dbo.detail d, dbo.sale s where d.id_product = '" + id + "' and d.id_sale = s.id";
                using SqlCommand selectCommand = new SqlCommand(text, conDB);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log("get detail stadistics", ex.ToString(), text);
                return new DataTable();
            }
        }
    }
}
