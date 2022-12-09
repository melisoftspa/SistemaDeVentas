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
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "InventoryForm";
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.InventoryForm));
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
            ((System.ComponentModel.ISupportInitialize)this.InventoryDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.DetailsPackDatagrid).BeginInit();
            base.SuspendLayout();
            this.InventoryDataGrid.AllowUserToAddRows = false;
            this.InventoryDataGrid.AllowUserToDeleteRows = false;
            this.InventoryDataGrid.AllowUserToResizeColumns = false;
            this.InventoryDataGrid.AllowUserToResizeRows = false;
            this.InventoryDataGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.InventoryDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InventoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
            this.InventoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InventoryDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.InventoryDataGrid.Location = new System.Drawing.Point(12, 98);
            this.InventoryDataGrid.Name = "InventoryDataGrid";
            this.InventoryDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InventoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.InventoryDataGrid.RowHeadersVisible = false;
            this.InventoryDataGrid.Size = new System.Drawing.Size(516, 615);
            this.InventoryDataGrid.TabIndex = 2;
            this.InventoryDataGrid.TabStop = false;
            this.InventoryDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(InventoryDataGrid_CellClick);
            this.InventoryDataGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(InventoryDataGrid_CellEnter);
            this.InventoryDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(InventoryDataGrid_KeyDown);
            this.filterTextBox.Location = new System.Drawing.Point(125, 72);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(403, 20);
            this.filterTextBox.TabIndex = 1;
            this.filterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(filterTextBox_KeyDown);
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(125, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar Productos :";
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre del Producto:";
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(6, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cantidad  Actual:";
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(6, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Precio de Compra (Neto):";
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(6, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Stock Minimo:";
            this.name_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.name_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.name_input.Location = new System.Drawing.Point(163, 6);
            this.name_input.Name = "name_input";
            this.name_input.Size = new System.Drawing.Size(285, 20);
            this.name_input.TabIndex = 3;
            this.amount_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.amount_input.Location = new System.Drawing.Point(163, 32);
            this.amount_input.Name = "amount_input";
            this.amount_input.Size = new System.Drawing.Size(134, 20);
            this.amount_input.TabIndex = 4;
            this.amount_input.Text = "0";
            this.price_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.price_input.Location = new System.Drawing.Point(163, 58);
            this.price_input.Name = "price_input";
            this.price_input.Size = new System.Drawing.Size(134, 20);
            this.price_input.TabIndex = 5;
            this.price_input.Text = "0";
            this.minimum_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.minimum_input.Location = new System.Drawing.Point(163, 110);
            this.minimum_input.Name = "minimum_input";
            this.minimum_input.Size = new System.Drawing.Size(134, 20);
            this.minimum_input.TabIndex = 7;
            this.minimum_input.Text = "0";
            this.InsertButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.InsertButton.BackColor = System.Drawing.Color.ForestGreen;
            this.InsertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.InsertButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.InsertButton.Location = new System.Drawing.Point(538, 655);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(129, 58);
            this.InsertButton.TabIndex = 13;
            this.InsertButton.Text = "Crear Nuevo Producto";
            this.InsertButton.UseVisualStyleBackColor = false;
            this.InsertButton.Click += new System.EventHandler(InsertButton_Click);
            this.SaveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.SaveButton.BackColor = System.Drawing.Color.SteelBlue;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.SaveButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.SaveButton.Location = new System.Drawing.Point(701, 655);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(137, 58);
            this.SaveButton.TabIndex = 14;
            this.SaveButton.Text = "Guardar Cambios";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Visible = false;
            this.SaveButton.Click += new System.EventHandler(SaveButton_Click);
            this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.DeleteButton.BackColor = System.Drawing.Color.DarkRed;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.DeleteButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DeleteButton.Location = new System.Drawing.Point(874, 655);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(122, 58);
            this.DeleteButton.TabIndex = 15;
            this.DeleteButton.Text = "Eliminar Producto";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Visible = false;
            this.DeleteButton.Click += new System.EventHandler(DeleteButton_Click);
            this.CleanButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.CleanButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("CleanButton.BackgroundImage");
            this.CleanButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CleanButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CleanButton.Location = new System.Drawing.Point(347, 58);
            this.CleanButton.Name = "CleanButton";
            this.CleanButton.Size = new System.Drawing.Size(78, 71);
            this.CleanButton.TabIndex = 16;
            this.CleanButton.UseVisualStyleBackColor = true;
            this.CleanButton.Click += new System.EventHandler(cleanButton_Click);
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label7.ForeColor = System.Drawing.Color.Tomato;
            this.label7.Location = new System.Drawing.Point(6, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Codigo de Barras:";
            this.barcode_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.barcode_input.Location = new System.Drawing.Point(163, 136);
            this.barcode_input.Name = "barcode_input";
            this.barcode_input.Size = new System.Drawing.Size(134, 20);
            this.barcode_input.TabIndex = 8;
            this.pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 80);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(122, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(324, 31);
            this.label6.TabIndex = 20;
            this.label6.Text = "Inventario de Productos";
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label8.ForeColor = System.Drawing.Color.Tomato;
            this.label8.Location = new System.Drawing.Point(6, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "¿Excento de IVA?";
            this.isExenta_checkbox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.isExenta_checkbox.AutoSize = true;
            this.isExenta_checkbox.Location = new System.Drawing.Point(163, 243);
            this.isExenta_checkbox.Name = "isExenta_checkbox";
            this.isExenta_checkbox.Size = new System.Drawing.Size(35, 17);
            this.isExenta_checkbox.TabIndex = 11;
            this.isExenta_checkbox.Text = "Si";
            this.isExenta_checkbox.UseVisualStyleBackColor = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label9.ForeColor = System.Drawing.Color.Tomato;
            this.label9.Location = new System.Drawing.Point(6, 219);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Tipo de IVA:";
            this.listIVA_combobox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.listIVA_combobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.listIVA_combobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.listIVA_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listIVA_combobox.FormattingEnabled = true;
            this.listIVA_combobox.Location = new System.Drawing.Point(163, 216);
            this.listIVA_combobox.MaxDropDownItems = 10;
            this.listIVA_combobox.Name = "listIVA_combobox";
            this.listIVA_combobox.Size = new System.Drawing.Size(285, 21);
            this.listIVA_combobox.TabIndex = 9;
            this.listIVA_combobox.SelectedIndexChanged += new System.EventHandler(listIVA_combobox_SelectedIndexChanged);
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label10.ForeColor = System.Drawing.Color.Tomato;
            this.label10.Location = new System.Drawing.Point(6, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Fecha Vencimiento:";
            this.expiration_datetimepicker.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.expiration_datetimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expiration_datetimepicker.Location = new System.Drawing.Point(163, 266);
            this.expiration_datetimepicker.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.expiration_datetimepicker.Name = "expiration_datetimepicker";
            this.expiration_datetimepicker.Size = new System.Drawing.Size(134, 20);
            this.expiration_datetimepicker.TabIndex = 12;
            this.expiration_datetimepicker.Value = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label11.ForeColor = System.Drawing.Color.Tomato;
            this.label11.Location = new System.Drawing.Point(6, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Departamento:";
            this.category_combobox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.category_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category_combobox.Enabled = false;
            this.category_combobox.FormattingEnabled = true;
            this.category_combobox.Location = new System.Drawing.Point(163, 162);
            this.category_combobox.MaxDropDownItems = 9;
            this.category_combobox.Name = "category_combobox";
            this.category_combobox.Size = new System.Drawing.Size(285, 21);
            this.category_combobox.Sorted = true;
            this.category_combobox.TabIndex = 8;
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(534, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(462, 586);
            this.tabControl1.TabIndex = 28;
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(454, 560);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nuevo Producto";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.subcategory_combobox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.subcategory_combobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.subcategory_combobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.subcategory_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subcategory_combobox.FormattingEnabled = true;
            this.subcategory_combobox.Location = new System.Drawing.Point(163, 189);
            this.subcategory_combobox.MaxDropDownItems = 9;
            this.subcategory_combobox.Name = "subcategory_combobox";
            this.subcategory_combobox.Size = new System.Drawing.Size(285, 21);
            this.subcategory_combobox.Sorted = true;
            this.subcategory_combobox.TabIndex = 9;
            this.subcategory_combobox.SelectedValueChanged += new System.EventHandler(subcategory_combobox_SelectedValueChanged);
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label17.ForeColor = System.Drawing.Color.Tomato;
            this.label17.Location = new System.Drawing.Point(6, 192);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "Categoría:";
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label16.ForeColor = System.Drawing.Color.Tomato;
            this.label16.Location = new System.Drawing.Point(6, 87);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(144, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "Precio de Venta (Bruto):";
            this.sales_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.sales_input.Enabled = false;
            this.sales_input.Location = new System.Drawing.Point(163, 84);
            this.sales_input.Name = "sales_input";
            this.sales_input.ReadOnly = true;
            this.sales_input.Size = new System.Drawing.Size(134, 20);
            this.sales_input.TabIndex = 6;
            this.sales_input.Text = "0";
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.packamount_input);
            this.tabPage2.Controls.Add(this.packname_input);
            this.tabPage2.Controls.Add(this.packbarcode_input);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.DetailsPackDatagrid);
            this.tabPage2.Controls.Add(this.pack_checkbox);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(454, 560);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Armar Pack";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label15.ForeColor = System.Drawing.Color.Tomato;
            this.label15.Location = new System.Drawing.Point(14, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "Cantidad";
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label14.ForeColor = System.Drawing.Color.Tomato;
            this.label14.Location = new System.Drawing.Point(14, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Nombre del Producto";
            this.packamount_input.Location = new System.Drawing.Point(148, 96);
            this.packamount_input.Name = "packamount_input";
            this.packamount_input.ReadOnly = true;
            this.packamount_input.Size = new System.Drawing.Size(35, 20);
            this.packamount_input.TabIndex = 33;
            this.packamount_input.KeyDown += new System.Windows.Forms.KeyEventHandler(packamount_input_KeyDown);
            this.packname_input.Location = new System.Drawing.Point(148, 70);
            this.packname_input.Name = "packname_input";
            this.packname_input.ReadOnly = true;
            this.packname_input.Size = new System.Drawing.Size(180, 20);
            this.packname_input.TabIndex = 32;
            this.packname_input.TabStop = false;
            this.packbarcode_input.Location = new System.Drawing.Point(148, 44);
            this.packbarcode_input.Name = "packbarcode_input";
            this.packbarcode_input.ReadOnly = true;
            this.packbarcode_input.Size = new System.Drawing.Size(180, 20);
            this.packbarcode_input.TabIndex = 31;
            this.packbarcode_input.KeyDown += new System.Windows.Forms.KeyEventHandler(packbarcode_input_KeyDown);
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label13.ForeColor = System.Drawing.Color.Tomato;
            this.label13.Location = new System.Drawing.Point(14, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Código de Barra";
            this.DetailsPackDatagrid.AllowUserToAddRows = false;
            this.DetailsPackDatagrid.AllowUserToDeleteRows = false;
            this.DetailsPackDatagrid.AllowUserToResizeColumns = false;
            this.DetailsPackDatagrid.AllowUserToResizeRows = false;
            this.DetailsPackDatagrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.DetailsPackDatagrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DetailsPackDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DetailsPackDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DetailsPackDatagrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.DetailsPackDatagrid.Location = new System.Drawing.Point(17, 122);
            this.DetailsPackDatagrid.Name = "DetailsPackDatagrid";
            this.DetailsPackDatagrid.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DetailsPackDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DetailsPackDatagrid.RowHeadersVisible = false;
            this.DetailsPackDatagrid.Size = new System.Drawing.Size(417, 427);
            this.DetailsPackDatagrid.TabIndex = 29;
            this.DetailsPackDatagrid.TabStop = false;
            this.DetailsPackDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DetailsPackDatagrid_CellClick);
            this.DetailsPackDatagrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DetailsPackDatagrid_CellDoubleClick);
            this.DetailsPackDatagrid.KeyDown += new System.Windows.Forms.KeyEventHandler(DetailsPackDatagrid_KeyDown);
            this.pack_checkbox.AutoSize = true;
            this.pack_checkbox.Location = new System.Drawing.Point(148, 21);
            this.pack_checkbox.Name = "pack_checkbox";
            this.pack_checkbox.Size = new System.Drawing.Size(35, 17);
            this.pack_checkbox.TabIndex = 22;
            this.pack_checkbox.Text = "Si";
            this.pack_checkbox.UseVisualStyleBackColor = true;
            this.pack_checkbox.CheckedChanged += new System.EventHandler(pack_checkbox_CheckedChanged);
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label12.ForeColor = System.Drawing.Color.Tomato;
            this.label12.Location = new System.Drawing.Point(14, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Activar Pack";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            base.ClientSize = new System.Drawing.Size(1008, 729);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.DeleteButton);
            base.Controls.Add(this.SaveButton);
            base.Controls.Add(this.InsertButton);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.filterTextBox);
            base.Controls.Add(this.InventoryDataGrid);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "InventoryForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario de Productos";
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(InventoryForm_FormClosing);
            base.Shown += new System.EventHandler(InventoryForm_Shown);
            ((System.ComponentModel.ISupportInitialize)this.InventoryDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.DetailsPackDatagrid).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}