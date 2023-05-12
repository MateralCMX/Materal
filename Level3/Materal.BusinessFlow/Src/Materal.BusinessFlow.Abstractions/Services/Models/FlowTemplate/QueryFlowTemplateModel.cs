using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models.FlowTemplate
{
    public class QueryFlowTemplateModel : BaseQueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 数据模型唯一标识
        /// </summary>
        [Equal]
        public Guid? DataModelID { get; set; }
    }
}
