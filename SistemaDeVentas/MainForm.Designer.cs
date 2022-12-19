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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuPrincipalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambioDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsOutletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compraVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stadisticsButton = new System.Windows.Forms.Button();
            this.historicalButton = new System.Windows.Forms.Button();
            this.SalesButton = new System.Windows.Forms.Button();
            this.PurchasesButton = new System.Windows.Forms.Button();
            this.InventoryButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(24, 462);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(426, 265);
            this.label1.TabIndex = 2;
            this.label1.Text = "Aqui puede crear, modificar o eliminar (no recomendado) productos como el precio," +
    " nombre, stock minimo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(462, 462);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(426, 265);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aqui se registran los ingresos a almacen, como tambien devoluciones, el producto " +
    "debe estar registrado previamente en el Inventario";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(918, 462);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(426, 265);
            this.label3.TabIndex = 7;
            this.label3.Text = "Aqui se registran las ventas realizadas";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(1374, 462);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(426, 265);
            this.label4.TabIndex = 8;
            this.label4.Text = "Aqui se puede ver todo el historial de ventas, ingresos y modificaciones de los p" +
    "roductos a travez del tiempo";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(1830, 462);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(426, 265);
            this.label5.TabIndex = 9;
            this.label5.Text = "Aqui se pueden ver estadisticas de los productos, como cual se vendio mas, cual s" +
    "e vende menos, las ganancias registradas, etc.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPrincipalToolStripMenuItem,
            this.configuraciónToolStripMenuItem,
            this.facturaciónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 5, 0, 5);
            this.menuStrip1.Size = new System.Drawing.Size(2280, 44);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuPrincipalToolStripMenuItem
            // 
            this.menuPrincipalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.passwordMenuItem,
            this.newUserToolStripMenuItem,
            this.cambioDeUsuarioToolStripMenuItem});
            this.menuPrincipalToolStripMenuItem.Name = "menuPrincipalToolStripMenuItem";
            this.menuPrincipalToolStripMenuItem.Size = new System.Drawing.Size(101, 34);
            this.menuPrincipalToolStripMenuItem.Text = "Usuario";
            // 
            // passwordMenuItem
            // 
            this.passwordMenuItem.Name = "passwordMenuItem";
            this.passwordMenuItem.Size = new System.Drawing.Size(317, 40);
            this.passwordMenuItem.Text = "Cambiar contraseña";
            this.passwordMenuItem.Click += new System.EventHandler(this.passwordMenuItem_Click);
            // 
            // newUserToolStripMenuItem
            // 
            this.newUserToolStripMenuItem.Name = "newUserToolStripMenuItem";
            this.newUserToolStripMenuItem.Size = new System.Drawing.Size(317, 40);
            this.newUserToolStripMenuItem.Text = "Crear nuevo usuario";
            this.newUserToolStripMenuItem.Click += new System.EventHandler(this.newUserToolStripMenuItem_Click);
            // 
            // cambioDeUsuarioToolStripMenuItem
            // 
            this.cambioDeUsuarioToolStripMenuItem.Name = "cambioDeUsuarioToolStripMenuItem";
            this.cambioDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(317, 40);
            this.cambioDeUsuarioToolStripMenuItem.Text = "Cambio de Usuario";
            this.cambioDeUsuarioToolStripMenuItem.Click += new System.EventHandler(this.cambioDeUsuarioToolStripMenuItem_Click);
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailsOutletToolStripMenuItem});
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(161, 34);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            // 
            // detailsOutletToolStripMenuItem
            // 
            this.detailsOutletToolStripMenuItem.Name = "detailsOutletToolStripMenuItem";
            this.detailsOutletToolStripMenuItem.Size = new System.Drawing.Size(297, 40);
            this.detailsOutletToolStripMenuItem.Text = "Datos de Sucursal";
            // 
            // facturaciónToolStripMenuItem
            // 
            this.facturaciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compraVentaToolStripMenuItem});
            this.facturaciónToolStripMenuItem.Name = "facturaciónToolStripMenuItem";
            this.facturaciónToolStripMenuItem.Size = new System.Drawing.Size(137, 34);
            this.facturaciónToolStripMenuItem.Text = "Facturación";
            // 
            // compraVentaToolStripMenuItem
            // 
            this.compraVentaToolStripMenuItem.Name = "compraVentaToolStripMenuItem";
            this.compraVentaToolStripMenuItem.Size = new System.Drawing.Size(277, 40);
            this.compraVentaToolStripMenuItem.Text = "Compra / Venta";
            this.compraVentaToolStripMenuItem.Click += new System.EventHandler(this.compraVentaToolStripMenuItem_Click);
            // 
            // stadisticsButton
            // 
            this.stadisticsButton.AutoEllipsis = true;
            this.stadisticsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stadisticsButton.BackgroundImage")));
            this.stadisticsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.stadisticsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stadisticsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.stadisticsButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.stadisticsButton.Location = new System.Drawing.Point(1901, 88);
            this.stadisticsButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.stadisticsButton.Name = "stadisticsButton";
            this.stadisticsButton.Size = new System.Drawing.Size(283, 358);
            this.stadisticsButton.TabIndex = 5;
            this.stadisticsButton.Text = "Estadisticas";
            this.stadisticsButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.stadisticsButton.UseVisualStyleBackColor = true;
            this.stadisticsButton.Click += new System.EventHandler(this.stadisticsButton_Click);
            // 
            // historicalButton
            // 
            this.historicalButton.AutoSize = true;
            this.historicalButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("historicalButton.BackgroundImage")));
            this.historicalButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.historicalButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.historicalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.historicalButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.historicalButton.Location = new System.Drawing.Point(1440, 79);
            this.historicalButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.historicalButton.Name = "historicalButton";
            this.historicalButton.Size = new System.Drawing.Size(330, 367);
            this.historicalButton.TabIndex = 4;
            this.historicalButton.Text = "Historial";
            this.historicalButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.historicalButton.UseVisualStyleBackColor = true;
            this.historicalButton.Click += new System.EventHandler(this.historicalButton_Click);
            // 
            // SalesButton
            // 
            this.SalesButton.AutoSize = true;
            this.SalesButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SalesButton.BackgroundImage")));
            this.SalesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SalesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SalesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SalesButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.SalesButton.Location = new System.Drawing.Point(993, 79);
            this.SalesButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.SalesButton.Name = "SalesButton";
            this.SalesButton.Size = new System.Drawing.Size(305, 367);
            this.SalesButton.TabIndex = 3;
            this.SalesButton.Text = "Ventas";
            this.SalesButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SalesButton.UseVisualStyleBackColor = true;
            this.SalesButton.Click += new System.EventHandler(this.SalesButton_Click);
            // 
            // PurchasesButton
            // 
            this.PurchasesButton.AutoSize = true;
            this.PurchasesButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PurchasesButton.BackgroundImage")));
            this.PurchasesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PurchasesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PurchasesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PurchasesButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.PurchasesButton.Location = new System.Drawing.Point(539, 79);
            this.PurchasesButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.PurchasesButton.Name = "PurchasesButton";
            this.PurchasesButton.Size = new System.Drawing.Size(315, 367);
            this.PurchasesButton.TabIndex = 1;
            this.PurchasesButton.Text = "Ingreso a Almacen";
            this.PurchasesButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PurchasesButton.UseVisualStyleBackColor = true;
            this.PurchasesButton.Click += new System.EventHandler(this.PurchasesButton_Click);
            // 
            // InventoryButton
            // 
            this.InventoryButton.AutoSize = true;
            this.InventoryButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InventoryButton.BackgroundImage")));
            this.InventoryButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InventoryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InventoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InventoryButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.InventoryButton.Location = new System.Drawing.Point(104, 79);
            this.InventoryButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.InventoryButton.Name = "InventoryButton";
            this.InventoryButton.Size = new System.Drawing.Size(272, 367);
            this.InventoryButton.TabIndex = 0;
            this.InventoryButton.Text = "Inventario";
            this.InventoryButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.InventoryButton.UseVisualStyleBackColor = true;
            this.InventoryButton.Click += new System.EventHandler(this.InventoryButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2280, 780);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stadisticsButton);
            this.Controls.Add(this.historicalButton);
            this.Controls.Add(this.SalesButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PurchasesButton);
            this.Controls.Add(this.InventoryButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Ventas";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}