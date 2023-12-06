using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.Swagger
{
    /// <summary>
    /// 查询Swagger项配置模型
    /// </summary>
    public class QuerySwaggerItemModel : FilterModel
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        public Guid SwaggerConfigID { get; set; }
    }
}
