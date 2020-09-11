using Deploy.Enums;
using Materal.Model;

namespace Deploy.PresentationModel.DefaultData
{
    /// <summary>
    /// 查询默认数据过滤器请求模型
    /// </summary>
    public class QueryDefaultDataFilterRequestModel : PageRequestModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ApplicationTypeEnum? ApplicationType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
    }
}
