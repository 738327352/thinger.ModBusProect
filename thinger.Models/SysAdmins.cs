using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.Models
{
    [Serializable]
    public class SysAdmins
    {
        public int SysAdminnsId { get; set; }

        public String SysAccount { get; set; }
        public String AdminName { get; set; }
        public String AdminPwd { get; set; }

    }
}
