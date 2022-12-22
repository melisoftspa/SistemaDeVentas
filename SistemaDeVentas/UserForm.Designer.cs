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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
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
            this.SuspendLayout();
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(64, 35);
            this.lblOldPassword.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(188, 30);
            this.lblOldPassword.TabIndex = 1;
            this.lblOldPassword.Text = "Actual Contraseña:";
            // 
            // lblCompletePassword
            // 
            this.lblCompletePassword.AutoSize = true;
            this.lblCompletePassword.Location = new System.Drawing.Point(64, 270);
            this.lblCompletePassword.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCompletePassword.Name = "lblCompletePassword";
            this.lblCompletePassword.Size = new System.Drawing.Size(194, 30);
            this.lblCompletePassword.TabIndex = 2;
            this.lblCompletePassword.Text = "Repetir Contraseña:";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(64, 210);
            this.lblNewPassword.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(184, 30);
            this.lblNewPassword.TabIndex = 3;
            this.lblNewPassword.Text = "Nueva Contraseña";
            // 
            // newPwd_input
            // 
            this.newPwd_input.Location = new System.Drawing.Point(306, 203);
            this.newPwd_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.newPwd_input.Name = "newPwd_input";
            this.newPwd_input.PasswordChar = '*';
            this.newPwd_input.Size = new System.Drawing.Size(462, 35);
            this.newPwd_input.TabIndex = 3;
            // 
            // reNewPwd_input
            // 
            this.reNewPwd_input.Location = new System.Drawing.Point(306, 263);
            this.reNewPwd_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.reNewPwd_input.Name = "reNewPwd_input";
            this.reNewPwd_input.PasswordChar = '*';
            this.reNewPwd_input.Size = new System.Drawing.Size(462, 35);
            this.reNewPwd_input.TabIndex = 4;
            // 
            // oldPwd_input
            // 
            this.oldPwd_input.Location = new System.Drawing.Point(306, 28);
            this.oldPwd_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.oldPwd_input.Name = "oldPwd_input";
            this.oldPwd_input.PasswordChar = '*';
            this.oldPwd_input.Size = new System.Drawing.Size(462, 35);
            this.oldPwd_input.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(536, 323);
            this.saveButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(236, 53);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Guardar";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(24, 192);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(776, 5);
            this.label8.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(64, 323);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(378, 106);
            this.label5.TabIndex = 42;
            this.label5.Text = "* la contraseña solo puede contener valores alfanumerios (A..Z)(0..9)";
            // 
            // vendorname_input
            // 
            this.vendorname_input.Location = new System.Drawing.Point(306, 88);
            this.vendorname_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.vendorname_input.Name = "vendorname_input";
            this.vendorname_input.Size = new System.Drawing.Size(462, 35);
            this.vendorname_input.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 95);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 30);
            this.label1.TabIndex = 43;
            this.label1.Text = "Nombre Vendedor:";
            // 
            // inInvoice_checkbox
            // 
            this.inInvoice_checkbox.AutoSize = true;
            this.inInvoice_checkbox.Location = new System.Drawing.Point(306, 145);
            this.inInvoice_checkbox.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.inInvoice_checkbox.Name = "inInvoice_checkbox";
            this.inInvoice_checkbox.Size = new System.Drawing.Size(459, 34);
            this.inInvoice_checkbox.TabIndex = 44;
            this.inInvoice_checkbox.Text = "¿Desea que aparezca el nombre en la Boleta?";
            this.inInvoice_checkbox.UseVisualStyleBackColor = true;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 443);
            this.Controls.Add(this.inInvoice_checkbox);
            this.Controls.Add(this.vendorname_input);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.oldPwd_input);
            this.Controls.Add(this.reNewPwd_input);
            this.Controls.Add(this.newPwd_input);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.lblCompletePassword);
            this.Controls.Add(this.lblOldPassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Usuarios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}