namespace SistemaDeVentas
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnExit = new FontAwesome.Sharp.IconButton();
            this.btnVentas = new FontAwesome.Sharp.IconButton();
            this.btnHome = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.btnMenu = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btmMin = new FontAwesome.Sharp.IconButton();
            this.btmMax = new FontAwesome.Sharp.IconButton();
            this.btnCloseForm = new FontAwesome.Sharp.IconButton();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.panelListProduct = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Listado = new System.Windows.Forms.TabPage();
            this.InventoryDataGrid = new System.Windows.Forms.DataGridView();
            this.Grilla = new System.Windows.Forms.TabPage();
            this.btnClearListProduct = new FontAwesome.Sharp.IconButton();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelTotal = new System.Windows.Forms.Panel();
            this.DetailDataGrid = new System.Windows.Forms.DataGridView();
            this.delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.Editar = new System.Windows.Forms.DataGridViewImageColumn();
            this.less = new System.Windows.Forms.DataGridViewImageColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            this.panelListProduct.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Listado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            this.panelTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailDataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panelMenu.Controls.Add(this.btnExit);
            this.panelMenu.Controls.Add(this.btnVentas);
            this.panelMenu.Controls.Add(this.btnHome);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 887);
            this.panelMenu.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.IconChar = FontAwesome.Sharp.IconChar.SignOut;
            this.btnExit.IconColor = System.Drawing.Color.White;
            this.btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(0, 837);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(250, 50);
            this.btnExit.TabIndex = 3;
            this.btnExit.Tag = "Salir";
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnVentas
            // 
            this.btnVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVentas.FlatAppearance.BorderSize = 0;
            this.btnVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentas.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnVentas.ForeColor = System.Drawing.Color.White;
            this.btnVentas.IconChar = FontAwesome.Sharp.IconChar.Donate;
            this.btnVentas.IconColor = System.Drawing.Color.White;
            this.btnVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVentas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentas.Location = new System.Drawing.Point(0, 159);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Size = new System.Drawing.Size(250, 50);
            this.btnVentas.TabIndex = 2;
            this.btnVentas.Tag = "Vender";
            this.btnVentas.Text = "Vender";
            this.btnVentas.UseVisualStyleBackColor = true;
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.btnHome.IconColor = System.Drawing.Color.White;
            this.btnHome.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(0, 109);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(250, 50);
            this.btnHome.TabIndex = 1;
            this.btnHome.Tag = "Inicio";
            this.btnHome.Text = "Inicio";
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.Transparent;
            this.panelLogo.Controls.Add(this.btnMenu);
            this.panelLogo.Controls.Add(this.pictureBox1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 109);
            this.panelLogo.TabIndex = 0;
            // 
            // btnMenu
            // 
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.IconChar = FontAwesome.Sharp.IconChar.Navicon;
            this.btnMenu.IconColor = System.Drawing.Color.White;
            this.btnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenu.IconSize = 30;
            this.btnMenu.Location = new System.Drawing.Point(138, 3);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(106, 103);
            this.btnMenu.TabIndex = 2;
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.White;
            this.panelTitleBar.Controls.Add(this.btmMin);
            this.panelTitleBar.Controls.Add(this.btmMax);
            this.panelTitleBar.Controls.Add(this.btnCloseForm);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(250, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1590, 60);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // btmMin
            // 
            this.btmMin.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btmMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btmMin.FlatAppearance.BorderSize = 0;
            this.btmMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btmMin.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btmMin.IconColor = System.Drawing.Color.White;
            this.btmMin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btmMin.Location = new System.Drawing.Point(1377, 0);
            this.btmMin.Name = "btmMin";
            this.btmMin.Size = new System.Drawing.Size(71, 60);
            this.btmMin.TabIndex = 2;
            this.btmMin.UseVisualStyleBackColor = false;
            this.btmMin.Click += new System.EventHandler(this.btmMin_Click);
            // 
            // btmMax
            // 
            this.btmMax.BackColor = System.Drawing.Color.RoyalBlue;
            this.btmMax.Dock = System.Windows.Forms.DockStyle.Right;
            this.btmMax.FlatAppearance.BorderSize = 0;
            this.btmMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btmMax.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            this.btmMax.IconColor = System.Drawing.Color.White;
            this.btmMax.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btmMax.Location = new System.Drawing.Point(1448, 0);
            this.btmMax.Name = "btmMax";
            this.btmMax.Size = new System.Drawing.Size(71, 60);
            this.btmMax.TabIndex = 1;
            this.btmMax.UseVisualStyleBackColor = false;
            this.btmMax.Click += new System.EventHandler(this.btmMax_Click);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.BackColor = System.Drawing.Color.Salmon;
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.IconChar = FontAwesome.Sharp.IconChar.XmarkSquare;
            this.btnCloseForm.IconColor = System.Drawing.Color.White;
            this.btnCloseForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCloseForm.Location = new System.Drawing.Point(1519, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(71, 60);
            this.btnCloseForm.TabIndex = 0;
            this.btnCloseForm.UseVisualStyleBackColor = false;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDesktop.Controls.Add(this.panelListProduct);
            this.panelDesktop.Controls.Add(this.panelTotal);
            this.panelDesktop.Controls.Add(this.iconPictureBox1);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(250, 60);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(1590, 827);
            this.panelDesktop.TabIndex = 2;
            // 
            // panelListProduct
            // 
            this.panelListProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListProduct.BackColor = System.Drawing.Color.LightYellow;
            this.panelListProduct.Controls.Add(this.tabControl1);
            this.panelListProduct.Controls.Add(this.btnClearListProduct);
            this.panelListProduct.Controls.Add(this.iconPictureBox2);
            this.panelListProduct.Controls.Add(this.txtSearch);
            this.panelListProduct.Location = new System.Drawing.Point(6, 6);
            this.panelListProduct.Name = "panelListProduct";
            this.panelListProduct.Size = new System.Drawing.Size(834, 809);
            this.panelListProduct.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Listado);
            this.tabControl1.Controls.Add(this.Grilla);
            this.tabControl1.Location = new System.Drawing.Point(3, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(831, 727);
            this.tabControl1.TabIndex = 3;
            // 
            // Listado
            // 
            this.Listado.Controls.Add(this.InventoryDataGrid);
            this.Listado.Location = new System.Drawing.Point(4, 39);
            this.Listado.Name = "Listado";
            this.Listado.Padding = new System.Windows.Forms.Padding(3);
            this.Listado.Size = new System.Drawing.Size(823, 684);
            this.Listado.TabIndex = 0;
            this.Listado.Text = "Listado";
            this.Listado.UseVisualStyleBackColor = true;
            // 
            // InventoryDataGrid
            // 
            this.InventoryDataGrid.AllowUserToAddRows = false;
            this.InventoryDataGrid.AllowUserToDeleteRows = false;
            this.InventoryDataGrid.AllowUserToResizeRows = false;
            this.InventoryDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InventoryDataGrid.BackgroundColor = System.Drawing.SystemColors.Info;
            this.InventoryDataGrid.ColumnHeadersHeight = 40;
            this.InventoryDataGrid.Location = new System.Drawing.Point(10, 9);
            this.InventoryDataGrid.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.InventoryDataGrid.MultiSelect = false;
            this.InventoryDataGrid.Name = "InventoryDataGrid";
            this.InventoryDataGrid.ReadOnly = true;
            this.InventoryDataGrid.RowHeadersVisible = false;
            this.InventoryDataGrid.RowHeadersWidth = 72;
            this.InventoryDataGrid.Size = new System.Drawing.Size(803, 666);
            this.InventoryDataGrid.TabIndex = 32;
            this.InventoryDataGrid.TabStop = false;
            // 
            // Grilla
            // 
            this.Grilla.Location = new System.Drawing.Point(4, 39);
            this.Grilla.Name = "Grilla";
            this.Grilla.Padding = new System.Windows.Forms.Padding(3);
            this.Grilla.Size = new System.Drawing.Size(823, 684);
            this.Grilla.TabIndex = 1;
            this.Grilla.Text = "Grilla";
            this.Grilla.UseVisualStyleBackColor = true;
            // 
            // btnClearListProduct
            // 
            this.btnClearListProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearListProduct.FlatAppearance.BorderSize = 0;
            this.btnClearListProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearListProduct.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnClearListProduct.IconColor = System.Drawing.Color.DodgerBlue;
            this.btnClearListProduct.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClearListProduct.Location = new System.Drawing.Point(766, 3);
            this.btnClearListProduct.Name = "btnClearListProduct";
            this.btnClearListProduct.Size = new System.Drawing.Size(65, 70);
            this.btnClearListProduct.TabIndex = 2;
            this.btnClearListProduct.UseVisualStyleBackColor = true;
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.iconPictureBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Barcode;
            this.iconPictureBox2.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox2.IconSize = 51;
            this.iconPictureBox2.Location = new System.Drawing.Point(708, 8);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(52, 51);
            this.iconPictureBox2.TabIndex = 1;
            this.iconPictureBox2.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSearch.Location = new System.Drawing.Point(3, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Buscar por SKU o por nombre de producto";
            this.txtSearch.Size = new System.Drawing.Size(757, 51);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // panelTotal
            // 
            this.panelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotal.BackColor = System.Drawing.Color.PapayaWhip;
            this.panelTotal.Controls.Add(this.DetailDataGrid);
            this.panelTotal.Controls.Add(this.panel1);
            this.panelTotal.Location = new System.Drawing.Point(846, 6);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(736, 809);
            this.panelTotal.TabIndex = 2;
            // 
            // DetailDataGrid
            // 
            this.DetailDataGrid.AllowUserToAddRows = false;
            this.DetailDataGrid.AllowUserToDeleteRows = false;
            this.DetailDataGrid.AllowUserToResizeColumns = false;
            this.DetailDataGrid.AllowUserToResizeRows = false;
            this.DetailDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.DetailDataGrid.ColumnHeadersHeight = 40;
            this.DetailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DetailDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.delete,
            this.Editar,
            this.less,
            this.Edit});
            this.DetailDataGrid.Location = new System.Drawing.Point(8, 273);
            this.DetailDataGrid.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.DetailDataGrid.MultiSelect = false;
            this.DetailDataGrid.Name = "DetailDataGrid";
            this.DetailDataGrid.ReadOnly = true;
            this.DetailDataGrid.RowHeadersVisible = false;
            this.DetailDataGrid.RowHeadersWidth = 72;
            this.DetailDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DetailDataGrid.Size = new System.Drawing.Size(721, 348);
            this.DetailDataGrid.TabIndex = 31;
            this.DetailDataGrid.TabStop = false;
            // 
            // delete
            // 
            this.delete.HeaderText = "";
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.delete.MinimumWidth = 9;
            this.delete.Name = "delete";
            this.delete.ReadOnly = true;
            this.delete.Width = 30;
            // 
            // Editar
            // 
            this.Editar.HeaderText = "";
            this.Editar.Image = ((System.Drawing.Image)(resources.GetObject("Editar.Image")));
            this.Editar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Editar.MinimumWidth = 9;
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Width = 30;
            // 
            // less
            // 
            this.less.HeaderText = "";
            this.less.Image = ((System.Drawing.Image)(resources.GetObject("less.Image")));
            this.less.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.less.MinimumWidth = 9;
            this.less.Name = "less";
            this.less.ReadOnly = true;
            this.less.Width = 30;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Edit.MinimumWidth = 9;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 30;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 175);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(88, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 65);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vuelto";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 27.85714F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(238, 10);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(127, 87);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "$ 0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(32, 32);
            this.iconPictureBox1.TabIndex = 4;
            this.iconPictureBox1.TabStop = false;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1840, 887);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartForm_FormClosed);
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.Resize += new System.EventHandler(this.StartForm_Resize);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.panelDesktop.ResumeLayout(false);
            this.panelListProduct.ResumeLayout(false);
            this.panelListProduct.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Listado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            this.panelTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DetailDataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelMenu;
        private Panel panelLogo;
        private PictureBox pictureBox1;
        private Panel panelTitleBar;
        private Panel panelDesktop;
        private FontAwesome.Sharp.IconButton btnHome;
        private FontAwesome.Sharp.IconButton btnMenu;
        private FontAwesome.Sharp.IconButton btnExit;
        private FontAwesome.Sharp.IconButton btnVentas;
        private FontAwesome.Sharp.IconButton btmMin;
        private FontAwesome.Sharp.IconButton btmMax;
        private FontAwesome.Sharp.IconButton btnCloseForm;
        private TextBox txtSearch;
        private Panel panelTotal;
        private Panel panel1;
        private Panel panelListProduct;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private TabControl tabControl1;
        private TabPage Listado;
        private TabPage Grilla;
        private FontAwesome.Sharp.IconButton btnClearListProduct;
        private DataGridView InventoryDataGrid;
        private DataGridView DetailDataGrid;
        private DataGridViewImageColumn delete;
        private DataGridViewImageColumn Editar;
        private DataGridViewImageColumn less;
        private DataGridViewImageColumn Edit;
        private Label label1;
        private Label lblTotal;
    }
}