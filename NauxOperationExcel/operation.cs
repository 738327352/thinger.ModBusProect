using System;
using System.IO;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

public class ExcelDataProcessor
{
    public static void Main(string[] args)
    {
        // --- 设置文件路径 ---
        string filePath = Path.GetFullPath("E:\\苏州组宁\\公司文件\\付工任务\\特性参数\\20259-21\\二期在线主数据.xls");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"错误：文件 '{filePath}' 未找到。");
            return;
        }

        // --- 声明 Excel Interop 对象 ---
        Excel.Application excelApp = null;
        Excel.Workbook workbook = null;
        Excel.Worksheet sourceSheet = null;
        Excel.Worksheet resultSheet = null;

        try
        {
            // --- 1. 启动 Excel 应用程序 ---
            excelApp = new Excel.Application();
            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;

            // --- 2. 打开工作簿 ---
            workbook = excelApp.Workbooks.Open(filePath);

            // --- 3. 获取源工作表和结果工作表 ---
            sourceSheet = (Excel.Worksheet)workbook.Sheets[1];

            string resultSheetName = "提取结果";
            resultSheet = GetOrCreateSheet(workbook, resultSheetName);

            // --- 4. 遍历C列并提取数据 ---
            int sourceRow = 9; // 从C9开始
            int resultRow = 1; // 结果写入从第一行开始
            int successCount = 0;

            // 正则表达式模式
            string pattern = @"(\d+)-(AT)-([A-Z0-9]+)";
            Regex regex = new Regex(pattern);

            Console.WriteLine($"开始处理工作表 '{sourceSheet.Name}' 的C列数据...");

            while (true)
            {
                Excel.Range sourceCell = sourceSheet.Range[$"C{sourceRow}"];
                string cellValue = (sourceCell.Value2 as string)?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(cellValue))
                {
                    // 遇到空单元格，停止处理
                    Console.WriteLine($"在C{sourceRow}处遇到空单元格，停止处理。");
                    break;
                }

                Match match = regex.Match(cellValue);

                if (match.Success)
                {
                    string part1 = match.Groups[1].Value;
                    string part2 = match.Groups[2].Value;
                    string part3 = match.Groups[3].Value;

                    // 写入数据到结果工作表
                    resultSheet.Range[$"A{resultRow}"].Value2 = part1;
                    resultSheet.Range[$"B{resultRow}"].Value2 = part2;
                    resultSheet.Range[$"C{resultRow}"].Value2 = part3;
                    resultSheet.Range[$"D{resultRow}"].Value2 = cellValue; // **新增：将原始值写入D列**

                    Console.WriteLine($"已从 C{sourceRow} 成功提取并写入到 A{resultRow}, B{resultRow}, C{resultRow}，原始值写入D{resultRow}。");
                    successCount++;
                    resultRow++;
                }
                else
                {
                    Console.WriteLine($"C{sourceRow}单元格内容 '{cellValue}' 不匹配预定格式，跳过。");
                }

                // 释放当前单元格的COM对象
                Marshal.ReleaseComObject(sourceCell);
                sourceRow++;
            }

            Console.WriteLine($"\n处理完成！总共提取了 {successCount} 条记录。");

            // --- 5. 保存并关闭工作簿 ---
            workbook.Save();
            Console.WriteLine($"结果已成功保存到 '{filePath}' 的 '{resultSheetName}' 工作表中。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"处理过程中发生错误: {ex.Message}");
        }
        finally
        {
            // --- 6. 清理和释放资源 (非常重要！) ---
            if (workbook != null)
            {
                workbook.Close(false);
            }
            if (excelApp != null)
            {
                excelApp.Quit();
            }

            if (sourceSheet != null) Marshal.ReleaseComObject(sourceSheet);
            if (resultSheet != null) Marshal.ReleaseComObject(resultSheet);
            if (workbook != null) Marshal.ReleaseComObject(workbook);
            if (excelApp != null) Marshal.ReleaseComObject(excelApp);

            // 为了确保所有COM对象都被释放，可以调用垃圾回收
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("资源已释放。");
        }
    }

    /// <summary>
    /// 获取指定名称的工作表，如果不存在则创建它。
    /// </summary>
    private static Excel.Worksheet GetOrCreateSheet(Excel.Workbook workbook, string sheetName)
    {
        Excel.Worksheet sheet = null;
        try
        {
            sheet = (Excel.Worksheet)workbook.Sheets[sheetName];
        }
        catch
        {
            // 工作表不存在，创建它
            sheet = (Excel.Worksheet)workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
            sheet.Name = sheetName;
            Console.WriteLine($"已创建新的工作表: '{sheetName}'");
        }
        return sheet;
    }
}