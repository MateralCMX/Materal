using Materal.Utils.Model;
using System.Linq.Expressions;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public class BaseQueryModel : PageRequestModel, IQueryModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Equal]
        public Guid? ID { get; set; }
        /// <summary>
        /// 获得查询表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Expression<Func<T, bool>> GetQueryExpression<T>() => GetSearchExpression<T>();
    }
}
