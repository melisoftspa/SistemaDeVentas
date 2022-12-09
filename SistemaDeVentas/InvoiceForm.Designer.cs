namespace SistemaDeVentas
{
    partial class InvoiceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private PictureBox pictureBox1;

        private Label label6;

        private DateTimePicker startTimePicker;

        private Label label1;

        private DateTimePicker endTimePicker;

        private Label label2;

        private Button search_button;

        private DateTimePicker oDateTimePicker;

        //private SalesSystemDB salesSystemDB;

        private BindingSource salesSystemDB1BindingSource;

        //private SalesSystemDB salesSystemDB1;

        //private invoiceTableAdapter invoiceDataTableTableAdapter;

        private DataGridView dataGridView1;

        private BindingSource invoiceDataTableBindingSource;

        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;

        private DataGridViewCheckBoxColumn stateDataGridViewCheckBoxColumn1;

        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;

        private DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn1;

        private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn1;

        private DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn1;

        private DataGridViewTextBoxColumn nameproviderDataGridViewTextBoxColumn1;

        private DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn1;

        private DataGridViewCheckBoxColumn statepaymentDataGridViewCheckBoxColumn1;

        private DataGridViewCheckBoxColumn paymentcheckDataGridViewCheckBoxColumn1;

        private DataGridViewTextBoxColumn paymentchecknumberDataGridViewTextBoxColumn1;

        private DataGridViewTextBoxColumn paymentcheckdateDataGridViewTextBoxColumn1;

        private DataGridViewTextBoxColumn paymentchecktotalDataGridViewTextBoxColumn1;

        private DataGridViewCheckBoxColumn paymentcashDataGridViewCheckBoxColumn1;

        private DataGridViewTextBoxColumn paymentcashtotalDataGridViewTextBoxColumn1;

        private Label totalcheck_label;

        private TextBox totalcheck_input;

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
            //this.Text = "InvoiceForm";
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.InvoiceForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.search_button = new System.Windows.Forms.Button();
            this.oDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.invoiceDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameproviderDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statepaymentDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.paymentcheckDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.paymentchecknumberDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paymentchecktotalDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paymentcashDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.paymentcashtotalDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salesSystemDB1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            //this.salesSystemDB1 = new SistemaDeVentas.SalesSystemDB();
            //this.invoiceDataTableTableAdapter = new SistemaDeVentas.SalesSystemDBTableAdaptersTableAdapters.invoiceTableAdapter();
            this.totalcheck_label = new System.Windows.Forms.Label();
            this.totalcheck_input = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.invoiceDataTableBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.salesSystemDB1BindingSource).BeginInit();
            //((System.ComponentModel.ISupportInitialize)this.salesSystemDB1).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 90);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(106, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 31);
            this.label6.TabIndex = 46;
            this.label6.Text = "Compras";
            this.startTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startTimePicker.Location = new System.Drawing.Point(167, 46);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(132, 26);
            this.startTimePicker.TabIndex = 47;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label1.Location = new System.Drawing.Point(109, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 18);
            this.label1.TabIndex = 48;
            this.label1.Text = "Desde: ";
            this.endTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endTimePicker.Location = new System.Drawing.Point(167, 78);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(132, 26);
            this.endTimePicker.TabIndex = 49;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.label2.Location = new System.Drawing.Point(109, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 50;
            this.label2.Text = "Hasta: ";
            this.search_button.BackColor = System.Drawing.SystemColors.Window;
            this.search_button.BackgroundImage = (System.Drawing.Image)resources.GetObject("search_button.BackgroundImage");
            this.search_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.search_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.search_button.Location = new System.Drawing.Point(305, 52);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(45, 36);
            this.search_button.TabIndex = 52;
            this.search_button.UseVisualStyleBackColor = false;
            this.search_button.Click += new System.EventHandler(search_button_Click);
            this.oDateTimePicker.Location = new System.Drawing.Point(12, 110);
            this.oDateTimePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.oDateTimePicker.Name = "oDateTimePicker";
            this.oDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.oDateTimePicker.TabIndex = 53;
            this.oDateTimePicker.TabStop = false;
            this.oDateTimePicker.Visible = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(this.idDataGridViewTextBoxColumn1, this.stateDataGridViewCheckBoxColumn1, this.nameDataGridViewTextBoxColumn1, this.dateDataGridViewTextBoxColumn1, this.amountDataGridViewTextBoxColumn1, this.totalDataGridViewTextBoxColumn1, this.nameproviderDataGridViewTextBoxColumn1, this.notesDataGridViewTextBoxColumn1, this.statepaymentDataGridViewCheckBoxColumn1, this.paymentcheckDataGridViewCheckBoxColumn1, this.paymentchecknumberDataGridViewTextBoxColumn1, this.paymentchecktotalDataGridViewTextBoxColumn1, this.paymentcashDataGridViewCheckBoxColumn1, this.paymentcashtotalDataGridViewTextBoxColumn1);
            this.dataGridView1.DataSource = this.invoiceDataTableBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 110);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(984, 534);
            this.dataGridView1.TabIndex = 54;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_RowEnter);
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_RowLeave);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(dataGridView1_UserAddedRow);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyDown);
            this.dataGridView1.Validated += new System.EventHandler(dataGridView1_Validated);
            this.invoiceDataTableBindingSource.DataMember = "InvoiceDataTable";
            this.invoiceDataTableBindingSource.DataSource = this.salesSystemDB1BindingSource;
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "id";
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.Visible = false;
            this.stateDataGridViewCheckBoxColumn1.DataPropertyName = "state";
            this.stateDataGridViewCheckBoxColumn1.HeaderText = "state";
            this.stateDataGridViewCheckBoxColumn1.Name = "stateDataGridViewCheckBoxColumn1";
            this.stateDataGridViewCheckBoxColumn1.Visible = false;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Nombre de Compra";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.Width = 150;
            this.dateDataGridViewTextBoxColumn1.DataPropertyName = "date";
            dataGridViewCellStyle.Format = "d";
            dataGridViewCellStyle.NullValue = null;
            this.dateDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle;
            this.dateDataGridViewTextBoxColumn1.HeaderText = "Fecha";
            this.dateDataGridViewTextBoxColumn1.Name = "dateDataGridViewTextBoxColumn1";
            this.amountDataGridViewTextBoxColumn1.DataPropertyName = "amount";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.amountDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.amountDataGridViewTextBoxColumn1.HeaderText = "Cant. Productos";
            this.amountDataGridViewTextBoxColumn1.Name = "amountDataGridViewTextBoxColumn1";
            this.amountDataGridViewTextBoxColumn1.Width = 70;
            this.totalDataGridViewTextBoxColumn1.DataPropertyName = "total";
            dataGridViewCellStyle3.Format = "C0";
            dataGridViewCellStyle3.NullValue = null;
            this.totalDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.totalDataGridViewTextBoxColumn1.HeaderText = "Total Compra";
            this.totalDataGridViewTextBoxColumn1.Name = "totalDataGridViewTextBoxColumn1";
            this.nameproviderDataGridViewTextBoxColumn1.DataPropertyName = "name_provider";
            this.nameproviderDataGridViewTextBoxColumn1.HeaderText = "Nombre Proveedor";
            this.nameproviderDataGridViewTextBoxColumn1.Name = "nameproviderDataGridViewTextBoxColumn1";
            this.notesDataGridViewTextBoxColumn1.DataPropertyName = "notes";
            this.notesDataGridViewTextBoxColumn1.HeaderText = "Nota Interna";
            this.notesDataGridViewTextBoxColumn1.Name = "notesDataGridViewTextBoxColumn1";
            this.statepaymentDataGridViewCheckBoxColumn1.DataPropertyName = "state_payment";
            this.statepaymentDataGridViewCheckBoxColumn1.HeaderText = "¿Pagado?";
            this.statepaymentDataGridViewCheckBoxColumn1.Name = "statepaymentDataGridViewCheckBoxColumn1";
            this.paymentcheckDataGridViewCheckBoxColumn1.DataPropertyName = "payment_check";
            this.paymentcheckDataGridViewCheckBoxColumn1.HeaderText = "Pago Con Cheque";
            this.paymentcheckDataGridViewCheckBoxColumn1.Name = "paymentcheckDataGridViewCheckBoxColumn1";
            this.paymentchecknumberDataGridViewTextBoxColumn1.DataPropertyName = "payment_check_number";
            dataGridViewCellStyle4.NullValue = null;
            this.paymentchecknumberDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.paymentchecknumberDataGridViewTextBoxColumn1.HeaderText = "Nro. del Cheque";
            this.paymentchecknumberDataGridViewTextBoxColumn1.Name = "paymentchecknumberDataGridViewTextBoxColumn1";
            this.paymentchecktotalDataGridViewTextBoxColumn1.DataPropertyName = "payment_check_total";
            dataGridViewCellStyle5.Format = "C0";
            dataGridViewCellStyle5.NullValue = null;
            this.paymentchecktotalDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.paymentchecktotalDataGridViewTextBoxColumn1.HeaderText = "Total Cobro Cheque";
            this.paymentchecktotalDataGridViewTextBoxColumn1.Name = "paymentchecktotalDataGridViewTextBoxColumn1";
            this.paymentcashDataGridViewCheckBoxColumn1.DataPropertyName = "payment_cash";
            this.paymentcashDataGridViewCheckBoxColumn1.HeaderText = "Pago Con Efectivo";
            this.paymentcashDataGridViewCheckBoxColumn1.Name = "paymentcashDataGridViewCheckBoxColumn1";
            this.paymentcashtotalDataGridViewTextBoxColumn1.DataPropertyName = "payment_cash_total";
            dataGridViewCellStyle6.Format = "C0";
            dataGridViewCellStyle6.NullValue = null;
            this.paymentcashtotalDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.paymentcashtotalDataGridViewTextBoxColumn1.HeaderText = "Total Pago Efectivo";
            this.paymentcashtotalDataGridViewTextBoxColumn1.Name = "paymentcashtotalDataGridViewTextBoxColumn1";
            //this.salesSystemDB1BindingSource.DataSource = this.salesSystemDB1;
            this.salesSystemDB1BindingSource.Position = 0;
            //this.salesSystemDB1.DataSetName = "SalesSystemDB";
            //this.salesSystemDB1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            //this.invoiceDataTableTableAdapter.ClearBeforeFill = true;
            this.totalcheck_label.AutoSize = true;
            this.totalcheck_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.totalcheck_label.Location = new System.Drawing.Point(15, 660);
            this.totalcheck_label.Name = "totalcheck_label";
            this.totalcheck_label.Size = new System.Drawing.Size(192, 29);
            this.totalcheck_label.TabIndex = 55;
            this.totalcheck_label.Text = "Total Compras:";
            this.totalcheck_input.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.totalcheck_input.Enabled = false;
            this.totalcheck_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.totalcheck_input.Location = new System.Drawing.Point(213, 660);
            this.totalcheck_input.Name = "totalcheck_input";
            this.totalcheck_input.ReadOnly = true;
            this.totalcheck_input.Size = new System.Drawing.Size(196, 35);
            this.totalcheck_input.TabIndex = 56;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            base.ClientSize = new System.Drawing.Size(1008, 729);
            base.Controls.Add(this.totalcheck_input);
            base.Controls.Add(this.totalcheck_label);
            base.Controls.Add(this.dataGridView1);
            base.Controls.Add(this.oDateTimePicker);
            base.Controls.Add(this.search_button);
            base.Controls.Add(this.startTimePicker);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.endTimePicker);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.pictureBox1);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "InvoiceForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compras y Ventas";
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(UserForm_FormClosing);
            base.Load += new System.EventHandler(InvoiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.invoiceDataTableBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.salesSystemDB1BindingSource).EndInit();
            //((System.ComponentModel.ISupportInitialize)this.salesSystemDB1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}