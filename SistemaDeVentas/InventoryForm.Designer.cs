namespace SistemaDeVentas
{
    partial class InventoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private TextBox filterTextBox;

        private Label label1;

        private DataGridView InventoryDataGrid;

        private Label label2;

        private Label label3;

        private Label label4;

        private Label label5;

        private TextBox name_input;

        private TextBox amount_input;

        private TextBox price_input;

        private TextBox minimum_input;

        private Button InsertButton;

        private Button SaveButton;

        private Button DeleteButton;

        private Button CleanButton;

        private Label label7;

        private TextBox barcode_input;

        private PictureBox pictureBox1;

        private Label label6;

        private Label label8;

        private CheckBox isExenta_checkbox;

        private Label label9;

        private ComboBox listIVA_combobox;

        private Label label10;

        public DateTimePicker expiration_datetimepicker;

        private Label label11;

        private ComboBox category_combobox;

        private TabControl tabControl1;

        private TabPage tabPage1;

        private TabPage tabPage2;

        private CheckBox pack_checkbox;

        private Label label12;

        private TextBox packbarcode_input;

        private Label label13;

        private DataGridView DetailsPackDatagrid;

        private Label label15;

        private Label label14;

        private TextBox packamount_input;

        private TextBox packname_input;

        private Label label16;

        private TextBox sales_input;

        private ComboBox subcategory_combobox;

        private Label label17;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.InventoryDataGrid = new System.Windows.Forms.DataGridView();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.name_input = new System.Windows.Forms.TextBox();
            this.amount_input = new System.Windows.Forms.TextBox();
            this.price_input = new System.Windows.Forms.TextBox();
            this.minimum_input = new System.Windows.Forms.TextBox();
            this.InsertButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CleanButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.barcode_input = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.isExenta_checkbox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.listIVA_combobox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.expiration_datetimepicker = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.category_combobox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.subcategory_combobox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.sales_input = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.packamount_input = new System.Windows.Forms.TextBox();
            this.packname_input = new System.Windows.Forms.TextBox();
            this.packbarcode_input = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.DetailsPackDatagrid = new System.Windows.Forms.DataGridView();
            this.pack_checkbox = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPackDatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // InventoryDataGrid
            // 
            this.InventoryDataGrid.AllowUserToAddRows = false;
            this.InventoryDataGrid.AllowUserToDeleteRows = false;
            this.InventoryDataGrid.AllowUserToResizeColumns = false;
            this.InventoryDataGrid.AllowUserToResizeRows = false;
            this.InventoryDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InventoryDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InventoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.InventoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InventoryDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.InventoryDataGrid.Location = new System.Drawing.Point(24, 226);
            this.InventoryDataGrid.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.InventoryDataGrid.Name = "InventoryDataGrid";
            this.InventoryDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InventoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.InventoryDataGrid.RowHeadersVisible = false;
            this.InventoryDataGrid.RowHeadersWidth = 72;
            this.InventoryDataGrid.Size = new System.Drawing.Size(1032, 1156);
            this.InventoryDataGrid.TabIndex = 2;
            this.InventoryDataGrid.TabStop = false;
            this.InventoryDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InventoryDataGrid_CellClick);
            this.InventoryDataGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.InventoryDataGrid_CellEnter);
            this.InventoryDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InventoryDataGrid_KeyDown);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(250, 166);
            this.filterTextBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(801, 35);
            this.filterTextBox.TabIndex = 1;
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            this.filterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filterTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(250, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar Productos :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre del Producto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cantidad  Actual:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(12, 140);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Precio de Compra (Neto):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(12, 260);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "Stock Minimo:";
            // 
            // name_input
            // 
            this.name_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.name_input.Location = new System.Drawing.Point(326, 14);
            this.name_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.name_input.Name = "name_input";
            this.name_input.Size = new System.Drawing.Size(568, 35);
            this.name_input.TabIndex = 3;
            // 
            // amount_input
            // 
            this.amount_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.amount_input.Enabled = false;
            this.amount_input.Location = new System.Drawing.Point(326, 74);
            this.amount_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.amount_input.Name = "amount_input";
            this.amount_input.Size = new System.Drawing.Size(266, 35);
            this.amount_input.TabIndex = 4;
            this.amount_input.Text = "0";
            // 
            // price_input
            // 
            this.price_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.price_input.Location = new System.Drawing.Point(326, 134);
            this.price_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.price_input.Name = "price_input";
            this.price_input.Size = new System.Drawing.Size(266, 35);
            this.price_input.TabIndex = 5;
            this.price_input.Text = "0";
            // 
            // minimum_input
            // 
            this.minimum_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minimum_input.Location = new System.Drawing.Point(326, 254);
            this.minimum_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.minimum_input.Name = "minimum_input";
            this.minimum_input.Size = new System.Drawing.Size(266, 35);
            this.minimum_input.TabIndex = 7;
            this.minimum_input.Text = "0";
            // 
            // InsertButton
            // 
            this.InsertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InsertButton.BackColor = System.Drawing.Color.ForestGreen;
            this.InsertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InsertButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.InsertButton.Location = new System.Drawing.Point(1102, 1248);
            this.InsertButton.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(257, 134);
            this.InsertButton.TabIndex = 13;
            this.InsertButton.Text = "Crear Nuevo Producto";
            this.InsertButton.UseVisualStyleBackColor = false;
            this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.SteelBlue;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.SaveButton.Location = new System.Drawing.Point(1419, 1246);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(274, 134);
            this.SaveButton.TabIndex = 14;
            this.SaveButton.Text = "Guardar Cambios";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Visible = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.BackColor = System.Drawing.Color.DarkRed;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DeleteButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DeleteButton.Location = new System.Drawing.Point(1750, 1246);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(243, 134);
            this.DeleteButton.TabIndex = 15;
            this.DeleteButton.Text = "Eliminar Producto";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Visible = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CleanButton
            // 
            this.CleanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CleanButton.BackgroundImage")));
            this.CleanButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CleanButton.Location = new System.Drawing.Point(696, 134);
            this.CleanButton.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.CleanButton.Name = "CleanButton";
            this.CleanButton.Size = new System.Drawing.Size(156, 164);
            this.CleanButton.TabIndex = 16;
            this.CleanButton.UseVisualStyleBackColor = true;
            this.CleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Tomato;
            this.label7.Location = new System.Drawing.Point(12, 320);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "Codigo de Barras:";
            // 
            // barcode_input
            // 
            this.barcode_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barcode_input.Location = new System.Drawing.Point(326, 314);
            this.barcode_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.barcode_input.Name = "barcode_input";
            this.barcode_input.Size = new System.Drawing.Size(266, 35);
            this.barcode_input.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(24, 28);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 184);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(243, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(540, 54);
            this.label6.TabIndex = 20;
            this.label6.Text = "Inventario de Productos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.Tomato;
            this.label8.Location = new System.Drawing.Point(12, 564);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(187, 25);
            this.label8.TabIndex = 21;
            this.label8.Text = "¿Excento de IVA?";
            // 
            // isExenta_checkbox
            // 
            this.isExenta_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isExenta_checkbox.AutoSize = true;
            this.isExenta_checkbox.Location = new System.Drawing.Point(326, 560);
            this.isExenta_checkbox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.isExenta_checkbox.Name = "isExenta_checkbox";
            this.isExenta_checkbox.Size = new System.Drawing.Size(55, 34);
            this.isExenta_checkbox.TabIndex = 11;
            this.isExenta_checkbox.Text = "Si";
            this.isExenta_checkbox.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.Tomato;
            this.label9.Location = new System.Drawing.Point(12, 506);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 25);
            this.label9.TabIndex = 23;
            this.label9.Text = "Tipo de IVA:";
            // 
            // listIVA_combobox
            // 
            this.listIVA_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listIVA_combobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.listIVA_combobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.listIVA_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listIVA_combobox.FormattingEnabled = true;
            this.listIVA_combobox.Location = new System.Drawing.Point(326, 498);
            this.listIVA_combobox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.listIVA_combobox.MaxDropDownItems = 10;
            this.listIVA_combobox.Name = "listIVA_combobox";
            this.listIVA_combobox.Size = new System.Drawing.Size(568, 38);
            this.listIVA_combobox.TabIndex = 9;
            this.listIVA_combobox.SelectedIndexChanged += new System.EventHandler(this.listIVA_combobox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.Tomato;
            this.label10.Location = new System.Drawing.Point(12, 628);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(204, 25);
            this.label10.TabIndex = 26;
            this.label10.Text = "Fecha Vencimiento:";
            // 
            // expiration_datetimepicker
            // 
            this.expiration_datetimepicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expiration_datetimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expiration_datetimepicker.Location = new System.Drawing.Point(326, 614);
            this.expiration_datetimepicker.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.expiration_datetimepicker.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.expiration_datetimepicker.Name = "expiration_datetimepicker";
            this.expiration_datetimepicker.Size = new System.Drawing.Size(266, 35);
            this.expiration_datetimepicker.TabIndex = 12;
            this.expiration_datetimepicker.Value = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.Tomato;
            this.label11.Location = new System.Drawing.Point(12, 380);
            this.label11.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(154, 25);
            this.label11.TabIndex = 27;
            this.label11.Text = "Departamento:";
            // 
            // category_combobox
            // 
            this.category_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.category_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category_combobox.Enabled = false;
            this.category_combobox.FormattingEnabled = true;
            this.category_combobox.Location = new System.Drawing.Point(326, 374);
            this.category_combobox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.category_combobox.MaxDropDownItems = 9;
            this.category_combobox.Name = "category_combobox";
            this.category_combobox.Size = new System.Drawing.Size(568, 38);
            this.category_combobox.Sorted = true;
            this.category_combobox.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1068, 146);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(926, 1058);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.subcategory_combobox);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.sales_input);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.category_combobox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.CleanButton);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.expiration_datetimepicker);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.name_input);
            this.tabPage1.Controls.Add(this.listIVA_combobox);
            this.tabPage1.Controls.Add(this.amount_input);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.price_input);
            this.tabPage1.Controls.Add(this.isExenta_checkbox);
            this.tabPage1.Controls.Add(this.minimum_input);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.barcode_input);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabPage1.Size = new System.Drawing.Size(918, 1015);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nuevo Producto";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // subcategory_combobox
            // 
            this.subcategory_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subcategory_combobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.subcategory_combobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.subcategory_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subcategory_combobox.FormattingEnabled = true;
            this.subcategory_combobox.Location = new System.Drawing.Point(326, 436);
            this.subcategory_combobox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.subcategory_combobox.MaxDropDownItems = 9;
            this.subcategory_combobox.Name = "subcategory_combobox";
            this.subcategory_combobox.Size = new System.Drawing.Size(568, 38);
            this.subcategory_combobox.Sorted = true;
            this.subcategory_combobox.TabIndex = 9;
            this.subcategory_combobox.SelectedValueChanged += new System.EventHandler(this.subcategory_combobox_SelectedValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label17.ForeColor = System.Drawing.Color.Tomato;
            this.label17.Location = new System.Drawing.Point(12, 444);
            this.label17.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(113, 25);
            this.label17.TabIndex = 30;
            this.label17.Text = "Categoría:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.ForeColor = System.Drawing.Color.Tomato;
            this.label16.Location = new System.Drawing.Point(12, 200);
            this.label16.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(246, 25);
            this.label16.TabIndex = 28;
            this.label16.Text = "Precio de Venta (Bruto):";
            // 
            // sales_input
            // 
            this.sales_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sales_input.Enabled = false;
            this.sales_input.Location = new System.Drawing.Point(326, 194);
            this.sales_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.sales_input.Name = "sales_input";
            this.sales_input.ReadOnly = true;
            this.sales_input.Size = new System.Drawing.Size(266, 35);
            this.sales_input.TabIndex = 6;
            this.sales_input.Text = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.packamount_input);
            this.tabPage2.Controls.Add(this.packname_input);
            this.tabPage2.Controls.Add(this.packbarcode_input);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.DetailsPackDatagrid);
            this.tabPage2.Controls.Add(this.pack_checkbox);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabPage2.Size = new System.Drawing.Size(918, 1219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Armar Pack";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.Tomato;
            this.label15.Location = new System.Drawing.Point(27, 228);
            this.label15.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 25);
            this.label15.TabIndex = 35;
            this.label15.Text = "Cantidad";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.Color.Tomato;
            this.label14.Location = new System.Drawing.Point(27, 168);
            this.label14.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(214, 25);
            this.label14.TabIndex = 34;
            this.label14.Text = "Nombre del Producto";
            // 
            // packamount_input
            // 
            this.packamount_input.Location = new System.Drawing.Point(297, 222);
            this.packamount_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.packamount_input.Name = "packamount_input";
            this.packamount_input.ReadOnly = true;
            this.packamount_input.Size = new System.Drawing.Size(66, 35);
            this.packamount_input.TabIndex = 33;
            this.packamount_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.packamount_input_KeyDown);
            // 
            // packname_input
            // 
            this.packname_input.Location = new System.Drawing.Point(297, 162);
            this.packname_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.packname_input.Name = "packname_input";
            this.packname_input.ReadOnly = true;
            this.packname_input.Size = new System.Drawing.Size(355, 35);
            this.packname_input.TabIndex = 32;
            this.packname_input.TabStop = false;
            // 
            // packbarcode_input
            // 
            this.packbarcode_input.Location = new System.Drawing.Point(297, 102);
            this.packbarcode_input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.packbarcode_input.Name = "packbarcode_input";
            this.packbarcode_input.ReadOnly = true;
            this.packbarcode_input.Size = new System.Drawing.Size(355, 35);
            this.packbarcode_input.TabIndex = 31;
            this.packbarcode_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.packbarcode_input_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.Tomato;
            this.label13.Location = new System.Drawing.Point(27, 108);
            this.label13.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(169, 25);
            this.label13.TabIndex = 30;
            this.label13.Text = "Código de Barra";
            // 
            // DetailsPackDatagrid
            // 
            this.DetailsPackDatagrid.AllowUserToAddRows = false;
            this.DetailsPackDatagrid.AllowUserToDeleteRows = false;
            this.DetailsPackDatagrid.AllowUserToResizeColumns = false;
            this.DetailsPackDatagrid.AllowUserToResizeRows = false;
            this.DetailsPackDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsPackDatagrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DetailsPackDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DetailsPackDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DetailsPackDatagrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.DetailsPackDatagrid.Location = new System.Drawing.Point(34, 282);
            this.DetailsPackDatagrid.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.DetailsPackDatagrid.Name = "DetailsPackDatagrid";
            this.DetailsPackDatagrid.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DetailsPackDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DetailsPackDatagrid.RowHeadersVisible = false;
            this.DetailsPackDatagrid.RowHeadersWidth = 72;
            this.DetailsPackDatagrid.Size = new System.Drawing.Size(833, 882);
            this.DetailsPackDatagrid.TabIndex = 29;
            this.DetailsPackDatagrid.TabStop = false;
            this.DetailsPackDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DetailsPackDatagrid_CellClick);
            this.DetailsPackDatagrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DetailsPackDatagrid_CellDoubleClick);
            this.DetailsPackDatagrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailsPackDatagrid_KeyDown);
            // 
            // pack_checkbox
            // 
            this.pack_checkbox.AutoSize = true;
            this.pack_checkbox.Location = new System.Drawing.Point(297, 48);
            this.pack_checkbox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pack_checkbox.Name = "pack_checkbox";
            this.pack_checkbox.Size = new System.Drawing.Size(55, 34);
            this.pack_checkbox.TabIndex = 22;
            this.pack_checkbox.Text = "Si";
            this.pack_checkbox.UseVisualStyleBackColor = true;
            this.pack_checkbox.CheckedChanged += new System.EventHandler(this.pack_checkbox_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.Tomato;
            this.label12.Location = new System.Drawing.Point(27, 50);
            this.label12.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 25);
            this.label12.TabIndex = 23;
            this.label12.Text = "Activar Pack";
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2016, 1404);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.InventoryDataGrid);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "InventoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario de Productos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryForm_FormClosing);
            this.Shown += new System.EventHandler(this.InventoryForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailsPackDatagrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}