using Materal.Common;
using System.IO;

namespace Materal.ExcelHelper.Model
{
    public class ExcelWorkbook
    {
        public ExcelWorkbook(string filePatch)
        {
            if (!File.Exists(filePatch))
            {
                throw new MateralException("文件不存在");
            }
            FilePatch = filePatch;
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePatch { get; }
    }
}
