namespace SistemaDeVentas
{
    partial class UserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label lblOldPassword;

        private Label lblCompletePassword;

        private Label lblNewPassword;

        private TextBox newPwd_input;

        private TextBox reNewPwd_input;

        private TextBox oldPwd_input;

        private Button saveButton;

        private Label label8;

        private Label label5;

        private TextBox vendorname_input;

        private Label label1;

        private CheckBox inInvoice_checkbox;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "UserForm";
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.UserForm));
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblCompletePassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.newPwd_input = new System.Windows.Forms.TextBox();
            this.reNewPwd_input = new System.Windows.Forms.TextBox();
            this.oldPwd_input = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.vendorname_input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inInvoice_checkbox = new System.Windows.Forms.CheckBox();
            base.SuspendLayout();
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(32, 15);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(97, 13);
            this.lblOldPassword.TabIndex = 1;
            this.lblOldPassword.Text = "Actual Contraseña:";
            this.lblCompletePassword.AutoSize = true;
            this.lblCompletePassword.Location = new System.Drawing.Point(32, 117);
            this.lblCompletePassword.Name = "lblCompletePassword";
            this.lblCompletePassword.Size = new System.Drawing.Size(101, 13);
            this.lblCompletePassword.TabIndex = 2;
            this.lblCompletePassword.Text = "Repetir Contraseña:";
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(32, 91);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(96, 13);
            this.lblNewPassword.TabIndex = 3;
            this.lblNewPassword.Text = "Nueva Contraseña";
            this.newPwd_input.Location = new System.Drawing.Point(153, 88);
            this.newPwd_input.Name = "newPwd_input";
            this.newPwd_input.PasswordChar = '*';
            this.newPwd_input.Size = new System.Drawing.Size(233, 20);
            this.newPwd_input.TabIndex = 3;
            this.reNewPwd_input.Location = new System.Drawing.Point(153, 114);
            this.reNewPwd_input.Name = "reNewPwd_input";
            this.reNewPwd_input.PasswordChar = '*';
            this.reNewPwd_input.Size = new System.Drawing.Size(233, 20);
            this.reNewPwd_input.TabIndex = 4;
            this.oldPwd_input.Location = new System.Drawing.Point(153, 12);
            this.oldPwd_input.Name = "oldPwd_input";
            this.oldPwd_input.PasswordChar = '*';
            this.oldPwd_input.Size = new System.Drawing.Size(233, 20);
            this.oldPwd_input.TabIndex = 1;
            this.saveButton.Location = new System.Drawing.Point(268, 140);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(118, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Guardar";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(saveButton_Click);
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(12, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(388, 2);
            this.label8.TabIndex = 41;
            this.label5.Location = new System.Drawing.Point(32, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 46);
            this.label5.TabIndex = 42;
            this.label5.Text = "* la contraseña solo puede contener valores alfanumerios (A..Z)(0..9)";
            this.vendorname_input.Location = new System.Drawing.Point(153, 38);
            this.vendorname_input.Name = "vendorname_input";
            this.vendorname_input.Size = new System.Drawing.Size(233, 20);
            this.vendorname_input.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Nombre Vendedor:";
            this.inInvoice_checkbox.AutoSize = true;
            this.inInvoice_checkbox.Location = new System.Drawing.Point(153, 63);
            this.inInvoice_checkbox.Name = "inInvoice_checkbox";
            this.inInvoice_checkbox.Size = new System.Drawing.Size(245, 17);
            this.inInvoice_checkbox.TabIndex = 44;
            this.inInvoice_checkbox.Text = "¿Desea que aparezca el nombre en la Boleta?";
            this.inInvoice_checkbox.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(426, 192);
            base.Controls.Add(this.inInvoice_checkbox);
            base.Controls.Add(this.vendorname_input);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.saveButton);
            base.Controls.Add(this.oldPwd_input);
            base.Controls.Add(this.reNewPwd_input);
            base.Controls.Add(this.newPwd_input);
            base.Controls.Add(this.lblNewPassword);
            base.Controls.Add(this.lblCompletePassword);
            base.Controls.Add(this.lblOldPassword);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "UserForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Usuarios";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(UserForm_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}