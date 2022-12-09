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
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "Historic";
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.Historic));
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
            ((System.ComponentModel.ISupportInitialize)this.MainDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.detailDataGrid).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.startTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startTimePicker.Location = new System.Drawing.Point(388, 26);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(132, 26);
            this.startTimePicker.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label1.Location = new System.Drawing.Point(310, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Desde: ";
            this.endTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endTimePicker.Location = new System.Drawing.Point(388, 58);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(132, 26);
            this.endTimePicker.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label2.Location = new System.Drawing.Point(314, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta: ";
            this.MainDataGrid.AllowUserToAddRows = false;
            this.MainDataGrid.AllowUserToDeleteRows = false;
            this.MainDataGrid.AllowUserToResizeColumns = false;
            this.MainDataGrid.AllowUserToResizeRows = false;
            this.MainDataGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.MainDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MainDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
            this.MainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MainDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.MainDataGrid.Location = new System.Drawing.Point(12, 136);
            this.MainDataGrid.Name = "MainDataGrid";
            this.MainDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MainDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.MainDataGrid.RowHeadersVisible = false;
            this.MainDataGrid.Size = new System.Drawing.Size(508, 516);
            this.MainDataGrid.TabIndex = 4;
            this.MainDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(SaleDataGrid_CellClick);
            this.detailDataGrid.AllowUserToAddRows = false;
            this.detailDataGrid.AllowUserToDeleteRows = false;
            this.detailDataGrid.AllowUserToResizeColumns = false;
            this.detailDataGrid.AllowUserToResizeRows = false;
            this.detailDataGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.detailDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.detailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.detailDataGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.detailDataGrid.Location = new System.Drawing.Point(541, 136);
            this.detailDataGrid.Name = "detailDataGrid";
            this.detailDataGrid.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.detailDataGrid.RowHeadersVisible = false;
            this.detailDataGrid.Size = new System.Drawing.Size(455, 516);
            this.detailDataGrid.TabIndex = 5;
            this.labelGrid1.AutoSize = true;
            this.labelGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.labelGrid1.Location = new System.Drawing.Point(12, 115);
            this.labelGrid1.Name = "labelGrid1";
            this.labelGrid1.Size = new System.Drawing.Size(53, 18);
            this.labelGrid1.TabIndex = 6;
            this.labelGrid1.Text = "Venta: ";
            this.detail_label.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.detail_label.AutoSize = true;
            this.detail_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.detail_label.Location = new System.Drawing.Point(538, 115);
            this.detail_label.Name = "detail_label";
            this.detail_label.Size = new System.Drawing.Size(61, 18);
            this.detail_label.TabIndex = 7;
            this.detail_label.Text = "Detalle: ";
            this.MostrarButton.BackColor = System.Drawing.SystemColors.Window;
            this.MostrarButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("MostrarButton.BackgroundImage");
            this.MostrarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MostrarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.MostrarButton.Location = new System.Drawing.Point(478, 90);
            this.MostrarButton.Name = "MostrarButton";
            this.MostrarButton.Size = new System.Drawing.Size(42, 31);
            this.MostrarButton.TabIndex = 8;
            this.MostrarButton.UseVisualStyleBackColor = false;
            this.MostrarButton.Click += new System.EventHandler(MostrarButton_Click);
            this.total_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.total_label.AutoSize = true;
            this.total_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.total_label.Location = new System.Drawing.Point(223, 665);
            this.total_label.Name = "total_label";
            this.total_label.Size = new System.Drawing.Size(98, 18);
            this.total_label.TabIndex = 9;
            this.total_label.Text = "Total Ventas: ";
            this.total_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.total_input.Enabled = false;
            this.total_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.total_input.Location = new System.Drawing.Point(327, 658);
            this.total_input.Name = "total_input";
            this.total_input.Size = new System.Drawing.Size(100, 29);
            this.total_input.TabIndex = 10;
            this.groupBox1.Controls.Add(this.decrease_radio);
            this.groupBox1.Controls.Add(this.producto_radio);
            this.groupBox1.Controls.Add(this.ingreso_radio);
            this.groupBox1.Controls.Add(this.ventas_radio);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.groupBox1.Location = new System.Drawing.Point(101, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 117);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.decrease_radio.AutoSize = true;
            this.decrease_radio.Location = new System.Drawing.Point(6, 87);
            this.decrease_radio.Name = "decrease_radio";
            this.decrease_radio.Size = new System.Drawing.Size(76, 21);
            this.decrease_radio.TabIndex = 3;
            this.decrease_radio.Text = "Mermas";
            this.decrease_radio.UseVisualStyleBackColor = true;
            this.decrease_radio.CheckedChanged += new System.EventHandler(decrease_radio_CheckedChanged);
            this.producto_radio.AutoSize = true;
            this.producto_radio.Location = new System.Drawing.Point(6, 62);
            this.producto_radio.Name = "producto_radio";
            this.producto_radio.Size = new System.Drawing.Size(90, 21);
            this.producto_radio.TabIndex = 2;
            this.producto_radio.Text = "Productos";
            this.producto_radio.UseVisualStyleBackColor = true;
            this.producto_radio.CheckedChanged += new System.EventHandler(producto_radio_CheckedChanged);
            this.ingreso_radio.AutoSize = true;
            this.ingreso_radio.Location = new System.Drawing.Point(6, 36);
            this.ingreso_radio.Name = "ingreso_radio";
            this.ingreso_radio.Size = new System.Drawing.Size(143, 21);
            this.ingreso_radio.TabIndex = 1;
            this.ingreso_radio.Text = "Ingreso a Almacen";
            this.ingreso_radio.UseVisualStyleBackColor = true;
            this.ingreso_radio.CheckedChanged += new System.EventHandler(ingreso_radio_CheckedChanged);
            this.ventas_radio.AutoSize = true;
            this.ventas_radio.Checked = true;
            this.ventas_radio.Location = new System.Drawing.Point(6, 9);
            this.ventas_radio.Name = "ventas_radio";
            this.ventas_radio.Size = new System.Drawing.Size(70, 21);
            this.ventas_radio.TabIndex = 0;
            this.ventas_radio.TabStop = true;
            this.ventas_radio.Text = "Ventas";
            this.ventas_radio.UseVisualStyleBackColor = true;
            this.ventas_radio.CheckedChanged += new System.EventHandler(ventas_radio_CheckedChanged);
            this.pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.totalInvoice_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalInvoice_input.Enabled = false;
            this.totalInvoice_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalInvoice_input.Location = new System.Drawing.Point(117, 658);
            this.totalInvoice_input.Name = "totalInvoice_input";
            this.totalInvoice_input.Size = new System.Drawing.Size(100, 29);
            this.totalInvoice_input.TabIndex = 15;
            this.totalInvoice_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalInvoice_label.AutoSize = true;
            this.totalInvoice_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalInvoice_label.Location = new System.Drawing.Point(12, 665);
            this.totalInvoice_label.Name = "totalInvoice_label";
            this.totalInvoice_label.Size = new System.Drawing.Size(99, 18);
            this.totalInvoice_label.TabIndex = 14;
            this.totalInvoice_label.Text = "Total Boletas:";
            this.totalTax_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalTax_input.Enabled = false;
            this.totalTax_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalTax_input.Location = new System.Drawing.Point(327, 693);
            this.totalTax_input.Name = "totalTax_input";
            this.totalTax_input.Size = new System.Drawing.Size(100, 29);
            this.totalTax_input.TabIndex = 17;
            this.totalTax_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalTax_label.AutoSize = true;
            this.totalTax_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalTax_label.Location = new System.Drawing.Point(247, 700);
            this.totalTax_label.Name = "totalTax_label";
            this.totalTax_label.Size = new System.Drawing.Size(74, 18);
            this.totalTax_label.TabIndex = 16;
            this.totalTax_label.Text = "Total IVA: ";
            this.totalAfecta_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalAfecta_input.Enabled = false;
            this.totalAfecta_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalAfecta_input.Location = new System.Drawing.Point(541, 658);
            this.totalAfecta_input.Name = "totalAfecta_input";
            this.totalAfecta_input.Size = new System.Drawing.Size(100, 29);
            this.totalAfecta_input.TabIndex = 19;
            this.totalAfecta_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalAfecta_label.AutoSize = true;
            this.totalAfecta_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalAfecta_label.Location = new System.Drawing.Point(444, 665);
            this.totalAfecta_label.Name = "totalAfecta_label";
            this.totalAfecta_label.Size = new System.Drawing.Size(94, 18);
            this.totalAfecta_label.TabIndex = 18;
            this.totalAfecta_label.Text = "Total Afecta: ";
            this.totalExenta_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalExenta_input.Enabled = false;
            this.totalExenta_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalExenta_input.Location = new System.Drawing.Point(541, 693);
            this.totalExenta_input.Name = "totalExenta_input";
            this.totalExenta_input.Size = new System.Drawing.Size(100, 29);
            this.totalExenta_input.TabIndex = 21;
            this.totalExenta_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalExenta_label.AutoSize = true;
            this.totalExenta_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalExenta_label.Location = new System.Drawing.Point(444, 700);
            this.totalExenta_label.Name = "totalExenta_label";
            this.totalExenta_label.Size = new System.Drawing.Size(98, 18);
            this.totalExenta_label.TabIndex = 20;
            this.totalExenta_label.Text = "Total Exenta: ";
            this.category_combobox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.category_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category_combobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.category_combobox.FormattingEnabled = true;
            this.category_combobox.Location = new System.Drawing.Point(635, 26);
            this.category_combobox.Name = "category_combobox";
            this.category_combobox.Size = new System.Drawing.Size(361, 28);
            this.category_combobox.TabIndex = 22;
            this.category_combobox.SelectedValueChanged += new System.EventHandler(category_combobox_SelectedValueChanged);
            this.category_label.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.category_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.category_label.Location = new System.Drawing.Point(531, 20);
            this.category_label.Name = "category_label";
            this.category_label.Size = new System.Drawing.Size(98, 39);
            this.category_label.TabIndex = 23;
            this.category_label.Text = "Venta por Categoría:";
            this.category_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totalCategory_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.totalCategory_input.Enabled = false;
            this.totalCategory_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalCategory_input.Location = new System.Drawing.Point(635, 60);
            this.totalCategory_input.Name = "totalCategory_input";
            this.totalCategory_input.Size = new System.Drawing.Size(142, 29);
            this.totalCategory_input.TabIndex = 24;
            this.totalCategory_label.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.totalCategory_label.AutoSize = true;
            this.totalCategory_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalCategory_label.Location = new System.Drawing.Point(584, 67);
            this.totalCategory_label.Name = "totalCategory_label";
            this.totalCategory_label.Size = new System.Drawing.Size(45, 18);
            this.totalCategory_label.TabIndex = 25;
            this.totalCategory_label.Text = "Total:";
            this.totalPercent_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.totalPercent_input.Enabled = false;
            this.totalPercent_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalPercent_input.Location = new System.Drawing.Point(856, 92);
            this.totalPercent_input.Name = "totalPercent_input";
            this.totalPercent_input.Size = new System.Drawing.Size(140, 29);
            this.totalPercent_input.TabIndex = 26;
            this.totalpercent_label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.totalpercent_label1.AutoSize = true;
            this.totalpercent_label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalpercent_label1.Location = new System.Drawing.Point(783, 71);
            this.totalpercent_label1.Name = "totalpercent_label1";
            this.totalpercent_label1.Size = new System.Drawing.Size(213, 18);
            this.totalpercent_label1.TabIndex = 27;
            this.totalpercent_label1.Text = "Total Ganancia Sobre Efectivo:";
            this.percent_input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.percent_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.percent_input.Location = new System.Drawing.Point(813, 93);
            this.percent_input.MaxLength = 3;
            this.percent_input.Name = "percent_input";
            this.percent_input.Size = new System.Drawing.Size(37, 29);
            this.percent_input.TabIndex = 28;
            this.percent_input.Text = "30";
            this.percent_input.KeyDown += new System.Windows.Forms.KeyEventHandler(percent_input_KeyDown);
            this.percent_input.KeyUp += new System.Windows.Forms.KeyEventHandler(percent_input_KeyUp);
            this.percent_label.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.percent_label.AutoSize = true;
            this.percent_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.percent_label.Location = new System.Drawing.Point(728, 96);
            this.percent_label.Name = "percent_label";
            this.percent_label.Size = new System.Drawing.Size(79, 18);
            this.percent_label.TabIndex = 29;
            this.percent_label.Text = "Porcentaje";
            this.totalPaymentCash_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalPaymentCash_label.AutoSize = true;
            this.totalPaymentCash_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalPaymentCash_label.Location = new System.Drawing.Point(662, 665);
            this.totalPaymentCash_label.Name = "totalPaymentCash_label";
            this.totalPaymentCash_label.Size = new System.Drawing.Size(145, 18);
            this.totalPaymentCash_label.TabIndex = 30;
            this.totalPaymentCash_label.Text = "Total Pago Efectivo: ";
            this.totalPaymentCash_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalPaymentCash_input.Enabled = false;
            this.totalPaymentCash_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalPaymentCash_input.Location = new System.Drawing.Point(813, 658);
            this.totalPaymentCash_input.Name = "totalPaymentCash_input";
            this.totalPaymentCash_input.Size = new System.Drawing.Size(100, 29);
            this.totalPaymentCash_input.TabIndex = 31;
            this.totalPaymentOtherCash_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalPaymentOtherCash_label.AutoSize = true;
            this.totalPaymentOtherCash_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalPaymentOtherCash_label.Location = new System.Drawing.Point(662, 700);
            this.totalPaymentOtherCash_label.Name = "totalPaymentOtherCash_label";
            this.totalPaymentOtherCash_label.Size = new System.Drawing.Size(130, 18);
            this.totalPaymentOtherCash_label.TabIndex = 32;
            this.totalPaymentOtherCash_label.Text = "Total Otros Pago: ";
            this.totalPaymentOtherCash_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalPaymentOtherCash_input.Enabled = false;
            this.totalPaymentOtherCash_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.totalPaymentOtherCash_input.Location = new System.Drawing.Point(813, 693);
            this.totalPaymentOtherCash_input.Name = "totalPaymentOtherCash_input";
            this.totalPaymentOtherCash_input.Size = new System.Drawing.Size(100, 29);
            this.totalPaymentOtherCash_input.TabIndex = 33;
            this.cancelsale_button.BackColor = System.Drawing.SystemColors.Window;
            this.cancelsale_button.BackgroundImage = (System.Drawing.Image)resources.GetObject("cancelsale_button.BackgroundImage");
            this.cancelsale_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cancelsale_button.Enabled = false;
            this.cancelsale_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cancelsale_button.ForeColor = System.Drawing.Color.Black;
            this.cancelsale_button.Location = new System.Drawing.Point(431, 90);
            this.cancelsale_button.Name = "cancelsale_button";
            this.cancelsale_button.Size = new System.Drawing.Size(41, 31);
            this.cancelsale_button.TabIndex = 34;
            this.cancelsale_button.UseVisualStyleBackColor = false;
            this.cancelsale_button.Click += new System.EventHandler(cancelsale_button_Click);
            this.print_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.print_button.BackColor = System.Drawing.SystemColors.Window;
            this.print_button.BackgroundImage = (System.Drawing.Image)resources.GetObject("print_button.BackgroundImage");
            this.print_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.print_button.Enabled = false;
            this.print_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.print_button.Location = new System.Drawing.Point(925, 658);
            this.print_button.Name = "print_button";
            this.print_button.Size = new System.Drawing.Size(71, 63);
            this.print_button.TabIndex = 35;
            this.print_button.UseVisualStyleBackColor = false;
            this.print_button.Click += new System.EventHandler(print_button_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            base.ClientSize = new System.Drawing.Size(1008, 729);
            base.Controls.Add(this.print_button);
            base.Controls.Add(this.cancelsale_button);
            base.Controls.Add(this.totalPaymentOtherCash_input);
            base.Controls.Add(this.totalPaymentOtherCash_label);
            base.Controls.Add(this.totalPaymentCash_input);
            base.Controls.Add(this.totalPaymentCash_label);
            base.Controls.Add(this.percent_label);
            base.Controls.Add(this.percent_input);
            base.Controls.Add(this.totalpercent_label1);
            base.Controls.Add(this.totalPercent_input);
            base.Controls.Add(this.totalCategory_label);
            base.Controls.Add(this.totalCategory_input);
            base.Controls.Add(this.category_label);
            base.Controls.Add(this.category_combobox);
            base.Controls.Add(this.totalExenta_input);
            base.Controls.Add(this.totalExenta_label);
            base.Controls.Add(this.totalAfecta_input);
            base.Controls.Add(this.totalAfecta_label);
            base.Controls.Add(this.totalTax_input);
            base.Controls.Add(this.totalTax_label);
            base.Controls.Add(this.totalInvoice_input);
            base.Controls.Add(this.totalInvoice_label);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.total_input);
            base.Controls.Add(this.total_label);
            base.Controls.Add(this.startTimePicker);
            base.Controls.Add(this.MostrarButton);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.detail_label);
            base.Controls.Add(this.endTimePicker);
            base.Controls.Add(this.labelGrid1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.detailDataGrid);
            base.Controls.Add(this.MainDataGrid);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "Historic";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Historial";
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Historic_FormClosing);
            ((System.ComponentModel.ISupportInitialize)this.MainDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.detailDataGrid).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}