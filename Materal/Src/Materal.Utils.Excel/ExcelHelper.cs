using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Materal.Utils.Excel
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// 读取Excel到工作簿
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns>工作簿对象</returns>
        public static IWorkbook ReadExcelToWorkbook(string filePath)
        {
            if (!File.Exists(filePath)) throw new UtilException("文件不存在");
            using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read);
            IWorkbook result = ReadExcelToWorkbook(fs);
            return result;
        }
        /// <summary>
        /// 读取Excel到工作簿
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <returns>工作簿对象</returns>
        public static IWorkbook ReadExcelToWorkbook(FileStream fileStream)
        {
            string extensionName = Path.GetExtension(fileStream.Name).ToLower();
            IWorkbook workbook = extensionName switch
            {
                ".xlsx" => new XSSFWorkbook(fileStream),
                ".xls" => new HSSFWorkbook(fileStream),
                _ => throw new UtilException($"文件{fileStream.Name}不是Excel文件"),
            };
            return workbook;
        }
    }
}
