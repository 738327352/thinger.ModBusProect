using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using thinger.Dal;
using thinger.Models;
namespace thinger.BLL
{
    public class SysAdminsManager
    {

        private SysAdminsService sysAdminsService = new SysAdminsService();

        public SysAdmins AdminLogin(SysAdmins Admin) { 
        
        return sysAdminsService.AdminLogin(Admin);
        
        }


    }
}
