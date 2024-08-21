using NPOI.SS.UserModel;

namespace Materal.Utils.Excel
{
    /// <summary>
    /// 工作簿扩展类
    /// </summary>
    public static class IWorkbookExtension
    {
        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="filePath"></param>
        public static void SaveAs(this IWorkbook workbook, string filePath)
        {
            FileInfo fileInfo = new(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            using FileStream fileStream = fileInfo.Open(FileMode.OpenOrCreate);
            workbook.Write(fileStream, false);
        }
    }
}
