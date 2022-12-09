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
    public partial class InvoiceForm : Form
    {
        private string start => startTimePicker.Value.ToString("yyyy-MM-dd");

        private string end => endTimePicker.Value.ToString("yyyy-MM-dd");

        public InvoiceForm()
        {
            InitializeComponent();
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.mainForm.Show();
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ConDB.getInvoices(start, end);
        }

        private void dateTimePicker2_OnValidated(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = oDateTimePicker.Text.ToString();
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            //invoiceDataTableTableAdapter.Fill();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex < dataGridView1.RowCount)
            {
                if (oDateTimePicker != null)
                {
                    oDateTimePicker.Visible = false;
                }
                if (e.ColumnIndex == 3 || e.ColumnIndex == 11)
                {
                    oDateTimePicker = new DateTimePicker();
                    dataGridView1.Controls.Add(oDateTimePicker);
                    oDateTimePicker.Format = DateTimePickerFormat.Short;
                    Rectangle cellDisplayRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, cutOverflow: true);
                    oDateTimePicker.Size = new Size(cellDisplayRectangle.Width, cellDisplayRectangle.Height);
                    oDateTimePicker.Location = new Point(cellDisplayRectangle.X, cellDisplayRectangle.Y);
                    oDateTimePicker.TextChanged += dateTimePicker2_OnValidated;
                    oDateTimePicker.Visible = true;
                }
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[0].Value = Guid.NewGuid();
            ((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[1].Value = 1;
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //invoiceDataTableTableAdapter.Update(salesSystemDB1.InvoiceDataTable);
            dataGridView1.Refresh();
        }

        private void dataGridView1_Validated(object sender, EventArgs e)
        {
            //invoiceDataTableTableAdapter.Update(salesSystemDB1.InvoiceDataTable);
            dataGridView1.Refresh();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //invoiceDataTableTableAdapter.Update(salesSystemDB1.InvoiceDataTable);
            dataGridView1.Refresh();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            _ = (DataGridView)sender;
            if (Keys.Delete == e.KeyData)
            {
                ((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[1].Value = 1;
                //invoiceDataTableTableAdapter.Update(salesSystemDB.InvoiceDataTable);
                //invoiceDataTableTableAdapter.Fill(salesSystemDB1.InvoiceDataTable);
                dataGridView1.Refresh();
            }
        }

    }
}
