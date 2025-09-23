namespace excelOperation
{
    partial class Form2
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
            this.ExcelFileInput = new System.Windows.Forms.TextBox();
            this.ExcelFileLab = new System.Windows.Forms.Label();
            this.ChangeBtn = new System.Windows.Forms.Button();
            this.SavingBtn = new System.Windows.Forms.Button();
            this.ExecuteBtn = new System.Windows.Forms.Button();
            this.PortBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PortModelInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExcelFileInput
            // 
            this.ExcelFileInput.Location = new System.Drawing.Point(144, 42);
            this.ExcelFileInput.Name = "ExcelFileInput";
            this.ExcelFileInput.Size = new System.Drawing.Size(492, 25);
            this.ExcelFileInput.TabIndex = 0;
            // 
            // ExcelFileLab
            // 
            this.ExcelFileLab.AutoSize = true;
            this.ExcelFileLab.Location = new System.Drawing.Point(31, 45);
            this.ExcelFileLab.Name = "ExcelFileLab";
            this.ExcelFileLab.Size = new System.Drawing.Size(107, 15);
            this.ExcelFileLab.TabIndex = 1;
            this.ExcelFileLab.Text = "Excel文件地址";
            // 
            // ChangeBtn
            // 
            this.ChangeBtn.Location = new System.Drawing.Point(565, 377);
            this.ChangeBtn.Name = "ChangeBtn";
            this.ChangeBtn.Size = new System.Drawing.Size(105, 61);
            this.ChangeBtn.TabIndex = 2;
            this.ChangeBtn.Text = "Change";
            this.ChangeBtn.UseVisualStyleBackColor = true;
            this.ChangeBtn.Click += new System.EventHandler(this.Change);
            // 
            // SavingBtn
            // 
            this.SavingBtn.Location = new System.Drawing.Point(694, 377);
            this.SavingBtn.Name = "SavingBtn";
            this.SavingBtn.Size = new System.Drawing.Size(94, 61);
            this.SavingBtn.TabIndex = 3;
            this.SavingBtn.Text = "Saving";
            this.SavingBtn.UseVisualStyleBackColor = true;
            this.SavingBtn.Click += new System.EventHandler(this.Saving);
            // 
            // ExecuteBtn
            // 
            this.ExecuteBtn.Location = new System.Drawing.Point(388, 377);
            this.ExecuteBtn.Name = "ExecuteBtn";
            this.ExecuteBtn.Size = new System.Drawing.Size(156, 61);
            this.ExecuteBtn.TabIndex = 4;
            this.ExecuteBtn.Text = "executeCode";
            this.ExecuteBtn.UseVisualStyleBackColor = true;
            this.ExecuteBtn.Click += new System.EventHandler(this.ExecuteExcelOperation);
            // 
            // PortBtn
            // 
            this.PortBtn.Location = new System.Drawing.Point(34, 377);
            this.PortBtn.Name = "PortBtn";
            this.PortBtn.Size = new System.Drawing.Size(131, 61);
            this.PortBtn.TabIndex = 5;
            this.PortBtn.Text = "新建今日报表";
            this.PortBtn.UseVisualStyleBackColor = true;
            this.PortBtn.Click += new System.EventHandler(this.NewPortFile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "今日报表模板地址";
            // 
            // PortModelInput
            // 
            this.PortModelInput.Location = new System.Drawing.Point(144, 96);
            this.PortModelInput.Name = "PortModelInput";
            this.PortModelInput.Size = new System.Drawing.Size(492, 25);
            this.PortModelInput.TabIndex = 7;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PortModelInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortBtn);
            this.Controls.Add(this.ExecuteBtn);
            this.Controls.Add(this.SavingBtn);
            this.Controls.Add(this.ChangeBtn);
            this.Controls.Add(this.ExcelFileLab);
            this.Controls.Add(this.ExcelFileInput);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ExcelFileInput;
        private System.Windows.Forms.Label ExcelFileLab;
        private System.Windows.Forms.Button ChangeBtn;
        private System.Windows.Forms.Button SavingBtn;
        private System.Windows.Forms.Button ExecuteBtn;
        private System.Windows.Forms.Button PortBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PortModelInput;
    }
}