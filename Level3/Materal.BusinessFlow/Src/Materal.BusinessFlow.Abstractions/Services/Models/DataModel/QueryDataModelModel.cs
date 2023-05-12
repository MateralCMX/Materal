using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models.DataModel
{
    public class QueryDataModelModel : BaseQueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
    }
}
