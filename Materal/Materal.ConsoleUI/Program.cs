using System.Data;
using Materal.ExcelHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            var excelManager = new ExcelManager();
            DataSet excelData = excelManager.ReadExcelToDataSet(@"D:\Test.xlsx", 0);
        }
    }
}
