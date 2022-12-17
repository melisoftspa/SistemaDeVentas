namespace SistemaDeVentas
{
    partial class SalesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private DataGridView InventoryDataGrid;

        private Label label1;

        private TextBox filterTextBox;

        private TextBox price_input;

        private TextBox amount_input;

        private TextBox name_input;

        private Label label4;

        private Label label3;

        private Label label2;

        private TextBox barcode_input;

        private Label label5;

        private Label label7;

        private DataGridView DetailDataGrid;

        private Label label8;

        private Label label9;

        private TextBox totalSale_input;

        private Button processButton;

        private PictureBox pictureBox1;

        private Label label6;

        private Label label10;

        private TextBox change_input;

        private Label label11;

        private TextBox payment_cash_input;

        private Label label12;

        private TextBox payment_other_input;

        private Label label13;

        private TextBox notes_input;

        private Label label14;

        private Button Addproductsale_button;

        private Button CleanButton;

        private TextBox kg_input;

        private Label label15;

        private Button processButton2;

        private CheckBox decrease_checkbox;

        private Label label16;

        private Label label17;

        private Label label18;

        private Button reprint_button;

        private Label vendor_label;

        private System.Windows.Forms.Timer timer;

        private Label timer_label;

        private PictureBox pictureBox2;

        private DataGridViewImageColumn Editar;

        private DataGridViewImageColumn less;

        private TextBox ticket_input;

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
            this.components = new System.ComponentModel.Container();
            this.InventoryDataGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.price_input = new System.Windows.Forms.TextBox();
            this.amount_input = new System.Windows.Forms.TextBox();
            this.name_input = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.barcode_input = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DetailDataGrid = new System.Windows.Forms.DataGridView();
            this.Editar = new System.Windows.Forms.DataGridViewImageColumn();
            this.less = new System.Windows.Forms.DataGridViewImageColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.totalSale_input = new System.Windows.Forms.TextBox();
            this.processButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.change_input = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.payment_cash_input = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.payment_other_input = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.notes_input = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Addproductsale_button = new System.Windows.Forms.Button();
            this.CleanButton = new System.Windows.Forms.Button();
            this.kg_input = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.processButton2 = new System.Windows.Forms.Button();
            this.decrease_checkbox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.reprint_button = new System.Windows.Forms.Button();
            this.vendor_label = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer_label = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ticket_input = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
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
            this.InventoryDataGrid.Location = new System.Drawing.Point(14, 113);
            this.InventoryDataGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.InventoryDataGrid.MultiSelect = false;
            this.InventoryDataGrid.Name = "InventoryDataGrid";
            this.InventoryDataGrid.ReadOnly = true;
            this.InventoryDataGrid.RowHeadersVisible = false;
            this.InventoryDataGrid.Size = new System.Drawing.Size(511, 714);
            this.InventoryDataGrid.TabIndex = 31;
            this.InventoryDataGrid.TabStop = false;
            this.InventoryDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InventoryDataGrid_CellClick);
            this.InventoryDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InventoryDataGrid_CellContentClick);
            this.InventoryDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.InventoryDataGrid_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(121, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "[F6] Buscar Productos :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.filterTextBox.Location = new System.Drawing.Point(121, 83);
            this.filterTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(328, 23);
            this.filterTextBox.TabIndex = 1;
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            this.filterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filterTextBox_KeyDown);
            // 
            // price_input
            // 
            this.price_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.price_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.price_input.Location = new System.Drawing.Point(693, 163);
            this.price_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.price_input.Name = "price_input";
            this.price_input.ReadOnly = true;
            this.price_input.Size = new System.Drawing.Size(226, 23);
            this.price_input.TabIndex = 4;
            this.price_input.TabStop = false;
            this.price_input.Text = "$0";
            this.price_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.price_input_KeyDown);
            this.price_input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.price_input_KeyUp);
            // 
            // amount_input
            // 
            this.amount_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.amount_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.amount_input.Location = new System.Drawing.Point(693, 195);
            this.amount_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.amount_input.Name = "amount_input";
            this.amount_input.Size = new System.Drawing.Size(226, 23);
            this.amount_input.TabIndex = 5;
            this.amount_input.Text = "1";
            this.amount_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.amount_input_KeyPress);
            this.amount_input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.amount_input_KeyUp);
            // 
            // name_input
            // 
            this.name_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.name_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.name_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.name_input.Location = new System.Drawing.Point(693, 129);
            this.name_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.name_input.Name = "name_input";
            this.name_input.ReadOnly = true;
            this.name_input.Size = new System.Drawing.Size(464, 23);
            this.name_input.TabIndex = 3;
            this.name_input.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(534, 166);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Precio:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(534, 198);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Cantidad:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(534, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Nombre del Producto:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // barcode_input
            // 
            this.barcode_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.barcode_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.barcode_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.barcode_input.Location = new System.Drawing.Point(693, 80);
            this.barcode_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barcode_input.Name = "barcode_input";
            this.barcode_input.Size = new System.Drawing.Size(226, 23);
            this.barcode_input.TabIndex = 2;
            this.barcode_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.barcode_input_KeyDown);
            this.barcode_input.Leave += new System.EventHandler(this.barcode_input_Leave);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(534, 83);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "[F7] Codigo de Barras:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Tomato;
            this.label7.Location = new System.Drawing.Point(532, 261);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 20);
            this.label7.TabIndex = 38;
            this.label7.Text = "Detalle de Venta:";
            // 
            // DetailDataGrid
            // 
            this.DetailDataGrid.AllowUserToAddRows = false;
            this.DetailDataGrid.AllowUserToDeleteRows = false;
            this.DetailDataGrid.AllowUserToResizeColumns = false;
            this.DetailDataGrid.AllowUserToResizeRows = false;
            this.DetailDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.DetailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DetailDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Editar,
            this.less});
            this.DetailDataGrid.Location = new System.Drawing.Point(537, 287);
            this.DetailDataGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DetailDataGrid.MultiSelect = false;
            this.DetailDataGrid.Name = "DetailDataGrid";
            this.DetailDataGrid.ReadOnly = true;
            this.DetailDataGrid.RowHeadersVisible = false;
            this.DetailDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DetailDataGrid.Size = new System.Drawing.Size(622, 370);
            this.DetailDataGrid.TabIndex = 30;
            this.DetailDataGrid.TabStop = false;
            this.DetailDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DetailDataGrid_CellClick);
            this.DetailDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DetailDataGrid_CellDoubleClick);
            this.DetailDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DetailDataGrid_CellFormatting);
            this.DetailDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailDataGrid_KeyDown);
            // 
            // Editar
            // 
            this.Editar.HeaderText = "";
            this.Editar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Width = 20;
            // 
            // less
            // 
            this.less.HeaderText = "";
            this.less.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.less.Name = "less";
            this.less.ReadOnly = true;
            this.less.Width = 20;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(538, 258);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(621, 2);
            this.label8.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.Tomato;
            this.label9.Location = new System.Drawing.Point(933, 738);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 20);
            this.label9.TabIndex = 41;
            this.label9.Text = "Total:";
            // 
            // totalSale_input
            // 
            this.totalSale_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalSale_input.BackColor = System.Drawing.Color.Blue;
            this.totalSale_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalSale_input.ForeColor = System.Drawing.Color.White;
            this.totalSale_input.Location = new System.Drawing.Point(999, 723);
            this.totalSale_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.totalSale_input.Name = "totalSale_input";
            this.totalSale_input.ReadOnly = true;
            this.totalSale_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.totalSale_input.Size = new System.Drawing.Size(163, 38);
            this.totalSale_input.TabIndex = 42;
            this.totalSale_input.TabStop = false;
            this.totalSale_input.Text = "$0";
            // 
            // processButton
            // 
            this.processButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.processButton.BackColor = System.Drawing.Color.Gainsboro;
            this.processButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.processButton.Enabled = false;
            this.processButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.processButton.ForeColor = System.Drawing.Color.White;
            this.processButton.Location = new System.Drawing.Point(1096, 774);
            this.processButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(66, 59);
            this.processButton.TabIndex = 12;
            this.processButton.UseVisualStyleBackColor = false;
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(14, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 92);
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(114, 18);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 31);
            this.label6.TabIndex = 45;
            this.label6.Text = "Ventas";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(537, 117);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(621, 2);
            this.label10.TabIndex = 46;
            // 
            // change_input
            // 
            this.change_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.change_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.change_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.change_input.ForeColor = System.Drawing.Color.White;
            this.change_input.Location = new System.Drawing.Point(999, 665);
            this.change_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.change_input.Name = "change_input";
            this.change_input.ReadOnly = true;
            this.change_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.change_input.Size = new System.Drawing.Size(163, 38);
            this.change_input.TabIndex = 48;
            this.change_input.TabStop = false;
            this.change_input.Text = "$0";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.Tomato;
            this.label11.Location = new System.Drawing.Point(919, 680);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 20);
            this.label11.TabIndex = 47;
            this.label11.Text = "Vuelto:";
            // 
            // payment_cash_input
            // 
            this.payment_cash_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.payment_cash_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.payment_cash_input.Location = new System.Drawing.Point(726, 665);
            this.payment_cash_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.payment_cash_input.Name = "payment_cash_input";
            this.payment_cash_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.payment_cash_input.Size = new System.Drawing.Size(163, 38);
            this.payment_cash_input.TabIndex = 9;
            this.payment_cash_input.Text = "$0";
            this.payment_cash_input.TextChanged += new System.EventHandler(this.payment_cash_input_TextChanged);
            this.payment_cash_input.Enter += new System.EventHandler(this.payment_cash_input_Enter);
            this.payment_cash_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.payment_cash_input_KeyDown);
            this.payment_cash_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.payment_cash_input_KeyPress);
            this.payment_cash_input.Leave += new System.EventHandler(this.payment_cash_input_Leave);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.Tomato;
            this.label12.Location = new System.Drawing.Point(531, 680);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(161, 20);
            this.label12.TabIndex = 51;
            this.label12.Text = "[F8] Pago Efectivo:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // payment_other_input
            // 
            this.payment_other_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.payment_other_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.payment_other_input.Location = new System.Drawing.Point(726, 723);
            this.payment_other_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.payment_other_input.Name = "payment_other_input";
            this.payment_other_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.payment_other_input.Size = new System.Drawing.Size(163, 38);
            this.payment_other_input.TabIndex = 10;
            this.payment_other_input.Text = "$0";
            this.payment_other_input.TextChanged += new System.EventHandler(this.payment_other_input_TextChanged);
            this.payment_other_input.Enter += new System.EventHandler(this.payment_other_input_Enter);
            this.payment_other_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.payment_other_input_KeyDown);
            this.payment_other_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.payment_other_input_KeyPress);
            this.payment_other_input.Leave += new System.EventHandler(this.payment_other_input_Leave);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.Tomato;
            this.label13.Location = new System.Drawing.Point(541, 738);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(152, 20);
            this.label13.TabIndex = 49;
            this.label13.Text = "[F9] Pago Tarjeta:";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // notes_input
            // 
            this.notes_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.notes_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.notes_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.notes_input.Location = new System.Drawing.Point(624, 778);
            this.notes_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.notes_input.Multiline = true;
            this.notes_input.Name = "notes_input";
            this.notes_input.Size = new System.Drawing.Size(202, 51);
            this.notes_input.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.Color.Tomato;
            this.label14.Location = new System.Drawing.Point(534, 783);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 54;
            this.label14.Text = "[F10] Nota:";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // Addproductsale_button
            // 
            this.Addproductsale_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Addproductsale_button.BackColor = System.Drawing.SystemColors.Window;
            this.Addproductsale_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Addproductsale_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Addproductsale_button.Location = new System.Drawing.Point(939, 163);
            this.Addproductsale_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Addproductsale_button.Name = "Addproductsale_button";
            this.Addproductsale_button.Size = new System.Drawing.Size(72, 59);
            this.Addproductsale_button.TabIndex = 7;
            this.Addproductsale_button.UseVisualStyleBackColor = false;
            this.Addproductsale_button.Click += new System.EventHandler(this.Addproductsale_button_Click);
            // 
            // CleanButton
            // 
            this.CleanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CleanButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CleanButton.Location = new System.Drawing.Point(1096, 163);
            this.CleanButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CleanButton.Name = "CleanButton";
            this.CleanButton.Size = new System.Drawing.Size(62, 59);
            this.CleanButton.TabIndex = 8;
            this.CleanButton.UseVisualStyleBackColor = true;
            this.CleanButton.Click += new System.EventHandler(this.CleanButton_Click);
            // 
            // kg_input
            // 
            this.kg_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kg_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.kg_input.Location = new System.Drawing.Point(693, 228);
            this.kg_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.kg_input.Name = "kg_input";
            this.kg_input.Size = new System.Drawing.Size(226, 23);
            this.kg_input.TabIndex = 6;
            this.kg_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.kg_input_KeyPress);
            this.kg_input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.kg_input_KeyUp);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.Tomato;
            this.label15.Location = new System.Drawing.Point(534, 232);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 56;
            this.label15.Text = "Kilos:";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // processButton2
            // 
            this.processButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.processButton2.BackColor = System.Drawing.Color.Gainsboro;
            this.processButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.processButton2.Enabled = false;
            this.processButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.processButton2.ForeColor = System.Drawing.Color.White;
            this.processButton2.Location = new System.Drawing.Point(918, 778);
            this.processButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.processButton2.Name = "processButton2";
            this.processButton2.Size = new System.Drawing.Size(68, 55);
            this.processButton2.TabIndex = 57;
            this.processButton2.UseVisualStyleBackColor = false;
            this.processButton2.Click += new System.EventHandler(this.processButton2_Click);
            // 
            // decrease_checkbox
            // 
            this.decrease_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.decrease_checkbox.AutoSize = true;
            this.decrease_checkbox.Location = new System.Drawing.Point(950, 232);
            this.decrease_checkbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.decrease_checkbox.Name = "decrease_checkbox";
            this.decrease_checkbox.Size = new System.Drawing.Size(88, 19);
            this.decrease_checkbox.TabIndex = 58;
            this.decrease_checkbox.Text = "¿Es Merma?";
            this.decrease_checkbox.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.ForeColor = System.Drawing.Color.Tomato;
            this.label16.Location = new System.Drawing.Point(834, 785);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.MaximumSize = new System.Drawing.Size(93, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 26);
            this.label16.TabIndex = 59;
            this.label16.Text = "[F11] Procesar Venta S/B";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label17.ForeColor = System.Drawing.Color.Tomato;
            this.label17.Location = new System.Drawing.Point(1011, 785);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.MaximumSize = new System.Drawing.Size(93, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 26);
            this.label17.TabIndex = 60;
            this.label17.Text = "[F12] Procesar Venta C/B";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label18.ForeColor = System.Drawing.Color.Tomato;
            this.label18.Location = new System.Drawing.Point(995, 57);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.MaximumSize = new System.Drawing.Size(93, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 26);
            this.label18.TabIndex = 62;
            this.label18.Text = "Re-Imprimir ultima venta";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // reprint_button
            // 
            this.reprint_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reprint_button.BackColor = System.Drawing.Color.Lime;
            this.reprint_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.reprint_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.reprint_button.ForeColor = System.Drawing.Color.White;
            this.reprint_button.Location = new System.Drawing.Point(1091, 54);
            this.reprint_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.reprint_button.Name = "reprint_button";
            this.reprint_button.Size = new System.Drawing.Size(66, 59);
            this.reprint_button.TabIndex = 61;
            this.reprint_button.TabStop = false;
            this.reprint_button.UseVisualStyleBackColor = false;
            this.reprint_button.Click += new System.EventHandler(this.reprint_button_Click);
            // 
            // vendor_label
            // 
            this.vendor_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vendor_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.vendor_label.ForeColor = System.Drawing.Color.OrangeRed;
            this.vendor_label.Location = new System.Drawing.Point(405, 3);
            this.vendor_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.vendor_label.Name = "vendor_label";
            this.vendor_label.Size = new System.Drawing.Size(754, 23);
            this.vendor_label.TabIndex = 63;
            this.vendor_label.Text = "Bienvenido, ";
            this.vendor_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer_label
            // 
            this.timer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.timer_label.Location = new System.Drawing.Point(398, 27);
            this.timer_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timer_label.Name = "timer_label";
            this.timer_label.Size = new System.Drawing.Size(374, 36);
            this.timer_label.TabIndex = 64;
            this.timer_label.Text = "DD-MM-YYYY 00:00:00";
            this.timer_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(924, 80);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 27);
            this.pictureBox2.TabIndex = 65;
            this.pictureBox2.TabStop = false;
            // 
            // ticket_input
            // 
            this.ticket_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticket_input.Location = new System.Drawing.Point(999, 90);
            this.ticket_input.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ticket_input.Name = "ticket_input";
            this.ticket_input.Size = new System.Drawing.Size(84, 23);
            this.ticket_input.TabIndex = 66;
            this.ticket_input.TabStop = false;
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1176, 804);
            this.Controls.Add(this.ticket_input);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.timer_label);
            this.Controls.Add(this.vendor_label);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.reprint_button);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.decrease_checkbox);
            this.Controls.Add(this.processButton2);
            this.Controls.Add(this.kg_input);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.CleanButton);
            this.Controls.Add(this.Addproductsale_button);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.notes_input);
            this.Controls.Add(this.payment_cash_input);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.payment_other_input);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.change_input);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.totalSale_input);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DetailDataGrid);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.barcode_input);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.price_input);
            this.Controls.Add(this.amount_input);
            this.Controls.Add(this.name_input);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.InventoryDataGrid);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SalesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Ventas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesForm_FormClosing);
            this.Shown += new System.EventHandler(this.SalesForm_Shown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SalesForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}