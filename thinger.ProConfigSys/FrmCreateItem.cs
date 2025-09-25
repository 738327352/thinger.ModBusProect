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
       private  ProjectManager projectManager = new ProjectManager();
        public FrmCreateItem()
        {
            InitializeComponent();
        
        }


        public FrmCreateItem(Projects projects):this(){
            this.btnSave.Text = "重命名";
            this.Text = "重命名项目";

        
        
        
        
        
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

            if (projectManager.IsRepeatForInster(projects))
            {
                MessageBox.Show("该对象已存在");

            }
            else {
                try
                {
                    projects.ProjectId = projectManager.Insert(projects);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("添加失败，原因：" + ex.Message);
                    return;
                }
                this.Tag= projects;
                

                MessageBox.Show("添加成功");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
           


        }


            //调用业务
           






    }
    }

