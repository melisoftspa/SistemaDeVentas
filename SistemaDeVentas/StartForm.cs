using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeVentas
{
    public partial class StartForm : Form
    {
        private int borderSize = 2;
        private Size formSize;
        private DetailsForm detailsalesform;
        private DataTable detailsDataTable = new DataTable();

        public StartForm()
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);
            InventoryDataGrid.DataSource = ConDB.getProductsList("", isSales: true);
            setProductsGridFormat();
            setDetailsGridFormat();
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

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;//Standar Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;
            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>
            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void StartForm_Resize(object sender, EventArgs e)
        {
            AjustForm();
        }

        private void AjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(8,8,8,0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                    {
                        this.Padding = new Padding(borderSize);
                    }
                    break;
            }
        }

        private void btmMin_Click(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            this.WindowState = FormWindowState.Minimized;
        }

        private void btmMax_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            } else
            {
                this.WindowState = FormWindowState.Normal;
                formSize = this.ClientSize;
            }
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConDB.mainForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void CollapseMenu()
        {
            if(this.panelMenu.Width > 200)
            {
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                btnMenu.Dock = DockStyle.Top;
                foreach(Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            } 
            else
            {
                panelMenu.Width = 250;
                pictureBox1.Visible = true;
                btnMenu.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.TopLeft;
                    menuButton.Padding = new Padding(10,0,0,0);
                }
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
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
            InventoryDataGrid.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            InventoryDataGrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["sale_price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["bar_code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            InventoryDataGrid.Columns["expiration"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void txtSearch_Enter(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            /// TODO: Si presiona enter, entra en la modalidad de agregar al carro de compra.
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                /// TODO: Busca en la base de dato, la existencia del codigo de barra.
                List<string> productByBarcode = ConDB.getProductByBarcode(txtSearch.Text.Trim(), true);
                /// TODO: Si encontro 1 producto, agregar en el carro de compra (la lista es de 7 elementos)
                if (productByBarcode.Count == 7)
                {
                    //row_selected_id = productByBarcode[0];
                    //name_input.Text = (current_name = productByBarcode[1]);
                    //price_input.Text = (current_price = float.Parse(ConDB.validNumber(productByBarcode[2])).ToString("C").Replace(",00", ""));
                    //current_tax = float.Parse(productByBarcode[5]);
                    //current_exenta = Convert.ToBoolean(Convert.ToInt32(productByBarcode[6]));
                    //current_stock = int.Parse(productByBarcode[3]);
                    //current_amount = 1f;
                    addToDetails(productByBarcode);
                    //barcode_input.SelectAll();
                }
                else
                {
                    //lblMensajeErrorCodeBar.Text = "Producto no ingresado en Almacen"; 
                }
                return;
            }
            /// TODO: Busca información digitada 
            InventoryDataGrid.DataSource = ConDB.getProductsList(txtSearch.Text.Trim(), isSales: true);
        }

        private void addToDetails(List<string> productByBarcode)
        {
            //lblMensajeErrorCodeBar.Text = string.Empty;
            if (productByBarcode[0] == "")
            {
                MessageBox.Show("Debe seleccionar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            //if (current_amount <= 0f)
            //{
            //    MessageBox.Show("La cantidad no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    return;
            //}
            float num = 0f;
            string text = productByBarcode[1];
            //try
            //{
            //    if (!string.IsNullOrEmpty(kg_input.Text))
            //    {
            //        float num2 = float.Parse(kg_input.Text.Replace(".", ","));
            //        num = float.Parse(ConDB.validNumber(price_input.Text));
            //        num = ConDB.Round(float.Parse(Math.Ceiling(num * num2).ToString()).ToString());
            //        if (current_name.Contains("KG"))
            //        {
            //            var name_text = current_name.Split("-");
            //            current_name = name_text[0];
            //        }
            //        text = $"{current_name} - KG: {num2}";
            //    }
            //    else
            //    {
            //        text = current_name;
            //        num = float.Parse(ConDB.validNumber(price_input.Text));
            //    }
            //}
            //catch (Exception)
            //{
            //    num = float.Parse(ConDB.validNumber(current_price));
            //    text = current_name;
            //}
            bool flag = true;
            for (int i = 0; i < detailsDataTable.Rows.Count; i++)
            {
                if (detailsDataTable.Rows[i]["id"].ToString() == productByBarcode[0])
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
                if (productByBarcode[0] == default(Guid).ToString())
                {
                    productByBarcode[0] = Guid.NewGuid().ToString();
                }
                detailsDataTable.Rows.Add(productByBarcode[0], text, float.Parse(ConDB.validNumber(productByBarcode[2])).ToString("C").Replace(",00", ""), 1, getTotalRow("1", num.ToString("C").Replace(",00", "")), float.Parse(productByBarcode[5]), Convert.ToBoolean(Convert.ToInt32(productByBarcode[6])));
            }
            //cleanfields();
            //calculateTotalSale();
            //barcode_input.Focus();
            //Addproductsale_button.Enabled = true;
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

        private string getTotalRow(string amount, string price)
        {
            float num = float.Parse(ConDB.validNumber(amount.Trim()));
            float num2 = float.Parse(ConDB.validNumber(price.Trim()));
            return (num * num2).ToString("C").Replace(",00", "");
        }
    }
}
