using System.Data;

namespace SistemaDeVentas
{
    public partial class InventoryForm : Form
    {
        private string row_selected_id = string.Empty;

        private string filter_word = string.Empty;

        private string row_selected_id_pack = string.Empty;

        private string current_idcategory = string.Empty;

        private string current_idsubcategory = string.Empty;

        private string idpack_product = default(Guid).ToString();

        private DataTable detailsDataTable = new DataTable();

        public InventoryForm()
        {
            InitializeComponent();
        }

        private void filterTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void loadListCBIVA()
        {
            listIVA_combobox.DataSource = ConDB.getTaxList();
            listIVA_combobox.DisplayMember = "Value";
            listIVA_combobox.ValueMember = "id";
        }

        private void loadCategory()
        {
            category_combobox.DataSource = ConDB.getCategoryList();
            category_combobox.DisplayMember = "Text";
            category_combobox.ValueMember = "id";
            subcategory_combobox.DataSource = ConDB.getCategoryList(subcategory: true);
            subcategory_combobox.DisplayMember = "Text";
            subcategory_combobox.ValueMember = "id";
        }

        private void formatDataGrid()
        {
            InventoryDataGrid.Columns["name"].HeaderText = "Nombre del Producto";
            InventoryDataGrid.Columns["amount"].HeaderText = "Stock actual";
            InventoryDataGrid.Columns["price"].HeaderText = "Precio de Compra";
            InventoryDataGrid.Columns["sale_price"].Visible = false;
            if (ConDB.userRole == 1 || ConDB.userRole == 2)
            {
                InventoryDataGrid.Columns["sale_price"].Visible = true;
                InventoryDataGrid.Columns["sale_price"].HeaderText = "Precio de Venta";
                InventoryDataGrid.Columns["sale_price"].DefaultCellStyle.Format = "C";
                InventoryDataGrid.Columns["sale_price"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();
                InventoryDataGrid.Columns["sale_price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            InventoryDataGrid.Columns["minimum"].HeaderText = "Stock Minimo";
            InventoryDataGrid.Columns["bar_code"].HeaderText = "Codigo de Barras";
            InventoryDataGrid.Columns["expiration"].HeaderText = "Fecha Vencimiento";
            InventoryDataGrid.Columns["id"].Visible = false;
            InventoryDataGrid.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["minimum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["price"].DefaultCellStyle.Format = "C";
            InventoryDataGrid.Columns["price"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();
            InventoryDataGrid.Columns["bar_code"].Visible = false;
            InventoryDataGrid.Columns["photo"].Visible = false;
            InventoryDataGrid.Columns["stock"].Visible = false;
            InventoryDataGrid.Columns["price"].Visible = true;
            InventoryDataGrid.Columns["line"].Visible = false;
            InventoryDataGrid.Columns["date"].Visible = false;
            InventoryDataGrid.Columns["state"].Visible = false;
            InventoryDataGrid.Columns["id_tax"].Visible = false;
            InventoryDataGrid.Columns["exenta"].Visible = false;
            InventoryDataGrid.Columns["id_category"].Visible = false;
            InventoryDataGrid.Columns["isPack"].Visible = false;
            InventoryDataGrid.Columns["id_pack"].Visible = false;
            InventoryDataGrid.Columns["id_subcategory"].Visible = false;
            InventoryDataGrid.Refresh();            

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
        }

        private void setDetailsGridFormat()
        {
            DetailsPackDatagrid.DataSource = detailsDataTable;
            detailsDataTable.Columns.Add("id");
            detailsDataTable.Columns.Add("name");
            detailsDataTable.Columns.Add("price");
            detailsDataTable.Columns.Add("amount");
            detailsDataTable.Columns.Add("barcode");
            DetailsPackDatagrid.Columns["id"].Visible = false;
            DetailsPackDatagrid.Columns["barcode"].Visible = false;
            DetailsPackDatagrid.Columns["price"].Visible = false;
            DetailsPackDatagrid.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DetailsPackDatagrid.Columns["price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DetailsPackDatagrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DetailsPackDatagrid.Columns["name"].HeaderText = "Nombre del Producto";
            DetailsPackDatagrid.Columns["price"].HeaderText = "Precio Unidad";
            DetailsPackDatagrid.Columns["amount"].HeaderText = "Cantidad";
            DetailsPackDatagrid.Columns["price"].DefaultCellStyle.Format = "C";
            DetailsPackDatagrid.Columns["price"].DefaultCellStyle.FormatProvider = ConDB.getCultureInfo();

            // Now that DataGridView has calculated it's Widths; we can now store each column Width values.
            for (int i = 0; i <= DetailsPackDatagrid.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = DetailsPackDatagrid.Columns[i].Width;
                // Remove AutoSizing:
                DetailsPackDatagrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // Set Width to calculated AutoSize value:
                DetailsPackDatagrid.Columns[i].Width = colw;
            }
        }

        private void InventoryDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            row_selected_id = InventoryDataGrid.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            string text = InventoryDataGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
            string text2 = InventoryDataGrid.Rows[e.RowIndex].Cells["amount"].FormattedValue.ToString();
            string text3 = InventoryDataGrid.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString();
            string text4 = InventoryDataGrid.Rows[e.RowIndex].Cells["minimum"].FormattedValue.ToString();
            string text5 = InventoryDataGrid.Rows[e.RowIndex].Cells["bar_code"].FormattedValue.ToString();
            string selectedValue = InventoryDataGrid.Rows[e.RowIndex].Cells["id_tax"].FormattedValue.ToString();
            current_idcategory = InventoryDataGrid.Rows[e.RowIndex].Cells["id_category"].FormattedValue.ToString();
            current_idsubcategory = InventoryDataGrid.Rows[e.RowIndex].Cells["id_subcategory"].FormattedValue.ToString();
            string value = InventoryDataGrid.Rows[e.RowIndex].Cells["isPack"].FormattedValue.ToString();
            idpack_product = InventoryDataGrid.Rows[e.RowIndex].Cells["id_pack"].FormattedValue.ToString();
            if (string.IsNullOrEmpty(idpack_product))
            {
                idpack_product = default(Guid).ToString();
            }
            string text6 = InventoryDataGrid.Rows[e.RowIndex].Cells["sale_price"].FormattedValue.ToString();
            name_input.Text = text;
            amount_input.Text = text2.Replace(",00", "");
            price_input.Text = text3.Replace(",00", "");
            if (ConDB.userRole == 1 || ConDB.userRole == 2)
            {
                sales_input.Text = text6.Replace(",00", "");
            }
            minimum_input.Text = text4;
            barcode_input.Text = text5;
            listIVA_combobox.SelectedValue = selectedValue;
            if (string.IsNullOrEmpty(current_idcategory))
            {
                category_combobox.SelectedIndex = 0;
            }
            else
            {
                foreach (DataRowView item in category_combobox.Items)
                {
                    if (item.Row[0].ToString() == current_idcategory)
                    {
                        category_combobox.SelectedItem = item;
                    }
                }
            }
            if (string.IsNullOrEmpty(current_idsubcategory))
            {
                subcategory_combobox.SelectedIndex = 0;
            }
            else
            {
                foreach (DataRowView item2 in subcategory_combobox.Items)
                {
                    if (item2.Row[0].ToString() == current_idsubcategory)
                    {
                        subcategory_combobox.SelectedItem = item2;
                    }
                }
            }
            pack_checkbox.Checked = bool.Parse(value);
            if (pack_checkbox.Checked)
            {
                detailsDataTable = ConDB.getProductsListPack(idpack_product);
                DetailsPackDatagrid.DataSource = detailsDataTable;
            }
            SaveButton.Visible = true;
            DeleteButton.Visible = true;
            InsertButton.Visible = false;
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void clean()
        {
            InventoryDataGrid.DataSource = ConDB.getProductsList("", isSales: false, isInventory: true);
            formatDataGrid();
            loadListCBIVA();
            loadCategory();
            //if (ConDB.userRole == 1 || ConDB.userRole == 2)
            //{
            //    sales_input.Enabled = true;
            //    sales_input.ReadOnly = false;
            //}
            name_input.Text = string.Empty;
            amount_input.Text = "0";
            price_input.Text = "0";
            minimum_input.Text = "0";
            barcode_input.Text = string.Empty;
            sales_input.Text = "0";
            filterTextBox.Text = (filter_word = string.Empty);
            SaveButton.Visible = false;
            DeleteButton.Visible = false;
            InsertButton.Visible = true;
            packamount_input.ReadOnly = true;
            packamount_input.Text = string.Empty;
            packbarcode_input.ReadOnly = true;
            packbarcode_input.Text = string.Empty;
            packname_input.ReadOnly = true;
            packname_input.Text = string.Empty;
            pack_checkbox.Checked = false;
            detailsDataTable.Clear();
            tabPage1.Focus();
            foreach (DataRowView item in listIVA_combobox.Items)
            {
                if (item.Row[1].ToString() == "IVA 19%")
                {
                    listIVA_combobox.SelectedItem = item;
                }
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            string name = ConDB.validString(name_input.Text);
            string amount = ConDB.validNumber(amount_input.Text);
            string text = ConDB.validNumber(price_input.Text);
            string text2 = ConDB.validNumber(minimum_input.Text);
            name_input.Text = name;
            if (text == "error")
            {
                MessageBox.Show("El precio no es valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (text2 == "error")
            {
                MessageBox.Show("El stock no es valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Guid.NewGuid().ToString();
            idpack_product = ((pack_checkbox.Checked && default(Guid).ToString() == idpack_product) ? Guid.NewGuid().ToString() : default(Guid).ToString());
            if (ConDB.CreateProduct(name, amount, text, text2, barcode_input.Text, listIVA_combobox.SelectedValue.ToString(), isExenta_checkbox.Checked, expiration_datetimepicker.Value, current_idcategory, pack_checkbox.Checked, idpack_product, current_idsubcategory))
            {
                MessageBox.Show("Producto creado exitosamente", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (pack_checkbox.Checked)
                {
                    for (int i = 0; i < DetailsPackDatagrid.RowCount; i++)
                    {
                        ConDB.CreatePackProduct(idpack_product, DetailsPackDatagrid.Rows[i].Cells["id"].FormattedValue.ToString(), DetailsPackDatagrid.Rows[i].Cells["name"].FormattedValue.ToString(), DetailsPackDatagrid.Rows[i].Cells["amount"].FormattedValue.ToString(), DetailsPackDatagrid.Rows[i].Cells["barcode"].FormattedValue.ToString());
                    }
                }
                //if ((ConDB.userRole == 1 || ConDB.userRole == 2) && ConDB.RegisterWareHouseEntry(row_selected_id, name_input.Text, sales_input.Text, amount))
                //{
                //    MessageBox.Show("Entrada en almacen exitosa", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //}
                clean();
                filterTextBox.Focus();
            }
            else
            {
                MessageBox.Show("No se pudo crear el producto, puede que ya exista un producto con ese código de barra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string name = ConDB.validString(name_input.Text);
            string amount = ConDB.validNumber(amount_input.Text);
            string text = ConDB.validNumber(price_input.Text);
            string text2 = ConDB.validNumber(minimum_input.Text);
            idpack_product = ((pack_checkbox.Checked && default(Guid).ToString() == idpack_product) ? Guid.NewGuid().ToString() : idpack_product);
            name_input.Text = name;
            if (text == "error")
            {
                MessageBox.Show("El precio no es valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (text2 == "error")
            {
                MessageBox.Show("El stock no es valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (ConDB.UpdateProduct(row_selected_id, name, amount, text, text2, barcode_input.Text, listIVA_combobox.SelectedValue.ToString(), isExenta_checkbox.Checked, expiration_datetimepicker.Value, current_idcategory, pack_checkbox.Checked, idpack_product, current_idsubcategory))
            {
                MessageBox.Show("Producto modificado exitosamente", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (pack_checkbox.Checked)
                {
                    for (int i = 0; i < DetailsPackDatagrid.RowCount; i++)
                    {
                        ConDB.UpdatePackProduct(idpack_product, DetailsPackDatagrid.Rows[i].Cells["id"].FormattedValue.ToString(), DetailsPackDatagrid.Rows[i].Cells["name"].FormattedValue.ToString(), DetailsPackDatagrid.Rows[i].Cells["amount"].FormattedValue.ToString(), DetailsPackDatagrid.Rows[i].Cells["barcode"].FormattedValue.ToString());
                    }
                }
                //if ((ConDB.userRole == 1 || ConDB.userRole == 2) && ConDB.RegisterWareHouseEntry(row_selected_id, name_input.Text, sales_input.Text, amount))
                //{
                //    MessageBox.Show("Entrada en almacen exitosa", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //}
                clean();
                filterTextBox.Focus();
            }
            else
            {
                MessageBox.Show("No se pudo modificar el producto, puede que ya exista un producto con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea borra este producto: " + name_input.Text + "? esto conlleva a perdida de información", "Confirmar Borrado!!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                if (ConDB.DeleteProduct(row_selected_id, name_input.Text))
                {
                    clean();
                    MessageBox.Show("El Producto fue eliminado exitosamente", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    filterTextBox.Focus();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void InventoryForm_Shown(object sender, EventArgs e)
        {
            setDetailsGridFormat();
            clean();
            filterTextBox.Focus();
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.mainForm.Show();
        }

        private void listIVA_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(listIVA_combobox.SelectedValue.ToString()) && ConDB.getTaxIsExenta(Guid.Parse(listIVA_combobox.SelectedValue.ToString())))
                {
                    isExenta_checkbox.Checked = true;
                }
                else
                {
                    isExenta_checkbox.Checked = false;
                }
            }
            catch
            {
            }
        }

        private void pack_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (pack_checkbox.Checked)
            {
                packbarcode_input.ReadOnly = false;
                packamount_input.ReadOnly = false;
                packbarcode_input.Focus();
                return;
            }
            packbarcode_input.ReadOnly = true;
            packamount_input.ReadOnly = true;
            packbarcode_input.Text = string.Empty;
            packamount_input.Text = string.Empty;
            packname_input.Text = string.Empty;
            detailsDataTable.Clear();
        }

        private void packbarcode_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                List<string> productByBarcode = ConDB.getProductByBarcode(packbarcode_input.Text);
                if (productByBarcode.Count != 0)
                {
                    row_selected_id_pack = productByBarcode[0];
                    packamount_input.Text = "6";
                    detailsDataTable.Rows.Add(productByBarcode[0], productByBarcode[1], productByBarcode[2], packamount_input.Text, packbarcode_input.Text);
                    packname_input.Text = string.Empty;
                    packname_input.Text = productByBarcode[1];
                    packamount_input.Focus();
                    packamount_input.SelectAll();
                }
            }
        }

        private void packamount_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (!int.TryParse(ConDB.validNumber(packamount_input.Text), out var _) || e.KeyCode != Keys.Return)
            {
                return;
            }
            for (int i = 0; i < detailsDataTable.Rows.Count; i++)
            {
                if (detailsDataTable.Rows[i]["id"].ToString() == row_selected_id_pack)
                {
                    detailsDataTable.Rows[i]["amount"] = int.Parse(packamount_input.Text);
                }
            }
            packamount_input.Text = "0";
            packbarcode_input.Text = string.Empty;
            packname_input.Text = string.Empty;
            packbarcode_input.Focus();
        }

        private void DetailsPackDatagrid_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.KeyData == Keys.Add)
            {
                float num = float.Parse(DetailsPackDatagrid.Rows[dataGridView.FirstDisplayedScrollingRowIndex].Cells["amount"].FormattedValue.ToString()) + 1f;
                DetailsPackDatagrid.Rows[dataGridView.FirstDisplayedScrollingRowIndex].Cells["amount"].Value = num;
            }
            if (e.KeyData == Keys.Subtract)
            {
                float num2 = float.Parse(DetailsPackDatagrid.Rows[dataGridView.FirstDisplayedScrollingRowIndex].Cells["amount"].FormattedValue.ToString()) - 1f;
                if (num2 < 1f)
                {
                    num2 = 1f;
                }
                DetailsPackDatagrid.Rows[dataGridView.FirstDisplayedScrollingRowIndex].Cells["amount"].Value = num2;
            }
            if (Keys.Delete == e.KeyData)
            {
                DetailsPackDatagrid.Rows.RemoveAt(dataGridView.FirstDisplayedScrollingRowIndex);
            }
        }

        private void DetailsPackDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                _ = e.RowIndex;
                _ = DetailsPackDatagrid.RowCount;
            }
        }

        private void DetailsPackDatagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex < DetailsPackDatagrid.RowCount)
            {
                TextBox textBox = packname_input;
                string text = (packamount_input.Text = string.Empty);
                textBox.Text = text;
                packname_input.Text = DetailsPackDatagrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
                packamount_input.Text = DetailsPackDatagrid.Rows[e.RowIndex].Cells["amount"].FormattedValue.ToString().Replace(",00", "");
                packbarcode_input.Text = DetailsPackDatagrid.Rows[e.RowIndex].Cells["barcode"].FormattedValue.ToString();
                row_selected_id_pack = DetailsPackDatagrid.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                packamount_input.Focus();
                packamount_input.SelectAll();
            }
        }

        private void subcategory_combobox_SelectedValueChanged(object sender, EventArgs e)
        {
            current_idcategory = ((DataRowView)subcategory_combobox.Items[subcategory_combobox.SelectedIndex]).Row["id_category"].ToString();
            current_idsubcategory = ((DataRowView)subcategory_combobox.Items[subcategory_combobox.SelectedIndex]).Row["id"].ToString();
            foreach (DataRowView item in category_combobox.Items)
            {
                if (item.Row[0].ToString() == current_idcategory)
                {
                    category_combobox.SelectedItem = item;
                }
            }
        }

        private void InventoryDataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            InventoryDataGrid_CellClick(sender, e);
        }

        private void InventoryDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            filter_word = ConDB.validString(filterTextBox.Text);
            filterTextBox.Text = filter_word;
            InventoryDataGrid.DataSource = ConDB.getProductsList(filter_word, isSales: false, isInventory: true);
            formatDataGrid();
        }
    }
}
