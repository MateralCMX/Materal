using Materal.Model;

namespace ConfigCenter.PresentationModel.Project
{
    /// <summary>
    /// 查询项目过滤器请求模型
    /// </summary>
    public class QueryProjectFilterRequestModel : FilterModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
