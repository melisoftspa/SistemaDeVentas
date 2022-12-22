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

        private Label label8;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseForm));
            this.InventoryDataGrid = new System.Windows.Forms.DataGridView();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InsertButton = new System.Windows.Forms.Button();
            this.price_input = new System.Windows.Forms.TextBox();
            this.name_input = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.amount_input = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.barcode_input = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.btnCleanSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.InventoryDataGrid.Location = new System.Drawing.Point(24, 224);
            this.InventoryDataGrid.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
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
            this.InventoryDataGrid.Size = new System.Drawing.Size(1131, 778);
            this.InventoryDataGrid.TabIndex = 3;
            this.InventoryDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InventoryDataGrid_CellClick);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(214, 164);
            this.filterTextBox.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(941, 35);
            this.filterTextBox.TabIndex = 4;
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            this.filterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filterTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(214, 127);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Buscar Productos :";
            // 
            // InsertButton
            // 
            this.InsertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InsertButton.BackColor = System.Drawing.Color.ForestGreen;
            this.InsertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InsertButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.InsertButton.Location = new System.Drawing.Point(1656, 575);
            this.InsertButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(258, 134);
            this.InsertButton.TabIndex = 29;
            this.InsertButton.Text = "Registrar Ingreso";
            this.InsertButton.UseVisualStyleBackColor = false;
            this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
            // 
            // price_input
            // 
            this.price_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.price_input.Location = new System.Drawing.Point(1456, 389);
            this.price_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.price_input.Name = "price_input";
            this.price_input.Size = new System.Drawing.Size(216, 35);
            this.price_input.TabIndex = 26;
            this.price_input.Text = "0";
            // 
            // name_input
            // 
            this.name_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.name_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.name_input.Location = new System.Drawing.Point(1456, 270);
            this.name_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.name_input.Name = "name_input";
            this.name_input.ReadOnly = true;
            this.name_input.Size = new System.Drawing.Size(454, 35);
            this.name_input.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(1198, 397);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(246, 25);
            this.label4.TabIndex = 21;
            this.label4.Text = "Precio de Venta (Bruto):";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(1223, 278);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "Nombre del Producto:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(1275, 337);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 25);
            this.label3.TabIndex = 20;
            this.label3.Text = "Cantidad (unid):";
            // 
            // amount_input
            // 
            this.amount_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.amount_input.Location = new System.Drawing.Point(1456, 329);
            this.amount_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.amount_input.Name = "amount_input";
            this.amount_input.Size = new System.Drawing.Size(216, 35);
            this.amount_input.TabIndex = 25;
            this.amount_input.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(24, 25);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 185);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(202, 25);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(429, 54);
            this.label5.TabIndex = 31;
            this.label5.Text = "Ingreso a Almacen";
            // 
            // barcode_input
            // 
            this.barcode_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.barcode_input.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.barcode_input.Location = new System.Drawing.Point(1456, 157);
            this.barcode_input.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.barcode_input.Name = "barcode_input";
            this.barcode_input.Size = new System.Drawing.Size(454, 35);
            this.barcode_input.TabIndex = 33;
            this.barcode_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(1257, 164);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 25);
            this.label6.TabIndex = 32;
            this.label6.Text = "Codigo de Barras:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(1167, 228);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(751, 10);
            this.label8.TabIndex = 41;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.BackColor = System.Drawing.Color.DarkRed;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DeleteButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DeleteButton.Location = new System.Drawing.Point(1374, 575);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(244, 134);
            this.DeleteButton.TabIndex = 42;
            this.DeleteButton.Text = "Eliminar Producto";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Visible = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // btnCleanSearch
            // 
            this.btnCleanSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCleanSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCleanSearch.BackgroundImage")));
            this.btnCleanSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCleanSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCleanSearch.Location = new System.Drawing.Point(1168, 156);
            this.btnCleanSearch.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCleanSearch.Name = "btnCleanSearch";
            this.btnCleanSearch.Size = new System.Drawing.Size(55, 52);
            this.btnCleanSearch.TabIndex = 68;
            this.btnCleanSearch.UseVisualStyleBackColor = true;
            this.btnCleanSearch.Click += new System.EventHandler(this.btnCleanSearch_Click);
            // 
            // PurchaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1938, 1029);
            this.Controls.Add(this.btnCleanSearch);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.barcode_input);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.price_input);
            this.Controls.Add(this.amount_input);
            this.Controls.Add(this.name_input);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.InventoryDataGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "PurchaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingresos a Almacen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PurchaseForm_FormClosing);
            this.Load += new System.EventHandler(this.PurchaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCleanSearch;
    }
}