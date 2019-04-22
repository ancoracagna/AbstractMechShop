namespace AbstractMechShopView
{
    partial class FormStockLoad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.SaveBtnExcel = new System.Windows.Forms.Button();
            this.StockColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComponentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockColumn,
            this.ComponentColumn,
            this.CountColumn});
            this.dataGridView.Location = new System.Drawing.Point(2, 50);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(523, 402);
            this.dataGridView.TabIndex = 0;
            // 
            // SaveBtnExcel
            // 
            this.SaveBtnExcel.Location = new System.Drawing.Point(12, 12);
            this.SaveBtnExcel.Name = "SaveBtnExcel";
            this.SaveBtnExcel.Size = new System.Drawing.Size(186, 32);
            this.SaveBtnExcel.TabIndex = 1;
            this.SaveBtnExcel.Text = "Сохранить в Excel";
            this.SaveBtnExcel.UseVisualStyleBackColor = true;
            this.SaveBtnExcel.Click += new System.EventHandler(this.SaveBtnExcel_Click);
            // 
            // StockColumn
            // 
            this.StockColumn.HeaderText = "Склад";
            this.StockColumn.Name = "StockColumn";
            this.StockColumn.Width = 150;
            // 
            // ComponentColumn
            // 
            this.ComponentColumn.HeaderText = "Компонент";
            this.ComponentColumn.Name = "ComponentColumn";
            this.ComponentColumn.Width = 250;
            // 
            // CountColumn
            // 
            this.CountColumn.HeaderText = "Количество";
            this.CountColumn.Name = "CountColumn";
            // 
            // FormStockLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 450);
            this.Controls.Add(this.SaveBtnExcel);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormStockLoad";
            this.Text = "Загруженность складов";
            this.Load += new System.EventHandler(this.FormStockLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button SaveBtnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComponentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountColumn;
    }
}