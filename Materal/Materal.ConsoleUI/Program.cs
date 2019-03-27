using Materal.ExcelHelper;
using Materal.NetworkHelper;
using System.Data;
using System.IO;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            var excelManager = new ExcelManager();
            byte[] body = HttpManager.SendGetBytes("http://192.168.0.125:8903/UploadFiles/敏感词.xlsx");
            using (var fileStream = new FileStream("敏感词.xlsx", FileMode.OpenOrCreate))
            {
                using (var binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(body);
                }
            }
            DataSet excelData = excelManager.ReadExcelToDataSet("敏感词.xlsx", 0);
            File.Delete("敏感词.xlsx");
        }
    }
}
