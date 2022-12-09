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
    public partial class UserForm : Form
    {
        public bool _newUser;

        public UserForm(bool NewUser = false)
        {
            InitializeComponent();
            _newUser = NewUser;
            vendorname_input.Text = ConDB.userName;
            inInvoice_checkbox.Checked = ConDB.userNameInvoice;
            if (_newUser)
            {
                lblOldPassword.Text = "Nombre Acceso";
                oldPwd_input.PasswordChar = char.Parse("\0");
                vendorname_input.Text = string.Empty;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string text = ConDB.validString(oldPwd_input.Text);
            if (!_newUser && oldPwd_input.Text != text)
            {
                MessageBox.Show("La contraseña actual no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (ConDB.validString(newPwd_input.Text) != newPwd_input.Text)
            {
                MessageBox.Show("La contraseña nueva no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (newPwd_input.Text != reNewPwd_input.Text)
            {
                MessageBox.Show("La nueva contraseña y repetir contraseña no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (_newUser)
            {
                if (ConDB.newUser(oldPwd_input.Text, newPwd_input.Text, vendorname_input.Text, inInvoice_checkbox.Checked))
                {
                    MessageBox.Show("Usuario creado con exito", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Error al crear el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else if (ConDB.changePassword(newPwd_input.Text, vendorname_input.Text, inInvoice_checkbox.Checked))
            {
                MessageBox.Show("contraseña guardada con exito", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Error al cambiar la contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConDB.mainForm.Show();
        }

    }
}
