using Materal.Utils.Model;

namespace Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig
{
    /// <summary>
    /// 查询Swagger配置请求模型
    /// </summary>
    public class QuerySwaggerConfigRequestModel : FilterModel
    {
        /// <summary>
        /// 键
        /// </summary>
        [Contains]
        public string? Key { get; set; }
    }
}
