namespace SistemaDeVentas
{
    partial class initBDForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label label1;

        private PictureBox pictureBox1;

        private PictureBox pictureBox2;

        private Label label2;


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
            //this.Text = "initBDForm";
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.initBDForm));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
            base.SuspendLayout();
            this.label1.Location = new System.Drawing.Point(70, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. Primero escriba \"servicios\" en el buscador de windows y haga click en \"Servicios Aplicacion\" (3) ";
            this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new System.Drawing.Point(70, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 331);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            //this.pictureBox2.BackgroundImage = SistemaDeVentas.Properties.Resources.serviciso;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(385, 73);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(550, 429);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.label2.Location = new System.Drawing.Point(382, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(553, 47);
            this.label2.TabIndex = 3;
            this.label2.Text = "2. luego en el panel \"Servicios (locales)\" busque \"SQL Server(SQLEXPRESS)\", haga click derecho sobre el y de click en \"Iniciar\". Eso es todo, la base de datos esta levantada.";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(968, 532);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.label1);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "initBDForm";
            this.Text = "Iniciar Base de Datos";
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
            base.ResumeLayout(false);
        }

        #endregion
    }
}