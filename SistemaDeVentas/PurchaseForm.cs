using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeVentas
{
    public partial class PurchaseForm : Form
    {
        private string row_selected_id = "";

        private string filter_word = "";

        private string original_amount = "";

        public PurchaseForm()
        {
            InitializeComponent();
            InventoryDataGrid.DataSource = ConDB.getProductsList(filter_word);
            formatDataGrid();
            DeleteButton.Visible = false;
        }

        private void filterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                filter_word = ConDB.validString(filterTextBox.Text);
                filterTextBox.Text = filter_word;
                InventoryDataGrid.DataSource = ConDB.getProductsList(filter_word);
                formatDataGrid();
            }
        }

        private void formatDataGrid()
        {
            InventoryDataGrid.Columns["name"].HeaderText = "Nombre del Producto";
            InventoryDataGrid.Columns["amount"].HeaderText = "Cantidad";
            InventoryDataGrid.Columns["sale_price"].HeaderText = "precio de venta";
            InventoryDataGrid.Columns["price"].HeaderText = "precio de compra";
            InventoryDataGrid.Columns["sale_price"].DefaultCellStyle.Format = "C";
            InventoryDataGrid.Columns["sale_price"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();
            InventoryDataGrid.Columns["price"].DefaultCellStyle.Format = "C";
            InventoryDataGrid.Columns["price"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();
            InventoryDataGrid.Columns["id"].Visible = false;
            InventoryDataGrid.Columns["name"].Width = 280;
            InventoryDataGrid.Columns["amount"].Width = 60;
            InventoryDataGrid.Columns["sale_price"].Width = 60;
            InventoryDataGrid.Columns["price"].Width = 60;
            InventoryDataGrid.Columns["minimum"].Visible = false;
            InventoryDataGrid.Columns["bar_code"].Visible = false;
            InventoryDataGrid.Columns["photo"].Visible = false;
            InventoryDataGrid.Columns["stock"].Visible = false;
            InventoryDataGrid.Columns["line"].Visible = false;
            InventoryDataGrid.Columns["date"].Visible = false;
            InventoryDataGrid.Columns["state"].Visible = false;
        }

        private void clean()
        {
            name_input.Text = string.Empty;
            amount_input.Text = "0,0";
            price_input.Text = "0,0";
            filterTextBox.Text = string.Empty;
            barcode_input.Text = string.Empty;
            barcode_input.Focus();
        }

        private void InventoryDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                row_selected_id = InventoryDataGrid.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                original_amount = InventoryDataGrid.Rows[e.RowIndex].Cells["amount"].FormattedValue.ToString();
                string text = InventoryDataGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
                string text2 = InventoryDataGrid.Rows[e.RowIndex].Cells["sale_price"].FormattedValue.ToString();
                name_input.Text = text;
                amount_input.Text = original_amount;
                price_input.Text = text2;
                price_input.Focus();
                DeleteButton.Visible = true;
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (!(row_selected_id == ""))
            {
                string text = ConDB.validNumber(amount_input.Text);
                string text2 = ConDB.validNumber(price_input.Text);
                if (text2 == "error")
                {
                    MessageBox.Show("El precio no es valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (text == "error")
                {
                    MessageBox.Show("La cantidad no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (ConDB.RegisterWareHouseEntry(row_selected_id, name_input.Text, text2, text))
                {
                    MessageBox.Show("Entrada en almacen exitosa", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    InventoryDataGrid.DataSource = ConDB.getProductsList(filter_word);
                    formatDataGrid();
                    clean();
                    filterTextBox.Focus();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el producto, puede que ya exista un producto con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void PurchaseForm_Shown(object sender, EventArgs e)
        {
            filterTextBox.Focus();
        }

        private void PurchaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.mainForm.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                List<string> productByBarcode = ConDB.getProductByBarcode(barcode_input.Text);
                if (productByBarcode.Count != 0)
                {
                    row_selected_id = productByBarcode[0];
                    filter_word = ConDB.validString(productByBarcode[1]);
                    filterTextBox.Text = filter_word;
                    name_input.Text = filter_word;
                    price_input.Text = productByBarcode[2];
                    amount_input.Text = productByBarcode[3];
                    InventoryDataGrid.DataSource = ConDB.getProductsList(filter_word);
                    formatDataGrid();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea borra este producto? esto conlleva a perdida de información", "Confirmar Borrado!!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                if (ConDB.DeleteWareHouseEntry(row_selected_id))
                {
                    MessageBox.Show("El Producto fue eliminado exitosamente del almacen", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    InventoryDataGrid.DataSource = ConDB.getProductsList(filter_word);
                    formatDataGrid();
                    clean();
                    filterTextBox.Focus();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el producto del almacen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            barcode_input.Focus();
        }

    }
}
