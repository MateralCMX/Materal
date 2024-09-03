#if NET6_0
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.MergeBlock.Repository.Abstractions
{
    /// <summary>
    /// 表构建器扩展
    /// </summary>
    public static class TableBuilderExtension
    {
        /// <summary>
        /// 设置表注释
        /// </summary>
        /// <param name="_"></param>
        /// <param name="_2"></param>
        public static void HasComment(this TableBuilder _, string _2)
        {
        }
    }
}
#endif
