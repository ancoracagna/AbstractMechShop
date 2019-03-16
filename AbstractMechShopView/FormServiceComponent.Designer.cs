namespace AbstractMechShopView
{
    partial class FormServiceComponent
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxComponent = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Компонент:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Количество:";
            // 
            // comboBoxComponent
            // 
            this.comboBoxComponent.FormattingEnabled = true;
            this.comboBoxComponent.Location = new System.Drawing.Point(145, 36);
            this.comboBoxComponent.Name = "comboBoxComponent";
            this.comboBoxComponent.Size = new System.Drawing.Size(309, 21);
            this.comboBoxComponent.TabIndex = 2;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(145, 71);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(309, 20);
            this.textBoxCount.TabIndex = 3;
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(145, 122);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(145, 33);
            this.SaveBtn.TabIndex = 4;
            this.SaveBtn.Text = "Сохранить";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(309, 122);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(145, 33);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "Отменить";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // FormServiceComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 179);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxComponent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormServiceComponent";
            this.Text = "Добавить услугу";
            this.Load += new System.EventHandler(this.FormServiceComponent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxComponent;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}