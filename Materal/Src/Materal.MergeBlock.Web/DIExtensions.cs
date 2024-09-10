using Materal.MergeBlock.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.Web
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtensions
    {
        /// <summary>
        /// 添加MergeBlock
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddMergeBlockCore(this WebApplicationBuilder builder)
        {
            builder.Services.AddMergeBlockCore(builder.Configuration);
            return builder;
        }
    }
}
