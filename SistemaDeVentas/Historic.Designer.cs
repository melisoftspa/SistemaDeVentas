namespace SistemaDeVentas
{
    partial class Historic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private string nroticket = string.Empty;

        private DateTimePicker startTimePicker;

        private Label label2;

        private DateTimePicker endTimePicker;

        private Label label1;

        private Label detail_label;

        private Label labelGrid1;

        private DataGridView detailDataGrid;

        private DataGridView MainDataGrid;

        private Button MostrarButton;

        private TextBox total_input;

        private Label total_label;

        private GroupBox groupBox1;

        private RadioButton producto_radio;

        private RadioButton ingreso_radio;

        private RadioButton ventas_radio;

        private PictureBox pictureBox1;

        private TextBox totalInvoice_input;

        private Label totalInvoice_label;

        private TextBox totalTax_input;

        private Label totalTax_label;

        private TextBox totalAfecta_input;

        private Label totalAfecta_label;

        private TextBox totalExenta_input;

        private Label totalExenta_label;

        private ComboBox category_combobox;

        private Label category_label;

        private TextBox totalCategory_input;

        private Label totalCategory_label;

        private TextBox totalPercent_input;

        private Label totalpercent_label1;

        private TextBox percent_input;

        private Label percent_label;

        private Label totalPaymentCash_label;

        private TextBox totalPaymentCash_input;

        private Label totalPaymentOtherCash_label;

        private TextBox totalPaymentOtherCash_input;

        private RadioButton decrease_radio;

        private Button cancelsale_button;

        private Button print_button;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Historic));
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.MainDataGrid = new System.Windows.Forms.DataGridView();
            this.detailDataGrid = new System.Windows.Forms.DataGridView();
            this.labelGrid1 = new System.Windows.Forms.Label();
            this.detail_label = new System.Windows.Forms.Label();
            this.MostrarButton = new System.Windows.Forms.Button();
            this.total_label = new System.Windows.Forms.Label();
            this.total_input = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.decrease_radio = new System.Windows.Forms.RadioButton();
            this.producto_radio = new System.Windows.Forms.RadioButton();
            this.ingreso_radio = new System.Windows.Forms.RadioButton();
            this.ventas_radio = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.totalInvoice_input = new System.Windows.Forms.TextBox();
            this.totalInvoice_label = new System.Windows.Forms.Label();
            this.totalTax_input = new System.Windows.Forms.TextBox();
            this.totalTax_label = new System.Windows.Forms.Label();
            this.totalAfecta_input = new System.Windows.Forms.TextBox();
            this.totalAfecta_label = new System.Windows.Forms.Label();
            this.totalExenta_input = new System.Windows.Forms.TextBox();
            this.totalExenta_label = new System.Windows.Forms.Label();
            this.category_combobox = new System.Windows.Forms.ComboBox();
            this.category_label = new System.Windows.Forms.Label();
            this.totalCategory_input = new System.Windows.Forms.TextBox();
            this.totalCategory_label = new System.Windows.Forms.Label();
            this.totalPercent_input = new System.Windows.Forms.TextBox();
            this.totalpercent_label1 = new System.Windows.Forms.Label();
            this.percent_input = new System.Windows.Forms.TextBox();
            this.percent_label = new System.Windows.Forms.Label();
            this.totalPaymentCash_label = new System.Windows.Forms.Label();
            this.totalPaymentCash_input = new System.Windows.Forms.TextBox();
            this.totalPaymentOtherCash_label = new System.Windows.Forms.Label();
            this.totalPaymentOtherCash_input = new System.Windows.Forms.TextBox();
            this.cancelsale_button = new System.Windows.Forms.Button();
            this.print_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblResumenPrint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // startTimePicker
            // 
            this.startTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startTimePicker.Location = new System.Drawing.Point(780, 46);
            this.startTimePicker.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(260, 39);
            this.startTimePicker.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(667, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Desde: ";
            // 
            // endTimePicker
            // 
            this.endTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endTimePicker.Location = new System.Drawing.Point(780, 99);
            this.endTimePicker.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(260, 39);
            this.endTimePicker.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(674, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta: ";
            // 
            // MainDataGrid
            // 
            this.MainDataGrid.AllowUserToAddRows = false;
            this.MainDataGrid.AllowUserToDeleteRows = false;
            this.MainDataGrid.AllowUserToResizeColumns = false;
            this.MainDataGrid.AllowUserToResizeRows = false;
            this.MainDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MainDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.MainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MainDataGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.MainDataGrid.Location = new System.Drawing.Point(24, 314);
            this.MainDataGrid.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MainDataGrid.Name = "MainDataGrid";
            this.MainDataGrid.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MainDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.MainDataGrid.RowHeadersVisible = false;
            this.MainDataGrid.RowHeadersWidth = 72;
            this.MainDataGrid.Size = new System.Drawing.Size(1016, 675);
            this.MainDataGrid.TabIndex = 4;
            this.MainDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SaleDataGrid_CellClick);
            // 
            // detailDataGrid
            // 
            this.detailDataGrid.AllowUserToAddRows = false;
            this.detailDataGrid.AllowUserToDeleteRows = false;
            this.detailDataGrid.AllowUserToResizeColumns = false;
            this.detailDataGrid.AllowUserToResizeRows = false;
            this.detailDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.detailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.detailDataGrid.DefaultCellStyle = dataGridViewCellStyle11;
            this.detailDataGrid.Location = new System.Drawing.Point(1082, 314);
            this.detailDataGrid.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.detailDataGrid.Name = "detailDataGrid";
            this.detailDataGrid.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.detailDataGrid.RowHeadersVisible = false;
            this.detailDataGrid.RowHeadersWidth = 72;
            this.detailDataGrid.Size = new System.Drawing.Size(910, 284);
            this.detailDataGrid.TabIndex = 5;
            // 
            // labelGrid1
            // 
            this.labelGrid1.AutoSize = true;
            this.labelGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelGrid1.Location = new System.Drawing.Point(24, 265);
            this.labelGrid1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelGrid1.Name = "labelGrid1";
            this.labelGrid1.Size = new System.Drawing.Size(93, 30);
            this.labelGrid1.TabIndex = 6;
            this.labelGrid1.Text = "Venta: ";
            // 
            // detail_label
            // 
            this.detail_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.detail_label.AutoSize = true;
            this.detail_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.detail_label.Location = new System.Drawing.Point(1076, 265);
            this.detail_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.detail_label.Name = "detail_label";
            this.detail_label.Size = new System.Drawing.Size(107, 30);
            this.detail_label.TabIndex = 7;
            this.detail_label.Text = "Detalle: ";
            // 
            // MostrarButton
            // 
            this.MostrarButton.BackColor = System.Drawing.SystemColors.Window;
            this.MostrarButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MostrarButton.BackgroundImage")));
            this.MostrarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MostrarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MostrarButton.Location = new System.Drawing.Point(925, 192);
            this.MostrarButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MostrarButton.Name = "MostrarButton";
            this.MostrarButton.Size = new System.Drawing.Size(84, 72);
            this.MostrarButton.TabIndex = 8;
            this.MostrarButton.UseVisualStyleBackColor = false;
            this.MostrarButton.Click += new System.EventHandler(this.MostrarButton_Click);
            // 
            // total_label
            // 
            this.total_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.total_label.AutoSize = true;
            this.total_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.total_label.Location = new System.Drawing.Point(1572, 621);
            this.total_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.total_label.Name = "total_label";
            this.total_label.Size = new System.Drawing.Size(170, 30);
            this.total_label.TabIndex = 9;
            this.total_label.Text = "Total Ventas: ";
            // 
            // total_input
            // 
            this.total_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.total_input.Enabled = false;
            this.total_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.total_input.Location = new System.Drawing.Point(1779, 612);
            this.total_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.total_input.Name = "total_input";
            this.total_input.Size = new System.Drawing.Size(210, 44);
            this.total_input.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.decrease_radio);
            this.groupBox1.Controls.Add(this.producto_radio);
            this.groupBox1.Controls.Add(this.ingreso_radio);
            this.groupBox1.Controls.Add(this.ventas_radio);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(202, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox1.Size = new System.Drawing.Size(300, 203);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // decrease_radio
            // 
            this.decrease_radio.AutoSize = true;
            this.decrease_radio.Location = new System.Drawing.Point(12, 162);
            this.decrease_radio.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.decrease_radio.Name = "decrease_radio";
            this.decrease_radio.Size = new System.Drawing.Size(125, 33);
            this.decrease_radio.TabIndex = 3;
            this.decrease_radio.Text = "Mermas";
            this.decrease_radio.UseVisualStyleBackColor = true;
            this.decrease_radio.CheckedChanged += new System.EventHandler(this.decrease_radio_CheckedChanged);
            // 
            // producto_radio
            // 
            this.producto_radio.AutoSize = true;
            this.producto_radio.Location = new System.Drawing.Point(12, 115);
            this.producto_radio.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.producto_radio.Name = "producto_radio";
            this.producto_radio.Size = new System.Drawing.Size(147, 33);
            this.producto_radio.TabIndex = 2;
            this.producto_radio.Text = "Productos";
            this.producto_radio.UseVisualStyleBackColor = true;
            this.producto_radio.CheckedChanged += new System.EventHandler(this.producto_radio_CheckedChanged);
            // 
            // ingreso_radio
            // 
            this.ingreso_radio.AutoSize = true;
            this.ingreso_radio.Location = new System.Drawing.Point(12, 68);
            this.ingreso_radio.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ingreso_radio.Name = "ingreso_radio";
            this.ingreso_radio.Size = new System.Drawing.Size(237, 33);
            this.ingreso_radio.TabIndex = 1;
            this.ingreso_radio.Text = "Ingreso a Almacen";
            this.ingreso_radio.UseVisualStyleBackColor = true;
            this.ingreso_radio.CheckedChanged += new System.EventHandler(this.ingreso_radio_CheckedChanged);
            // 
            // ventas_radio
            // 
            this.ventas_radio.AutoSize = true;
            this.ventas_radio.Checked = true;
            this.ventas_radio.Location = new System.Drawing.Point(12, 21);
            this.ventas_radio.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ventas_radio.Name = "ventas_radio";
            this.ventas_radio.Size = new System.Drawing.Size(111, 33);
            this.ventas_radio.TabIndex = 0;
            this.ventas_radio.TabStop = true;
            this.ventas_radio.Text = "Ventas";
            this.ventas_radio.UseVisualStyleBackColor = true;
            this.ventas_radio.CheckedChanged += new System.EventHandler(this.ventas_radio_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(24, 46);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 185);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // totalInvoice_input
            // 
            this.totalInvoice_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalInvoice_input.Enabled = false;
            this.totalInvoice_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalInvoice_input.Location = new System.Drawing.Point(1337, 612);
            this.totalInvoice_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalInvoice_input.Name = "totalInvoice_input";
            this.totalInvoice_input.Size = new System.Drawing.Size(109, 44);
            this.totalInvoice_input.TabIndex = 15;
            // 
            // totalInvoice_label
            // 
            this.totalInvoice_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalInvoice_label.AutoSize = true;
            this.totalInvoice_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalInvoice_label.Location = new System.Drawing.Point(1079, 621);
            this.totalInvoice_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalInvoice_label.Name = "totalInvoice_label";
            this.totalInvoice_label.Size = new System.Drawing.Size(169, 30);
            this.totalInvoice_label.TabIndex = 14;
            this.totalInvoice_label.Text = "Total Boletas:";
            // 
            // totalTax_input
            // 
            this.totalTax_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalTax_input.Enabled = false;
            this.totalTax_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalTax_input.Location = new System.Drawing.Point(1337, 793);
            this.totalTax_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalTax_input.Name = "totalTax_input";
            this.totalTax_input.Size = new System.Drawing.Size(210, 44);
            this.totalTax_input.TabIndex = 17;
            // 
            // totalTax_label
            // 
            this.totalTax_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalTax_label.AutoSize = true;
            this.totalTax_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalTax_label.Location = new System.Drawing.Point(1079, 802);
            this.totalTax_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalTax_label.Name = "totalTax_label";
            this.totalTax_label.Size = new System.Drawing.Size(131, 30);
            this.totalTax_label.TabIndex = 16;
            this.totalTax_label.Text = "Total IVA: ";
            // 
            // totalAfecta_input
            // 
            this.totalAfecta_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalAfecta_input.Enabled = false;
            this.totalAfecta_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalAfecta_input.Location = new System.Drawing.Point(1337, 670);
            this.totalAfecta_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalAfecta_input.Name = "totalAfecta_input";
            this.totalAfecta_input.Size = new System.Drawing.Size(210, 44);
            this.totalAfecta_input.TabIndex = 19;
            // 
            // totalAfecta_label
            // 
            this.totalAfecta_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalAfecta_label.AutoSize = true;
            this.totalAfecta_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalAfecta_label.Location = new System.Drawing.Point(1079, 679);
            this.totalAfecta_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalAfecta_label.Name = "totalAfecta_label";
            this.totalAfecta_label.Size = new System.Drawing.Size(163, 30);
            this.totalAfecta_label.TabIndex = 18;
            this.totalAfecta_label.Text = "Total Afecta: ";
            // 
            // totalExenta_input
            // 
            this.totalExenta_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalExenta_input.Enabled = false;
            this.totalExenta_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalExenta_input.Location = new System.Drawing.Point(1779, 670);
            this.totalExenta_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalExenta_input.Name = "totalExenta_input";
            this.totalExenta_input.Size = new System.Drawing.Size(210, 44);
            this.totalExenta_input.TabIndex = 21;
            // 
            // totalExenta_label
            // 
            this.totalExenta_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalExenta_label.AutoSize = true;
            this.totalExenta_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalExenta_label.Location = new System.Drawing.Point(1572, 679);
            this.totalExenta_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalExenta_label.Name = "totalExenta_label";
            this.totalExenta_label.Size = new System.Drawing.Size(169, 30);
            this.totalExenta_label.TabIndex = 20;
            this.totalExenta_label.Text = "Total Exenta: ";
            // 
            // category_combobox
            // 
            this.category_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.category_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category_combobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.category_combobox.FormattingEnabled = true;
            this.category_combobox.Location = new System.Drawing.Point(1270, 60);
            this.category_combobox.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.category_combobox.Name = "category_combobox";
            this.category_combobox.Size = new System.Drawing.Size(718, 40);
            this.category_combobox.TabIndex = 22;
            this.category_combobox.SelectedValueChanged += new System.EventHandler(this.category_combobox_SelectedValueChanged);
            // 
            // category_label
            // 
            this.category_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.category_label.Location = new System.Drawing.Point(1062, 46);
            this.category_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.category_label.Name = "category_label";
            this.category_label.Size = new System.Drawing.Size(196, 90);
            this.category_label.TabIndex = 23;
            this.category_label.Text = "Venta por Categoría:";
            this.category_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalCategory_input
            // 
            this.totalCategory_input.Enabled = false;
            this.totalCategory_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalCategory_input.Location = new System.Drawing.Point(1270, 146);
            this.totalCategory_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalCategory_input.Name = "totalCategory_input";
            this.totalCategory_input.Size = new System.Drawing.Size(472, 44);
            this.totalCategory_input.TabIndex = 24;
            // 
            // totalCategory_label
            // 
            this.totalCategory_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCategory_label.AutoSize = true;
            this.totalCategory_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalCategory_label.Location = new System.Drawing.Point(1168, 155);
            this.totalCategory_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalCategory_label.Name = "totalCategory_label";
            this.totalCategory_label.Size = new System.Drawing.Size(77, 30);
            this.totalCategory_label.TabIndex = 25;
            this.totalCategory_label.Text = "Total:";
            // 
            // totalPercent_input
            // 
            this.totalPercent_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalPercent_input.Enabled = false;
            this.totalPercent_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalPercent_input.Location = new System.Drawing.Point(1318, 906);
            this.totalPercent_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalPercent_input.Name = "totalPercent_input";
            this.totalPercent_input.Size = new System.Drawing.Size(276, 44);
            this.totalPercent_input.TabIndex = 26;
            // 
            // totalpercent_label1
            // 
            this.totalpercent_label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalpercent_label1.AutoSize = true;
            this.totalpercent_label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalpercent_label1.Location = new System.Drawing.Point(1082, 858);
            this.totalpercent_label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalpercent_label1.Name = "totalpercent_label1";
            this.totalpercent_label1.Size = new System.Drawing.Size(366, 30);
            this.totalpercent_label1.TabIndex = 27;
            this.totalpercent_label1.Text = "Total Ganancia Sobre Efectivo:";
            // 
            // percent_input
            // 
            this.percent_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.percent_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.percent_input.Location = new System.Drawing.Point(1236, 906);
            this.percent_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.percent_input.MaxLength = 3;
            this.percent_input.Name = "percent_input";
            this.percent_input.Size = new System.Drawing.Size(70, 44);
            this.percent_input.TabIndex = 28;
            this.percent_input.Text = "30";
            this.percent_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.percent_input_KeyDown);
            this.percent_input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.percent_input_KeyUp);
            // 
            // percent_label
            // 
            this.percent_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.percent_label.AutoSize = true;
            this.percent_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.percent_label.Location = new System.Drawing.Point(1082, 915);
            this.percent_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.percent_label.Name = "percent_label";
            this.percent_label.Size = new System.Drawing.Size(142, 30);
            this.percent_label.TabIndex = 29;
            this.percent_label.Text = "Porcentaje:";
            // 
            // totalPaymentCash_label
            // 
            this.totalPaymentCash_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalPaymentCash_label.AutoSize = true;
            this.totalPaymentCash_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalPaymentCash_label.Location = new System.Drawing.Point(1079, 744);
            this.totalPaymentCash_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalPaymentCash_label.Name = "totalPaymentCash_label";
            this.totalPaymentCash_label.Size = new System.Drawing.Size(248, 30);
            this.totalPaymentCash_label.TabIndex = 30;
            this.totalPaymentCash_label.Text = "Total Pago Efectivo: ";
            // 
            // totalPaymentCash_input
            // 
            this.totalPaymentCash_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalPaymentCash_input.Enabled = false;
            this.totalPaymentCash_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalPaymentCash_input.Location = new System.Drawing.Point(1337, 735);
            this.totalPaymentCash_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalPaymentCash_input.Name = "totalPaymentCash_input";
            this.totalPaymentCash_input.Size = new System.Drawing.Size(210, 44);
            this.totalPaymentCash_input.TabIndex = 31;
            // 
            // totalPaymentOtherCash_label
            // 
            this.totalPaymentOtherCash_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalPaymentOtherCash_label.AutoSize = true;
            this.totalPaymentOtherCash_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalPaymentOtherCash_label.Location = new System.Drawing.Point(1572, 744);
            this.totalPaymentOtherCash_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalPaymentOtherCash_label.Name = "totalPaymentOtherCash_label";
            this.totalPaymentOtherCash_label.Size = new System.Drawing.Size(220, 30);
            this.totalPaymentOtherCash_label.TabIndex = 32;
            this.totalPaymentOtherCash_label.Text = "Total Otros Pago: ";
            // 
            // totalPaymentOtherCash_input
            // 
            this.totalPaymentOtherCash_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalPaymentOtherCash_input.Enabled = false;
            this.totalPaymentOtherCash_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalPaymentOtherCash_input.Location = new System.Drawing.Point(1779, 735);
            this.totalPaymentOtherCash_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalPaymentOtherCash_input.Name = "totalPaymentOtherCash_input";
            this.totalPaymentOtherCash_input.Size = new System.Drawing.Size(210, 44);
            this.totalPaymentOtherCash_input.TabIndex = 33;
            // 
            // cancelsale_button
            // 
            this.cancelsale_button.BackColor = System.Drawing.SystemColors.Window;
            this.cancelsale_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelsale_button.BackgroundImage")));
            this.cancelsale_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cancelsale_button.Enabled = false;
            this.cancelsale_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cancelsale_button.ForeColor = System.Drawing.Color.Black;
            this.cancelsale_button.Location = new System.Drawing.Point(740, 192);
            this.cancelsale_button.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.cancelsale_button.Name = "cancelsale_button";
            this.cancelsale_button.Size = new System.Drawing.Size(82, 72);
            this.cancelsale_button.TabIndex = 34;
            this.cancelsale_button.UseVisualStyleBackColor = false;
            this.cancelsale_button.Click += new System.EventHandler(this.cancelsale_button_Click);
            // 
            // print_button
            // 
            this.print_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.print_button.BackColor = System.Drawing.SystemColors.Window;
            this.print_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("print_button.BackgroundImage")));
            this.print_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.print_button.Enabled = false;
            this.print_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.print_button.Location = new System.Drawing.Point(1740, 844);
            this.print_button.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.print_button.Name = "print_button";
            this.print_button.Size = new System.Drawing.Size(142, 145);
            this.print_button.TabIndex = 35;
            this.print_button.UseVisualStyleBackColor = false;
            this.print_button.Click += new System.EventHandler(this.print_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(695, 152);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 30);
            this.label3.TabIndex = 36;
            this.label3.Text = "Anular Boleta";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(902, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 30);
            this.label4.TabIndex = 37;
            this.label4.Text = "Búsqueda";
            // 
            // lblResumenPrint
            // 
            this.lblResumenPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResumenPrint.AutoSize = true;
            this.lblResumenPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResumenPrint.Location = new System.Drawing.Point(1643, 807);
            this.lblResumenPrint.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblResumenPrint.Name = "lblResumenPrint";
            this.lblResumenPrint.Size = new System.Drawing.Size(345, 30);
            this.lblResumenPrint.TabIndex = 38;
            this.lblResumenPrint.Text = "Imprimir Resumen de Ventas";
            // 
            // Historic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(2016, 1005);
            this.Controls.Add(this.lblResumenPrint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.print_button);
            this.Controls.Add(this.cancelsale_button);
            this.Controls.Add(this.totalPaymentOtherCash_input);
            this.Controls.Add(this.totalPaymentOtherCash_label);
            this.Controls.Add(this.totalPaymentCash_input);
            this.Controls.Add(this.totalPaymentCash_label);
            this.Controls.Add(this.percent_label);
            this.Controls.Add(this.percent_input);
            this.Controls.Add(this.totalpercent_label1);
            this.Controls.Add(this.totalPercent_input);
            this.Controls.Add(this.totalCategory_label);
            this.Controls.Add(this.totalCategory_input);
            this.Controls.Add(this.category_label);
            this.Controls.Add(this.category_combobox);
            this.Controls.Add(this.totalExenta_input);
            this.Controls.Add(this.totalExenta_label);
            this.Controls.Add(this.totalAfecta_input);
            this.Controls.Add(this.totalAfecta_label);
            this.Controls.Add(this.totalTax_input);
            this.Controls.Add(this.totalTax_label);
            this.Controls.Add(this.totalInvoice_input);
            this.Controls.Add(this.totalInvoice_label);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.total_input);
            this.Controls.Add(this.total_label);
            this.Controls.Add(this.startTimePicker);
            this.Controls.Add(this.MostrarButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.detail_label);
            this.Controls.Add(this.endTimePicker);
            this.Controls.Add(this.labelGrid1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.detailDataGrid);
            this.Controls.Add(this.MainDataGrid);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "Historic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Historic_FormClosing);
            this.Load += new System.EventHandler(this.Historic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailDataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label3;
        private Label label4;
        private Label lblResumenPrint;
    }
}