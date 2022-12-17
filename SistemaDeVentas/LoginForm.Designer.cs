namespace SistemaDeVentas
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private TextBox pwd_input;

        private TextBox username_input;

        private Label label2;

        private Label label1;

        private Button loginButton;

        private LinkLabel conection_label;

        private Label lblVersion;

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
            this.pwd_input = new System.Windows.Forms.TextBox();
            this.username_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.conection_label = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pwd_input
            // 
            this.pwd_input.Location = new System.Drawing.Point(232, 155);
            this.pwd_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pwd_input.Name = "pwd_input";
            this.pwd_input.PasswordChar = '*';
            this.pwd_input.Size = new System.Drawing.Size(462, 35);
            this.pwd_input.TabIndex = 11;
            this.pwd_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwd_input_KeyDown);
            // 
            // username_input
            // 
            this.username_input.Location = new System.Drawing.Point(232, 74);
            this.username_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(462, 35);
            this.username_input.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 162);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "Contraseña:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Usuario:";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(320, 247);
            this.loginButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(150, 53);
            this.loginButton.TabIndex = 12;
            this.loginButton.Text = "Ingresar";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // conection_label
            // 
            this.conection_label.AutoSize = true;
            this.conection_label.Location = new System.Drawing.Point(226, 385);
            this.conection_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.conection_label.Name = "conection_label";
            this.conection_label.Size = new System.Drawing.Size(345, 30);
            this.conection_label.TabIndex = 13;
            this.conection_label.TabStop = true;
            this.conection_label.Text = "Solucionar Problemas de Coneccion";
            this.conection_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.conection_label_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(656, 406);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(112, 30);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "version 1.1";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(798, 457);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.conection_label);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.pwd_input);
            this.Controls.Add(this.username_input);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}