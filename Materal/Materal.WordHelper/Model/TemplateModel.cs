using NPOI.XWPF.UserModel;
using System;
using System.Data;

namespace Materal.WordHelper.Model
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class TemplateModel
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
    }
    /// <summary>
    /// 字符串模板模型
    /// </summary>
    public class StringTemplateModel : TemplateModel
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
    /// <summary>
    /// 表格模板模型
    /// </summary>
    public class TableTemplateModel : TemplateModel
    {
        /// <summary>
        /// 值
        /// </summary>
        public DataTable Value { get; set; }
        /// <summary>
        /// 开始行数
        /// </summary>
        public int StartRowNumber { get; set; }
        /// <summary>
        /// 设置单元格
        /// </summary>
        public Func<int ,int, XWPFParagraph, XWPFRun> OnSetCellText { get; set; }
    }
}
