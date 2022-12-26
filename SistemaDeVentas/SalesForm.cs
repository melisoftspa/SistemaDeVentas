using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using LibPrintTicket;
using Pdf417EncoderLibraryExt;
using SistemaDeVentas.Models;

namespace SistemaDeVentas
{
    public partial class SalesForm : Form
    {
        private string _welcome = "¡Hola!, {0}, has vendido: {1} boletas y un total de: {2}.";

        private string row_selected_id = "";

        private string filter_word = "";

        private float current_amount;

        private float current_tax;

        private string current_name = "";

        private string current_price = "";

        private float taxInvoice;

        private float subTotalInvoice;

        private float totalInvoice;

        private bool current_exenta;

        private int current_stock;

        private DataTable detailsDataTable = new DataTable();

        private DataTable _fullInventary = new DataTable();

        private readonly Font printFont;

        private readonly StreamReader streamToPrint;

        private DetailsForm detailsalesform;

        private int current_invoice_nroTicket = -1;

        private float current_invoice_subTotalInvoice;

        private float current_invoice_taxInvoice;

        private float current_invoice_totalInvoice;

        private string cuurent_invoice_payment_cash = string.Empty;

        private string current_invoice_payment_other = string.Empty;

        private string current_invoice_change = string.Empty;

        private Guid current_invoice_id_sale;

        public bool ButtonSalesProcess
        {
            get
            {
                return processButton.Enabled;
            }
            set
            {
                processButton.Enabled = value;
            }
        }

