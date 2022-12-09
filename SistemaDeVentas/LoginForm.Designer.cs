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

        private Label label3;

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
            //this.Text = "LoginForm";
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.LoginForm));
            this.pwd_input = new System.Windows.Forms.TextBox();
            this.username_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.conection_label = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this.pwd_input.Location = new System.Drawing.Point(116, 67);
            this.pwd_input.Name = "pwd_input";
            this.pwd_input.PasswordChar = '*';
            this.pwd_input.Size = new System.Drawing.Size(233, 20);
            this.pwd_input.TabIndex = 11;
            this.pwd_input.KeyDown += new System.Windows.Forms.KeyEventHandler(pwd_input_KeyDown);
            this.username_input.Location = new System.Drawing.Point(116, 32);
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(233, 20);
            this.username_input.TabIndex = 10;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Contraseña:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Usuario:";
            this.loginButton.Location = new System.Drawing.Point(160, 107);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 12;
            this.loginButton.Text = "Ingresar";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(loginButton_Click);
            this.conection_label.AutoSize = true;
            this.conection_label.Location = new System.Drawing.Point(113, 167);
            this.conection_label.Name = "conection_label";
            this.conection_label.Size = new System.Drawing.Size(178, 13);
            this.conection_label.TabIndex = 13;
            this.conection_label.TabStop = true;
            this.conection_label.Text = "Solucionar Problemas de Coneccion";
            this.conection_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(conection_label_LinkClicked);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "version 1.1";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            base.ClientSize = new System.Drawing.Size(399, 198);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.conection_label);
            base.Controls.Add(this.loginButton);
            base.Controls.Add(this.pwd_input);
            base.Controls.Add(this.username_input);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "LoginForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(LoginForm_FormClosing);
            base.Load += new System.EventHandler(LoginForm_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}