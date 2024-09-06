using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Materal.MergeBlock.Swagger.Abstractions
{
    /// <summary>
    /// Swagger配置服务
    /// </summary>
    internal record class MergeBlockSwaggerConfigService(string Name) : ISwaggerConfigService
    {
        /// <summary>
        /// 配置Swagger
        /// </summary>
        /// <param name="options"></param>
        public void ConfigSwagger(SwaggerUIOptions options) => options.SwaggerEndpoint($"/swagger/{Name}/swagger.json", Name);
    }
}
