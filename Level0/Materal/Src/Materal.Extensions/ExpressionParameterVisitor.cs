using System.Linq.Expressions;

namespace Materal.Extensions
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class ExpressionParameterVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map"></param>
        public ExpressionParameterVisitor(Dictionary<ParameterExpression, ParameterExpression> map) => _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp) => new ExpressionParameterVisitor(map).Visit(exp);
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out ParameterExpression? replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
