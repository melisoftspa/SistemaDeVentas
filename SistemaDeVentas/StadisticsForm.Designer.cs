namespace SistemaDeVentas
{
    partial class StadisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private MenuStrip menuStrip1;

        private ToolStripMenuItem backup_itemMenu;

        private ToolStripMenuItem passwordMenuItem;

        private FolderBrowserDialog backupFolderBrowser;

        private ToolStripMenuItem csvMenuItem;

        private PictureBox pictureBox1;

        private Label label1;

        private DataGridView MainDataGrid;

        private DataGridView detailDataGrid;

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
            //this.Text = "StadisticsForm";
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SistemaDeVentas.StadisticsForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backup_itemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MainDataGrid = new System.Windows.Forms.DataGridView();
            this.detailDataGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.MainDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.detailDataGrid).BeginInit();
            base.SuspendLayout();
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.backup_itemMenu });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(989, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            this.backup_itemMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.passwordMenuItem, this.csvMenuItem });
            this.backup_itemMenu.ForeColor = System.Drawing.Color.Tomato;
            this.backup_itemMenu.Name = "backup_itemMenu";
            this.backup_itemMenu.Size = new System.Drawing.Size(72, 20);
            this.backup_itemMenu.Text = "Seguridad";
            this.passwordMenuItem.ForeColor = System.Drawing.Color.Tomato;
            this.passwordMenuItem.Name = "passwordMenuItem";
            this.passwordMenuItem.Size = new System.Drawing.Size(177, 22);
            this.passwordMenuItem.Text = "Copia de Seguridad";
            this.passwordMenuItem.Click += new System.EventHandler(passwordMenuItem_Click);
            this.csvMenuItem.ForeColor = System.Drawing.Color.Tomato;
            this.csvMenuItem.Name = "csvMenuItem";
            this.csvMenuItem.Size = new System.Drawing.Size(177, 22);
            this.csvMenuItem.Text = "subir csv";
            this.csvMenuItem.Click += new System.EventHandler(csvMenuItem_Click);
            this.backupFolderBrowser.Description = "Seleccione la carpeta donde desea guardar la copia de respado:";
            this.pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(98, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 31);
            this.label1.TabIndex = 14;
            this.label1.Text = "Estadisticas";
            this.MainDataGrid.AllowUserToAddRows = false;
            this.MainDataGrid.AllowUserToDeleteRows = false;
            this.MainDataGrid.AllowUserToResizeColumns = false;
            this.MainDataGrid.AllowUserToResizeRows = false;
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
            this.MainDataGrid.Location = new System.Drawing.Point(12, 95);
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
            this.MainDataGrid.Size = new System.Drawing.Size(480, 397);
            this.MainDataGrid.TabIndex = 19;
            this.MainDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(MainDataGrid_CellClick);
            this.detailDataGrid.AllowUserToAddRows = false;
            this.detailDataGrid.AllowUserToDeleteRows = false;
            this.detailDataGrid.AllowUserToResizeColumns = false;
            this.detailDataGrid.AllowUserToResizeRows = false;
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
            this.detailDataGrid.Location = new System.Drawing.Point(516, 95);
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
            this.detailDataGrid.Size = new System.Drawing.Size(447, 397);
            this.detailDataGrid.TabIndex = 20;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            base.ClientSize = new System.Drawing.Size(989, 533);
            base.Controls.Add(this.detailDataGrid);
            base.Controls.Add(this.MainDataGrid);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.menuStrip1);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "StadisticsForm";
            this.Text = "Estadisticas";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(StadisticsForm_FormClosing);
            base.Load += new System.EventHandler(StadisticsForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.MainDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.detailDataGrid).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}