using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.DataConvertLib;

namespace thinger.ModBusProect
{
    public partial class FrmMainModbusRTU : Form
    {
        public FrmMainModbusRTU()
        {
            InitializeComponent();
            //启动初始化
            InitParm();
        }

        private bool iSconnect = false;
        private ModBusRTULib.ModBusRTU modBusRTU = new ModBusRTULib.ModBusRTU();

        public void InitParm()
        {

            //获取本机端口号
            String[] portList = SerialPort.GetPortNames();

            if (portList.Length > 0)
            {

                this.cmb_port.Items.AddRange(portList);
                this.cmb_port.SelectedIndex = 0;

            }
            //初始化i波特率 
            this.cmb_BudRate.Items.AddRange(new String[] { "2400", "4800", "9600", "19200", "38400" });
            this.cmb_BudRate.SelectedIndex = 2;

            //初始化校验位
            this.cmb_Parity.DataSource = Enum.GetNames(typeof(Parity));
            this.cmb_Parity.SelectedIndex = 0;

            // 初始化停止位
            this.cmb_StopBits.DataSource = Enum.GetNames(typeof(StopBits));
            this.cmb_StopBits.SelectedIndex = 1;
            //初始化数据位

            this.cmb_DataBits.Items.AddRange(new String[] { "7", "8" });
            this.cmb_DataBits.SelectedIndex = 1;

            //初始化大小端
            this.cmb_DataForMat.DataSource = Enum.GetNames(typeof(DataFormat));
            this.cmb_DataForMat.SelectedIndex = 0;



            //存储区
            this.cmb_StoreArea.DataSource = Enum.GetNames(typeof(StoreArea));
            this.cmb_StoreArea.SelectedIndex = 0;

            //数据类型
            this.cmb_DataType.DataSource = Enum.GetNames(typeof(DataType));
            this.cmb_DataType.SelectedIndex = 0;



        }



        public enum StoreArea
        {


            输出线圈0x,
            输入线圈1x,
            输出寄存器3x,
            输入寄存器4x,


        }













        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //将字符串转换为枚举值
            Parity parity = (Parity)Enum.Parse(typeof(Parity), this.cmb_Parity.Text, true);
            StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), this.cmb_StopBits.Text, true);

            if (iSconnect)
            {
                AddLog(1, "ModBus已经连接成功");
                return;

            }


            iSconnect = modBusRTU.Connect(this.cmb_port.Text.ToString(), Convert.ToInt32(this.cmb_BudRate.Text), parity,
                  Convert.ToInt16(this.cmb_DataBits.Text), stopBits);
            if (iSconnect)
            {
                AddLog(2, "ModBus连接成功");

            }
            else {
                
                AddLog(0, "ModBus连接失败");
            }

        }

        public void AddLog(int level, String info)
        {

            ListViewItem lst = new ListViewItem(DateTime.Now.ToString("F"), level);

            lst.SubItems.Add(info);

            //让最新的数据在最上面
            this.lst_info.Items.Insert(0, lst);

        }

        private void btn_DisConnect_Click(object sender, EventArgs e)
        {

            modBusRTU.DisConnect();
            iSconnect = false;
            AddLog(0, "ModBus已断开连接");


        }
    }
}
