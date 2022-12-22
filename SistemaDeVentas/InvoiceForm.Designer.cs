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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.search_button = new System.Windows.Forms.Button();
            this.oDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.invoiceDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.salesSystemDB1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.totalcheck_label = new System.Windows.Forms.Label();
            this.totalcheck_input = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDataTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesSystemDB1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(24, 28);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 208);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(212, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 54);
            this.label6.TabIndex = 46;
            this.label6.Text = "Compras";
            // 
            // startTimePicker
            // 
            this.startTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startTimePicker.Location = new System.Drawing.Point(334, 106);
            this.startTimePicker.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(260, 39);
            this.startTimePicker.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(218, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 30);
            this.label1.TabIndex = 48;
            this.label1.Text = "Desde: ";
            // 
            // endTimePicker
            // 
            this.endTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endTimePicker.Location = new System.Drawing.Point(334, 180);
            this.endTimePicker.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(260, 39);
            this.endTimePicker.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(218, 194);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 30);
            this.label2.TabIndex = 50;
            this.label2.Text = "Hasta: ";
            // 
            // search_button
            // 
            this.search_button.BackColor = System.Drawing.SystemColors.Window;
            this.search_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.search_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.search_button.Location = new System.Drawing.Point(610, 120);
            this.search_button.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(90, 83);
            this.search_button.TabIndex = 52;
            this.search_button.UseVisualStyleBackColor = false;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // oDateTimePicker
            // 
            this.oDateTimePicker.Location = new System.Drawing.Point(24, 254);
            this.oDateTimePicker.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.oDateTimePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.oDateTimePicker.Name = "oDateTimePicker";
            this.oDateTimePicker.Size = new System.Drawing.Size(396, 35);
            this.oDateTimePicker.TabIndex = 53;
            this.oDateTimePicker.TabStop = false;
            this.oDateTimePicker.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.stateDataGridViewCheckBoxColumn1,
            this.nameDataGridViewTextBoxColumn1,
            this.dateDataGridViewTextBoxColumn1,
            this.amountDataGridViewTextBoxColumn1,
            this.totalDataGridViewTextBoxColumn1,
            this.nameproviderDataGridViewTextBoxColumn1,
            this.notesDataGridViewTextBoxColumn1,
            this.statepaymentDataGridViewCheckBoxColumn1,
            this.paymentcheckDataGridViewCheckBoxColumn1,
            this.paymentchecknumberDataGridViewTextBoxColumn1,
            this.paymentchecktotalDataGridViewTextBoxColumn1,
            this.paymentcashDataGridViewCheckBoxColumn1,
            this.paymentcashtotalDataGridViewTextBoxColumn1});
            this.dataGridView1.DataSource = this.invoiceDataTableBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(24, 254);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.Size = new System.Drawing.Size(1968, 954);
            this.dataGridView1.TabIndex = 54;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.Validated += new System.EventHandler(this.dataGridView1_Validated);
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "id";
            this.idDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.Visible = false;
            this.idDataGridViewTextBoxColumn1.Width = 175;
            // 
            // stateDataGridViewCheckBoxColumn1
            // 
            this.stateDataGridViewCheckBoxColumn1.DataPropertyName = "state";
            this.stateDataGridViewCheckBoxColumn1.HeaderText = "state";
            this.stateDataGridViewCheckBoxColumn1.MinimumWidth = 9;
            this.stateDataGridViewCheckBoxColumn1.Name = "stateDataGridViewCheckBoxColumn1";
            this.stateDataGridViewCheckBoxColumn1.Visible = false;
            this.stateDataGridViewCheckBoxColumn1.Width = 175;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Nombre de Compra";
            this.nameDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.Width = 150;
            // 
            // dateDataGridViewTextBoxColumn1
            // 
            this.dateDataGridViewTextBoxColumn1.DataPropertyName = "date";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.dateDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dateDataGridViewTextBoxColumn1.HeaderText = "Fecha";
            this.dateDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.dateDataGridViewTextBoxColumn1.Name = "dateDataGridViewTextBoxColumn1";
            this.dateDataGridViewTextBoxColumn1.Width = 175;
            // 
            // amountDataGridViewTextBoxColumn1
            // 
            this.amountDataGridViewTextBoxColumn1.DataPropertyName = "amount";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.amountDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.amountDataGridViewTextBoxColumn1.HeaderText = "Cant. Productos";
            this.amountDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.amountDataGridViewTextBoxColumn1.Name = "amountDataGridViewTextBoxColumn1";
            this.amountDataGridViewTextBoxColumn1.Width = 70;
            // 
            // totalDataGridViewTextBoxColumn1
            // 
            this.totalDataGridViewTextBoxColumn1.DataPropertyName = "total";
            dataGridViewCellStyle3.Format = "C0";
            dataGridViewCellStyle3.NullValue = null;
            this.totalDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.totalDataGridViewTextBoxColumn1.HeaderText = "Total Compra";
            this.totalDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.totalDataGridViewTextBoxColumn1.Name = "totalDataGridViewTextBoxColumn1";
            this.totalDataGridViewTextBoxColumn1.Width = 175;
            // 
            // nameproviderDataGridViewTextBoxColumn1
            // 
            this.nameproviderDataGridViewTextBoxColumn1.DataPropertyName = "name_provider";
            this.nameproviderDataGridViewTextBoxColumn1.HeaderText = "Nombre Proveedor";
            this.nameproviderDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.nameproviderDataGridViewTextBoxColumn1.Name = "nameproviderDataGridViewTextBoxColumn1";
            this.nameproviderDataGridViewTextBoxColumn1.Width = 175;
            // 
            // notesDataGridViewTextBoxColumn1
            // 
            this.notesDataGridViewTextBoxColumn1.DataPropertyName = "notes";
            this.notesDataGridViewTextBoxColumn1.HeaderText = "Nota Interna";
            this.notesDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.notesDataGridViewTextBoxColumn1.Name = "notesDataGridViewTextBoxColumn1";
            this.notesDataGridViewTextBoxColumn1.Width = 175;
            // 
            // statepaymentDataGridViewCheckBoxColumn1
            // 
            this.statepaymentDataGridViewCheckBoxColumn1.DataPropertyName = "state_payment";
            this.statepaymentDataGridViewCheckBoxColumn1.HeaderText = "¿Pagado?";
            this.statepaymentDataGridViewCheckBoxColumn1.MinimumWidth = 9;
            this.statepaymentDataGridViewCheckBoxColumn1.Name = "statepaymentDataGridViewCheckBoxColumn1";
            this.statepaymentDataGridViewCheckBoxColumn1.Width = 175;
            // 
            // paymentcheckDataGridViewCheckBoxColumn1
            // 
            this.paymentcheckDataGridViewCheckBoxColumn1.DataPropertyName = "payment_check";
            this.paymentcheckDataGridViewCheckBoxColumn1.HeaderText = "Pago Con Cheque";
            this.paymentcheckDataGridViewCheckBoxColumn1.MinimumWidth = 9;
            this.paymentcheckDataGridViewCheckBoxColumn1.Name = "paymentcheckDataGridViewCheckBoxColumn1";
            this.paymentcheckDataGridViewCheckBoxColumn1.Width = 175;
            // 
            // paymentchecknumberDataGridViewTextBoxColumn1
            // 
            this.paymentchecknumberDataGridViewTextBoxColumn1.DataPropertyName = "payment_check_number";
            dataGridViewCellStyle4.NullValue = null;
            this.paymentchecknumberDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.paymentchecknumberDataGridViewTextBoxColumn1.HeaderText = "Nro. del Cheque";
            this.paymentchecknumberDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.paymentchecknumberDataGridViewTextBoxColumn1.Name = "paymentchecknumberDataGridViewTextBoxColumn1";
            this.paymentchecknumberDataGridViewTextBoxColumn1.Width = 175;
            // 
            // paymentchecktotalDataGridViewTextBoxColumn1
            // 
            this.paymentchecktotalDataGridViewTextBoxColumn1.DataPropertyName = "payment_check_total";
            dataGridViewCellStyle5.Format = "C0";
            dataGridViewCellStyle5.NullValue = null;
            this.paymentchecktotalDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.paymentchecktotalDataGridViewTextBoxColumn1.HeaderText = "Total Cobro Cheque";
            this.paymentchecktotalDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.paymentchecktotalDataGridViewTextBoxColumn1.Name = "paymentchecktotalDataGridViewTextBoxColumn1";
            this.paymentchecktotalDataGridViewTextBoxColumn1.Width = 175;
            // 
            // paymentcashDataGridViewCheckBoxColumn1
            // 
            this.paymentcashDataGridViewCheckBoxColumn1.DataPropertyName = "payment_cash";
            this.paymentcashDataGridViewCheckBoxColumn1.HeaderText = "Pago Con Efectivo";
            this.paymentcashDataGridViewCheckBoxColumn1.MinimumWidth = 9;
            this.paymentcashDataGridViewCheckBoxColumn1.Name = "paymentcashDataGridViewCheckBoxColumn1";
            this.paymentcashDataGridViewCheckBoxColumn1.Width = 175;
            // 
            // paymentcashtotalDataGridViewTextBoxColumn1
            // 
            this.paymentcashtotalDataGridViewTextBoxColumn1.DataPropertyName = "payment_cash_total";
            dataGridViewCellStyle6.Format = "C0";
            dataGridViewCellStyle6.NullValue = null;
            this.paymentcashtotalDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.paymentcashtotalDataGridViewTextBoxColumn1.HeaderText = "Total Pago Efectivo";
            this.paymentcashtotalDataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.paymentcashtotalDataGridViewTextBoxColumn1.Name = "paymentcashtotalDataGridViewTextBoxColumn1";
            this.paymentcashtotalDataGridViewTextBoxColumn1.Width = 175;
            // 
            // invoiceDataTableBindingSource
            // 
            this.invoiceDataTableBindingSource.DataMember = "InvoiceDataTable";
            this.invoiceDataTableBindingSource.DataSource = this.salesSystemDB1BindingSource;
            // 
            // totalcheck_label
            // 
            this.totalcheck_label.AutoSize = true;
            this.totalcheck_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalcheck_label.Location = new System.Drawing.Point(30, 1523);
            this.totalcheck_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalcheck_label.Name = "totalcheck_label";
            this.totalcheck_label.Size = new System.Drawing.Size(320, 48);
            this.totalcheck_label.TabIndex = 55;
            this.totalcheck_label.Text = "Total Compras:";
            // 
            // totalcheck_input
            // 
            this.totalcheck_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totalcheck_input.Enabled = false;
            this.totalcheck_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalcheck_input.Location = new System.Drawing.Point(426, 1317);
            this.totalcheck_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.totalcheck_input.Name = "totalcheck_input";
            this.totalcheck_input.ReadOnly = true;
            this.totalcheck_input.Size = new System.Drawing.Size(388, 55);
            this.totalcheck_input.TabIndex = 56;
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(2016, 1404);
            this.Controls.Add(this.totalcheck_input);
            this.Controls.Add(this.totalcheck_label);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.oDateTimePicker);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.startTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "InvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compras y Ventas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserForm_FormClosing);
            this.Load += new System.EventHandler(this.InvoiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDataTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesSystemDB1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}