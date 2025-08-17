using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using thinger.DataConvertLib;
using thinger.ModBusRTULib;

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
        private DataFormat dataFormat = DataFormat.ABCD;
        

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
            输出寄存器4x,
            输入寄存器3x,


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

        public void AddLog(int level, string info)
        {
            
            ListViewItem lst = new ListViewItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "), level);
            
           
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

        #region 读取方法
        //读取端口信号
        private void btn_Read_Click(object sender, EventArgs e)
        {
            if (CommonVerity()) {

                byte devId = byte.Parse(this.cmb_Slave.Text.Trim());
                ushort start = ushort.Parse(this.cmb_Start.Text.Trim());
                ushort length = ushort.Parse(this.cmb_Length.Text.Trim());

                DataType dataType = (DataType)Enum.Parse(typeof(DataType), this.cmb_DataType.Text,true);
                StoreArea storeArea = (StoreArea)Enum.Parse(typeof(StoreArea), this.cmb_StoreArea.Text, true);
                dataFormat = (DataFormat)Enum.Parse(typeof(DataFormat),this.cmb_DataForMat.Text,true);



                switch (dataType)
                {
                    case DataType.Bool:
                        ReadBool(storeArea, devId, start, length);
                        break;
                    case DataType.Byte:
                        ReadByte(storeArea, devId, start, length);
                        break;
                    case DataType.Short:
                        ReadShort(storeArea, devId, start, length);
                        break;
                    case DataType.UShort:
                        ReadUshort(storeArea, devId, start, length);
                        break;
                    case DataType.Int:
                        break;
                    case DataType.UInt:
                        break;
                    case DataType.Float:
                        break;
                    case DataType.Double:                       
                    case DataType.Long:              
                    case DataType.ULong:                      
                    case DataType.String:                     
                    case DataType.ByteArray:                  
                    case DataType.HexString:
                        AddLog(0, "暂不支持该数据类型");
                        return;
                }

            }   
        }

        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        private void ReadBool(StoreArea storeArea, byte devId, ushort start, ushort length) {

            byte[] result = null;

            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    result = modBusRTU.ReadOutPutColls(devId, start, length);
                    break;
                case StoreArea.输入线圈1x:
                    result = modBusRTU.ReadIntPut(devId, start, length);
                    break;
                case StoreArea.输出寄存器4x:
                    result = modBusRTU.ReadOutRegisters(devId, start, length);
                    break;
                case StoreArea.输入寄存器3x:
                    result = modBusRTU.ReadIntPutRegisters(devId, start, length);
                    break;
                default:
                    break;                  


            }
            if (result == null)
            {

                AddLog(2, "读取失败，检查参数是否正常");
            }
            else {
                AddLog(3, "读取成功:"+StringLib.GetStringFromValueArray( BitLib.GetBitArrayFromByteArray(result,0,length)));
            }


        }
        /// <summary>
        /// 读取字节值
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        private void ReadByte(StoreArea storeArea, byte devId, ushort start, ushort length)
        {

            byte[] result = null;

            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    result = modBusRTU.ReadOutPutColls(devId, start, length);
                    break;
                case StoreArea.输入线圈1x:
                    result = modBusRTU.ReadIntPut(devId, start, length);
                    break;
                case StoreArea.输出寄存器4x:
                    result = modBusRTU.ReadOutRegisters(devId, start, length);
                    break;
                case StoreArea.输入寄存器3x:
                    result = modBusRTU.ReadIntPutRegisters(devId, start, length);
                    break;
                default:
                    break;


            }
            if (result == null)
            {

                AddLog(2, "读取失败，检查参数是否正常");
            }
            else
            {
                AddLog(3, "读取成功:" + StringLib.GetStringFromValueArray(result));
            }


        }
        /// <summary>
        /// 读取short类型数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        private void ReadShort(StoreArea storeArea, byte devId, ushort start, ushort length)
        {

            byte[] result = null;

            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输入线圈1x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输出寄存器4x:
                    result = modBusRTU.ReadOutRegisters(devId, start, length);
                    break;
                case StoreArea.输入寄存器3x:
                    result = modBusRTU.ReadIntPutRegisters(devId, start, length);
                    break;
                default:
                    break;


            }
            if (result == null)
            {

                AddLog(2, "读取失败，检查参数是否正常");
            }
            else
            {
                AddLog(3, "读取成功:" + StringLib.GetStringFromValueArray(ShortLib.GetShortArrayFromByteArray(result,this.dataFormat)));
            }


        }

        /// <summary>
        /// 读取Ushort数据类型
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        private void ReadUshort(StoreArea storeArea, byte devId, ushort start, ushort length)
        {

            byte[] result = null;

            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输入线圈1x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输出寄存器4x:
                    result = modBusRTU.ReadOutRegisters(devId, start, length);
                    break;
                case StoreArea.输入寄存器3x:
                    result = modBusRTU.ReadIntPutRegisters(devId, start, length);
                    break;
                default:
                    break;


            }
            if (result == null)
            {

                AddLog(2, "读取失败，检查参数是否正常");
            }
            else
            {
                AddLog(3, "读取成功:" + StringLib.GetStringFromValueArray(UShortLib.GetUShortArrayFromByteArray(result,this.dataFormat)));
            }


        }
        /// <summary>
        ///  读取Int数据类型
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        private void ReadInt(StoreArea storeArea, byte devId, ushort start, ushort length)
        {

            byte[] result = null;

            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输入线圈1x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输出寄存器4x:
                    result = modBusRTU.ReadOutRegisters(devId, start, (ushort)(length*2));
                    break;
                case StoreArea.输入寄存器3x:
                    result = modBusRTU.ReadIntPutRegisters(devId, start, (ushort)(length*2));
                    break;
                default:
                    break;


            }
            if (result == null)
            {

                AddLog(2, "读取失败，检查参数是否正常");
            }
            else
            {
                AddLog(3, "读取成功:" + StringLib.GetStringFromValueArray(IntLib.GetIntArrayFromByteArray(result, this.dataFormat)));
            }


        }
        /// <summary>
        /// 读取Uint数据类型
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        private void ReadUint(StoreArea storeArea, byte devId, ushort start, ushort length)
        {

            byte[] result = null;

            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输入线圈1x:
                    AddLog(2, "不受支持的存储区");
                    return;
                case StoreArea.输出寄存器4x:
                    result = modBusRTU.ReadOutRegisters(devId, start, (ushort)(length * 2));
                    break;
                case StoreArea.输入寄存器3x:
                    result = modBusRTU.ReadIntPutRegisters(devId, start, (ushort)(length * 2));
                    break;
                default:
                    break;


            }
            if (result == null)
            {

                AddLog(2, "读取失败，检查参数是否正常");
            }
            else
            {
                AddLog(3, "读取成功:" + StringLib.GetStringFromValueArray(UIntLib.GetUIntArrayFromByteArray(result, this.dataFormat)));
            }


        }


        #endregion



        #region 写入方法
        /// <summary>
        /// 布尔写入方法
        /// </summary>
        /// <param name="storeArea">存储区</param>
        /// <param name="devId">站地址</param>
        /// <param name="start">起始地址</param>
        /// <param name="setValue">值</param>
        private void WritBool(StoreArea storeArea, byte devId, ushort start, string setValue) { 
        
            bool result = false;
            switch (storeArea) {
                case StoreArea.输出线圈0x:
                    bool[] values = BitLib.GetBitArrayFromBitArrayString(setValue);
                    if (values.Length == 1)
                    {

                        result = modBusRTU.PreSetSingColl(devId, start, values[0]);
                    }
                    else { 
                    result=modBusRTU.PreSetMultiColls(devId, start, values);
                    }

                        break;
                case StoreArea.输入线圈1x:

                    break;
                case StoreArea.输出寄存器4x:
                    break;
                case StoreArea.输入寄存器3x:
                    break;

                 

            }

            if (result)
            {
                AddLog(3, "写入成功");
            }
            else {
            AddLog(2, "写入成功");
            }




        }
        /// <summary>
        /// 写入字节数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="setValue"></param>
        private void WritByte(StoreArea storeArea, byte devId, ushort start, string setValue)
        {

            bool result = false;
            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                  
                    break;
                case StoreArea.输入线圈1x:

                    break;
                case StoreArea.输出寄存器4x:
                    result=modBusRTU.PreSetMultiRegisters(devId, start, ByteArrayLib.GetByteArrayFromHexString(setValue));
                    break;
                case StoreArea.输入寄存器3x:
                    break;



            }

            if (result)
            {
                AddLog(3, "写入成功");
            }
            else
            {
                AddLog(2, "写入成功");
            }




        }
        /// <summary>
        /// 写入Short数据类型
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="setValue"></param>
        private void WritShort(StoreArea storeArea, byte devId, ushort start, string setValue)
        {

            bool result = false;
            switch (storeArea)
            {
                case StoreArea.输出线圈0x:

                    break;
                case StoreArea.输入线圈1x:

                    break;
                case StoreArea.输出寄存器4x:
                    short[] values = ShortLib.GetShortArrayFromString(setValue);
                    if (values.Length == 1)
                    {
                        modBusRTU.PreSetSingRegister(devId, start, values[0]);
                    }
                    else {
                        modBusRTU.PreSetMultiRegisters(devId, start, ByteArrayLib.GetByteArrayFromShortArray(values));
                    }
                        break;
                case StoreArea.输入寄存器3x:
                    break;



            }

            if (result)
            {
                AddLog(3, "写入成功");
            }
            else
            {
                AddLog(2, "写入成功");
            }




        }
        /// <summary>
        /// 写入UShort数据类型
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="devId"></param>
        /// <param name="start"></param>
        /// <param name="setValue"></param>
        private void WritUshort(StoreArea storeArea, byte devId, ushort start, string setValue)
        {

            bool result = false;
            switch (storeArea)
            {
                case StoreArea.输出线圈0x:

                    break;
                case StoreArea.输入线圈1x:

                    break;
                case StoreArea.输出寄存器4x:
                    ushort[] values = UShortLib.GetUShortArrayFromString(setValue);
                    if (values.Length == 1)
                    {
                        modBusRTU.PreSetSingRegister(devId, start, values[0]);
                    }
                    else
                    {
                        modBusRTU.PreSetMultiRegisters(devId, start, ByteArrayLib.GetByteArrayFromUShortArray(values));
                    }
                    break;
                case StoreArea.输入寄存器3x:
                    break;



            }

            if (result)
            {
                AddLog(3, "写入成功");
            }
            else
            {
                AddLog(2, "写入成功");
            }




        }
        #endregion

        //通用方法验证格式是否正确
        private bool CommonVerity() {

            if (iSconnect == false) {
                AddLog(0, "请检查连接是否正常");
                return false;
            }
            if (byte.TryParse(this.cmb_Slave.Text.Trim(),out _) == false) {
                AddLog(0, "请检查站地址是否正常");
                return false;
            }
            if (byte.TryParse(this.cmb_DataBits.Text.Trim(), out _)==false) {
                AddLog(0, "请检查数据位是否正常"); 
                return false;
            }
            if (ushort.TryParse(this.cmb_Start.Text.Trim(), out _) == false) { 
               AddLog(0,"请检查起始地址是否正常") ; 
                return false;
            }
            if (ushort.TryParse(this.cmb_Length.Text.Trim(), out _) == false)
            {
                AddLog(0, "请检查起始地址是否正常");
                return false;
            }
          return true;
        }

        private void btn_Written_Click(object sender, EventArgs e)
        {
            byte devid = byte.Parse(this.cmb_Slave.Text.Trim());
            ushort start = ushort.Parse(this.cmb_Start.Text.Trim());
            string setValue = this.cmb_WriteData.Text.Trim();

            DataType dataType = (DataType)Enum.Parse(typeof(DataType), this.cmb_DataType.Text.Trim());
            StoreArea storeArea = (StoreArea)Enum.Parse(typeof(StoreArea), this.cmb_StoreArea.Text.Trim());

           


            switch (dataType)
            {
                case DataType.Bool:
                    WritBool(storeArea, devid, start, setValue);
                    break;
                case DataType.Byte:
                    break;
                case DataType.Short:
                    break;
                case DataType.UShort:
                    break;
                case DataType.Int:
                    break;
                case DataType.UInt:
                    break;
                case DataType.Float:
                    break;
                case DataType.Double:
                case DataType.Long: 
                case DataType.ULong:
                case DataType.String:
                case DataType.ByteArray:
                case DataType.HexString:
                default:
                    break;
            }


        }




    }
}
