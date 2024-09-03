using Swashbuckle.AspNetCore.SwaggerUI;

namespace Materal.MergeBlock.Swagger.Abstractions
{
    /// <summary>
    /// Swagger配置服务
    /// </summary>
    public interface ISwaggerConfigService
    {
        /// <summary>
        /// 配置Swagger
        /// </summary>
        /// <param name="options"></param>
        void ConfigSwagger(SwaggerUIOptions options);
    }
}
