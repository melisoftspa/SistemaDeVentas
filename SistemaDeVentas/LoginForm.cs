
namespace SistemaDeVentas
{
    public partial class LoginForm : Form
    {
        private bool conectionActive;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void conection_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new initBDForm().ShowDialog();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (conectionActive)
            {
                login();
            }
            else if (!ConDB.OpenDB)
            {
                MessageBox.Show("No se pudo conectar a la base de datos, haga click en 'solucionar problemas de coneccion' y siga los pasos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                conectionActive = false;
            }
            else
            {
                login();
                conectionActive = true;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = $"Versión: {Application.ProductVersion}";
            if (!ConDB.OpenDB)
            {
                MessageBox.Show("No se pudo conectar a la base de datos, haga click en 'solucionar problemas de coneccion' y siga los pasos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                conectionActive = false;
            }
            else
            {
                conectionActive = true;
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.closeConnection();
        }

        private void login()
        {
            string username = ConDB.validString(username_input.Text.Trim());
            string pasword = ConDB.validString(pwd_input.Text);
            if (ConDB.login(username, pasword))
            {
                Hide();
                ConDB.mainForm = new MainForm();
                ConDB.mainForm.Location = Screen.AllScreens[0].WorkingArea.Location;
                ConDB.mainForm.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("El usuario y/o la contraseña son incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void pwd_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                loginButton_Click(sender, null);
            }
        }

    }
}
