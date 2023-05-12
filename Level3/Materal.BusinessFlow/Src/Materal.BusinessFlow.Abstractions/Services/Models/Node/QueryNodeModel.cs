using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models.Node
{
    public class QueryNodeModel : BaseQueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 处理类型
        /// </summary>
        [Equal]
        public NodeHandleTypeEnum? HandleType { get; set; }
        /// <summary>
        /// 步骤唯一标识
        /// </summary>
        [Equal]
        public Guid? StepID { get; set; }
    }
}
