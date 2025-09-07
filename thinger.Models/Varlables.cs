using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.Models
{
     class Varlables
    {
        public int VarableId { get; set; }
        public string VarableName { get; set; }
        public int CGid { get; set; }
        public string DataType { get; set; }
        public bool IsMaxAlarm { get; set; } = false;
        public bool IsMinAlarm { get; set; } = false;
        public string AlarmMax { get; set; } = string.Empty; //端口号针对以太网
        public string AlarmMin { get; set; } = string.Empty;//串口号
        public string IsFiled { get; set; } = string.Empty;//波特率
        public string AlarmMaxNode { get; set; } = string.Empty;//数据位
        public float Scale { get; set; } = 0; //校验位
        public float Offsent { get; set; } = 0;
        public string Comments { get; set; } = string.Empty;

      










    }
}
