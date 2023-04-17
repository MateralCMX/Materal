using System.Linq.Expressions;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public interface IQueryModel
    {
        /// <summary>
        /// 页面位序
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 显示数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 获得查询表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Expression<Func<T, bool>> GetQueryExpression<T>();
    }
}
