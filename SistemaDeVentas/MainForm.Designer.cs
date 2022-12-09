namespace SistemaDeVentas
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Button InventoryButton;

        private Button PurchasesButton;

        private Label label1;

        private Label label2;

        private Button SalesButton;

        private Button historicalButton;

        private Button stadisticsButton;

        private Label label3;

        private Label label4;

        private Label label5;

        private MenuStrip menuStrip1;

        private ToolStripMenuItem menuPrincipalToolStripMenuItem;

        private ToolStripMenuItem passwordMenuItem;

        private ToolStripMenuItem newUserToolStripMenuItem;

        private ToolStripMenuItem configuraciónToolStripMenuItem;

        private ToolStripMenuItem detailsOutletToolStripMenuItem;

        private ToolStripMenuItem facturaciónToolStripMenuItem;

        private ToolStripMenuItem compraVentaToolStripMenuItem;

        private ToolStripMenuItem cambioDeUsuarioToolStripMenuItem;

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
            //this.Text = "MainForm";

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuPrincipalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsOutletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compraVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stadisticsButton = new System.Windows.Forms.Button();
            this.historicalButton = new System.Windows.Forms.Button();
            this.SalesButton = new System.Windows.Forms.Button();
            this.PurchasesButton = new System.Windows.Forms.Button();
            this.InventoryButton = new System.Windows.Forms.Button();
            this.cambioDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoEllipsis = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label1.Location = new System.Drawing.Point(12, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 115);
            this.label1.TabIndex = 2;
            this.label1.Text = "Aqui puede crear, modificar o eliminar (no recomendado) productos como el precio, nombre, stock minimo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.AutoEllipsis = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label2.Location = new System.Drawing.Point(231, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 115);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aqui se registran los ingresos a almacen, como tambien devoluciones, el producto debe estar registrado previamente en el Inventario";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label3.AutoEllipsis = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label3.Location = new System.Drawing.Point(459, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 115);
            this.label3.TabIndex = 7;
            this.label3.Text = "Aqui se registran las ventas realizadas";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label4.AutoEllipsis = true;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label4.Location = new System.Drawing.Point(687, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 115);
            this.label4.TabIndex = 8;
            this.label4.Text = "Aqui se puede ver todo el historial de ventas, ingresos y modificaciones de los productos a travez del tiempo";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label5.AutoEllipsis = true;
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label5.Location = new System.Drawing.Point(915, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 115);
            this.label5.TabIndex = 9;
            this.label5.Text = "Aqui se pueden ver estadisticas de los productos, como cual se vendio mas, cual se vende menos, las ganancias registradas, etc.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.menuPrincipalToolStripMenuItem, this.configuraciónToolStripMenuItem, this.facturaciónToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1140, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuPrincipalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.passwordMenuItem, this.newUserToolStripMenuItem, this.cambioDeUsuarioToolStripMenuItem });
            this.menuPrincipalToolStripMenuItem.Name = "menuPrincipalToolStripMenuItem";
            this.menuPrincipalToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.menuPrincipalToolStripMenuItem.Text = "Usuario";
            this.passwordMenuItem.Name = "passwordMenuItem";
            this.passwordMenuItem.Size = new System.Drawing.Size(180, 22);
            this.passwordMenuItem.Text = "Cambiar contraseña";
            this.passwordMenuItem.Click += new System.EventHandler(passwordMenuItem_Click);
            this.newUserToolStripMenuItem.Name = "newUserToolStripMenuItem";
            this.newUserToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newUserToolStripMenuItem.Text = "Crear nuevo usuario";
            this.newUserToolStripMenuItem.Click += new System.EventHandler(newUserToolStripMenuItem_Click);
            this.configuraciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.detailsOutletToolStripMenuItem });
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            this.detailsOutletToolStripMenuItem.Name = "detailsOutletToolStripMenuItem";
            this.detailsOutletToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.detailsOutletToolStripMenuItem.Text = "Datos de Sucursal";
            this.facturaciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.compraVentaToolStripMenuItem });
            this.facturaciónToolStripMenuItem.Name = "facturaciónToolStripMenuItem";
            this.facturaciónToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.facturaciónToolStripMenuItem.Text = "Facturación";
            this.compraVentaToolStripMenuItem.Name = "compraVentaToolStripMenuItem";
            this.compraVentaToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.compraVentaToolStripMenuItem.Text = "Compra / Venta";
            this.compraVentaToolStripMenuItem.Click += new System.EventHandler(compraVentaToolStripMenuItem_Click);
            this.stadisticsButton.AutoEllipsis = true;
            this.stadisticsButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("stadisticsButton.BackgroundImage");
            this.stadisticsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stadisticsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stadisticsButton.Enabled = false;
            this.stadisticsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.stadisticsButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.stadisticsButton.Location = new System.Drawing.Point(928, 57);
            this.stadisticsButton.Name = "stadisticsButton";
            this.stadisticsButton.Size = new System.Drawing.Size(183, 132);
            this.stadisticsButton.TabIndex = 5;
            this.stadisticsButton.Text = "Estadisticas";
            this.stadisticsButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.stadisticsButton.UseVisualStyleBackColor = true;
            this.stadisticsButton.Click += new System.EventHandler(stadisticsButton_Click);
            this.historicalButton.AutoSize = true;
            this.historicalButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("historicalButton.BackgroundImage");
            this.historicalButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.historicalButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.historicalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.historicalButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.historicalButton.Location = new System.Drawing.Point(699, 57);
            this.historicalButton.Name = "historicalButton";
            this.historicalButton.Size = new System.Drawing.Size(183, 132);
            this.historicalButton.TabIndex = 4;
            this.historicalButton.Text = "Historial";
            this.historicalButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.historicalButton.UseVisualStyleBackColor = true;
            this.historicalButton.Click += new System.EventHandler(historicalButton_Click);
            this.SalesButton.AutoSize = true;
            this.SalesButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("SalesButton.BackgroundImage");
            this.SalesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SalesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SalesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.SalesButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.SalesButton.Location = new System.Drawing.Point(472, 57);
            this.SalesButton.Name = "SalesButton";
            this.SalesButton.Size = new System.Drawing.Size(183, 136);
            this.SalesButton.TabIndex = 3;
            this.SalesButton.Text = "Ventas";
            this.SalesButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SalesButton.UseVisualStyleBackColor = true;
            this.SalesButton.Click += new System.EventHandler(SalesButton_Click);
            this.PurchasesButton.AutoSize = true;
            this.PurchasesButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("PurchasesButton.BackgroundImage");
            this.PurchasesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PurchasesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PurchasesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.PurchasesButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.PurchasesButton.Location = new System.Drawing.Point(247, 57);
            this.PurchasesButton.Name = "PurchasesButton";
            this.PurchasesButton.Size = new System.Drawing.Size(185, 140);
            this.PurchasesButton.TabIndex = 1;
            this.PurchasesButton.Text = "Ingreso a Almacen";
            this.PurchasesButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PurchasesButton.UseVisualStyleBackColor = true;
            this.PurchasesButton.Click += new System.EventHandler(PurchasesButton_Click);
            this.InventoryButton.AutoSize = true;
            this.InventoryButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("InventoryButton.BackgroundImage");
            this.InventoryButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InventoryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InventoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.InventoryButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.InventoryButton.Location = new System.Drawing.Point(25, 57);
            this.InventoryButton.Name = "InventoryButton";
            this.InventoryButton.Size = new System.Drawing.Size(183, 140);
            this.InventoryButton.TabIndex = 0;
            this.InventoryButton.Text = "Inventario";
            this.InventoryButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.InventoryButton.UseVisualStyleBackColor = true;
            this.InventoryButton.Click += new System.EventHandler(InventoryButton_Click);
            this.cambioDeUsuarioToolStripMenuItem.Name = "cambioDeUsuarioToolStripMenuItem";
            this.cambioDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cambioDeUsuarioToolStripMenuItem.Text = "Cambio de Usuario";
            this.cambioDeUsuarioToolStripMenuItem.Click += new System.EventHandler(cambioDeUsuarioToolStripMenuItem_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            base.ClientSize = new System.Drawing.Size(1140, 338);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.stadisticsButton);
            base.Controls.Add(this.historicalButton);
            base.Controls.Add(this.SalesButton);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.PurchasesButton);
            base.Controls.Add(this.InventoryButton);
            base.Controls.Add(this.menuStrip1);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.MainMenuStrip = this.menuStrip1;
            base.Name = "MainForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Ventas";
            base.Shown += new System.EventHandler(MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}