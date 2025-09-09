using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.Models;

namespace thinger.ProConfigSys
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLoagin loagin = new FrmLoagin();
       
            DialogResult result = loagin.ShowDialog();

            if (result == DialogResult.OK)
            {

                Application.Run(new FrmMain());
            }
            else { 
            
            Application.Exit();
            }



           
            

          
            
            
            
            








        }

        public static SysAdmins currentSysAdmins = null;

        public static SysAdmins lastSysAdmins = null;

    }
}
