using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.Models;
using thinger.BLL;

namespace thinger.ProConfigSys
{
    public partial class FrmCreateItem : Form
    {
        public FrmCreateItem()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //数据验证

            if (this.txt_ProjectName.Text == null) {
                MessageBox.Show("请输入项目名称");
                    this.txt_ProjectName.Focus();
                return; }

            //封装对象
          
            ;
            Projects projects = new Projects(){
                
                projectName = this.txt_ProjectName.Text.Trim()

            };
          
            ProjectManager projectManager = new ProjectManager();




            if (projectManager.IsRepeatForInster(projects))
            {
                MessageBox.Show("该对象已存在");

            }
            else MessageBox.Show("该对象可以使用");
        }


            //调用业务
           






    }
    }

