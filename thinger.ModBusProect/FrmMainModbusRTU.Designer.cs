namespace thinger.ModBusProect
{
    partial class FrmMainModbusRTU
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainModbusRTU));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_DisConnect = new System.Windows.Forms.Button();
            this.cmb_DataForMat = new System.Windows.Forms.ComboBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_Parity = new System.Windows.Forms.ComboBox();
            this.cmb_BudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_StopBits = new System.Windows.Forms.ComboBox();
            this.停止位 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_DataBits = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_port = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_Length = new System.Windows.Forms.TextBox();
            this.btn_Written = new System.Windows.Forms.Button();
            this.btn_Read = new System.Windows.Forms.Button();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_DataType = new System.Windows.Forms.ComboBox();
            this.cmb_StoreArea = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_Start = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_Slave = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lst_info = new System.Windows.Forms.ListView();
            this.label13 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.DateTimeView = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_DisConnect);
            this.groupBox1.Controls.Add(this.cmb_DataForMat);
            this.groupBox1.Controls.Add(this.btn_Connect);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmb_Parity);
            this.groupBox1.Controls.Add(this.cmb_BudRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmb_StopBits);
            this.groupBox1.Controls.Add(this.停止位);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmb_DataBits);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmb_port);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信参数";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btn_DisConnect
            // 
            this.btn_DisConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btn_DisConnect.Location = new System.Drawing.Point(545, 75);
            this.btn_DisConnect.Name = "btn_DisConnect";
            this.btn_DisConnect.Size = new System.Drawing.Size(84, 30);
            this.btn_DisConnect.TabIndex = 17;
            this.btn_DisConnect.Text = "断开连接";
            this.btn_DisConnect.UseVisualStyleBackColor = false;
            this.btn_DisConnect.Click += new System.EventHandler(this.btn_DisConnect_Click);
            // 
            // cmb_DataForMat
            // 
            this.cmb_DataForMat.FormattingEnabled = true;
            this.cmb_DataForMat.Location = new System.Drawing.Point(282, 80);
            this.cmb_DataForMat.Name = "cmb_DataForMat";
            this.cmb_DataForMat.Size = new System.Drawing.Size(69, 20);
            this.cmb_DataForMat.TabIndex = 10;
            // 
            // btn_Connect
            // 
            this.btn_Connect.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Connect.Location = new System.Drawing.Point(412, 75);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(84, 30);
            this.btn_Connect.TabIndex = 16;
            this.btn_Connect.Text = "建立连接";
            this.btn_Connect.UseVisualStyleBackColor = false;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(220, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "大小端：";
            // 
            // cmb_Parity
            // 
            this.cmb_Parity.FormattingEnabled = true;
            this.cmb_Parity.Location = new System.Drawing.Point(453, 32);
            this.cmb_Parity.Name = "cmb_Parity";
            this.cmb_Parity.Size = new System.Drawing.Size(69, 20);
            this.cmb_Parity.TabIndex = 8;
            // 
            // cmb_BudRate
            // 
            this.cmb_BudRate.FormattingEnabled = true;
            this.cmb_BudRate.Location = new System.Drawing.Point(282, 32);
            this.cmb_BudRate.Name = "cmb_BudRate";
            this.cmb_BudRate.Size = new System.Drawing.Size(69, 20);
            this.cmb_BudRate.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(376, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "校验位：";
            // 
            // cmb_StopBits
            // 
            this.cmb_StopBits.FormattingEnabled = true;
            this.cmb_StopBits.Location = new System.Drawing.Point(93, 75);
            this.cmb_StopBits.Name = "cmb_StopBits";
            this.cmb_StopBits.Size = new System.Drawing.Size(105, 20);
            this.cmb_StopBits.TabIndex = 7;
            // 
            // 停止位
            // 
            this.停止位.AutoSize = true;
            this.停止位.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.停止位.Location = new System.Drawing.Point(33, 77);
            this.停止位.Name = "停止位";
            this.停止位.Size = new System.Drawing.Size(71, 16);
            this.停止位.TabIndex = 6;
            this.停止位.Text = "停止位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(540, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "数据位：";
            // 
            // cmb_DataBits
            // 
            this.cmb_DataBits.FormattingEnabled = true;
            this.cmb_DataBits.Location = new System.Drawing.Point(617, 32);
            this.cmb_DataBits.Name = "cmb_DataBits";
            this.cmb_DataBits.Size = new System.Drawing.Size(63, 20);
            this.cmb_DataBits.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(220, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率：";
            // 
            // cmb_port
            // 
            this.cmb_port.FormattingEnabled = true;
            this.cmb_port.Location = new System.Drawing.Point(93, 32);
            this.cmb_port.Name = "cmb_port";
            this.cmb_port.Size = new System.Drawing.Size(105, 20);
            this.cmb_port.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_Length);
            this.groupBox2.Controls.Add(this.btn_Written);
            this.groupBox2.Controls.Add(this.btn_Read);
            this.groupBox2.Controls.Add(this.comboBox10);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmb_DataType);
            this.groupBox2.Controls.Add(this.cmb_StoreArea);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmb_Start);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cmb_Slave);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(22, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(686, 177);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "读写测试";
            // 
            // cmb_Length
            // 
            this.cmb_Length.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cmb_Length.Location = new System.Drawing.Point(287, 77);
            this.cmb_Length.Name = "cmb_Length";
            this.cmb_Length.Size = new System.Drawing.Size(145, 21);
            this.cmb_Length.TabIndex = 16;
            this.cmb_Length.Text = "10";
            // 
            // btn_Written
            // 
            this.btn_Written.Location = new System.Drawing.Point(585, 120);
            this.btn_Written.Name = "btn_Written";
            this.btn_Written.Size = new System.Drawing.Size(84, 30);
            this.btn_Written.TabIndex = 15;
            this.btn_Written.Text = "写入";
            this.btn_Written.UseVisualStyleBackColor = true;
            // 
            // btn_Read
            // 
            this.btn_Read.Location = new System.Drawing.Point(585, 75);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(84, 30);
            this.btn_Read.TabIndex = 14;
            this.btn_Read.Text = "读取";
            this.btn_Read.UseVisualStyleBackColor = true;
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // comboBox10
            // 
            this.comboBox10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Location = new System.Drawing.Point(93, 119);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(444, 24);
            this.comboBox10.TabIndex = 12;
            this.comboBox10.Text = "1 1 1 1 1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(14, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "写入数据：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(212, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "读取长度：";
            // 
            // cmb_DataType
            // 
            this.cmb_DataType.FormattingEnabled = true;
            this.cmb_DataType.Location = new System.Drawing.Point(542, 34);
            this.cmb_DataType.Name = "cmb_DataType";
            this.cmb_DataType.Size = new System.Drawing.Size(138, 20);
            this.cmb_DataType.TabIndex = 8;
            // 
            // cmb_StoreArea
            // 
            this.cmb_StoreArea.FormattingEnabled = true;
            this.cmb_StoreArea.Location = new System.Drawing.Point(282, 34);
            this.cmb_StoreArea.Name = "cmb_StoreArea";
            this.cmb_StoreArea.Size = new System.Drawing.Size(145, 20);
            this.cmb_StoreArea.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(450, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "数据类型：";
            // 
            // cmb_Start
            // 
            this.cmb_Start.FormattingEnabled = true;
            this.cmb_Start.Location = new System.Drawing.Point(93, 75);
            this.cmb_Start.Name = "cmb_Start";
            this.cmb_Start.Size = new System.Drawing.Size(105, 20);
            this.cmb_Start.TabIndex = 7;
            this.cmb_Start.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(14, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "起始地址：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(220, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 16);
            this.label11.TabIndex = 3;
            this.label11.Text = "存储区：";
            // 
            // cmb_Slave
            // 
            this.cmb_Slave.FormattingEnabled = true;
            this.cmb_Slave.Location = new System.Drawing.Point(93, 34);
            this.cmb_Slave.Name = "cmb_Slave";
            this.cmb_Slave.Size = new System.Drawing.Size(105, 20);
            this.cmb_Slave.TabIndex = 1;
            this.cmb_Slave.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(14, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "从站地址：";
            // 
            // lst_info
            // 
            this.lst_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DateTimeView,
            this.Message});
            this.lst_info.HideSelection = false;
            this.lst_info.Location = new System.Drawing.Point(23, 410);
            this.lst_info.MultiSelect = false;
            this.lst_info.Name = "lst_info";
            this.lst_info.Size = new System.Drawing.Size(695, 178);
            this.lst_info.SmallImageList = this.imageList1;
            this.lst_info.TabIndex = 2;
            this.lst_info.UseCompatibleStateImageBehavior = false;
            this.lst_info.View = System.Windows.Forms.View.Details;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(28, 382);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 3;
            this.label13.Text = "读取信息";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "waring1.png");
            this.imageList1.Images.SetKeyName(1, "warning2.png");
            this.imageList1.Images.SetKeyName(2, "warning3.png");
            // 
            // DateTimeView
            // 
            this.DateTimeView.Text = "日期时间";
            // 
            // Message
            // 
            this.Message.Text = "信息内容";
            // 
            // FrmMainModbusRTU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 600);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lst_info);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmMainModbusRTU";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_BudRate;
        private System.Windows.Forms.ComboBox cmb_DataForMat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_Parity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_StopBits;
        private System.Windows.Forms.Label 停止位;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_DataBits;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_DataType;
        private System.Windows.Forms.ComboBox cmb_StoreArea;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_Start;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_Slave;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_DisConnect;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Written;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lst_info;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox cmb_Length;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader DateTimeView;
        private System.Windows.Forms.ColumnHeader Message;
    }
}

