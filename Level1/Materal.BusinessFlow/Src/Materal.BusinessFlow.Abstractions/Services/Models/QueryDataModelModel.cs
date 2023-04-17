using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public class QueryDataModelModel : BaseQueryModel, IQueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
    }
}
