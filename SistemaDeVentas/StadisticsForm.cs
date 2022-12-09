
namespace SistemaDeVentas
{
    public partial class StadisticsForm : Form
    {
        public StadisticsForm()
        {
            InitializeComponent();
        }

        private void passwordMenuItem_Click(object sender, EventArgs e)
        {
            if (backupFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                if (ConDB.makeBackup(backupFolderBrowser.SelectedPath))
                {
                    MessageBox.Show("Se creo con exito el backup", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Error al crear el backup", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void StadisticsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.mainForm.Show();
        }

        private void csvMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamReader streamReader = new StreamReader("C:\\inventario.csv"))
            {
                streamReader.ReadLine();
                while (!streamReader.EndOfStream)
                {
                    string[] array = streamReader.ReadLine().Split(',');
                    ConDB.CreateProduct(array[0], array[1], array[2], array[3], array[4], array[5], exenta: false, DateTime.Now, array[8], isPack: false, null, null);
                }
            }
            MessageBox.Show("terminado");
        }

        private void StadisticsForm_Load(object sender, EventArgs e)
        {
            MainDataGrid.DataSource = ConDB.getStatistic();
        }

        private void MainDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                detailDataGrid.DataSource = ConDB.getDetailStatistic(MainDataGrid.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
            }
        }

    }
}
