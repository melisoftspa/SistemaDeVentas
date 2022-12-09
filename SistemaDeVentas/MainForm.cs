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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InventoryButton_Click(object sender, EventArgs e)
        {
            Hide();
            new InventoryForm().ShowDialog();
        }

        private void PurchasesButton_Click(object sender, EventArgs e)
        {
            Hide();
            new PurchaseForm().ShowDialog();
        }

        private void SalesButton_Click(object sender, EventArgs e)
        {
            Hide();
            new SalesForm().ShowDialog();
        }

        private void passwordMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new UserForm().ShowDialog();
        }

        private void stadisticsButton_Click(object sender, EventArgs e)
        {
            Hide();
            new StadisticsForm().ShowDialog();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            InventoryButton.Enabled = true;
            stadisticsButton.Enabled = false;
            PurchasesButton.Enabled = false;
            newUserToolStripMenuItem.Enabled = false;
            historicalButton.Enabled = false;
            facturaciónToolStripMenuItem.Enabled = false;
            detailsOutletToolStripMenuItem.Enabled = false;
            if (ConDB.userRole == 1 || ConDB.userRole == 2)
            {
                stadisticsButton.Enabled = true;
                InventoryButton.Enabled = true;
                PurchasesButton.Enabled = true;
                newUserToolStripMenuItem.Enabled = true;
                historicalButton.Enabled = true;
                facturaciónToolStripMenuItem.Enabled = true;
                detailsOutletToolStripMenuItem.Enabled = true;
            }
        }

        private void historicalButton_Click(object sender, EventArgs e)
        {
            Hide();
            new Historic().ShowDialog();
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new UserForm(NewUser: true).ShowDialog();
        }

        private void compraVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new InvoiceForm().ShowDialog();
        }

        private void cambioDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new LoginForm().ShowDialog();
        }

    }
}
