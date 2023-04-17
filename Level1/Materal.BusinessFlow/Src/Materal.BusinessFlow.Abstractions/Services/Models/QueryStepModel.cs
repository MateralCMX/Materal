using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public class QueryStepModel : BaseQueryModel, IQueryModel
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
        /// <summary>
        /// 下一步唯一标识
        /// </summary>
        [Equal]
        public Guid? NextID { get; set; }
        /// <summary>
        /// 上一步唯一标识
        /// </summary>
        [Equal]
        public Guid? UpID { get; set; }
    }
}
