using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Materal.ConvertHelper;
using Materal.ExcelHelper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            var tests = new List<TestClass>
            {
                new TestClass
                {
                    Name = "Name1",
                    Content = "Content1"
                },
                new TestClass
                {
                    Name = "Name2",
                    Content = "Content2"
                }
            };


            var dataTable = tests.ToDataTable();
            dataTable.TableName = "表格1";
            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            var manager = new ExcelManager();
            IWorkbook workbook = manager.DataSetToWorkbook<XSSFWorkbook>(dataSet, null, TableHeadConfig);
            const string filePath = @"D:\Test.xlsx";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using var streamWriter = new FileStream(filePath, FileMode.Create);
            workbook.Write(streamWriter);
        }

        public static int TableHeadConfig(IWorkbook workbook, ISheet sheet)
        {
            IRow row = sheet.CreateRow(0);
            ICell nameCell = row.CreateCell(0);
            nameCell.SetCellValue("姓名");
            ICell contentCell = row.CreateCell(1);
            contentCell.SetCellValue("内容");
            return 1;
        }
    }

    public class TestClass
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
