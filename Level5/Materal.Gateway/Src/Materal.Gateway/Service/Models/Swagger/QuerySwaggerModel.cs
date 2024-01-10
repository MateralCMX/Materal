using Materal.Utils.Model;

namespace Materal.Gateway.Service.Models.Swagger
{
    /// <summary>
    /// 查询Swagger配置模型
    /// </summary>
    public class QuerySwaggerModel : FilterModel
    {
        /// <summary>
        /// 键
        /// </summary>
        [Contains]
        public string? Key { get; set; }
    }
}
