using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thinger.ModBusRTULib
{
    public class ModBusRTU
    {
        public ModBusRTU()
        {
            serialPort = new SerialPort();
        }

        #region 字段与属性
        private SerialPort serialPort;
        //读取超时时间
        public int ReadTimeOut { get; set; } = 1000;        //写入超时时间
        public int WriteTimeOut { get; set; } = 1000;

        public int sleepTime { get; set; } = 50;

        private bool dtrEnable = false;
        private bool rtsEnable = false;
        private int ReceiveTimeOut { get; set; } = 500;
        public bool RtsEnable {
            get { return rtsEnable; }
            set {
                rtsEnable = value;
                this.serialPort.RtsEnable = value;

            } }

        public bool DtrEnable {
            get { return dtrEnable; }
            set {
                dtrEnable = value;
                serialPort.RtsEnable = value;

            }
        }



        


        #endregion 字段属性与方法
        #region 连接与断开连接
        /// <summary>
        /// 连接串口
        /// </summary>
        /// <param name="串口号"></param>
        /// <param name="波特率"></param>
        /// <param name="校验"></param>
        /// <param name="校验位"></param>
        /// <param name="停止位"></param>
        /// <returns></returns>
        public bool Connect(String portName, int budRate = 9600, Parity parity = Parity.None, int dataBaits = 8, StopBits stopBits = StopBits.One)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            serialPort.PortName = portName;
            serialPort.BaudRate = budRate;
            serialPort.Parity = parity;
            serialPort.StopBits = stopBits;
            serialPort.ReadTimeout = ReadTimeOut;
            serialPort.WriteTimeout = WriteTimeOut;
            serialPort.Open();
            return true;
        }
        //关闭连接
        public void DisConnect() {

            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }

        }
        #endregion
        #region 01读取输出线圈的方法
        /// <summary>
        /// 读取输出线圈的方法
        /// </summary>
        /// <param name="slaved">站地址</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="length">长度</param>
        /// <returns></returns>      
        public byte[] ReadOutPutColls(byte slaved, ushort startAddress, ushort length) {

            //拼接报文

            List<byte> SendCommand = new List<byte>();

            //从站地址
            SendCommand.Add(slaved);
            //功能码
            SendCommand.Add(0x01);
            //起始地
            SendCommand.Add((byte)(startAddress / 256));
            SendCommand.Add((byte)(startAddress % 256));
            //线圈数量
            SendCommand.Add((byte)(length / 256));
            SendCommand.Add((byte)(length % 256));

            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));

            
            byte[] receive = null;

            int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                if (receive.Length == byteLength + 5) {
                    byte[] result = new byte[byteLength];

                    Array.Copy(receive, 3, result, 0, byteLength);

                    return result;
                }

            }

            return null;
            //解析报文



        }

        #endregion 01h读取输出线圈的方法
        #region 01读取输入线圈的方法
        /// <summary>
        /// 读取输入线圈的方法
        /// </summary>
        /// <param name="slaved">站地址</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="length">场地</param>
        /// <param name="sendCommand"></param>
        /// <returns></returns>
        public byte[] ReadIntPut(byte slaved, ushort startAddress, ushort length) {

            List<byte> SendCommand = new List<byte>();

            SendCommand.Add(slaved);  //从站地址
            SendCommand.Add(0x02);    //功能码

            SendCommand.Add((byte)(startAddress / 256));//起始地
            SendCommand.Add((byte)(startAddress % 256));

            SendCommand.Add((byte)(length / 256));   //线圈数量
            SendCommand.Add((byte)(length % 256));

            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));   //拼接CRC验证


            byte[] receive = null;

            try
            {
                int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;

                if (SendAndReceive(SendCommand.ToArray(), ref receive)) {

                    if (receive.Length == byteLength) {
                        if (receive[0] == slaved && receive[1] == 0x02) {

                            byte[] result = new byte[byteLength];
                            Array.Copy(receive, 3, receive, 0, byteLength);  //将报文数据提取出来复制到result中

                            return (result);
                        }

                    }

                }


                return null;
            }
            catch (Exception)
            {

                throw;
            }







        }




        #endregion




        #region 读取输入和输出寄存器的方法
        /// <summary>
        /// 读取输出寄存器
        /// </summary>
        /// <param name="slaved">站地址</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public byte[] ReadOutRegisters(byte slaved, ushort startAddress, ushort length) {

            List<byte> SendCommand = new List<byte>();

            SendCommand.Add(slaved);
            SendCommand.Add(0x03);

            SendCommand.Add((byte)(startAddress / 256));
            SendCommand.Add((byte)(startAddress % 256));

            SendCommand.Add((byte)(length / 256));
            SendCommand.Add((byte)(length % 256));


            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));

            byte[] receive = null;

            int byteLength = length * 2;

            if (SendAndReceive(SendCommand.ToArray(), ref receive)) {

                if (receive.Length == 5 + byteLength) {

                    if (receive[0] == slaved && receive[1] == 0x03 && receive[2] == byteLength)
                    {
                        byte[] result = new byte[byteLength];

                        Array.Copy(receive, 3, result, 0, byteLength);

                        return result;

                    }

                }
            }
            return null;





        }
        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        /// <param name="slaved">站地址</param>
        /// <param name="startAddress">起始地址param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public byte[] ReadIntPutRegisters(byte slaved, ushort startAddress, ushort length)
        {

            List<byte> SendCommand = new List<byte>();

            SendCommand.Add(slaved);
            SendCommand.Add(0x04);

            SendCommand.Add((byte)(startAddress / 256));
            SendCommand.Add((byte)(startAddress % 256));

            SendCommand.Add((byte)(length / 256));
            SendCommand.Add((byte)(length % 256));


            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));

            byte[] receive = null;

            int byteLength = length * 2;

            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {

                if (receive.Length == 5 + byteLength)
                {

                    if (receive[0] == slaved && receive[1] == 0x04 && receive[2] == byteLength)
                    {
                        byte[] result = new byte[byteLength];

                        Array.Copy(receive, 3, result, 0, byteLength);

                        return result;

                    }

                }
            }
            return null;





        }
        /*
        public byte[] ReadOutRegisters(byte slaved, ushort startAddress, ushort length, List<byte> sendCommand)
        {

            List<byte> SendCommand = new List<byte>();

            SendCommand.Add(slaved);
            SendCommand.Add(0x03);

            SendCommand.Add((byte)(startAddress / 256));
            SendCommand.Add((byte)(startAddress % 256));

            SendCommand.Add((byte)(length / 256));
            SendCommand.Add((byte)(length % 256));


            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));

            byte[] receive = null;

            int byteLength = length * 2;

            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {

                if (receive.Length == 5 + byteLength)
                {

                    if (receive[0] == 0x01 && receive[1] == 0x03 && receive[2] == byteLength)
                    {
                        byte[] result = new byte[byteLength];

                        Array.Copy(receive, 3, result, 0, byteLength);

                        return result;

                    }

                }
            }
            return null;





        }
        */

        #endregion

        #region  05H预置单线圈
        /// <summary>
        /// 预置单线圈
        /// </summary>
        /// <param name="slaved"></param>
        /// <param name="startAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool PreSetSingColl(byte slaved, ushort startAddress, bool value)
        {
            List<byte> SendCommand = new List<byte>();

            SendCommand.Add(slaved);
            SendCommand.Add(0x05);

            SendCommand.Add((byte)(startAddress / 256));
            SendCommand.Add((byte)(startAddress % 256));

            SendCommand.Add(value ? (byte)0XFF : (byte)0X00);
            SendCommand.Add(0X00);
            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));
            byte[] receive = null;


            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {

                if (CheckCrc(receive) && receive.Length == 8)
                {
                    return ByteArrayEquals(SendCommand.ToArray(), receive);


                }
            }
             return false;

        }



        #endregion

        #region  06H预置单寄存器
        /// <summary>
        /// 预置单寄存器
        /// </summary>
        /// <param name="slaved">站地址</param>
        /// <param name="stardAddress">寄存器地址</param>
        /// <param name="value">字节数组（2个字节） 若不为两个字节则会报错</param>
        /// <returns>返回结果</returns>
        public bool PreSetSingRegister(byte slaved, ushort stardAddress, byte[] value) {
            List<byte> SendCommand = new List<byte>();

            SendCommand.Add(slaved);
            SendCommand.Add(0x06);

            SendCommand.Add((byte)(stardAddress / 256));
            SendCommand.Add((byte)(stardAddress % 256));

            SendCommand.AddRange(value);

            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));
            byte[] receive = null;

            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {

                if (receive.Length == 8)
                {

                    return ByteArrayEquals(SendCommand.ToArray(), receive);

                }
            }
            return false;

        }

        public bool PreSetSingRegister(byte slaved, ushort stardAddress, short value) {
            return PreSetSingRegister(slaved, stardAddress, BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public bool PreSetSingRegister(byte slaved, ushort stardAddress, ushort value)
        {
            return PreSetSingRegister(slaved, stardAddress, BitConverter.GetBytes(value).Reverse().ToArray());
        }

        #endregion

   
        /// <summary>
        /// 预置多线圈
        /// </summary>
        /// <param name="slaved"></param>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool PreSetMultiColls(byte slaved, ushort start, bool[] value) {


            List<byte> SendCommand = new List<byte>();

            byte[] byteArray = GetByteArrayFromBoolArray(value);

            SendCommand.Add(slaved);
            SendCommand.Add(0x0F);

            SendCommand.Add((byte)(start / 256));

            SendCommand.Add((byte)(start % 256));

            SendCommand.Add((byte)(value.Length / 256));
            SendCommand.Add((byte)(value.Length % 256));

            SendCommand.Add((byte)byteArray.Length);

            SendCommand.AddRange(byteArray);

            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));
            byte[] receive = null;


            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                for (int i = 0; i < 6; i++) {

                    if (SendCommand[i] == receive[i]) {
                        return false;
                    }
                }

                return true;
               
            }
            return false;





        }


        /// <summary>
        /// 预置多寄存器
        /// </summary>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool PreSetMultiRegisters(byte slaveId, ushort start, byte[] value) {

            if (value == null || value.Length == 0) { return false; }

            List<byte> SendCommand = new List<byte>();
            int RegisterLength = value.Length / 2;

            //拼接报文
            SendCommand.Add(slaveId); //站地址
            SendCommand.Add(0X10);//功能码

            SendCommand.Add(((byte)(start / 256)));//起始寄存器地址（高位）
            SendCommand.Add(((byte)(start % 256)));//起始寄存器地址（低位）

            SendCommand.Add(((byte)(RegisterLength / 256)));//（寄存器数量高位）
            SendCommand.Add(((byte)(RegisterLength % 256)));//（寄存器数量低位）

            SendCommand.Add((byte)value.Length);

            SendCommand.AddRange(value);


            SendCommand.AddRange(Crc16(SendCommand.ToArray(), SendCommand.Count));  //添加CRC校验码

            byte[] receive = null;
            //发送报文与接收报文
            if (SendAndReceive(SendCommand.ToArray(), ref receive)) {

                if (receive.Length != 0 || receive != null) {



                    return ByteArrayEquals(SendCommand.ToArray(), receive);


                }



            }

            return false;

        }






        #region 布尔数组转换为字节数组
        /// <summary>
        /// 拿到数组的字节
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private byte[] GetByteArrayFromBoolArray(bool[] value)
        {


            int byteLength = value.Length % 8 == 0 ? value.Length / 8 : value.Length / 8 + 1;

            byte[] result = new byte[byteLength];

            for (int i = 0; i < result.Length; i++)
            {
                //保证bool不会越界
                int total = value.Length < 8 * (i + 1) ? value.Length - 8 * i : 8;
                int startIndex = i * 8;
                for (int j = 0; j < total; j++)
                {
                    // 如果值为 true，设置对应的位（低位在前）
                  
                        result[i] = SetBitValue(result[i], j, value[8*i+j] ); 
                    

                }

            }
            return result;
        }
        /// <summary>
        /// 将字节的位做调整
        /// </summary>
        /// <param name="src"></param>
        /// <param name="bit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private byte SetBitValue(byte src, int bit, bool value) {

            return value ? (byte)(src | (byte)Math.Pow(2, bit)) : (byte)(src & ~(byte)Math.Pow(2, bit));


        }




        #endregion


        #region sendAndReceiveway
        /// <summary>
        /// 发送和接收的方法
        /// </summary>
        /// <param name="send"></param>
        /// <param name="receive"></param>
        /// <returns></returns>
        public bool SendAndReceive(byte[] send, ref byte[] receive)
        {

            try
            {
                //发送报文
                this.serialPort.Write(send, 0, send.Length);
                //定义一个buffer          
                byte[] buffer = new byte[1024];
                //定义一个内存                
                MemoryStream stream = new MemoryStream();
                //定义现在时间
                DateTime now = DateTime.Now;
                //循环读取
                while (true)
                {
                    //延迟
                    Thread.Sleep(sleepTime);
                    if (this.serialPort.BytesToRead > 0)
                    {//获取串口长度
                        int count = this.serialPort.Read(buffer, 0, buffer.Length);
                        //写入串口流
                        stream.Write(buffer, 0, count);
                    }
                    else
                    {
                        if (stream.Length > 0)
                        {
                            break;
                        }
                        else if (DateTime.Now.Millisecond > ReceiveTimeOut)
                            return false;
                    }
                    
                }
                receive = stream.ToArray();
                return true;

            }
            catch (Exception)
            {
                return false;
            }


        }


        #endregion


        #region 数组比较方法
        /// <summary>
        /// 对比两个书节数组是否相等
        /// </summary>
        /// <param name="byte1">输入第一个数组</param>
        /// <param name="byte2">输入第二个数组</param>
        /// <returns></returns>
        private bool ByteArrayEquals(byte[] byte1, byte[] byte2) {

            if (byte1 == null || byte2 == null) return false;

            if (byte1.Length != byte2.Length) return false;

            for (int i = 0; i < byte1.Length; i++) {

                if (byte1[i] != byte2[i]) return false;

            }

            return true;
        }
        #endregion


        private static byte[] crc_table = new byte[512];

        #region CRC验证
        /// <summary>
        /// 国标算法
        /// </summary>
        /// <param name="modbusframe">数据</param>
        /// <param name="Length">数据字节长度</param>
        /// <returns></returns>
        public static int crc16(byte[] modbusframe, int Length)
        {
            int i;
            int index;
            int crc_Low = 0xFF;
            int crc_High = 0xFF;

            for (i = 0; i < Length; i++)
            {
                index = crc_High ^ (char)modbusframe[i];
                crc_High = crc_Low ^ crc_table[index];
                crc_Low = (byte)crc_table[index + 256];
            }

            return crc_High * 256 + crc_Low;
        }
        /// <summary>
        /// CRC校验
        /// </summary>
        /// <param name="src">ST开头，&&结尾</param>
        /// <returns>十六进制数</returns>
        internal static string CRCEfficacy(string src)
        {
            byte[] byteArr = Encoding.UTF8.GetBytes(src);
            int len = byteArr.Length;
            int temp = crc16(byteArr, len);
            string result = temp.ToString("X");
            return result;
        }
        #endregion

        #region 生成CRC码
        /// <summary>
        /// 生成CRC码
        /// </summary>
        /// <param name="message">发送或返回的命令，CRC码除外</param>
        /// <param name="CRC">生成的CRC码</param>
        internal static void GetCRC(byte[] message, ref byte[] CRC)
        {
            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;
            for (int i = 0; i < message.Length - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);
                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    //下面两句所得结果一样
                    //CRCFull = (ushort)(CRCFull >> 1);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);
                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }

        /// <summary>
        /// 初始化CRC
        /// </summary>
        internal static void InitCRCStorage()
        {
            #region 初始化CRC
            crc_table[0] = 0x0;
            crc_table[1] = 0xC1;
            crc_table[2] = 0x81;
            crc_table[3] = 0x40;
            crc_table[4] = 0x1;
            crc_table[5] = 0xC0;
            crc_table[6] = 0x80;
            crc_table[7] = 0x41;
            crc_table[8] = 0x1;
            crc_table[9] = 0xC0;
            crc_table[10] = 0x80;
            crc_table[11] = 0x41;
            crc_table[12] = 0x0;
            crc_table[13] = 0xC1;
            crc_table[14] = 0x81;
            crc_table[15] = 0x40;
            crc_table[16] = 0x1;
            crc_table[17] = 0xC0;
            crc_table[18] = 0x80;
            crc_table[19] = 0x41;
            crc_table[20] = 0x0;
            crc_table[21] = 0xC1;
            crc_table[22] = 0x81;
            crc_table[23] = 0x40;
            crc_table[24] = 0x0;
            crc_table[25] = 0xC1;
            crc_table[26] = 0x81;
            crc_table[27] = 0x40;
            crc_table[28] = 0x1;
            crc_table[29] = 0xC0;
            crc_table[30] = 0x80;
            crc_table[31] = 0x41;
            crc_table[32] = 0x1;
            crc_table[33] = 0xC0;
            crc_table[34] = 0x80;
            crc_table[35] = 0x41;
            crc_table[36] = 0x0;
            crc_table[37] = 0xC1;
            crc_table[38] = 0x81;
            crc_table[39] = 0x40;
            crc_table[40] = 0x0;
            crc_table[41] = 0xC1;
            crc_table[42] = 0x81;
            crc_table[43] = 0x40;
            crc_table[44] = 0x1;
            crc_table[45] = 0xC0;
            crc_table[46] = 0x80;
            crc_table[47] = 0x41;
            crc_table[48] = 0x0;
            crc_table[49] = 0xC1;
            crc_table[50] = 0x81;
            crc_table[51] = 0x40;
            crc_table[52] = 0x1;
            crc_table[53] = 0xC0;
            crc_table[54] = 0x80;
            crc_table[55] = 0x41;
            crc_table[56] = 0x1;
            crc_table[57] = 0xC0;
            crc_table[58] = 0x80;
            crc_table[59] = 0x41;
            crc_table[60] = 0x0;
            crc_table[61] = 0xC1;
            crc_table[62] = 0x81;
            crc_table[63] = 0x40;
            crc_table[64] = 0x1;
            crc_table[65] = 0xC0;
            crc_table[66] = 0x80;
            crc_table[67] = 0x41;
            crc_table[68] = 0x0;
            crc_table[69] = 0xC1;
            crc_table[70] = 0x81;
            crc_table[71] = 0x40;
            crc_table[72] = 0x0;
            crc_table[73] = 0xC1;
            crc_table[74] = 0x81;
            crc_table[75] = 0x40;
            crc_table[76] = 0x1;
            crc_table[77] = 0xC0;
            crc_table[78] = 0x80;
            crc_table[79] = 0x41;
            crc_table[80] = 0x0;
            crc_table[81] = 0xC1;
            crc_table[82] = 0x81;
            crc_table[83] = 0x40;
            crc_table[84] = 0x1;
            crc_table[85] = 0xC0;
            crc_table[86] = 0x80;
            crc_table[87] = 0x41;
            crc_table[88] = 0x1;
            crc_table[89] = 0xC0;
            crc_table[90] = 0x80;
            crc_table[91] = 0x41;
            crc_table[92] = 0x0;
            crc_table[93] = 0xC1;
            crc_table[94] = 0x81;
            crc_table[95] = 0x40;
            crc_table[96] = 0x0;
            crc_table[97] = 0xC1;
            crc_table[98] = 0x81;
            crc_table[99] = 0x40;
            crc_table[100] = 0x1;
            crc_table[101] = 0xC0;
            crc_table[102] = 0x80;
            crc_table[103] = 0x41;
            crc_table[104] = 0x1;
            crc_table[105] = 0xC0;
            crc_table[106] = 0x80;
            crc_table[107] = 0x41;
            crc_table[108] = 0x0;
            crc_table[109] = 0xC1;
            crc_table[110] = 0x81;
            crc_table[111] = 0x40;
            crc_table[112] = 0x1;
            crc_table[113] = 0xC0;
            crc_table[114] = 0x80;
            crc_table[115] = 0x41;
            crc_table[116] = 0x0;
            crc_table[117] = 0xC1;
            crc_table[118] = 0x81;
            crc_table[119] = 0x40;
            crc_table[120] = 0x0;
            crc_table[121] = 0xC1;
            crc_table[122] = 0x81;
            crc_table[123] = 0x40;
            crc_table[124] = 0x1;
            crc_table[125] = 0xC0;
            crc_table[126] = 0x80;
            crc_table[127] = 0x41;
            crc_table[128] = 0x1;
            crc_table[129] = 0xC0;
            crc_table[130] = 0x80;
            crc_table[131] = 0x41;
            crc_table[132] = 0x0;
            crc_table[133] = 0xC1;
            crc_table[134] = 0x81;
            crc_table[135] = 0x40;
            crc_table[136] = 0x0;
            crc_table[137] = 0xC1;
            crc_table[138] = 0x81;
            crc_table[139] = 0x40;
            crc_table[140] = 0x1;
            crc_table[141] = 0xC0;
            crc_table[142] = 0x80;
            crc_table[143] = 0x41;
            crc_table[144] = 0x0;
            crc_table[145] = 0xC1;
            crc_table[146] = 0x81;
            crc_table[147] = 0x40;
            crc_table[148] = 0x1;
            crc_table[149] = 0xC0;
            crc_table[150] = 0x80;
            crc_table[151] = 0x41;
            crc_table[152] = 0x1;
            crc_table[153] = 0xC0;
            crc_table[154] = 0x80;
            crc_table[155] = 0x41;
            crc_table[156] = 0x0;
            crc_table[157] = 0xC1;
            crc_table[158] = 0x81;
            crc_table[159] = 0x40;
            crc_table[160] = 0x0;
            crc_table[161] = 0xC1;
            crc_table[162] = 0x81;
            crc_table[163] = 0x40;
            crc_table[164] = 0x1;
            crc_table[165] = 0xC0;
            crc_table[166] = 0x80;
            crc_table[167] = 0x41;
            crc_table[168] = 0x1;
            crc_table[169] = 0xC0;
            crc_table[170] = 0x80;
            crc_table[171] = 0x41;
            crc_table[172] = 0x0;
            crc_table[173] = 0xC1;
            crc_table[174] = 0x81;
            crc_table[175] = 0x40;
            crc_table[176] = 0x1;
            crc_table[177] = 0xC0;
            crc_table[178] = 0x80;
            crc_table[179] = 0x41;
            crc_table[180] = 0x0;
            crc_table[181] = 0xC1;
            crc_table[182] = 0x81;
            crc_table[183] = 0x40;
            crc_table[184] = 0x0;
            crc_table[185] = 0xC1;
            crc_table[186] = 0x81;
            crc_table[187] = 0x40;
            crc_table[188] = 0x1;
            crc_table[189] = 0xC0;
            crc_table[190] = 0x80;
            crc_table[191] = 0x41;
            crc_table[192] = 0x0;
            crc_table[193] = 0xC1;
            crc_table[194] = 0x81;
            crc_table[195] = 0x40;
            crc_table[196] = 0x1;
            crc_table[197] = 0xC0;
            crc_table[198] = 0x80;
            crc_table[199] = 0x41;
            crc_table[200] = 0x1;
            crc_table[201] = 0xC0;
            crc_table[202] = 0x80;
            crc_table[203] = 0x41;
            crc_table[204] = 0x0;
            crc_table[205] = 0xC1;
            crc_table[206] = 0x81;
            crc_table[207] = 0x40;
            crc_table[208] = 0x1;
            crc_table[209] = 0xC0;
            crc_table[210] = 0x80;
            crc_table[211] = 0x41;
            crc_table[212] = 0x0;
            crc_table[213] = 0xC1;
            crc_table[214] = 0x81;
            crc_table[215] = 0x40;
            crc_table[216] = 0x0;
            crc_table[217] = 0xC1;
            crc_table[218] = 0x81;
            crc_table[219] = 0x40;
            crc_table[220] = 0x1;
            crc_table[221] = 0xC0;
            crc_table[222] = 0x80;
            crc_table[223] = 0x41;
            crc_table[224] = 0x1;
            crc_table[225] = 0xC0;
            crc_table[226] = 0x80;
            crc_table[227] = 0x41;
            crc_table[228] = 0x0;
            crc_table[229] = 0xC1;
            crc_table[230] = 0x81;
            crc_table[231] = 0x40;
            crc_table[232] = 0x0;
            crc_table[233] = 0xC1;
            crc_table[234] = 0x81;
            crc_table[235] = 0x40;
            crc_table[236] = 0x1;
            crc_table[237] = 0xC0;
            crc_table[238] = 0x80;
            crc_table[239] = 0x41;
            crc_table[240] = 0x0;
            crc_table[241] = 0xC1;
            crc_table[242] = 0x81;
            crc_table[243] = 0x40;
            crc_table[244] = 0x1;
            crc_table[245] = 0xC0;
            crc_table[246] = 0x80;
            crc_table[247] = 0x41;
            crc_table[248] = 0x1;
            crc_table[249] = 0xC0;
            crc_table[250] = 0x80;
            crc_table[251] = 0x41;
            crc_table[252] = 0x0;
            crc_table[253] = 0xC1;
            crc_table[254] = 0x81;
            crc_table[255] = 0x40;
            crc_table[256] = 0x0;
            crc_table[257] = 0xC0;
            crc_table[258] = 0xC1;
            crc_table[259] = 0x1;
            crc_table[260] = 0xC3;
            crc_table[261] = 0x3;
            crc_table[262] = 0x2;
            crc_table[263] = 0xC2;
            crc_table[264] = 0xC6;
            crc_table[265] = 0x6;
            crc_table[266] = 0x7;
            crc_table[267] = 0xC7;
            crc_table[268] = 0x5;
            crc_table[269] = 0xC5;
            crc_table[270] = 0xC4;
            crc_table[271] = 0x4;
            crc_table[272] = 0xCC;
            crc_table[273] = 0xC;
            crc_table[274] = 0xD;
            crc_table[275] = 0xCD;
            crc_table[276] = 0xF;
            crc_table[277] = 0xCF;
            crc_table[278] = 0xCE;
            crc_table[279] = 0xE;
            crc_table[280] = 0xA;
            crc_table[281] = 0xCA;
            crc_table[282] = 0xCB;
            crc_table[283] = 0xB;
            crc_table[284] = 0xC9;
            crc_table[285] = 0x9;
            crc_table[286] = 0x8;
            crc_table[287] = 0xC8;
            crc_table[288] = 0xD8;
            crc_table[289] = 0x18;
            crc_table[290] = 0x19;
            crc_table[291] = 0xD9;
            crc_table[292] = 0x1B;
            crc_table[293] = 0xDB;
            crc_table[294] = 0xDA;
            crc_table[295] = 0x1A;
            crc_table[296] = 0x1E;
            crc_table[297] = 0xDE;
            crc_table[298] = 0xDF;
            crc_table[299] = 0x1F;
            crc_table[300] = 0xDD;
            crc_table[301] = 0x1D;
            crc_table[302] = 0x1C;
            crc_table[303] = 0xDC;
            crc_table[304] = 0x14;
            crc_table[305] = 0xD4;
            crc_table[306] = 0xD5;
            crc_table[307] = 0x15;
            crc_table[308] = 0xD7;
            crc_table[309] = 0x17;
            crc_table[310] = 0x16;
            crc_table[311] = 0xD6;
            crc_table[312] = 0xD2;
            crc_table[313] = 0x12;
            crc_table[314] = 0x13;
            crc_table[315] = 0xD3;
            crc_table[316] = 0x11;
            crc_table[317] = 0xD1;
            crc_table[318] = 0xD0;
            crc_table[319] = 0x10;
            crc_table[320] = 0xF0;
            crc_table[321] = 0x30;
            crc_table[322] = 0x31;
            crc_table[323] = 0xF1;
            crc_table[324] = 0x33;
            crc_table[325] = 0xF3;
            crc_table[326] = 0xF2;
            crc_table[327] = 0x32;
            crc_table[328] = 0x36;
            crc_table[329] = 0xF6;
            crc_table[330] = 0xF7;
            crc_table[331] = 0x37;
            crc_table[332] = 0xF5;
            crc_table[333] = 0x35;
            crc_table[334] = 0x34;
            crc_table[335] = 0xF4;
            crc_table[336] = 0x3C;
            crc_table[337] = 0xFC;
            crc_table[338] = 0xFD;
            crc_table[339] = 0x3D;
            crc_table[340] = 0xFF;
            crc_table[341] = 0x3F;
            crc_table[342] = 0x3E;
            crc_table[343] = 0xFE;
            crc_table[344] = 0xFA;
            crc_table[345] = 0x3A;
            crc_table[346] = 0x3B;
            crc_table[347] = 0xFB;
            crc_table[348] = 0x39;
            crc_table[349] = 0xF9;
            crc_table[350] = 0xF8;
            crc_table[351] = 0x38;
            crc_table[352] = 0x28;
            crc_table[353] = 0xE8;
            crc_table[354] = 0xE9;
            crc_table[355] = 0x29;
            crc_table[356] = 0xEB;
            crc_table[357] = 0x2B;
            crc_table[358] = 0x2A;
            crc_table[359] = 0xEA;
            crc_table[360] = 0xEE;
            crc_table[361] = 0x2E;
            crc_table[362] = 0x2F;
            crc_table[363] = 0xEF;
            crc_table[364] = 0x2D;
            crc_table[365] = 0xED;
            crc_table[366] = 0xEC;
            crc_table[367] = 0x2C;
            crc_table[368] = 0xE4;
            crc_table[369] = 0x24;
            crc_table[370] = 0x25;
            crc_table[371] = 0xE5;
            crc_table[372] = 0x27;
            crc_table[373] = 0xE7;
            crc_table[374] = 0xE6;
            crc_table[375] = 0x26;
            crc_table[376] = 0x22;
            crc_table[377] = 0xE2;
            crc_table[378] = 0xE3;
            crc_table[379] = 0x23;
            crc_table[380] = 0xE1;
            crc_table[381] = 0x21;
            crc_table[382] = 0x20;
            crc_table[383] = 0xE0;
            crc_table[384] = 0xA0;
            crc_table[385] = 0x60;
            crc_table[386] = 0x61;
            crc_table[387] = 0xA1;
            crc_table[388] = 0x63;
            crc_table[389] = 0xA3;
            crc_table[390] = 0xA2;
            crc_table[391] = 0x62;
            crc_table[392] = 0x66;
            crc_table[393] = 0xA6;
            crc_table[394] = 0xA7;
            crc_table[395] = 0x67;
            crc_table[396] = 0xA5;
            crc_table[397] = 0x65;
            crc_table[398] = 0x64;
            crc_table[399] = 0xA4;
            crc_table[400] = 0x6C;
            crc_table[401] = 0xAC;
            crc_table[402] = 0xAD;
            crc_table[403] = 0x6D;
            crc_table[404] = 0xAF;
            crc_table[405] = 0x6F;
            crc_table[406] = 0x6E;
            crc_table[407] = 0xAE;
            crc_table[408] = 0xAA;
            crc_table[409] = 0x6A;
            crc_table[410] = 0x6B;
            crc_table[411] = 0xAB;
            crc_table[412] = 0x69;
            crc_table[413] = 0xA9;
            crc_table[414] = 0xA8;
            crc_table[415] = 0x68;
            crc_table[416] = 0x78;
            crc_table[417] = 0xB8;
            crc_table[418] = 0xB9;
            crc_table[419] = 0x79;
            crc_table[420] = 0xBB;
            crc_table[421] = 0x7B;
            crc_table[422] = 0x7A;
            crc_table[423] = 0xBA;
            crc_table[424] = 0xBE;
            crc_table[425] = 0x7E;
            crc_table[426] = 0x7F;
            crc_table[427] = 0xBF;
            crc_table[428] = 0x7D;
            crc_table[429] = 0xBD;
            crc_table[430] = 0xBC;
            crc_table[431] = 0x7C;
            crc_table[432] = 0xB4;
            crc_table[433] = 0x74;
            crc_table[434] = 0x75;
            crc_table[435] = 0xB5;
            crc_table[436] = 0x77;
            crc_table[437] = 0xB7;
            crc_table[438] = 0xB6;
            crc_table[439] = 0x76;
            crc_table[440] = 0x72;
            crc_table[441] = 0xB2;
            crc_table[442] = 0xB3;
            crc_table[443] = 0x73;
            crc_table[444] = 0xB1;
            crc_table[445] = 0x71;
            crc_table[446] = 0x70;
            crc_table[447] = 0xB0;
            crc_table[448] = 0x50;
            crc_table[449] = 0x90;
            crc_table[450] = 0x91;
            crc_table[451] = 0x51;
            crc_table[452] = 0x93;
            crc_table[453] = 0x53;
            crc_table[454] = 0x52;
            crc_table[455] = 0x92;
            crc_table[456] = 0x96;
            crc_table[457] = 0x56;
            crc_table[458] = 0x57;
            crc_table[459] = 0x97;
            crc_table[460] = 0x55;
            crc_table[461] = 0x95;
            crc_table[462] = 0x94;
            crc_table[463] = 0x54;
            crc_table[464] = 0x9C;
            crc_table[465] = 0x5C;
            crc_table[466] = 0x5D;
            crc_table[467] = 0x9D;
            crc_table[468] = 0x5F;
            crc_table[469] = 0x9F;
            crc_table[470] = 0x9E;
            crc_table[471] = 0x5E;
            crc_table[472] = 0x5A;
            crc_table[473] = 0x9A;
            crc_table[474] = 0x9B;
            crc_table[475] = 0x5B;
            crc_table[476] = 0x99;
            crc_table[477] = 0x59;
            crc_table[478] = 0x58;
            crc_table[479] = 0x98;
            crc_table[480] = 0x88;
            crc_table[481] = 0x48;
            crc_table[482] = 0x49;
            crc_table[483] = 0x89;
            crc_table[484] = 0x4B;
            crc_table[485] = 0x8B;
            crc_table[486] = 0x8A;
            crc_table[487] = 0x4A;
            crc_table[488] = 0x4E;
            crc_table[489] = 0x8E;
            crc_table[490] = 0x8F;
            crc_table[491] = 0x4F;
            crc_table[492] = 0x8D;
            crc_table[493] = 0x4D;
            crc_table[494] = 0x4C;
            crc_table[495] = 0x8C;
            crc_table[496] = 0x44;
            crc_table[497] = 0x84;
            crc_table[498] = 0x85;
            crc_table[499] = 0x45;
            crc_table[500] = 0x87;
            crc_table[501] = 0x47;
            crc_table[502] = 0x46;
            crc_table[503] = 0x86;
            crc_table[504] = 0x82;
            crc_table[505] = 0x42;
            crc_table[506] = 0x43;
            crc_table[507] = 0x83;
            crc_table[508] = 0x41;
            crc_table[509] = 0x81;
            crc_table[510] = 0x80;
            crc_table[511] = 0x40;
            #endregion
        }

        internal static void GetCRC16(byte[] puchMsg, ref byte[] CRC)
        {
            int usDataLen = puchMsg.Length;
            byte uchCRCHi = 0xFF, uchCRCLo = 0xFF;

            int uIndex;
            for (int i = 0; i < usDataLen; i++)
            {
                uIndex = uchCRCLo ^ puchMsg[i];
                uchCRCLo = (byte)(uchCRCHi ^ auchCRCHi[uIndex]);
                uchCRCHi = auchCRCLo[uIndex];
            }
            CRC[1] = uchCRCLo;
            CRC[0] = uchCRCHi;
        }

        #region
        static byte[] auchCRCHi = {
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
0x40
};

        static byte[] auchCRCLo = {
0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
0x40
};
        #endregion
        #endregion

        #region  CRC校验
        private static readonly byte[] aucCRCHi = {
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40
         };
        private static readonly byte[] aucCRCLo = {
             0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
             0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
             0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
             0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
             0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
             0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
             0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
             0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
             0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
             0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
             0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
             0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
             0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
             0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
             0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
             0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
             0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
             0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
             0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
             0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
             0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
             0x41, 0x81, 0x80, 0x40
         };
        public static byte[] Crc16(byte[] pucFrame, int usLen)
        {
            int i = 0;
            byte crcHi = 0xFF;
            byte crcLo = 0xFF;
            UInt16 iIndex = 0x0000;

            while (usLen-- > 0)
            {
                iIndex = (UInt16)(crcLo ^ pucFrame[i++]);
                crcLo = (byte)(crcHi ^ aucCRCHi[iIndex]);
                crcHi = aucCRCLo[iIndex];
            }

            return new byte[] { crcLo, crcHi };
        }


        #endregion
        #region 
        /// <summary>
        /// 验证 Modbus RTU 消息的 CRC 校验码
        /// </summary>
        /// <param name="data">包含完整消息的字节数组（包括CRC校验码）</param>
        /// <returns>如果CRC校验通过返回true，否则返回false</returns>
        public bool CheckCrc(byte[] data)
        {
            // 检查数据长度是否足够包含CRC
            if (data == null || data.Length < 3) // 至少需要3字节：地址+功能码+CRC
            {
                return false;
            }

            // 提取消息内容（不包括CRC部分）
            byte[] message = new byte[data.Length - 2];
            Array.Copy(data, 0, message, 0, message.Length);

            // 计算消息的CRC
            byte[] calculatedCrc = CalculateCrc(message);

            // 获取接收到的CRC（最后2字节）
            byte receivedCrcLow = data[data.Length - 2];
            byte receivedCrcHigh = data[data.Length - 1];

            // 比较计算出的CRC和接收到的CRC
            return (calculatedCrc[0] == receivedCrcLow) &&
                   (calculatedCrc[1] == receivedCrcHigh);


        }


        /// <summary>
        /// 计算 Modbus RTU 消息的 CRC 校验码
        /// </summary>
        /// <param name="data">需要计算CRC的消息内容</param>
        /// <returns>包含CRC低字节和高字节的数组</returns>
        public byte[] CalculateCrc(byte[] data)
        {
            ushort crc = 0xFFFF;

            foreach (byte b in data)
            {
                crc ^= b;

                for (int i = 0; i < 8; i++)
                {
                    bool lsbSet = (crc & 0x0001) == 0x0001;
                    crc >>= 1;

                    if (lsbSet)
                    {
                        crc ^= 0xA001; // CRC多项式 (x^16 + x^15 + x^2 + 1)
                    }
                }
            }

            // 返回CRC（低字节在前，高字节在后）
            return new byte[]
            {
        (byte)(crc & 0xFF),       // 低字节
        (byte)((crc >> 8) & 0xFF) // 高字节
            };
        }
        #endregion


        #region 锁


        private Int32 m_waiters = 0;
        private AutoResetEvent m_waiterLock = new AutoResetEvent(false);


        public void Enter() {

            if (Interlocked.Increment(ref m_waiters) == 1) return;
            m_waiterLock.WaitOne();
        }


        public void Leave() {


            if (Interlocked.Decrement(ref m_waiters) == 0) return;
            m_waiterLock.Set();
        
        }


        #endregion





    }
}
