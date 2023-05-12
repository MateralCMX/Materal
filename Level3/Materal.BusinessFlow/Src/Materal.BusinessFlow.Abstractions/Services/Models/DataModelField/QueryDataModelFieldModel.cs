using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models.DataModelField
{
    public class QueryDataModelFieldModel : BaseQueryModel
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
        /// <summary>
        /// 数据类型
        /// </summary>
        [Equal]
        public DataTypeEnum? DataType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Contains]
        public string? Description { get; set; }
    }
}
