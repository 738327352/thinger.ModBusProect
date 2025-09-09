using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.BLL;
using thinger.Models;

namespace thinger.ProConfigSys
{
    public partial class FrmLoagin : Form
    {
        public FrmLoagin()
        {
            InitializeComponent();

        }
        private SysAdminsManager sysAdminsManager = new SysAdminsManager();
        private void btn_LoadSystem_Click(object sender, EventArgs e)
        {
            if (this.txt_LoadAccount.Text.Trim().Length == 0 || this.txt_LoadPwd.Text.Trim().Length == 0)
            {

                MessageBox.Show("账号或密码为空请输入账号密码", "错误提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
            SysAdmins sysAdmins = new SysAdmins()
            {

                SysAccount = this.txt_LoadAccount.Text,
                AdminPwd = this.txt_LoadPwd.Text,

            };

            try
            {
                sysAdmins = sysAdminsManager.AdminLogin(sysAdmins);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"登录错误");
                return;
            }

            if (sysAdmins != null)
            {

                Program.currentSysAdmins = sysAdmins;

                //此处可以记录登录信息
                //账号当前信息
                //
            
            this.DialogResult = DialogResult.OK;
            }
            else {
                MessageBox.Show("账号密码错误", "错误提示");
            }



        }

        private void check_(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {
                if (Program.currentSysAdmins != null)
                {

                Program.lastSysAdmins = Program.currentSysAdmins;

                }


        }
    }
}
