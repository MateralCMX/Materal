using Deploy.Enums;
using Materal.Model;

namespace Deploy.Services.Models.DefaultData
{
    /// <summary>
    /// 查询默认数据过滤器模型
    /// </summary>
    public class QueryDefaultDataFilterModel : PageRequestModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Equal]
        public ApplicationTypeEnum? ApplicationType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Equal]
        public string Key { get; set; }
    }
}
