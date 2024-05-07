namespace SistemaDeVentas
{
    partial class PurchaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private DataGridView InventoryDataGrid;

        private TextBox filterTextBox;

        private Label label1;

        private Button InsertButton;

        private TextBox price_input;

        private TextBox name_input;

        private Label label4;

        private Label label2;

        private Label label3;

        private TextBox amount_input;

        private PictureBox pictureBox1;

        private Label label5;

        private TextBox barcode_input;

        private Label label6;

        private Button DeleteButton;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseForm));
            InventoryDataGrid = new DataGridView();
            filterTextBox = new TextBox();
            label1 = new Label();
            InsertButton = new Button();
            price_input = new TextBox();
            name_input = new TextBox();
            label4 = new Label();
            label2 = new Label();
            label3 = new Label();
            amount_input = new TextBox();
            pictureBox1 = new PictureBox();
            label5 = new Label();
            barcode_input = new TextBox();
            label6 = new Label();
            DeleteButton = new Button();
            btnCleanSearch = new Button();
            ((System.ComponentModel.ISupportInitialize)InventoryDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // InventoryDataGrid
            // 
            InventoryDataGrid.AllowUserToAddRows = false;
            InventoryDataGrid.AllowUserToDeleteRows = false;
            InventoryDataGrid.AllowUserToResizeColumns = false;
            InventoryDataGrid.AllowUserToResizeRows = false;
            InventoryDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InventoryDataGrid.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            InventoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            InventoryDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            InventoryDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            InventoryDataGrid.Location = new Point(24, 224);
            InventoryDataGrid.Margin = new Padding(6, 7, 6, 7);
            InventoryDataGrid.Name = "InventoryDataGrid";
            InventoryDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            InventoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            InventoryDataGrid.RowHeadersVisible = false;
            InventoryDataGrid.RowHeadersWidth = 72;
            InventoryDataGrid.Size = new Size(607, 453);
            InventoryDataGrid.TabIndex = 3;
            InventoryDataGrid.CellClick += InventoryDataGrid_CellClick;
            // 
            // filterTextBox
            // 
            filterTextBox.Location = new Point(214, 164);
            filterTextBox.Margin = new Padding(6, 7, 6, 7);
            filterTextBox.Name = "filterTextBox";
            filterTextBox.Size = new Size(317, 35);
            filterTextBox.TabIndex = 4;
            filterTextBox.TextChanged += filterTextBox_TextChanged;
            filterTextBox.KeyDown += filterTextBox_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Tomato;
            label1.Location = new Point(214, 127);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(195, 25);
            label1.TabIndex = 5;
            label1.Text = "Buscar Productos :";
            // 
            // InsertButton
            // 
            InsertButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            InsertButton.BackColor = Color.ForestGreen;
            InsertButton.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point);
            InsertButton.ForeColor = SystemColors.ControlLight;
            InsertButton.Location = new Point(1047, 543);
            InsertButton.Margin = new Padding(6, 7, 6, 7);
            InsertButton.Name = "InsertButton";
            InsertButton.Size = new Size(258, 134);
            InsertButton.TabIndex = 29;
            InsertButton.Text = "Registrar Ingreso";
            InsertButton.UseVisualStyleBackColor = false;
            InsertButton.Click += InsertButton_Click;
            // 
            // price_input
            // 
            price_input.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            price_input.Location = new Point(975, 343);
            price_input.Margin = new Padding(6, 7, 6, 7);
            price_input.Name = "price_input";
            price_input.Size = new Size(216, 35);
            price_input.TabIndex = 26;
            price_input.Text = "0";
            // 
            // name_input
            // 
            name_input.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            name_input.CharacterCasing = CharacterCasing.Lower;
            name_input.Location = new Point(975, 224);
            name_input.Margin = new Padding(6, 7, 6, 7);
            name_input.Name = "name_input";
            name_input.ReadOnly = true;
            name_input.Size = new Size(339, 35);
            name_input.TabIndex = 24;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Tomato;
            label4.Location = new Point(717, 351);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(246, 25);
            label4.TabIndex = 21;
            label4.Text = "Precio de Venta (Bruto):";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Tomato;
            label2.Location = new Point(742, 232);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(221, 25);
            label2.TabIndex = 19;
            label2.Text = "Nombre del Producto:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Tomato;
            label3.Location = new Point(794, 291);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(169, 25);
            label3.TabIndex = 20;
            label3.Text = "Cantidad (unid):";
            // 
            // amount_input
            // 
            amount_input.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            amount_input.Location = new Point(975, 283);
            amount_input.Margin = new Padding(6, 7, 6, 7);
            amount_input.Name = "amount_input";
            amount_input.Size = new Size(216, 35);
            amount_input.TabIndex = 25;
            amount_input.Text = "0";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(24, 25);
            pictureBox1.Margin = new Padding(6, 7, 6, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(160, 185);
            pictureBox1.TabIndex = 30;
            pictureBox1.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Tomato;
            label5.Location = new Point(202, 25);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(429, 54);
            label5.TabIndex = 31;
            label5.Text = "Ingreso a Almacen";
            // 
            // barcode_input
            // 
            barcode_input.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            barcode_input.CharacterCasing = CharacterCasing.Lower;
            barcode_input.Location = new Point(860, 157);
            barcode_input.Margin = new Padding(6, 7, 6, 7);
            barcode_input.Name = "barcode_input";
            barcode_input.Size = new Size(454, 35);
            barcode_input.TabIndex = 33;
            barcode_input.KeyDown += textBox1_KeyDown;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.Tomato;
            label6.Location = new Point(661, 164);
            label6.Margin = new Padding(6, 0, 6, 0);
            label6.Name = "label6";
            label6.Size = new Size(187, 25);
            label6.TabIndex = 32;
            label6.Text = "Codigo de Barras:";
            // 
            // DeleteButton
            // 
            DeleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DeleteButton.BackColor = Color.DarkRed;
            DeleteButton.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point);
            DeleteButton.ForeColor = SystemColors.ControlLightLight;
            DeleteButton.Location = new Point(765, 543);
            DeleteButton.Margin = new Padding(6, 7, 6, 7);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(244, 134);
            DeleteButton.TabIndex = 42;
            DeleteButton.Text = "Eliminar Producto";
            DeleteButton.UseVisualStyleBackColor = false;
            DeleteButton.Visible = false;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // btnCleanSearch
            // 
            btnCleanSearch.BackgroundImage = (Image)resources.GetObject("btnCleanSearch.BackgroundImage");
            btnCleanSearch.BackgroundImageLayout = ImageLayout.Zoom;
            btnCleanSearch.ImageAlign = ContentAlignment.TopLeft;
            btnCleanSearch.Location = new Point(544, 156);
            btnCleanSearch.Margin = new Padding(7, 6, 7, 6);
            btnCleanSearch.Name = "btnCleanSearch";
            btnCleanSearch.Size = new Size(87, 52);
            btnCleanSearch.TabIndex = 68;
            btnCleanSearch.UseVisualStyleBackColor = true;
            btnCleanSearch.Click += btnCleanSearch_Click;
            // 
            // PurchaseForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1342, 704);
            Controls.Add(btnCleanSearch);
            Controls.Add(DeleteButton);
            Controls.Add(barcode_input);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(pictureBox1);
            Controls.Add(InsertButton);
            Controls.Add(price_input);
            Controls.Add(amount_input);
            Controls.Add(name_input);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(filterTextBox);
            Controls.Add(InventoryDataGrid);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 7, 6, 7);
            Name = "PurchaseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingresos a Almacen";
            FormClosing += PurchaseForm_FormClosing;
            Load += PurchaseForm_Load;
            ((System.ComponentModel.ISupportInitialize)InventoryDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCleanSearch;
    }
}