using Materal.Model;

namespace ConfigCenter.Services.Models.Project
{
    public class QueryProjectFilterModel : FilterModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Contains]
        public string Description { get; set; }
    }
}
