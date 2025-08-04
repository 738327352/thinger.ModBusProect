namespace thinger.ModBusProect
{
    partial class FrmModBusRtu
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
            this.SuspendLayout();
            // 
            // FrmModBusRtu
            // 
            this.ClientSize = new System.Drawing.Size(633, 452);
            this.Name = "FrmModBusRtu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_DataFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_StopBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Crc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_BaudRate;
        private System.Windows.Forms.Button btn_DisConnect;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_DataBits;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox bt_Length;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox bt_Start;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_Byte;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_StoreArea;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox txt_slavedId;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox13;
        private System.Windows.Forms.Button btn_Write;
        private System.Windows.Forms.ListView listInfo;
        private System.Windows.Forms.Label label14;
    }
}

