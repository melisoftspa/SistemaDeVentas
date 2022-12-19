using System.Data;

namespace SistemaDeVentas
{
    public partial class DetailsForm : Form
    {
        public DataTable DetailData { get; set; }

        public string Change { get; set; }

        public string Total { get; set; }

        public DetailsForm()
        {
            InitializeComponent();
            DetailData = new DataTable();
            DetailDataGrid.DataSource = DetailData;
            this.setDetailsGridFormat();
            change_input.Text = Change;
            total_input.Text = Total;
        }

        internal void setDetailsGridFormat()
        {
            DetailDataGrid.DataSource = DetailData;
            DetailData.Columns.Add("id");
            DetailData.Columns.Add("name");
            DetailData.Columns.Add("price");
            DetailData.Columns.Add("amount");
            DetailData.Columns.Add("total");
            DetailData.Columns.Add("tax");
            DetailData.Columns.Add("exenta");
            DetailDataGrid.Columns["id"].Visible = false;
            DetailDataGrid.Columns["tax"].Visible = false;
            DetailDataGrid.Columns["exenta"].Visible = false;
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
            DetailDataGrid.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DetailDataGrid.Columns["price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DetailDataGrid.Columns["amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DetailDataGrid.Columns["total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public void AddDetails(DataTable detailData)
        {
            DetailDataGrid.DataSource = detailData;
            DetailDataGrid.Refresh();
        }

        public void AddTotal(string total)
        {
            total_input.Text = total;
        }

        public void AddChange(string change)
        {
            change_input.Text = change;
        }

    }
}