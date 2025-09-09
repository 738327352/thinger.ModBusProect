namespace thinger.ProConfigSys
{
    partial class FrmLoagin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txt_LoadPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_LoadAccount = new System.Windows.Forms.TextBox();
            this.lable = new System.Windows.Forms.Label();
            this.btn_LoadSystem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkBox1);
            this.splitContainer1.Panel2.Controls.Add(this.txt_LoadPwd);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txt_LoadAccount);
            this.splitContainer1.Panel2.Controls.Add(this.lable);
            this.splitContainer1.Panel2.Controls.Add(this.btn_LoadSystem);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(214, 153);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 20);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // txt_LoadPwd
            // 
            this.txt_LoadPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_LoadPwd.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_LoadPwd.Location = new System.Drawing.Point(307, 93);
            this.txt_LoadPwd.Name = "txt_LoadPwd";
            this.txt_LoadPwd.PasswordChar = '*';
            this.txt_LoadPwd.Size = new System.Drawing.Size(290, 35);
            this.txt_LoadPwd.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(179, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "管理员密码：";
            // 
            // txt_LoadAccount
            // 
            this.txt_LoadAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_LoadAccount.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_LoadAccount.Location = new System.Drawing.Point(307, 46);
            this.txt_LoadAccount.Name = "txt_LoadAccount";
            this.txt_LoadAccount.Size = new System.Drawing.Size(290, 35);
            this.txt_LoadAccount.TabIndex = 2;
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable.Location = new System.Drawing.Point(179, 53);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(136, 21);
            this.lable.TabIndex = 1;
            this.lable.Text = "管理员账号：";
            // 
            // btn_LoadSystem
            // 
            this.btn_LoadSystem.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_LoadSystem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_LoadSystem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_LoadSystem.Location = new System.Drawing.Point(428, 141);
            this.btn_LoadSystem.Name = "btn_LoadSystem";
            this.btn_LoadSystem.Size = new System.Drawing.Size(169, 45);
            this.btn_LoadSystem.TabIndex = 0;
            this.btn_LoadSystem.Text = "登 录 系 统";
            this.btn_LoadSystem.UseVisualStyleBackColor = false;
            this.btn_LoadSystem.Click += new System.EventHandler(this.btn_LoadSystem_Click);
            // 
            // FrmLoagin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "FrmLoagin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txt_LoadAccount;
        private System.Windows.Forms.Label lable;
        private System.Windows.Forms.Button btn_LoadSystem;
        private System.Windows.Forms.TextBox txt_LoadPwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

