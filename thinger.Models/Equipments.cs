using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.Models
{
     class Equipments
    {
        public int EquipmentId { get; set; }
        public int ProjectId { get; set; }
        public int EtypeId { get; set; }
        public int PTyepeId { get; set; }
        public string EquipmentName { get; set; }=string.Empty;
        public string IPAddress { get; set; } = string.Empty;
        public string PortNo { get; set; } = string.Empty; //端口号针对以太网
        public string SerialNo { get; set; } = string.Empty;//串口号
        public int BaudRate { get; set; } = 0;//波特率
        public int DataBit { get; set; } = 0;//数据位
        public string ParityBit { get; set; } = string.Empty; //校验位
        public int StopBit { get; set; } = 0;
        public string OPCNodeName { get; set; } = string.Empty;

        public string OPCServerName { get; set; } = string.Empty;

        public int IsEnable { get; set; } = 0;
        public string Comments { get; set; } = string.Empty;


        //扩展属性
        public string ETypeName { get; set; }
        public string PTypeName { get; set; }











    }
}
