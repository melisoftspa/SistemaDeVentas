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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            pwd_input = new TextBox();
            username_input = new TextBox();
            label2 = new Label();
            label1 = new Label();
            loginButton = new Button();
            conection_label = new LinkLabel();
            lblVersion = new Label();
            SuspendLayout();
            // 
            // pwd_input
            // 
            pwd_input.Location = new Point(232, 155);
            pwd_input.Margin = new Padding(6, 7, 6, 7);
            pwd_input.Name = "pwd_input";
            pwd_input.PasswordChar = '*';
            pwd_input.Size = new Size(462, 35);
            pwd_input.TabIndex = 11;
            pwd_input.KeyDown += pwd_input_KeyDown;
            // 
            // username_input
            // 
            username_input.Location = new Point(232, 74);
            username_input.Margin = new Padding(6, 7, 6, 7);
            username_input.Name = "username_input";
            username_input.Size = new Size(462, 35);
            username_input.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(92, 162);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(123, 30);
            label2.TabIndex = 9;
            label2.Text = "Contraseña:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(92, 81);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(88, 30);
            label1.TabIndex = 8;
            label1.Text = "Usuario:";
            // 
            // loginButton
            // 
            loginButton.Location = new Point(320, 247);
            loginButton.Margin = new Padding(6, 7, 6, 7);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(150, 53);
            loginButton.TabIndex = 12;
            loginButton.Text = "Ingresar";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // conection_label
            // 
            conection_label.AutoSize = true;
            conection_label.Location = new Point(226, 385);
            conection_label.Margin = new Padding(6, 0, 6, 0);
            conection_label.Name = "conection_label";
            conection_label.Size = new Size(345, 30);
            conection_label.TabIndex = 13;
            conection_label.TabStop = true;
            conection_label.Text = "Solucionar Problemas de Coneccion";
            conection_label.LinkClicked += conection_label_LinkClicked;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(656, 406);
            lblVersion.Margin = new Padding(6, 0, 6, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(128, 30);
            lblVersion.TabIndex = 14;
            lblVersion.Text = "version 1.2.2";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(798, 457);
            Controls.Add(lblVersion);
            Controls.Add(conection_label);
            Controls.Add(loginButton);
            Controls.Add(pwd_input);
            Controls.Add(username_input);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 7, 6, 7);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            FormClosing += LoginForm_FormClosing;
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}