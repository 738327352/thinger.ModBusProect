using excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace excelOperation
{
    public partial class Form2 : Form
    {
        public Form2()
        {

            InitializeComponent();
            InitializeForm();   
        }

        private void Change(object sender, EventArgs e)
        {
            //更改设置
            EnabledForm(true);

        }

        private void Saving(object sender, EventArgs e)
        {
            //保存设置
            EnabledForm(false);
            Properties.Settings.Default.ExecelFileInput = ExcelFileInput.Text;
            Properties.Settings.Default.PortModelInput = PortModelInput.Text;
            Properties.Settings.Default.Save();
        }
        private void ExecuteExcelOperation(object sender, EventArgs e)
        {
            //执行Excel操作
            Operation();
        }
        private void NewPortFile(object sender, EventArgs e)
        {
            //创建新文件 -----------------------------Coding
            CreateFile();
        }
        public void InitializeForm()
        {
            EnabledForm(false);
            ExcelFileInput.Text = Properties.Settings.Default.ExecelFileInput;
            PortModelInput.Text = Properties.Settings.Default.PortModelInput;
        }
        private void EnabledForm(bool flag)
        {
            // WF中控件的启用与禁用
            ExcelFileInput.Enabled = flag;
            SavingBtn.Enabled = flag;
            PortModelInput.Enabled = flag;
            ExecuteBtn.Enabled = !flag;
            ChangeBtn.Enabled = !flag;
        }
        public void Operation()
        {
            // 创建Excel应用
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true; // 不隐藏Excel窗口

            try
            {
                // 打开工作簿
                excel.Workbook workbook = excelApp.Workbooks.Open(ExcelFileInput.Text);

                excel.Worksheet worksheet = workbook.Sheets[1]; // 获取第一个工作表

                // 读取数据
                excel.Range range = worksheet.Range["A1"];
                string value = range.Value?.ToString();
                Console.WriteLine($"A1单元格的值: {value}");

                // 写入数据
                worksheet.Range["B1"].Value = "Hello Excel";
                //worksheet.Range["A2:C5"].Value = "批量填充";

                // 保存
                workbook.Save();
                workbook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作Excel时出错: {ex.Message}  \n恁🐥八不看看文件地址对不对" );
            }
            finally
            {
                excelApp.Quit();
                // 释放COM对象
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        private void CreateFile()
        {

        }


    }
}
