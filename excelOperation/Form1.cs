using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace excelOperation
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView1;
        private Button btnOpenExcel;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // 初始化 DataGridView
            dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;

            // 初始化打开按钮
            btnOpenExcel = new Button();
            btnOpenExcel.Text = "打开Excel文件";
            btnOpenExcel.Dock = DockStyle.Top;
            btnOpenExcel.Click += BtnOpenExcel_Click;

            // 添加控件到窗体
            this.Controls.Add(dataGridView1);
            this.Controls.Add(btnOpenExcel);
        }

        private void BtnOpenExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Excel文件|*.xlsx;*.xls";
                    ofd.Title = "选择Excel文件";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Excel.Application excelApp = new Excel.Application();
                        Excel.Workbook workbook = excelApp.Workbooks.Open(ofd.FileName);
                        Excel.Worksheet worksheet = workbook.Sheets[1]; // 获取第一个工作表
                        Excel.Range range = worksheet.UsedRange;

                        // 获取数据范围
                        int rows = range.Rows.Count;
                        int cols = range.Columns.Count;

                        // 清除旧数据
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();

                        // 添加列
                        for (int i = 1; i <= cols; i++)
                        {
                            string columnName = Convert.ToString((range.Cells[1, i] as Excel.Range).Value2);
                            dataGridView1.Columns.Add(columnName, columnName);
                        }

                        // 添加数据行
                        for (int r = 2; r <= rows; r++)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);

                            for (int c = 1; c <= cols; c++)
                            {
                                row.Cells[c - 1].Value = (range.Cells[r, c] as Excel.Range).Value2;
                            }

                            dataGridView1.Rows.Add(row);
                        }

                        // 清理COM对象
                        workbook.Close();
                        excelApp.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取Excel文件时发生错误：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