        public SalesForm()
        {
            InitializeComponent();
            StartTimer();
            _fullInventary = ConDB.getProductsList("", isSales: true);
            InventoryDataGrid.DataSource = _fullInventary;
            setProductsGridFormat();
            vendor_label.Text = string.Format(_welcome, ConDB.userName, ConDB.getTotalInvoiceSales("", "", ConDB.userId), ConDB.getTotalSales("", "", ConDB.userId).ToString("c"));
            try
            {
                if (Screen.AllScreens[1] != null)
                {
                    detailsalesform = new DetailsForm();
                    detailsalesform.Location = Screen.AllScreens[1].WorkingArea.Location;
                    detailsalesform.Show();
                }
            }
            catch (Exception)
            {
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F6:
                    filterTextBox.Focus();
                    break;
                case Keys.F7:
                    barcode_input.Focus();
                    break;
                case Keys.F8:
                    payment_cash_input.Focus();
                    break;
                case Keys.F9:
                    payment_other_input.Focus();
                    break;
                case Keys.F10:
                    notes_input.Focus();
                    break;
                case Keys.F11:
                    if (processButton.Enabled)
                    {
                        processSales(invoice: false);
                    }
                    break;
                case Keys.F12:
                    if (processButton.Enabled)
                    {
                        processSales();
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SalesForm_Shown(object sender, EventArgs e)
        {
            setDetailsGridFormat();
            barcode_input.Focus();
        }

        private void filterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void setProductsGridFormat()
        {
            //InventoryDataGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
            InventoryDataGrid.AutoResizeColumns();
            InventoryDataGrid.AllowUserToOrderColumns = true;
            InventoryDataGrid.AllowUserToResizeColumns = true;
            InventoryDataGrid.Columns["name"].HeaderText = "Nombre del Producto";
            InventoryDataGrid.Columns["sale_price"].HeaderText = "Precio de Venta";
            InventoryDataGrid.Columns["amount"].HeaderText = "Stock Actual";
            InventoryDataGrid.Columns["expiration"].HeaderText = "Fecha Vencimiento";
            InventoryDataGrid.Columns["bar_code"].HeaderText = "Código Barra";
            InventoryDataGrid.Columns["sale_price"].DefaultCellStyle.Format = "C";
            InventoryDataGrid.Columns["sale_price"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();
            InventoryDataGrid.Columns["id"].Visible = false;
            InventoryDataGrid.Columns["amount"].Visible = true;
            InventoryDataGrid.Columns["minimum"].Visible = false;
            InventoryDataGrid.Columns["photo"].Visible = false;
            InventoryDataGrid.Columns["stock"].Visible = false;
            InventoryDataGrid.Columns["price"].Visible = false;
            InventoryDataGrid.Columns["line"].Visible = false;
            InventoryDataGrid.Columns["date"].Visible = false;
            InventoryDataGrid.Columns["state"].Visible = false;
            InventoryDataGrid.Columns["exenta"].Visible = false;
            InventoryDataGrid.Columns["id_tax"].Visible = false;
            InventoryDataGrid.Columns["_RowString"].Visible = false;
            InventoryDataGrid.Columns["id_category"].Visible = false;
            InventoryDataGrid.Columns["isPack"].Visible = false;
            InventoryDataGrid.Columns["id_pack"].Visible = false;
            InventoryDataGrid.Columns["id_subcategory"].Visible = false;
            // Now that DataGridView has calculated it's Widths; we can now store each column Width values.
            for (int i = 0; i <= InventoryDataGrid.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = InventoryDataGrid.Columns[i].Width;
                // Remove AutoSizing:
                InventoryDataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // Set Width to calculated AutoSize value:
                InventoryDataGrid.Columns[i].Width = colw;
            }
            InventoryDataGrid.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            InventoryDataGrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            InventoryDataGrid.Columns["sale_price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            InventoryDataGrid.Columns["bar_code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["expiration"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void setDetailsGridFormat()
        {
            DetailDataGrid.DataSource = detailsDataTable;
            detailsDataTable.Columns.Add("id");
            detailsDataTable.Columns.Add("name");
            detailsDataTable.Columns.Add("price");
            detailsDataTable.Columns.Add("amount");
            detailsDataTable.Columns.Add("total");
            detailsDataTable.Columns.Add("tax");
            detailsDataTable.Columns.Add("exenta");
            detailsDataTable.Columns.Add("stock");
            DetailDataGrid.Columns["id"].Visible = false;
            DetailDataGrid.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DetailDataGrid.Columns["price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DetailDataGrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DetailDataGrid.Columns["total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DetailDataGrid.Columns["tax"].Visible = false;
            DetailDataGrid.Columns["exenta"].Visible = false;
            DetailDataGrid.Columns["stock"].Visible = false;
            DetailDataGrid.Columns["name"].HeaderText = "Nombre del Producto";
            DetailDataGrid.Columns["price"].HeaderText = "Precio Unidad";
            DetailDataGrid.Columns["amount"].HeaderText = "Cantidad";
            DetailDataGrid.Columns["total"].HeaderText = "Total";
            DetailDataGrid.Columns["price"].DefaultCellStyle.Format = "C";
            DetailDataGrid.Columns["total"].DefaultCellStyle.Format = "C";
            DetailDataGrid.Columns["price"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();
            DetailDataGrid.Columns["total"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();
            // Now that DataGridView has calculated it's Widths; we can now store each column Width values.
            for (int i = 0; i <= DetailDataGrid.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = DetailDataGrid.Columns[i].Width;
                // Remove AutoSizing:
                DetailDataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // Set Width to calculated AutoSize value:
                DetailDataGrid.Columns[i].Width = colw;
            }
        }

        private void InventoryDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                row_selected_id = InventoryDataGrid.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                current_stock = int.Parse(InventoryDataGrid.Rows[e.RowIndex].Cells["amount"].FormattedValue.ToString());
                current_tax = float.Parse(ConDB.getTax(Guid.Parse(InventoryDataGrid.Rows[e.RowIndex].Cells["id_tax"].FormattedValue.ToString())));
                current_exenta = bool.Parse(InventoryDataGrid.Rows[e.RowIndex].Cells["exenta"].FormattedValue.ToString());
                string text = InventoryDataGrid.Rows[e.RowIndex].Cells["sale_price"].FormattedValue.ToString();
                string text2 = InventoryDataGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
                name_input.Text = (current_name = text2.ToUpper());
                price_input.Text = (current_price = text);
                amount_input.Focus();
                amount_input.SelectAll();
            }
        }

        private void addToDetails(string id_product = "")
        {
            lblMensajeErrorCodeBar.Text = string.Empty;
            if (row_selected_id == "" && name_input.ReadOnly && price_input.ReadOnly)
            {
                MessageBox.Show("Debe seleccionar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (current_amount <= 0f)
            {
                MessageBox.Show("La cantidad no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            float num = 0f;
            string text = current_name;
            try
            {
                if (!string.IsNullOrEmpty(kg_input.Text))
                {
                    float num2 = float.Parse(kg_input.Text.Replace(".", ","));
                    num = float.Parse(ConDB.validNumber(price_input.Text));
                    num = ConDB.Round(float.Parse(Math.Ceiling(num * num2).ToString()).ToString());
                    if (current_name.Contains("KG"))
                    {
                        var name_text = current_name.Split("-");
                        current_name = name_text[0];
                    }
                    text = $"{current_name} - KG: {num2}";
                }
                else
                {
                    text = current_name;
                    num = float.Parse(ConDB.validNumber(price_input.Text));
                }
            }
            catch (Exception)
            {
                num = float.Parse(ConDB.validNumber(current_price));
                text = current_name;
            }
            bool flag = true;
            for (int i = 0; i < detailsDataTable.Rows.Count; i++)
            {
                if (detailsDataTable.Rows[i]["id"].ToString() == id_product)
                {
                    flag = false;
                    detailsDataTable.Rows[i]["name"] = text;
                    detailsDataTable.Rows[i]["price"] = float.Parse(ConDB.validNumber(detailsDataTable.Rows[i]["price"].ToString())).ToString("C").Replace(",00", "");
                    detailsDataTable.Rows[i]["amount"] = int.Parse(detailsDataTable.Rows[i]["amount"].ToString()) + 1;
                    detailsDataTable.Rows[i]["total"] = ((float)int.Parse(ConDB.validNumber(detailsDataTable.Rows[i]["total"].ToString())) + num).ToString("C").Replace(",00", "");
                }
            }
            if (flag)
            {
                if (row_selected_id == default(Guid).ToString())
                {
                    row_selected_id = Guid.NewGuid().ToString();
                }
                detailsDataTable.Rows.Add(row_selected_id, text, price_input.Text, amount_input.Text, getTotalRow(amount_input.Text, num.ToString("C").Replace(",00", "")), current_tax, current_exenta);
            }
            cleanfields();
            calculateTotalSale();
            barcode_input.Focus();
            Addproductsale_button.Enabled = true;
            try
            {
                if (detailsalesform != null)
                {
                    detailsalesform.AddDetails(detailsDataTable);
                }
            }
            catch (Exception)
            {
            }
        }

        private void calculateTotalSale()
        {
            float num = 0f;
            for (int i = 0; i < detailsDataTable.Rows.Count; i++)
            {
                num += float.Parse(ConDB.validNumber(detailsDataTable.Rows[i]["total"].ToString()));
            }
            totalSale_input.Text = num.ToString("C").Replace(",00", "");
            try
            {
                if (detailsalesform != null)
                {
                    detailsalesform.AddTotal(totalSale_input.Text);
                }
            }
            catch (Exception)
            {
            }
        }

        private float convertToFloat(string word)
        {
            string obj = ConDB.validNumber(word);
            Console.WriteLine(obj);
            return float.Parse(obj);
        }

        private void amount_input_KeyUp(object sender, KeyEventArgs e)
        {
            lblStockMax.Text = string.Empty;
            if (e.KeyCode == Keys.Return)
            {
                string s = ConDB.validNumber(amount_input.Text.Trim());
                try
                {
                    if(int.Parse(s) > current_stock) { lblStockMax.Text = $"El stock máximo para la venta es de {current_stock}"; return; }
                    current_amount = float.Parse(s);
                }
                catch (Exception)
                {
                    MessageBox.Show("La cantidad no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if (name_input.ReadOnly && price_input.ReadOnly)
                {
                    addToDetails(row_selected_id);
                    return;
                }
                current_name = name_input.Text.Trim();
                addToDetails();
                name_input.ReadOnly = true;
                price_input.ReadOnly = true;
                name_input.TabStop = false;
                price_input.TabStop = false;
                barcode_input.Focus();
            }
        }

        private string getTotalRow(string amount, string price)
        {
            float num = float.Parse(ConDB.validNumber(amount.Trim()));
            float num2 = float.Parse(ConDB.validNumber(price.Trim()));
            return (num * num2).ToString("C").Replace(",00", "");
        }

        private void cleanfields()
        {
            row_selected_id = string.Empty;
            current_amount = 0f;
            name_input.Text = string.Empty;
            amount_input.Text = "1";
            payment_cash_input.Text = "$0";
            payment_other_input.Text = "$0";
            change_input.Text = "$0";
            price_input.Text = "$0";
            totalSale_input.Text = "$0";
            decrease_checkbox.Checked = false;
            filterTextBox.Text = string.Empty;
            barcode_input.Text = string.Empty;
            notes_input.Text = string.Empty;
            kg_input.Text = string.Empty;
            try
            {
                if (detailsalesform != null)
                {
                    detailsalesform.AddChange(change_input.Text);
                    detailsalesform.AddTotal(totalSale_input.Text);
                }
            }
            catch (Exception)
            {
            }
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            processSales();
        }

        internal void ClearFinishSale()
        {
            InventoryDataGrid.DataSource = ConDB.getProductsList("", isSales: true);
            detailsDataTable.Clear();
            cleanfields();
            EnabledProcessButton(active: false);
            setProductsGridFormat();
            barcode_input.Focus();
            vendor_label.Text = string.Format(_welcome, ConDB.userName, ConDB.getTotalInvoiceSales("", "", ConDB.userId), ConDB.getTotalSales("", "", ConDB.userId).ToString("c"));
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float num = 0f;
            float num2 = 0f;
            int i = 0;
            float num3 = 0f;
            float num4 = 0f;
            string text = null;
            for (num = (float)ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics); (float)i < num; i++)
            {
                if ((text = streamToPrint.ReadLine()) == null)
                {
                    break;
                }
                num2 = num4 + (float)i * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString(text, printFont, Brushes.Black, num3, num2, new StringFormat());
            }
            if (text != null)
            {
                ev.HasMorePages = true;
            }
            else
            {
                ev.HasMorePages = false;
            }
        }

        private void DetailDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex >= DetailDataGrid.RowCount)
            {
                return;
            }
            else if (e.ColumnIndex == 0)
            {
                DetailDataGrid.Rows.RemoveAt(e.RowIndex);
                calculateTotalSale();
            }
            else if (e.ColumnIndex == 1)
            {
                float num = float.Parse(DetailDataGrid.Rows[e.RowIndex].Cells["amount"].FormattedValue.ToString()) + 1f;
                if(Convert.ToInt32(num) <= current_stock)
                {
                    float num2 = float.Parse(ConDB.validNumber(DetailDataGrid.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString())) * num;
                    DetailDataGrid.Rows[e.RowIndex].Cells["amount"].Value = num;
                    DetailDataGrid.Rows[e.RowIndex].Cells["total"].Value = num2.ToString("C").Replace(",00", "");
                    calculateTotalSale();
                }
            }
            else if (e.ColumnIndex == 2)
            {
                float num3 = float.Parse(DetailDataGrid.Rows[e.RowIndex].Cells["amount"].FormattedValue.ToString()) - 1f;
                if (num3 < 1f)
                {
                    num3 = 1f;
                }
                float num4 = float.Parse(ConDB.validNumber(DetailDataGrid.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString())) * num3;
                DetailDataGrid.Rows[e.RowIndex].Cells["amount"].Value = num3;
                DetailDataGrid.Rows[e.RowIndex].Cells["total"].Value = num4.ToString("C").Replace(",00", "");
                calculateTotalSale();
            } 
            else if (e.ColumnIndex == 3)
            {
                TextBox textBox = name_input;
                TextBox textBox2 = price_input;
                string text = (amount_input.Text = string.Empty);
                string text3 = (textBox2.Text = text);
                textBox.Text = text3;
                name_input.Text = (current_name = DetailDataGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString());
                price_input.Text = (current_price = (DetailDataGrid.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString().Contains("$") ? DetailDataGrid.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString() : ("$" + DetailDataGrid.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString())));
                amount_input.Text = DetailDataGrid.Rows[e.RowIndex].Cells["amount"].FormattedValue.ToString().Replace("$", "");
                current_amount = float.Parse(ConDB.validNumber(amount_input.Text));
                row_selected_id = DetailDataGrid.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                DetailDataGrid.Rows.RemoveAt(e.RowIndex);
                if (name_input.Text.Contains("KG"))
                {
                    var kg_text = name_input.Text.Split(":");
                    kg_input.Text = kg_text[1].Trim();
                    kg_input.Focus();
                    kg_input.Select();
                } 
                else
                {
                    amount_input.Focus();
                }
                Addproductsale_button.Enabled = false;
            }
        }

        private void DetailDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex >= DetailDataGrid.RowCount)
            {
                return;
            }
        }

        private void SalesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.mainForm.Show();
            try
            {
                if (detailsalesform != null)
                {
                    detailsalesform.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private void barcode_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                List<string> productByBarcode = ConDB.getProductByBarcode(barcode_input.Text, true);
                if (productByBarcode.Count != 0)
                {
                    row_selected_id = productByBarcode[0];
                    name_input.Text = (current_name = productByBarcode[1]);
                    price_input.Text = (current_price = float.Parse(ConDB.validNumber(productByBarcode[2])).ToString("C").Replace(",00", ""));
                    current_tax = float.Parse(productByBarcode[5]);
                    current_exenta = Convert.ToBoolean(Convert.ToInt32(productByBarcode[6]));
                    current_stock = int.Parse(productByBarcode[3]);
                    current_amount = 1f;
                    addToDetails(row_selected_id);
                    barcode_input.SelectAll();
                } else { lblMensajeErrorCodeBar.Text = "Producto no ingresado en Almacen"; }
            }
        }

        private void barcode_input_Leave(object sender, EventArgs e)
        {
        }

        internal bool CalculeCashSale()
        {
            string text = ConDB.validNumber(payment_cash_input.Text);
            string text2 = ConDB.validNumber(payment_other_input.Text);
            if (text != "error" && text2 != "error")
            {
                float num = float.Parse(text);
                float num2 = float.Parse(text2);
                float num3 = num + num2;
                float num4 = float.Parse(ConDB.validNumber(totalSale_input.Text));
                if (num3 >= num4)
                {
                    return true;
                }
                change_input.Text = 0f.ToString("C").Replace(",00", "");
                try
                {
                    if (detailsalesform != null)
                    {
                        detailsalesform.AddChange(change_input.Text);
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
            return false;
        }

        internal void EnabledProcessButton(bool active)
        {
            processButton.Enabled = active;
            processButton2.Enabled = active;
            if (!active)
            {
                processButton.BackColor = Color.Gainsboro;
                processButton2.BackColor = Color.Gainsboro;
            }
            if (DetailDataGrid.RowCount > 0 && active && active)
            {
                processButton.BackColor = Color.ForestGreen;
                processButton2.BackColor = Color.ForestGreen;
            }
        }

        internal float CalculateChangeCashSale(bool Leave = false)
        {
            string text = ConDB.validNumber(payment_cash_input.Text);
            string s = ConDB.validNumber(payment_other_input.Text);
            if (text == "error")
            {
                return 0f;
            }
            float num = float.Parse(text);
            float num2 = float.Parse(s);
            float num3 = float.Parse(ConDB.validNumber(totalSale_input.Text));
            if (num2 == 0f && num >= num3)
            {
                EnabledProcessButton(active: true);
            }
            else if (num2 + num >= num3)
            {
                EnabledProcessButton(active: true);
                if (Leave && num != 0f)
                {
                    payment_other_input.Text = (num3 - num).ToString("C").Replace(",00", "");
                }
                if (Leave && num >= num3)
                {
                    payment_other_input.Text = 0f.ToString("C").Replace(",00", "");
                }
            }
            if (num >= num3)
            {
                change_input.Text = (num - num3).ToString("C").Replace(",00", "");
                num2 = 0f;
            }
            else
            {
                change_input.Text = "$0";
            }
            try
            {
                if (detailsalesform != null)
                {
                    detailsalesform.AddChange(change_input.Text);
                }
            }
            catch (Exception)
            {
            }
            if (num < num3)
            {
                num2 = num3 - num;
            }
            return num2;
        }

        internal float CalculateChangeCashOtherSale(bool Leave = false)
        {
            string s = ConDB.validNumber(payment_cash_input.Text);
            string text = ConDB.validNumber(payment_other_input.Text);
            string s2 = ConDB.validNumber(change_input.Text);
            if (text == "error")
            {
                return 0f;
            }
            float num = float.Parse(s);
            float num2 = float.Parse(text);
            float num3 = float.Parse(s2);
            float num4 = float.Parse(ConDB.validNumber(totalSale_input.Text));
            if (num == 0f && num2 >= num4)
            {
                EnabledProcessButton(active: true);
            }
            else if (num + num2 >= num4)
            {
                EnabledProcessButton(active: true);
                if (Leave && num2 != 0f)
                {
                    payment_cash_input.Text = (num4 - num2).ToString("C").Replace(",00", "");
                }
                else if (Leave && num2 < num4 && num3 == 0f)
                {
                    payment_cash_input.Text = (num4 - num2).ToString("C").Replace(",00", "");
                }
                else if (Leave && num < num4)
                {
                    payment_cash_input.Text = num4.ToString("C").Replace(",00", "");
                }
                else if (Leave && num > num4)
                {
                    return num;
                }
            }
            if (num2 >= num4)
            {
                change_input.Text = (num2 - num4).ToString("C").Replace(",00", "");
                try
                {
                    if (detailsalesform != null)
                    {
                        detailsalesform.AddChange(change_input.Text);
                    }
                }
                catch (Exception)
                {
                }
            }
            if (num2 < num4)
            {
                num = num4 - num2;
            }
            return num;
        }

        internal void OnlyNumberInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        internal void FormarInputCurrency()
        {
            double.TryParse(payment_cash_input.Text.Replace("$", ""), out var result);
            string text = result.ToString("C").Replace(",00", "");
            double.TryParse(payment_other_input.Text.Replace("$", ""), out var result2);
            string text2 = result2.ToString("C").Replace(",00", "");
            payment_cash_input.Text = text;
            payment_other_input.Text = text2;
        }

        private void payment_cash_input_KeyDown(object sender, KeyEventArgs e)
        {
            CalculateChangeCashSale();
            if (e.KeyData == Keys.F12 && processButton.Enabled)
            {
                processSales();
            }
            if (e.KeyData == Keys.F11 && processButton2.Enabled)
            {
                processSales(invoice: false);
            }
            if (e.KeyData == Keys.F10)
            {
                notes_input.Focus();
            }
            if (e.KeyData == Keys.F8)
            {
                payment_other_input.Focus();
            }
            if (e.KeyData == Keys.F7)
            {
                barcode_input.Focus();
            }
            if (e.KeyData == Keys.F6)
            {
                filterTextBox.Focus();
            }
        }

        private void payment_cash_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumberInput(sender, e);
        }

        private void payment_cash_input_Leave(object sender, EventArgs e)
        {
            CalculateChangeCashSale(Leave: true);
            FormarInputCurrency();
        }

        private void payment_cash_input_TextChanged(object sender, EventArgs e)
        {
            CalculateChangeCashSale();
        }

        private void payment_other_input_KeyDown(object sender, KeyEventArgs e)
        {
            CalculateChangeCashOtherSale();
            if (e.KeyData == Keys.F12 && processButton.Enabled)
            {
                processSales();
            }
            if (e.KeyData == Keys.F11 && processButton2.Enabled)
            {
                processSales(invoice: false);
            }
            if (e.KeyData == Keys.F10)
            {
                notes_input.Focus();
            }
            if (e.KeyData == Keys.F8)
            {
                payment_cash_input.Focus();
            }
            if (e.KeyData == Keys.F7)
            {
                barcode_input.Focus();
            }
            if (e.KeyData == Keys.F6)
            {
                filterTextBox.Focus();
            }
        }

        private void payment_other_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumberInput(sender, e);
        }

        private void payment_other_input_Leave(object sender, EventArgs e)
        {
            payment_cash_input.Text = CalculateChangeCashOtherSale(Leave: true).ToString("C");
            FormarInputCurrency();
        }

        private void payment_other_input_TextChanged(object sender, EventArgs e)
        {
            CalculateChangeCashOtherSale();
        }

        private void amount_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        internal void CalulateChangeCash(bool isCash)
        {
            double.TryParse(payment_cash_input.Text.Replace("$", ""), out var result);
            double.TryParse(payment_other_input.Text.Replace("$", ""), out var result2);
            double.TryParse(change_input.Text.Replace("$", ""), out var result3);
            double.TryParse(totalSale_input.Text.Replace("$", ""), out var result4);
            if (result == 0.0 && result2 == 0.0 && isCash)
            {
                result = result4;
            }
            else if (result == 0.0 && result2 == 0.0 && !isCash)
            {
                result2 = result4;
            }
            else if (result2 == 0.0 && result < result4)
            {
                result2 = result4 - result;
            }
            else if (result == 0.0 && result2 < result4)
            {
                result2 = result4 - result2;
            }
            string text = result.ToString("C").Replace(",00", "");
            string text2 = result2.ToString("C").Replace(",00", "");
            result3.ToString("C").Replace(",00", "");
            result4.ToString("C").Replace(",00", "");
            double num = result + result2 - result4;
            payment_cash_input.Text = text;
            payment_other_input.Text = text2;
            change_input.Text = ((num > 0.0) ? num.ToString("C").Replace(",00", "") : 0f.ToString("C").Replace(",00", ""));
            try
            {
                if (detailsalesform != null)
                {
                    detailsalesform.AddChange(change_input.Text);
                }
            }
            catch (Exception)
            {
            }
        }

        private void payment_other_input_Enter(object sender, EventArgs e)
        {
            CalulateChangeCash(isCash: false);
        }

        private void payment_cash_input_Enter(object sender, EventArgs e)
        {
            CalulateChangeCash(isCash: true);
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button button = new Button();
            Button button2 = new Button();
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            button.Text = "Si";
            button2.Text = "No";
            button.DialogResult = DialogResult.Yes;
            button2.DialogResult = DialogResult.No;
            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            button.SetBounds(228, 72, 75, 23);
            button2.SetBounds(309, 72, 75, 23);
            label.AutoSize = true;
            textBox.Anchor |= AnchorStyles.Right;
            button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[4] { label, textBox, button, button2 });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = button;
            form.CancelButton = button2;
            DialogResult result = form.ShowDialog();
            value = textBox.Text;
            return result;
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            //ClearFinishSale();
            cleanfields();
            name_input.ReadOnly = true;
            price_input.ReadOnly = true;
            name_input.TabStop = false;
            price_input.TabStop = false;
            barcode_input.Focus();
            calculateTotalSale();
        }

        private void Addproductsale_button_Click(object sender, EventArgs e)
        {
            cleanfields();
            name_input.ReadOnly = false;
            price_input.ReadOnly = false;
            name_input.TabStop = true;
            price_input.TabStop = true;
            current_name = string.Empty;
            name_input.Focus();
            row_selected_id = default(Guid).ToString();
            current_tax = 19f;
            current_stock = 1;
        }

        private void price_input_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void price_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                amount_input.SelectAll();
            }
        }

        private void DetailDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.KeyData == Keys.Add)
            {
                float num = float.Parse(DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["amount"].FormattedValue.ToString()) + 1f;
                float num2 = float.Parse(ConDB.validNumber(DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["price"].FormattedValue.ToString())) * num;
                DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["amount"].Value = num;
                DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["total"].Value = num2.ToString("C").Replace(",00", "");
            }
            if (e.KeyData == Keys.Subtract)
            {
                float num3 = float.Parse(DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["amount"].FormattedValue.ToString()) - 1f;
                if (num3 < 1f)
                {
                    num3 = 1f;
                }
                float num4 = float.Parse(ConDB.validNumber(DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["price"].FormattedValue.ToString())) * num3;
                DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["amount"].Value = num3;
                DetailDataGrid.Rows[dataGridView.CurrentCell.RowIndex].Cells["total"].Value = num4.ToString("C").Replace(",00", "");
            }
            if (Keys.Delete == e.KeyData)
            {
                DetailDataGrid.Rows.RemoveAt(dataGridView.CurrentCell.RowIndex);
            }
            calculateTotalSale();
            try
            {
                if (detailsalesform != null)
                {
                    detailsalesform.AddDetails((DataTable)DetailDataGrid.DataSource);
                }
            }
            catch (Exception)
            {
            }
        }

        private void InventoryDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            _ = e.Value;
        }

        private void DetailDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DetailDataGrid.Columns[e.ColumnIndex].Name == "name")
            {
                e.Value = e.Value.ToString().ToUpper();
                e.FormattingApplied = true;
            }
        }

        private void kg_input_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                amount_input_KeyUp(sender, e);
            }
        }

