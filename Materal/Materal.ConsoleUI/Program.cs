using Materal.ExcelHelper;
using Materal.NetworkHelper;
using System.Data;
using System.IO;
using System.Text;
using Materal.FileHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            //var excelManager = new ExcelManager();
            //byte[] body = HttpManager.SendGetBytes("http://192.168.0.125:8903/UploadFiles/敏感词.xlsx");
            //using (var fileStream = new FileStream("敏感词.xlsx", FileMode.OpenOrCreate))
            //{
            //    using (var binaryWriter = new BinaryWriter(fileStream))
            //    {
            //        binaryWriter.Write(body);
            //    }
            //}
            //DataSet excelData = excelManager.ReadExcelToDataSet("敏感词.xlsx", 0);
            //File.Delete("敏感词.xlsx");
            TextFileManager.WriteText(@"D:\Text.txt", GetDomainFileContent("Authority"), Encoding.UTF8);
        }
        private static string GetDomainFileContent(string subSystemName)
        {
            string result = "using Domain;\r\n";
            result += "using System;\r\n";
            result += "using System.Collections.Generic;\r\n";
            result += $"namespace {subSystemName}.Domain\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// 哈哈\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public sealed class ActionAuthority : BaseEntity<ActionAuthority>\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
    }
}
