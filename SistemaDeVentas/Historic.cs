using LibPrintTicket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeVentas
{
    public partial class Historic : Form
    {
        public Historic()
        {
            InitializeComponent();
            loadCategory();
        }

        private void loadCategory()
        {
            category_combobox.DataSource = ConDB.getCategoryList();
            category_combobox.DisplayMember = "Text";
            category_combobox.ValueMember = "id";
        }

        private void MostrarButton_Click(object sender, EventArgs e)
        {
            detailDataGrid.DataSource = null;
            cancelsale_button.Enabled = false;
            string start = startTimePicker.Value.ToString("yyyy-MM-dd");
            string end = endTimePicker.Value.ToString("yyyy-MM-dd");
            if (ventas_radio.Checked)
            {
                MainDataGrid.DataSource = ConDB.getListOfSales(start, end);
                formatDataGrid_Sales();
                totalInvoice_input.Text = ConDB.getTotalInvoiceSales(start, end).ToString();
                total_input.Text = ConDB.getTotalSales(start, end).ToString("C").Replace(",00", "");
                totalTax_input.Text = ConDB.getTotalTaxSales(start, end).ToString("C").Replace(",00", "");
                totalAfecta_input.Text = ConDB.getTotalSalesAfecta(start, end).ToString("C").Replace(",00", "");
                totalExenta_input.Text = ConDB.getTotalSalesExenta(start, end).ToString("C").Replace(",00", "");
                totalPercent_input.Text = (float.Parse(percent_input.Text) / 100f * ConDB.getTotalSalesAfecta(start, end, profit: true)).ToString("C").Replace(",00", "");
                totalPaymentCash_input.Text = ConDB.getTotalPaymentCash(start, end).ToString("C").Replace(",00", "");
                totalPaymentOtherCash_input.Text = ConDB.getTotalPaymentOtherCash(start, end).ToString("C").Replace(",00", "");
                if (MainDataGrid.RowCount > 0)
                {
                    print_button.Enabled = true;
                }
                else
                {
                    print_button.Enabled = false;
                }
            }
            else
            {
                if (ingreso_radio.Checked)
                {
                    return;
                }
                if (producto_radio.Checked)
                {
                    MainDataGrid.DataSource = ConDB.getHistoricalProducts();
                    formatDataGrid_historicalProducts();
                    totalInvoice_input.Text = ConDB.getTotalOutlay().ToString("C").Replace(",00", "");
                    total_input.Text = ConDB.getTotalSoldOut().ToString("C").Replace(",00", "");
                    totalTax_input.Text = ConDB.getTotalHopedSales().ToString("C").Replace(",00", "");
                }
                else if (decrease_radio.Checked)
                {
                    MainDataGrid.DataSource = ConDB.getListOfDecrease(start, end);
                    formatDataGrid_Decrease();
                    totalInvoice_input.Text = ((DataTable)MainDataGrid.DataSource).AsEnumerable().Sum((DataRow x) => float.Parse(x.Field<string>("Total"))).ToString("c0");
                }
            }
        }

        private void SaleDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && MainDataGrid.Columns.Contains("id"))
            {
                detailDataGrid.DataSource = ConDB.getListOfDetails(MainDataGrid.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                formatDataGrid_details();
                nroticket = MainDataGrid.Rows[e.RowIndex].Cells["ticket"].FormattedValue.ToString();
                cancelsale_button.Enabled = true;
            }
        }

        private void formatDataGrid_Decrease()
        {
            MainDataGrid.Columns["Nombre"].Width = 450;
            MainDataGrid.Columns["Cantidad"].Width = 60;
            MainDataGrid.Columns["IVA"].Width = 50;
            MainDataGrid.Columns["Total"].Width = 70;
            MainDataGrid.Columns["Nota"].Width = 150;
        }

        private void formatDataGrid_Sales()
        {
            MainDataGrid.Columns["amount"].HeaderText = "Cantidad de Items";
            MainDataGrid.Columns["total"].HeaderText = "Total de Venta";
            MainDataGrid.Columns["date"].HeaderText = "Fecha";
            MainDataGrid.Columns["tax"].HeaderText = "IVA";
            MainDataGrid.Columns["note"].HeaderText = "Nota de Venta";
            MainDataGrid.Columns["change"].HeaderText = "Vuelto a Cliente";
            MainDataGrid.Columns["payment_cash"].HeaderText = "Pago Efetivo";
            MainDataGrid.Columns["payment_other"].HeaderText = "Pago Tarjeta";
            MainDataGrid.Columns["ticket"].HeaderText = "Nro. Boleta";
            MainDataGrid.Columns["total"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["tax"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["change"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["payment_cash"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["payment_other"].DefaultCellStyle.Format = "c";
            // Now that DataGridView has calculated it's Widths; we can now store each column Width values.
            for (int i = 0; i <= MainDataGrid.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = MainDataGrid.Columns[i].Width;
                // Remove AutoSizing:
                MainDataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // Set Width to calculated AutoSize value:
                MainDataGrid.Columns[i].Width = colw;
                MainDataGrid.Columns[i].Visible = false;
            }
            MainDataGrid.Columns["ticket"].Visible = true;
            MainDataGrid.Columns["amount"].Visible = true;
            MainDataGrid.Columns["tax"].Visible = true;
            MainDataGrid.Columns["total"].Visible = true;
            MainDataGrid.Columns["change"].Visible = true;
            MainDataGrid.Columns["payment_cash"].Visible = true;
            MainDataGrid.Columns["payment_other"].Visible = true;
            MainDataGrid.Columns["date"].Visible = true;

            MainDataGrid.Columns["ticket"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MainDataGrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MainDataGrid.Columns["tax"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MainDataGrid.Columns["total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MainDataGrid.Columns["change"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MainDataGrid.Columns["payment_cash"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MainDataGrid.Columns["payment_other"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MainDataGrid.Columns["date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void formatDataGrid_details()
        {
            detailDataGrid.Columns["product_name"].HeaderText = "Producto";
            detailDataGrid.Columns["amount"].HeaderText = "Cantidad";
            detailDataGrid.Columns["price"].HeaderText = "Precio";
            detailDataGrid.Columns["total"].HeaderText = "Total";
            detailDataGrid.Columns["total_tax"].HeaderText = "IVA";
            detailDataGrid.Columns["price"].DefaultCellStyle.Format = "c";
            detailDataGrid.Columns["total"].DefaultCellStyle.Format = "c";
            detailDataGrid.Columns["total_tax"].DefaultCellStyle.Format = "c";
            // Now that DataGridView has calculated it's Widths; we can now store each column Width values.
            for (int i = 0; i <= detailDataGrid.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = detailDataGrid.Columns[i].Width;
                // Remove AutoSizing:
                detailDataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // Set Width to calculated AutoSize value:
                detailDataGrid.Columns[i].Width = colw;
                detailDataGrid.Columns[i].Visible = false;
            }
            detailDataGrid.Columns["product_name"].Visible = true;
            detailDataGrid.Columns["amount"].Visible = true;
            detailDataGrid.Columns["price"].Visible = true;
            detailDataGrid.Columns["total"].Visible = true;
            detailDataGrid.Columns["total_tax"].Visible = true;

            detailDataGrid.Columns["product_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            detailDataGrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            detailDataGrid.Columns["price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            detailDataGrid.Columns["total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            detailDataGrid.Columns["total_tax"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void formatDataGrid_entries()
        {
            MainDataGrid.Columns["product_name"].HeaderText = "Nombre del Producto";
            MainDataGrid.Columns["total"].HeaderText = "total de la compra";
            MainDataGrid.Columns["amount"].HeaderText = "Cantidad";
            MainDataGrid.Columns["date"].HeaderText = "fecha";
            MainDataGrid.Columns["id"].Visible = false;
            MainDataGrid.Columns["id_product"].Visible = false;
            MainDataGrid.Columns["id_user"].Visible = false;
            MainDataGrid.Columns["product_name"].Width = 250;
            MainDataGrid.Columns["amount"].Width = 60;
            MainDataGrid.Columns["total"].Width = 60;
            MainDataGrid.Columns["date"].Width = 100;
        }

        private void formatDataGrid_historicalProducts()
        {
            MainDataGrid.Columns["Stock Inicial"].Width = 70;
            MainDataGrid.Columns["Stock Actual"].Width = 70;
            MainDataGrid.Columns["Nombre Producto"].Width = 160;
            MainDataGrid.Columns["Precio Compra"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["Precio Venta"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["Total Venta Proyectada"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["Total Pendiente por Vender"].DefaultCellStyle.Format = "c";
            MainDataGrid.Columns["Total Vendido"].DefaultCellStyle.Format = "c";
        }

        private void ventas_radio_CheckedChanged(object sender, EventArgs e)
        {
            labelGrid1.Text = "Ventas:";
            detail_label.Visible = true;
            detailDataGrid.Visible = true;

            total_label.Visible = true;
            total_label.Text = "Total Ventas:";
            total_input.Visible = true;
            total_input.Text = "$0";

            totalInvoice_label.Visible = true;
            totalInvoice_label.Text = "Total Boletas:";
            totalInvoice_input.Visible = true;
            totalInvoice_input.Text = "$0";

            totalTax_label.Visible = true;
            totalTax_label.Text = "Total IVA:";
            totalTax_input.Visible = true;
            totalTax_input.Text = "$0";

            totalAfecta_label.Visible = true;
            totalAfecta_input.Visible = true;
            totalAfecta_input.Text = "$0";

            totalExenta_label.Visible = true;
            totalExenta_input.Visible = true;
            totalExenta_input.Text = "$0";

            MainDataGrid.DataSource = null;
            MainDataGrid.Refresh();
            detailDataGrid.DataSource = null;
            detailDataGrid.Refresh();
            startTimePicker.Enabled = true;
            endTimePicker.Enabled = true;
            totalPaymentCash_label.Visible = true;
            totalPaymentCash_input.Visible = true;
            totalPaymentCash_input.Text = "$0";
            totalPaymentOtherCash_label.Visible = true;
            totalPaymentOtherCash_input.Visible = true;
            totalPaymentOtherCash_input.Text = "$0";
            category_label.Visible = true;
            category_combobox.Visible = true;
            totalCategory_label.Visible = true;
            totalCategory_input.Visible = true;
            totalCategory_input.Text = "$0";
            totalpercent_label1.Visible = true;
            totalPercent_input.Visible = true;
            totalPercent_input.Text = "$0";
            percent_label.Visible = true;
            percent_input.Visible = true;
            print_button.Visible = true;
            lblResumenPrint.Visible = true;
        }

        private void ingreso_radio_CheckedChanged(object sender, EventArgs e)
        {
            labelGrid1.Text = "Ingreso a Almacen:";
            detail_label.Visible = false;
            detailDataGrid.Visible = false;
            total_label.Visible = false;
            total_input.Visible = false;
            total_input.Text = "$0";
            totalInvoice_label.Visible = false;
            totalInvoice_input.Visible = false;
            totalTax_label.Visible = false;
            totalTax_input.Visible = false;
            MainDataGrid.DataSource = null;
            MainDataGrid.Refresh();
            detailDataGrid.DataSource = null;
            detailDataGrid.Refresh();
            startTimePicker.Enabled = true;
            endTimePicker.Enabled = true;
            totalAfecta_label.Visible = false;
            totalAfecta_input.Visible = false;
            totalExenta_label.Visible = false;
            totalExenta_input.Visible = false;
            totalPaymentCash_label.Visible = false;
            totalPaymentCash_input.Visible = false;
            totalPaymentOtherCash_label.Visible = false;
            totalPaymentOtherCash_input.Visible = false;
            category_label.Visible = false;
            category_combobox.Visible = false;
            totalCategory_label.Visible = false;
            totalCategory_input.Visible = false;
            totalCategory_input.Text = "$0";
            totalpercent_label1.Visible = false;
            percent_label.Visible = false;
            percent_input.Visible = false;
            totalPercent_input.Visible = false;
            print_button.Visible = false;
            lblResumenPrint.Visible = false;
        }

        private void producto_radio_CheckedChanged(object sender, EventArgs e)
        {
            labelGrid1.Text = "Historico de Productos:";
            detailDataGrid.Visible = false;
            detail_label.Visible = false;
            total_label.Visible = true;
            total_label.Text = "Total Vendido:";
            total_input.Visible = true;
            totalInvoice_label.Visible = true;
            totalInvoice_label.Text = "Total Invertido:";
            totalInvoice_input.Visible = true;
            totalInvoice_input.Text = "$0";
            totalTax_label.Visible = true;
            totalTax_label.Text = "Total Por Vender:";
            totalTax_input.Visible = true;
            totalTax_input.Text = "$0";
            MainDataGrid.DataSource = null;
            MainDataGrid.Refresh();
            detailDataGrid.DataSource = null;
            detailDataGrid.Refresh();
            startTimePicker.Enabled = false;
            endTimePicker.Enabled = false;
            totalAfecta_label.Visible = false;
            totalAfecta_input.Visible = false;
            totalExenta_label.Visible = false;
            totalExenta_input.Visible = false;
            totalPaymentCash_label.Visible = false;
            totalPaymentCash_input.Visible = false;
            totalPaymentOtherCash_label.Visible = false;
            totalPaymentOtherCash_input.Visible = false;
            category_label.Visible = false;
            category_combobox.Visible = false;
            totalCategory_label.Visible = false;
            totalCategory_input.Visible = false;
            totalCategory_input.Text = "$0";
            totalpercent_label1.Visible = false;
            percent_label.Visible = false;
            percent_input.Visible = false;
            totalPercent_input.Visible = false;
            print_button.Visible = false;
            lblResumenPrint.Visible = false;
        }

        private void decrease_radio_CheckedChanged(object sender, EventArgs e)
        {
            labelGrid1.Text = "Mermas:";
            detail_label.Visible = false;
            detailDataGrid.Visible = false;
            total_label.Visible = false;
            total_input.Visible = false;
            total_input.Text = "$0";
            totalInvoice_label.Visible = true;
            totalInvoice_input.Visible = true;
            totalInvoice_input.Text = "$0";
            totalTax_label.Visible = false;
            totalTax_input.Visible = false;
            MainDataGrid.DataSource = null;
            MainDataGrid.Refresh();
            detailDataGrid.DataSource = null;
            detailDataGrid.Refresh();
            startTimePicker.Enabled = true;
            endTimePicker.Enabled = true;
            totalAfecta_label.Visible = false;
            totalAfecta_input.Visible = false;
            totalExenta_label.Visible = false;
            totalExenta_input.Visible = false;
            totalPaymentCash_label.Visible = false;
            totalPaymentCash_input.Visible = false;
            totalPaymentOtherCash_label.Visible = false;
            totalPaymentOtherCash_input.Visible = false;
            category_label.Visible = false;
            category_combobox.Visible = false;
            totalCategory_label.Visible = false;
            totalCategory_input.Visible = false;
            totalCategory_input.Text = "$0";
            totalpercent_label1.Visible = false;
            percent_label.Visible = false;
            percent_input.Visible = false;
            totalPercent_input.Visible = false;
            print_button.Visible = false;
            lblResumenPrint.Visible = false;
        }

        private void Historic_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.mainForm.Show();
        }

        private void category_combobox_SelectedValueChanged(object sender, EventArgs e)
        {
            string start = startTimePicker.Value.ToString("yyyy-MM-dd");
            string end = endTimePicker.Value.ToString("yyyy-MM-dd");
            totalCategory_input.Text = ConDB.getTotalSalesCategory(start, end, category_combobox.SelectedValue.ToString()).ToString("C").Replace(",00", "");
        }

        private void percent_input_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void percent_input_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(percent_input.Text))
            {
                string start = startTimePicker.Value.ToString("yyyy-MM-dd");
                string end = endTimePicker.Value.ToString("yyyy-MM-dd");
                totalPercent_input.Text = (float.Parse(percent_input.Text) / 100f * ConDB.getTotalSalesAfecta(start, end, profit: true)).ToString("C").Replace(",00", "");
            }
        }

        private void cancelsale_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea anular la venta #" + nroticket + "?", "Confirmar Anulación!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                ConDB.CancelSale(int.Parse(nroticket));
                detailDataGrid.DataSource = null;
                MostrarButton_Click(null, null);
            }
        }

        private void print_button_Click(object sender, EventArgs e)
        {
            if (MainDataGrid.RowCount <= 0)
            {
                return;
            }
            string text = startTimePicker.Value.ToString("yyyy-MM-dd");
            string text2 = endTimePicker.Value.ToString("yyyy-MM-dd");
            Ticket ticket = new Ticket();
            ticket.AddHeaderLine("===================================");
            ticket.AddHeaderLine("RESUMEN VENTA " + text + " AL " + text2);
            ticket.AddHeaderLine("===================================");
            ticket.AddHeaderLine("");
            ticket.AddHeaderLine("TOTAL BOLETAS: " + totalInvoice_input.Text.Replace(",00", ""));
            ticket.AddHeaderLine("TOTAL VENTAS: " + total_input.Text.Replace(",00", ""));
            ticket.AddHeaderLine("TOTAL IVA: " + totalTax_input.Text.Replace(",00", ""));
            ticket.AddHeaderLine("TOTAL AFECTA: " + totalAfecta_input.Text.Replace(",00", ""));
            ticket.AddHeaderLine("TOTAL EXENTA: " + totalExenta_input.Text.Replace(",00", ""));
            ticket.AddHeaderLine("TOTAL PAGO EFECTIVO: " + totalPaymentCash_input.Text.Replace(",00", ""));
            ticket.AddHeaderLine("TOTAL OTROS PAGOS: " + totalPaymentOtherCash_input.Text.Replace(",00", ""));
            ticket.AddHeaderLine("===================================");
            ticket.AddHeaderLine("GANANCIA=VENTAS-EXENTA:" + (float.Parse(ConDB.validNumber(total_input.Text)) - float.Parse(ConDB.validNumber(totalExenta_input.Text))).ToString("C").Replace(",00", ""));
            string text3 = (float.Parse("30") / 100f * ConDB.getTotalSalesAfecta(text, text2, profit: true)).ToString("C").Replace(",00", "");
            ticket.AddHeaderLine("30% SOBRE LA GANANCIA: " + text3);
            ticket.AddHeaderLine("===================================");
            ticket.AddHeaderLine("");
            ticket.AddHeaderLine("DETALLE POR DEPARTAMENTO/CATEGORÍA");
            ticket.AddHeaderLine("===================================");
            foreach (DataRow row in ConDB.getCategoryList().Rows)
            {
                string id = row["id"].ToString().ToUpper();
                if (!(ConDB.getTotalSalesCategory(text, text2, id) > 0f))
                {
                    continue;
                }
                ticket.AddHeaderLine("-----------------------------------");
                ticket.AddHeaderLine(ConDB.getTotalSalesCategory(text, text2, id).ToString("C").Replace(",00", "") + " DE " + row["Text"].ToString().Split('(')[0]);
                ticket.AddHeaderLine("-----------------------------------");
                foreach (DataRow row2 in ConDB.getTotalSalesSubCategory(text, text2, id).Rows)
                {
                    ticket.AddHeaderLine(Regex.Replace(row2["text"].ToString().Trim(), "\\t|\\n|\\r", "") + " : " + float.Parse(row2["total"].ToString()).ToString("C").Replace(",00", ""));
                }
                ticket.AddHeaderLine("");
            }
            ticket.AddHeaderLine("===================================");
            string parameters = ConDB.getParameters("IMPRESORA", "POS");
            if (ticket.PrinterExists(parameters))
            {
                ticket.PrintTicket(parameters);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Historic_Load(object sender, EventArgs e)
        {

        }
    }
}
