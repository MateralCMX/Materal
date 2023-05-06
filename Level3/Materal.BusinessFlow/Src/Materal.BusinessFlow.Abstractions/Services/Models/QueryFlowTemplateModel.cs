using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public class QueryFlowTemplateModel : BaseQueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 流程模版唯一标识
        /// </summary>
        [Equal]
        public Guid? FlowTemplateID { get; set; }
    }
}
