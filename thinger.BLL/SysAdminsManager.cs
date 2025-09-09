using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using thinger.Dal;
using thinger.Models;
namespace thinger.BLL
{
    public class SysAdminsManager
    {

        private SysAdminsService sysAdminsService = new SysAdminsService();

        public SysAdmins AdminLogin(SysAdmins Admin)
        {

            return sysAdminsService.AdminLogin(Admin);

        }


        public void SaveAdmin(SysAdmins admin)
        {

           sysAdminsService.SaveAdmin(admin);


        }

        public SysAdmins ReadAdmin()
        {

            return sysAdminsService.ReadPwd();


        }

        public void Deleate()
        {

            sysAdminsService.DeleatePwd();


        }
    }
}
