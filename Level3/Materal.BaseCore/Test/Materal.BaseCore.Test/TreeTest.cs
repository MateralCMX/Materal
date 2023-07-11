using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.Domain;
using Materal.BaseCore.ServiceImpl;
using Materal.Utils.Excel;
using NPOI.SS.UserModel;

namespace Materal.BaseCore.Test
{
    [TestClass]
    public class TreeTest : BaseTest
    {
        [TestMethod]
        public void ToTreeTest()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TreeData.xlsx");
            IWorkbook workbook = ExcelHelper.ReadExcelToWorkbook(path);
            ISheet sheet = workbook.GetSheetAt(0);
            List<DataModel> dataModels = new();
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                string cell3 = row.GetCell(3).StringCellValue;
                dataModels.Add(new()
                {
                    ID = Guid.Parse(row.GetCell(0).StringCellValue),
                    Name = row.GetCell(1).StringCellValue,
                    Code = row.GetCell(2).NumericCellValue,
                    ParentID = (cell3 is null || cell3 == "NULL") ? null : Guid.Parse(cell3)
                });
            }
            List<DataTreeModel> result = dataModels.ToTree<DataModel, DataTreeModel>();
         }
        public class DataModel : ITreeDomain
        {
            public Guid ID { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime UpdateTime { get; set; }
            public Guid? ParentID { get; set; }
            public string Name { get; set; } = string.Empty;
            public double Code { get; set; }
        }
        public class DataTreeModel : ITreeDTO<DataTreeModel>
        {
            public Guid ID { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime UpdateTime { get; set; }
            public Guid? ParentID { get; set; }
            public string Name { get; set; } = string.Empty;
            public double Code { get; set; }
            public List<DataTreeModel> Children { get; set; } = new();
        }
    }
}
