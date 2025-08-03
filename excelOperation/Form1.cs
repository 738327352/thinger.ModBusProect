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
        private DateTimePicker datePicker;
        private TextBox textBox;
        private Panel topPanel;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // 创建顶部面板用于容纳控件
            topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100
            };

            // 初始化日期选择器
            datePicker = new TimePicker
            {
                Location = new Point(10, 10),
                Size = new Size(200, 25),
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true
            };

            // 初始化文本框
            textBox = new TextBox
            {
                Location = new Point(10, 40),
                Size = new Size(200, 25),
                ForeColor = SystemColors.GrayText,
                Text = "请输入..."
            };

            // 添加事件处理
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == "请输入...")
                {
                    textBox.Text = "";
                    textBox.ForeColor = SystemColors.WindowText;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "请输入...";
                    textBox.ForeColor = SystemColors.GrayText;
                }
            };

            // 初始化打开按钮
            btnOpenExcel = new Button
            {
                Text = "打开Excel文件",
                Location = new Point(10, 70),
                Size = new Size(200, 25)
            };
            btnOpenExcel.Click += BtnOpenExcel_Click;

            // 初始化 DataGridView
            dataGridView1 = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            // 将控件添加到顶部面板
            topPanel.Controls.Add(datePicker);
            topPanel.Controls.Add(textBox);
            topPanel.Controls.Add(btnOpenExcel);

            // 将面板和DataGridView添加到窗体
            this.Controls.Add(dataGridView1);
            this.Controls.Add(topPanel);

            // 设置窗体的最小尺寸
            this.MinimumSize = new Size(400, 300);
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
