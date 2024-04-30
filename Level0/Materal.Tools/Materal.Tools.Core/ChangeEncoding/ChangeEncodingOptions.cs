using System.Text;

namespace Materal.Tools.Core.ChangeEncoding
{
    /// <summary>
    /// 更改编码选项
    /// </summary>
    public class ChangeEncodingOptions
    {
        /// <summary>
        /// 读取编码
        /// </summary>
        public Encoding? ReadEncoding { get; set; }
        /// <summary>
        /// 写入编码
        /// </summary>
        public Encoding WriteEncoding { get; set; } = Encoding.UTF8;
        /// <summary>
        /// 过滤器
        /// </summary>
        public Func<FileInfo, bool> Filter { get; set; } = m => true;
        /// <summary>
        /// 递归
        /// </summary>
        public bool Recursive { get; set; } = false;
    }
}
