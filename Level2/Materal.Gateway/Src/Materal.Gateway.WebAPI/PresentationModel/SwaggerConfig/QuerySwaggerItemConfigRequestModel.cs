using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig
{
    /// <summary>
    /// 查询Swagger项配置请求模型
    /// </summary>
    public class QuerySwaggerItemConfigRequestModel : FilterModel
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        public Guid SwaggerConfigID { get; set; }
    }
}
