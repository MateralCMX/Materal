using Materal.Utils.Excel;
using NPOI.SS.UserModel;

namespace Materal.Test.UtilsTests.ExcelTests
{
    /// <summary>
    /// Excel测试
    /// </summary>
    [TestClass]
    public class ExcelTest : MateralTestBase
    {
        private readonly string excelFilePath = Path.Combine(Environment.CurrentDirectory, "UtilsTests", "ExcelTests", "Data.xlsx");
        private readonly string newExcelFilePath = Path.Combine(Environment.CurrentDirectory, "UtilsTests", "ExcelTests", "NewData.xlsx");
        /// <summary>
        /// 打开和保存文件
        /// </summary>
        [TestMethod]
        public void OpenAndSave()
        {
            IWorkbook workbook = ExcelHelper.ReadExcelToWorkbook(excelFilePath);
            workbook.SaveAs(newExcelFilePath);
        }
        /// <summary>
        /// 转换为模型
        /// </summary>
        [TestMethod]
        public void ConvertToModel()
        {
            IWorkbook workbook = ExcelHelper.ReadExcelToWorkbook(excelFilePath);
            ISheet sheet = workbook.GetSheetAt(1);
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                Assert.IsTrue(row.Cells.Count == 3);
            }
        }
    }
}
