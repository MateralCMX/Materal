using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 高级上下文
    /// </summary>
    public class AdvancedContext
    {
        /// <summary>
        /// 应用程序构建器
        /// </summary>
        public IHostApplicationBuilder? AppBuilder { get; set; }
        /// <summary>
        /// 应用程序
        /// </summary>
        public IHost? App { get; set; }
    }
}
