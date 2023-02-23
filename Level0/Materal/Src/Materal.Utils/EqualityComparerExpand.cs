using System.Linq.Expressions;

namespace Materal.Utils
{
    /// <summary>
    /// 相等扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EqualityComparerExpand<T> : IEqualityComparer<T> where T : class
    {
        private readonly List<Expression<Func<T, object>>> _expressions;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="expressions"></param>
        public EqualityComparerExpand(List<Expression<Func<T, object>>> expressions)
        {
            _expressions = expressions;
        }
        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            if (x == null && y == null) return true;
            else if (x == null || y == null) return false;
            foreach (Expression<Func<T, object>> expression in _expressions)
            {
                object xValue = expression.Compile().Invoke(x);
                object yValue = expression.Compile().Invoke(y);
                if (!xValue.Equals(yValue)) return false;
            }
            return true;
        }
        /// <summary>
        /// 获得Hash值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return base.GetHashCode();
        }
    }
}
