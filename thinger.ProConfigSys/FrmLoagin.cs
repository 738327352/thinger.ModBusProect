using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
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

            SysAdmins adminsObj = sysAdminsManager.ReadAdmin();

            if (File.Exists("sysAdmins.obj"))
            {

                this.txt_LoadAccount.Text = adminsObj.SysAccount;
                this.txt_LoadPwd.Text = adminsObj.AdminPwd;
                this.checkBox1.Checked = true;



            }
            else {
                this.txt_LoadAccount.Text = null;
                this.txt_LoadPwd.Text = null;
                this.checkBox1.Checked = true;
                this.txt_LoadAccount.Focus();
            }

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
                MessageBox.Show(ex.Message, "登录错误");
                return;
            }

            if (sysAdmins != null)
            {

                Program.currentSysAdmins = sysAdmins;

                //此处可以记录登录信息
                //账号当前信息
                //

                this.DialogResult = DialogResult.OK;

                if (this.checkBox1.Checked)
                {


                    sysAdminsManager.SaveAdmin(sysAdmins);


                }
                else
                {
                    sysAdminsManager.Deleate();
                }

            }
            else
            {
                MessageBox.Show("账号密码错误", "错误提示");
            }



        }

        private void check_(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {

            SysAdmins admins = new SysAdmins()
            {
                AdminPwd = this.txt_LoadPwd.Text,
                SysAccount = this.txt_LoadAccount.Text,

            };
           
        }
    }
}
