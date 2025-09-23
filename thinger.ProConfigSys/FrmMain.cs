using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thinger.ProConfigSys
{
    public partial class FrmMain : Form
    {
        public FrmCreateItem frmCreateItem = new FrmCreateItem();
        public FrmMain()
        {
            InitializeComponent();
          
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
           DialogResult result =  MessageBox.Show("你确定要退出吗？","退出确认",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Cancel  ||result == DialogResult.No) { 
            e.Cancel = true; //窗体取消关闭
            
            }

            
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            

        }

        private void btn_AddProject_Click(object sender, EventArgs e)
        {
      
            //数据验证


            //封装对象


            //调用业务



          DialogResult result = frmCreateItem.ShowDialog();




        }
    }
}
