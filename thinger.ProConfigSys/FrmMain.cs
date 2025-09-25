using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.BLL;
using thinger.Models;

namespace thinger.ProConfigSys
{
    public partial class FrmMain : Form
    {
        private ProjectManager projectManager = new ProjectManager();

        public FrmCreateItem frmCreateItem = new FrmCreateItem();
        //将新增的项目存储在集合中
        private List<Projects> projectsList = new List<Projects>();
        public FrmMain()
        {
            InitializeComponent();

            this.dgv_Projects.AutoGenerateColumns = false;
            //初始化展示列表信息
            projectsList = projectManager.GetAllProjects();

            if (projectsList != null)
            {

                this.dgv_Projects.DataSource = projectsList;
                this.btn_Deleate.Enabled = this.btn_ReName.Enabled = true;

                // 禁止用户改变DataGridView的所有列的列宽   
                this.dgv_Projects.AllowUserToResizeColumns = false;
                //禁止用户改变DataGridView所有行的行高   
                this.dgv_Projects.AllowUserToResizeRows = false;
                // 禁止用户改变列头的高度   
                this.dgv_Projects.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                // 禁止用户改变列头的宽度   
                this.dgv_Projects.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            }
            else {
                this.dgv_Projects.DataSource = null;


            }
        }





        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要退出吗？", "退出确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Cancel || result == DialogResult.No) {
                e.Cancel = true; //窗体取消关闭            
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {


        }


        private void btn_AddProject_Click(object sender, EventArgs e)
        {
            FrmCreateItem frmCreateItem = new FrmCreateItem();
            DialogResult result = frmCreateItem.ShowDialog();

            //数据验证

            if (result == DialogResult.OK) {

                Projects newProject = (Projects)frmCreateItem.Tag;
                newProject.SN = this.projectsList.Count + 1;
                projectsList.Add((newProject));
                this.dgv_Projects.DataSource = null;
                this.dgv_Projects.DataSource = this.projectsList;
            }

        }

        private void btn_ReName_Click(object sender, EventArgs e)
        {
            Projects projects = new Projects()
            {
                projectName = (string)this.dgv_Projects.CurrentRow.Cells["ProjectName"].Value.ToString(),
                ProjectId = (int)this.dgv_Projects.CurrentRow.Cells["ProjectId"].Value,
                SN = this.projectsList.Count,

            };
            FrmCreateItem reName = new FrmCreateItem(projects);

            




            if (dgv_Projects.SelectedRows == null) {

                MessageBox.Show("请选择要修改的项目");
                return;
            } else {  
                 
               
                

                projectManager.Update(projects);
            }
        }






    }




            //if (this.dgv_Projects.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("请选择要修改的项目");
            //    return;
            //}
            //int projectId = (int)this.dgv_Projects.SelectedRows[0].Cells["ProjectId"].Value;
            //Projects oldProject = projectManager.GetProjectById(projectId);
            //if (oldProject == null)
            //{
            //    MessageBox.Show("未找到该项目，可能已被删除");
            //    return;
            //}
            //FrmReName frmReName = new FrmReName(oldProject);
            //DialogResult result = frmReName.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    Projects updatedProject = (Projects)frmReName.Tag;
            //    // 更新列表中的项目名称
            //    var projectInList = projectsList.FirstOrDefault(p => p.ProjectId == updatedProject.ProjectId);
            //    if (projectInList != null)
            //    {
            //        projectInList.projectName = updatedProject.projectName;
            //    }
            //    this.dgv_Projects.DataSource = null;
            //    this.dgv_Projects.DataSource = this.projectsList;
            //}

        }
    

