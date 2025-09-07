using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.Models
{
     class CommunicationsGroup
    {
        public int CGid { get; set; }
        public int EquipmentId { get; set; }
        public string CGName { get; set; } = string.Empty;
        public string  StarAddress { get; set; } = string.Empty;

        public int CGLength { get; set; }


        public int IsEnable { get; set; }
        public string Comments { get; set; } = string.Empty;


    }
}
