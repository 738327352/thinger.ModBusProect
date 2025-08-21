namespace thingerTask
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_Kuaxianchengfangwen = new System.Windows.Forms.Button();
            this.btn_TimeDelay = new System.Windows.Forms.Button();
            this.btn_MessageSoultion = new System.Windows.Forms.Button();
            this.lbl_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 74);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动Task任务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(409, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 74);
            this.button2.TabIndex = 1;
            this.button2.Text = "关闭Task任务";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(90, 184);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 74);
            this.button3.TabIndex = 2;
            this.button3.Text = "暂停Task任务";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(409, 184);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 74);
            this.button4.TabIndex = 3;
            this.button4.Text = "继续Task任务";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_Kuaxianchengfangwen
            // 
            this.btn_Kuaxianchengfangwen.Location = new System.Drawing.Point(90, 329);
            this.btn_Kuaxianchengfangwen.Name = "btn_Kuaxianchengfangwen";
            this.btn_Kuaxianchengfangwen.Size = new System.Drawing.Size(121, 49);
            this.btn_Kuaxianchengfangwen.TabIndex = 4;
            this.btn_Kuaxianchengfangwen.Text = "跨线程访问";
            this.btn_Kuaxianchengfangwen.UseVisualStyleBackColor = true;
            // 
            // btn_TimeDelay
            // 
            this.btn_TimeDelay.Location = new System.Drawing.Point(236, 329);
            this.btn_TimeDelay.Name = "btn_TimeDelay";
            this.btn_TimeDelay.Size = new System.Drawing.Size(121, 49);
            this.btn_TimeDelay.TabIndex = 5;
            this.btn_TimeDelay.Text = "耗时任务UI卡顿现场";
            this.btn_TimeDelay.UseVisualStyleBackColor = true;
            // 
            // btn_MessageSoultion
            // 
            this.btn_MessageSoultion.Location = new System.Drawing.Point(374, 329);
            this.btn_MessageSoultion.Name = "btn_MessageSoultion";
            this.btn_MessageSoultion.Size = new System.Drawing.Size(121, 49);
            this.btn_MessageSoultion.TabIndex = 6;
            this.btn_MessageSoultion.Text = "耗时任务UI卡顿解决";
            this.btn_MessageSoultion.UseVisualStyleBackColor = true;
            // 
            // lbl_Label
            // 
            this.lbl_Label.AutoSize = true;
            this.lbl_Label.Location = new System.Drawing.Point(569, 345);
            this.lbl_Label.Name = "lbl_Label";
            this.lbl_Label.Size = new System.Drawing.Size(89, 12);
            this.lbl_Label.TabIndex = 7;
            this.lbl_Label.Text = "待更新。。。。";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_Label);
            this.Controls.Add(this.btn_MessageSoultion);
            this.Controls.Add(this.btn_TimeDelay);
            this.Controls.Add(this.btn_Kuaxianchengfangwen);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_Kuaxianchengfangwen;
        private System.Windows.Forms.Button btn_TimeDelay;
        private System.Windows.Forms.Button btn_MessageSoultion;
        private System.Windows.Forms.Label lbl_Label;
    }
}

