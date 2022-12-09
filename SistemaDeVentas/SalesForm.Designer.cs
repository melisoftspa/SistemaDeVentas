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
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "SalesForm";
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.SalesForm));
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
            ((System.ComponentModel.ISupportInitialize)this.InventoryDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.DetailDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
            base.SuspendLayout();
            this.InventoryDataGrid.AllowUserToAddRows = false;
            this.InventoryDataGrid.AllowUserToDeleteRows = false;
            this.InventoryDataGrid.AllowUserToResizeColumns = false;
            this.InventoryDataGrid.AllowUserToResizeRows = false;
            this.InventoryDataGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.InventoryDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.InventoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InventoryDataGrid.Location = new System.Drawing.Point(12, 98);
            this.InventoryDataGrid.MultiSelect = false;
            this.InventoryDataGrid.Name = "InventoryDataGrid";
            this.InventoryDataGrid.ReadOnly = true;
            this.InventoryDataGrid.RowHeadersVisible = false;
            this.InventoryDataGrid.Size = new System.Drawing.Size(438, 619);
            this.InventoryDataGrid.TabIndex = 31;
            this.InventoryDataGrid.TabStop = false;
            this.InventoryDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(InventoryDataGrid_CellClick);
            this.InventoryDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(InventoryDataGrid_CellFormatting);
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(104, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "[F6] Buscar Productos :";
            this.label1.Click += new System.EventHandler(label1_Click);
            this.filterTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.filterTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.filterTextBox.Location = new System.Drawing.Point(104, 72);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(282, 20);
            this.filterTextBox.TabIndex = 1;
            this.filterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(filterTextBox_KeyDown);
            this.price_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.price_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.price_input.Location = new System.Drawing.Point(594, 141);
            this.price_input.Name = "price_input";
            this.price_input.ReadOnly = true;
            this.price_input.Size = new System.Drawing.Size(194, 23);
            this.price_input.TabIndex = 4;
            this.price_input.TabStop = false;
            this.price_input.Text = "$0";
            this.price_input.KeyDown += new System.Windows.Forms.KeyEventHandler(price_input_KeyDown);
            this.price_input.KeyUp += new System.Windows.Forms.KeyEventHandler(price_input_KeyUp);
            this.amount_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.amount_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.amount_input.Location = new System.Drawing.Point(594, 169);
            this.amount_input.Name = "amount_input";
            this.amount_input.Size = new System.Drawing.Size(194, 23);
            this.amount_input.TabIndex = 5;
            this.amount_input.Text = "1";
            this.amount_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(amount_input_KeyPress);
            this.amount_input.KeyUp += new System.Windows.Forms.KeyEventHandler(amount_input_KeyUp);
            this.name_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.name_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.name_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.name_input.Location = new System.Drawing.Point(594, 112);
            this.name_input.Name = "name_input";
            this.name_input.ReadOnly = true;
            this.name_input.Size = new System.Drawing.Size(398, 23);
            this.name_input.TabIndex = 3;
            this.name_input.TabStop = false;
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(458, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Precio:";
            this.label4.Click += new System.EventHandler(label4_Click);
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(458, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Cantidad:";
            this.label3.Click += new System.EventHandler(label3_Click);
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(458, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Nombre del Producto:";
            this.label2.Click += new System.EventHandler(label2_Click);
            this.barcode_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.barcode_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.barcode_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.barcode_input.Location = new System.Drawing.Point(594, 69);
            this.barcode_input.Name = "barcode_input";
            this.barcode_input.Size = new System.Drawing.Size(194, 23);
            this.barcode_input.TabIndex = 2;
            this.barcode_input.KeyDown += new System.Windows.Forms.KeyEventHandler(barcode_input_KeyDown);
            this.barcode_input.Leave += new System.EventHandler(barcode_input_Leave);
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(458, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "[F7] Codigo de Barras:";
            this.label5.Click += new System.EventHandler(label5_Click);
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label7.ForeColor = System.Drawing.Color.Tomato;
            this.label7.Location = new System.Drawing.Point(456, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 20);
            this.label7.TabIndex = 38;
            this.label7.Text = "Detalle de Venta:";
            this.DetailDataGrid.AllowUserToAddRows = false;
            this.DetailDataGrid.AllowUserToDeleteRows = false;
            this.DetailDataGrid.AllowUserToResizeColumns = false;
            this.DetailDataGrid.AllowUserToResizeRows = false;
            this.DetailDataGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.DetailDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.DetailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DetailDataGrid.Columns.AddRange(this.Editar, this.less);
            this.DetailDataGrid.Location = new System.Drawing.Point(460, 249);
            this.DetailDataGrid.MultiSelect = false;
            this.DetailDataGrid.Name = "DetailDataGrid";
            this.DetailDataGrid.ReadOnly = true;
            this.DetailDataGrid.RowHeadersVisible = false;
            this.DetailDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DetailDataGrid.Size = new System.Drawing.Size(533, 321);
            this.DetailDataGrid.TabIndex = 30;
            this.DetailDataGrid.TabStop = false;
            this.DetailDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DetailDataGrid_CellClick);
            this.DetailDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DetailDataGrid_CellDoubleClick);
            this.DetailDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(DetailDataGrid_CellFormatting);
            this.DetailDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(DetailDataGrid_KeyDown);
            this.Editar.HeaderText = "";
            this.Editar.Image = (System.Drawing.Image)resources.GetObject("Editar.Image");
            this.Editar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Width = 20;
            this.less.HeaderText = "";
            this.less.Image = (System.Drawing.Image)resources.GetObject("less.Image");
            this.less.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.less.Name = "less";
            this.less.ReadOnly = true;
            this.less.Width = 20;
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(461, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(532, 2);
            this.label8.TabIndex = 40;
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label9.ForeColor = System.Drawing.Color.Tomato;
            this.label9.Location = new System.Drawing.Point(800, 640);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 20);
            this.label9.TabIndex = 41;
            this.label9.Text = "Total:";
            this.totalSale_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.totalSale_input.BackColor = System.Drawing.Color.Blue;
            this.totalSale_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.totalSale_input.ForeColor = System.Drawing.Color.White;
            this.totalSale_input.Location = new System.Drawing.Point(856, 627);
            this.totalSale_input.Name = "totalSale_input";
            this.totalSale_input.ReadOnly = true;
            this.totalSale_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.totalSale_input.Size = new System.Drawing.Size(140, 38);
            this.totalSale_input.TabIndex = 42;
            this.totalSale_input.TabStop = false;
            this.totalSale_input.Text = "$0";
            this.processButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.processButton.BackColor = System.Drawing.Color.Gainsboro;
            this.processButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("processButton.BackgroundImage");
            this.processButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.processButton.Enabled = false;
            this.processButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.processButton.ForeColor = System.Drawing.Color.White;
            this.processButton.Location = new System.Drawing.Point(939, 671);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(57, 51);
            this.processButton.TabIndex = 12;
            this.processButton.UseVisualStyleBackColor = false;
            this.processButton.Click += new System.EventHandler(processButton_Click);
            this.pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(98, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 31);
            this.label6.TabIndex = 45;
            this.label6.Text = "Ventas";
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(460, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(532, 2);
            this.label10.TabIndex = 46;
            this.change_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.change_input.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
            this.change_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.change_input.ForeColor = System.Drawing.Color.White;
            this.change_input.Location = new System.Drawing.Point(856, 576);
            this.change_input.Name = "change_input";
            this.change_input.ReadOnly = true;
            this.change_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.change_input.Size = new System.Drawing.Size(140, 38);
            this.change_input.TabIndex = 48;
            this.change_input.TabStop = false;
            this.change_input.Text = "$0";
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label11.ForeColor = System.Drawing.Color.Tomato;
            this.label11.Location = new System.Drawing.Point(788, 589);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 20);
            this.label11.TabIndex = 47;
            this.label11.Text = "Vuelto:";
            this.payment_cash_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.payment_cash_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.payment_cash_input.Location = new System.Drawing.Point(622, 576);
            this.payment_cash_input.Name = "payment_cash_input";
            this.payment_cash_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.payment_cash_input.Size = new System.Drawing.Size(140, 38);
            this.payment_cash_input.TabIndex = 9;
            this.payment_cash_input.Text = "$0";
            this.payment_cash_input.TextChanged += new System.EventHandler(payment_cash_input_TextChanged);
            this.payment_cash_input.Enter += new System.EventHandler(payment_cash_input_Enter);
            this.payment_cash_input.KeyDown += new System.Windows.Forms.KeyEventHandler(payment_cash_input_KeyDown);
            this.payment_cash_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(payment_cash_input_KeyPress);
            this.payment_cash_input.Leave += new System.EventHandler(payment_cash_input_Leave);
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label12.ForeColor = System.Drawing.Color.Tomato;
            this.label12.Location = new System.Drawing.Point(455, 589);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(161, 20);
            this.label12.TabIndex = 51;
            this.label12.Text = "[F8] Pago Efectivo:";
            this.label12.Click += new System.EventHandler(label12_Click);
            this.payment_other_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.payment_other_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.payment_other_input.Location = new System.Drawing.Point(622, 627);
            this.payment_other_input.Name = "payment_other_input";
            this.payment_other_input.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.payment_other_input.Size = new System.Drawing.Size(140, 38);
            this.payment_other_input.TabIndex = 10;
            this.payment_other_input.Text = "$0";
            this.payment_other_input.TextChanged += new System.EventHandler(payment_other_input_TextChanged);
            this.payment_other_input.Enter += new System.EventHandler(payment_other_input_Enter);
            this.payment_other_input.KeyDown += new System.Windows.Forms.KeyEventHandler(payment_other_input_KeyDown);
            this.payment_other_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(payment_other_input_KeyPress);
            this.payment_other_input.Leave += new System.EventHandler(payment_other_input_Leave);
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label13.ForeColor = System.Drawing.Color.Tomato;
            this.label13.Location = new System.Drawing.Point(464, 640);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(152, 20);
            this.label13.TabIndex = 49;
            this.label13.Text = "[F9] Pago Tarjeta:";
            this.label13.Click += new System.EventHandler(label13_Click);
            this.notes_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.notes_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.notes_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.notes_input.Location = new System.Drawing.Point(535, 674);
            this.notes_input.Multiline = true;
            this.notes_input.Name = "notes_input";
            this.notes_input.Size = new System.Drawing.Size(174, 45);
            this.notes_input.TabIndex = 11;
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label14.ForeColor = System.Drawing.Color.Tomato;
            this.label14.Location = new System.Drawing.Point(458, 679);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 54;
            this.label14.Text = "[F10] Nota:";
            this.label14.Click += new System.EventHandler(label14_Click);
            this.Addproductsale_button.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.Addproductsale_button.BackColor = System.Drawing.SystemColors.Window;
            this.Addproductsale_button.BackgroundImage = (System.Drawing.Image)resources.GetObject("Addproductsale_button.BackgroundImage");
            this.Addproductsale_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Addproductsale_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold);
            this.Addproductsale_button.Location = new System.Drawing.Point(805, 141);
            this.Addproductsale_button.Name = "Addproductsale_button";
            this.Addproductsale_button.Size = new System.Drawing.Size(62, 51);
            this.Addproductsale_button.TabIndex = 7;
            this.Addproductsale_button.UseVisualStyleBackColor = false;
            this.Addproductsale_button.Click += new System.EventHandler(Addproductsale_button_Click);
            this.CleanButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.CleanButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("CleanButton.BackgroundImage");
            this.CleanButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CleanButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CleanButton.Location = new System.Drawing.Point(939, 141);
            this.CleanButton.Name = "CleanButton";
            this.CleanButton.Size = new System.Drawing.Size(53, 51);
            this.CleanButton.TabIndex = 8;
            this.CleanButton.UseVisualStyleBackColor = true;
            this.CleanButton.Click += new System.EventHandler(CleanButton_Click);
            this.kg_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.kg_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.kg_input.Location = new System.Drawing.Point(594, 198);
            this.kg_input.Name = "kg_input";
            this.kg_input.Size = new System.Drawing.Size(194, 23);
            this.kg_input.TabIndex = 6;
            this.kg_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(kg_input_KeyPress);
            this.kg_input.KeyUp += new System.Windows.Forms.KeyEventHandler(kg_input_KeyUp);
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label15.ForeColor = System.Drawing.Color.Tomato;
            this.label15.Location = new System.Drawing.Point(458, 201);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 56;
            this.label15.Text = "Kilos:";
            this.label15.Click += new System.EventHandler(label15_Click);
            this.processButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.processButton2.BackColor = System.Drawing.Color.Gainsboro;
            this.processButton2.BackgroundImage = (System.Drawing.Image)resources.GetObject("processButton2.BackgroundImage");
            this.processButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.processButton2.Enabled = false;
            this.processButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.processButton2.ForeColor = System.Drawing.Color.White;
            this.processButton2.Location = new System.Drawing.Point(787, 674);
            this.processButton2.Name = "processButton2";
            this.processButton2.Size = new System.Drawing.Size(58, 48);
            this.processButton2.TabIndex = 57;
            this.processButton2.UseVisualStyleBackColor = false;
            this.processButton2.Click += new System.EventHandler(processButton2_Click);
            this.decrease_checkbox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.decrease_checkbox.AutoSize = true;
            this.decrease_checkbox.Location = new System.Drawing.Point(805, 201);
            this.decrease_checkbox.Name = "decrease_checkbox";
            this.decrease_checkbox.Size = new System.Drawing.Size(85, 17);
            this.decrease_checkbox.TabIndex = 58;
            this.decrease_checkbox.Text = "¿Es Merma?";
            this.decrease_checkbox.UseVisualStyleBackColor = true;
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label16.ForeColor = System.Drawing.Color.Tomato;
            this.label16.Location = new System.Drawing.Point(715, 680);
            this.label16.MaximumSize = new System.Drawing.Size(80, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 39);
            this.label16.TabIndex = 59;
            this.label16.Text = "[F11] Procesar Venta S/B";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Click += new System.EventHandler(label16_Click);
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label17.ForeColor = System.Drawing.Color.Tomato;
            this.label17.Location = new System.Drawing.Point(867, 680);
            this.label17.MaximumSize = new System.Drawing.Size(80, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 39);
            this.label17.TabIndex = 60;
            this.label17.Text = "[F12] Procesar Venta C/B";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Click += new System.EventHandler(label17_Click);
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label18.ForeColor = System.Drawing.Color.Tomato;
            this.label18.Location = new System.Drawing.Point(853, 49);
            this.label18.MaximumSize = new System.Drawing.Size(80, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 26);
            this.label18.TabIndex = 62;
            this.label18.Text = "Re-Imprimir ultima venta";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label18.Click += new System.EventHandler(label18_Click);
            this.reprint_button.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.reprint_button.BackColor = System.Drawing.Color.Lime;
            this.reprint_button.BackgroundImage = (System.Drawing.Image)resources.GetObject("reprint_button.BackgroundImage");
            this.reprint_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.reprint_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.reprint_button.ForeColor = System.Drawing.Color.White;
            this.reprint_button.Location = new System.Drawing.Point(935, 47);
            this.reprint_button.Name = "reprint_button";
            this.reprint_button.Size = new System.Drawing.Size(57, 51);
            this.reprint_button.TabIndex = 61;
            this.reprint_button.TabStop = false;
            this.reprint_button.UseVisualStyleBackColor = false;
            this.reprint_button.Click += new System.EventHandler(reprint_button_Click);
            this.vendor_label.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.vendor_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.vendor_label.ForeColor = System.Drawing.Color.OrangeRed;
            this.vendor_label.Location = new System.Drawing.Point(347, 3);
            this.vendor_label.Name = "vendor_label";
            this.vendor_label.Size = new System.Drawing.Size(646, 20);
            this.vendor_label.TabIndex = 63;
            this.vendor_label.Text = "Bienvenido, ";
            this.vendor_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(timer_Tick);
            this.timer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.timer_label.Location = new System.Drawing.Point(341, 23);
            this.timer_label.Name = "timer_label";
            this.timer_label.Size = new System.Drawing.Size(321, 31);
            this.timer_label.TabIndex = 64;
            this.timer_label.Text = "DD-MM-YYYY 00:00:00";
            this.timer_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox2.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox2.BackgroundImage");
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(792, 69);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 23);
            this.pictureBox2.TabIndex = 65;
            this.pictureBox2.TabStop = false;
            this.ticket_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.ticket_input.Location = new System.Drawing.Point(856, 78);
            this.ticket_input.Name = "ticket_input";
            this.ticket_input.Size = new System.Drawing.Size(73, 20);
            this.ticket_input.TabIndex = 66;
            this.ticket_input.TabStop = false;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            base.ClientSize = new System.Drawing.Size(1008, 729);
            base.Controls.Add(this.ticket_input);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.timer_label);
            base.Controls.Add(this.vendor_label);
            base.Controls.Add(this.label18);
            base.Controls.Add(this.reprint_button);
            base.Controls.Add(this.label17);
            base.Controls.Add(this.label16);
            base.Controls.Add(this.decrease_checkbox);
            base.Controls.Add(this.processButton2);
            base.Controls.Add(this.kg_input);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.CleanButton);
            base.Controls.Add(this.Addproductsale_button);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.notes_input);
            base.Controls.Add(this.payment_cash_input);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.payment_other_input);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.change_input);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.processButton);
            base.Controls.Add(this.totalSale_input);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.DetailDataGrid);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.barcode_input);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.price_input);
            base.Controls.Add(this.amount_input);
            base.Controls.Add(this.name_input);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.filterTextBox);
            base.Controls.Add(this.InventoryDataGrid);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "SalesForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Ventas";
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SalesForm_FormClosing);
            base.Shown += new System.EventHandler(SalesForm_Shown);
            base.MouseClick += new System.Windows.Forms.MouseEventHandler(SalesForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)this.InventoryDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.DetailDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}