        private void kg_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void processButton2_Click(object sender, EventArgs e)
        {
            processSales(invoice: false);
        }

        internal void processSales(bool invoice = true)
        {
            if (detailsDataTable.Rows.Count <= 0)
            {
                barcode_input.Focus();
                return;
            }
            subTotalInvoice = (taxInvoice = (totalInvoice = 0f));
            for (int i = 0; i < detailsDataTable.Rows.Count; i++)
            {
                detailsDataTable.Rows[i]["total"] = (int.Parse(ConDB.validNumber(detailsDataTable.Rows[i]["total"].ToString())) + int.Parse(ConDB.validNumber(price_input.Text.ToString()))).ToString("C");
                float num = 0f;
                if (!bool.Parse(detailsDataTable.Rows[i]["exenta"].ToString()))
                {
                    num = float.Parse(detailsDataTable.Rows[i]["tax"].ToString()) / 100f;
                }
                float num2 = float.Parse(ConDB.validNumber(detailsDataTable.Rows[i]["total"].ToString()));
                float num3 = float.Parse(Math.Ceiling(num2 * num).ToString());
                float num4 = num2 - num3;
                float num5 = num4 + num3;
                subTotalInvoice += num4;
                taxInvoice += num3;
                totalInvoice += num5;
            }
            calculateTotalSale();
            if (decrease_checkbox.Checked)
            {
                string value = string.Empty;
                InputBox("Finalizar Venta", "Ingreso código de autorización para mermar venta", ref value);
                if (ConDB.getDecreaseCode(value, detailsDataTable, notes_input.Text))
                {
                    ClearFinishSale();
                    return;
                }
                MessageBox.Show("El código de autorización no fue correcto, se procede a venta normal", "Se realizara venta normal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (MessageBox.Show("Desea confirmar la venta?", "Confirmar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                float num6 = float.Parse(ConDB.validNumber(payment_cash_input.Text));
                float num7 = float.Parse(ConDB.validNumber(payment_other_input.Text));
                if (num6 == 0f && num7 == 0f)
                {
                    payment_cash_input.Text = totalInvoice.ToString("C").Replace(",00", "");
                }
                current_invoice_subTotalInvoice = subTotalInvoice;
                current_invoice_taxInvoice = taxInvoice;
                current_invoice_totalInvoice = totalInvoice;
                cuurent_invoice_payment_cash = payment_cash_input.Text.Replace(",00", "");
                current_invoice_payment_other = payment_other_input.Text.Replace(",00", "");
                current_invoice_change = change_input.Text.Replace(",00", "");
                current_invoice_id_sale = ConDB.insertSale(detailsDataTable.Rows.Count, current_invoice_taxInvoice, current_invoice_totalInvoice, notes_input.Text.Replace(",00", ""), cuurent_invoice_payment_cash, current_invoice_payment_other, current_invoice_change, ref current_invoice_nroTicket);
                if (current_invoice_id_sale == default(Guid))
                {
                    MessageBox.Show("Error al Procesar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                foreach (DataRow row in detailsDataTable.Rows)
                {
                    float num8 = float.Parse(row["tax"].ToString()) / 100f;
                    float num9 = float.Parse(ConDB.validNumber(row["total"].ToString()));
                    if (!ConDB.insertDetailSale(current_invoice_id_sale, Guid.Parse(row["id"].ToString()), row["name"].ToString(), row["price"].ToString(), row["amount"].ToString().Replace("$", ""), num9.ToString(), row["tax"].ToString(), float.Parse(Math.Ceiling(num9 * num8).ToString()).ToString()))
                    {
                        MessageBox.Show("no se pudo insertar " + row["name"].ToString());
                    }
                }
                try
                {
                    if (invoice)
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            PrintInvoice();
                        }));                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ClearFinishSale();
            }
            ticket_input.Text = current_invoice_nroTicket.ToString();
            barcode_input.Focus();
        }

        internal void PrintInvoice(string nroTicket = "")
        {
            Ticket ticket = new();
            DataTable dataTable;

            if (!string.IsNullOrEmpty(nroTicket))
            {
                current_invoice_nroTicket = int.Parse(nroTicket);
            }
            dataTable = ConDB.Current_invoice_details(current_invoice_nroTicket);

            if (!string.IsNullOrEmpty(nroTicket))
            {
                detailsDataTable = dataTable;
            }
            try
            {
                string text = $"TICKET: {current_invoice_nroTicket}, TOTAL: {current_invoice_totalInvoice}, FECHA: {DateTime.Now.ToShortDateString()}, HORA: {DateTime.Now.ToShortTimeString()}, VENDEDOR: {ConDB.userName}" + Environment.NewLine;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    text += string.Format("{0:C0}", dataTable.Rows[i]["amount"]).Replace(",00", "").Replace("$", "");
                    text += dataTable.Rows[i]["name"].ToString().ToUpper();
                    text = text + string.Format("{0:C0}", dataTable.Rows[i]["total"]).Replace(",00", "") + Environment.NewLine;
                }

                // https://www.codeproject.com/Articles/1347529/PDF417-Barcode-Encoder-Class-Library-and-Demo-App
                // create PDF417 barcode object
                Pdf417BarcodeEncoder Encoder = new Pdf417BarcodeEncoder
                {
                    // change default data columns
                    DefaultDataColumns = 10,
                };

                // encode barcode data
                //Encoder.Encode(text);
                //ticket.HeaderImage = Encoder.CreateBarcodeBitmap();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Bitmap headerImage2 = new Bitmap(Resources.barcode);
                //ticket.HeaderImage = headerImage2;
            }
            
            ticket.AddHeaderLine("DETALLE DE COMPRA");
            ticket.AddHeaderLine("");
            foreach (string dataHeadPo in ConDB.getDataHeadPos())
            {
                ticket.AddHeaderLine(dataHeadPo);
            }
            if (ConDB.userNameInvoice)
            {
                ticket.AddSubHeaderLine("VENDEDOR: " + ConDB.userName);
            }
            ticket.AddSubHeaderLine($"NOTA DE PEDIDO NRO#: {current_invoice_nroTicket}");
            ticket.AddSubHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + "       HORA: " + DateTime.Now.ToShortTimeString());
            float totalAcumulado = 0;
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                ticket.AddItem(string.Format("{0:C0}", dataTable.Rows[j]["amount"]).Replace(",00", "").Replace("$", ""), string.Format("{0} ({1} C/U)", dataTable.Rows[j]["name"].ToString().ToUpper(), float.Parse(dataTable.Rows[j]["price"].ToString()).ToString("C").Replace(",00", "")), string.Format("{0:C0}", dataTable.Rows[j]["total"]).Replace(",00", ""));
                current_invoice_totalInvoice = float.Parse(dataTable.Rows[j]["total_sales"].ToString());
                cuurent_invoice_payment_cash = dataTable.Rows[j]["payment_cash"].ToString();
                current_invoice_payment_other = dataTable.Rows[j]["payment_other"].ToString();
                current_invoice_change = dataTable.Rows[j]["change"].ToString();
                current_invoice_taxInvoice = float.Parse(dataTable.Rows[j]["tax"].ToString());
            }
            ticket.AddTotal("TOTAL", $"{current_invoice_totalInvoice:C0}".Replace(",00", ""));
            ticket.AddTotal("", "");
            ticket.AddTotal("EFECTIVO", $"{cuurent_invoice_payment_cash:C0}".Replace(",00", ""));
            ticket.AddTotal("TARJETA", $"{current_invoice_payment_other:C0}".Replace(",00", ""));
            ticket.AddTotal("CAMBIO", $"{current_invoice_change:C0}".Replace(",00", ""));
            ticket.AddTotal("", "");
            if (current_invoice_taxInvoice > 0f)
            {
                ticket.AddFooterLine("EL IVA DE ESTA COMPRA ES: " + $"{current_invoice_taxInvoice:C0}".Replace(",00", ""));
            }
            ticket.AddFooterLine("");
            ticket.AddFooterLine("VUELVA PRONTO");
            ticket.AddFooterLine("Para devolución o cambio de producto, favor presentar este detalle en buen estado.");
            if (!string.IsNullOrEmpty(ConDB.getParameters("BOLETA", "PROMOCION")))
            {
                ticket.AddFooterLine("===================================");
                string[] array = ConDB.getParameters("BOLETA", "PROMOCION").Split('|');
                foreach (string line in array)
                {
                    ticket.AddFooterLine(line);
                }
                if (string.IsNullOrEmpty(ConDB.getParameters("BOLETA", "ANUNCIO")))
                {
                    ticket.AddFooterLine("===================================");
                }
            }
            if (!string.IsNullOrEmpty(ConDB.getParameters("BOLETA", "ANUNCIO")))
            {
                ticket.AddFooterLine("===================================");
                string[] array = ConDB.getParameters("BOLETA", "ANUNCIO").Split('|');
                foreach (string line2 in array)
                {
                    ticket.AddFooterLine(line2);
                }
                ticket.AddFooterLine("===================================");
            }
            string parameters = ConDB.getParameters("IMPRESORA", "POS");
            if (ticket.PrinterExists(parameters))
            {
                ticket.PrintTicket(parameters);
            }
        }

        private static Bitmap ScaleImage(Bitmap bmp, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / bmp.Width;
            var ratioY = (double)maxHeight / bmp.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(bmp.Width * ratio);
            var newHeight = (int)(bmp.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private void reprint_button_Click(object sender, EventArgs e)
        {
            if (current_invoice_nroTicket > 0 || int.Parse(ticket_input.Text) > 0)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    PrintInvoice(ticket_input.Text);
                }));
            }
        }

        private void SalesForm_MouseClick(object sender, MouseEventArgs e)
        {
            barcode_input.Focus();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            barcode_input.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            filterTextBox.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (name_input.Enabled)
            {
                name_input.Focus();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (price_input.Enabled)
            {
                price_input.Focus();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(amount_input.Text))
            {
                amount_input.Focus();
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            kg_input.Focus();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            payment_cash_input.Focus();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            payment_other_input.Focus();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            notes_input.Focus();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            processButton2_Click(null, null);
        }

        private void label17_Click(object sender, EventArgs e)
        {
            processButton_Click(null, null);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            reprint_button_Click(null, null);
        }

        private void StartTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer_label.Text = DateTime.Now.ToString();
        }

        private void InventoryDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            filter_word = ConDB.validString(filterTextBox.Text.Trim());
            if (string.IsNullOrEmpty(filter_word)) {
                InventoryDataGrid.DataSource = ConDB.getProductsList("", isSales: true);
            } else {
                (InventoryDataGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("[_RowString] LIKE '%{0}%'", filter_word);

            }
            setProductsGridFormat();
        }

        private void btnCleanSearch_Click(object sender, EventArgs e)
        {
            ClearFinishSale();
            filterTextBox.Focus();
        }
    }
}
