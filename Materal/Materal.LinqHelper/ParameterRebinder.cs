using System.Collections.Generic;
using System.Linq.Expressions;

namespace Materal.LinqHelper
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map"></param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out ParameterExpression replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
