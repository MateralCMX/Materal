using Materal.Common;
using NPOI.SS.UserModel;

namespace Materal.ExcelHelper.Model
{
    public class ExcelRowModel
    {
        /// <summary>
        /// 表格行
        /// </summary>
        public List<IRow> Rows { get; set; } = new();
        /// <summary>
        /// 最大的单元格数
        /// </summary>
        public short MaxCellNum => Rows.Count > 0 ? Rows.Max(m => m.LastCellNum) : throw new MateralException("未能读取到有效数据");
    }
}
