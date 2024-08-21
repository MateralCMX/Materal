namespace Materal.Utils
{
    /// <summary>
    /// 相等扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EqualityComparerExpand<T>(List<Expression<Func<T, object>>> expressions) : IEqualityComparer<T> where T : class
    {
        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T? x, T? y)
        {
            if (x is null && y is null) return true;
            else if (x is null || y is null) return false;
            foreach (Expression<Func<T, object>> expression in expressions)
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
