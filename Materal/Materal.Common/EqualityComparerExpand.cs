using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Materal.Common
{
    public class EqualityComparerExpand<T> : IEqualityComparer<T> where T : class
    {
        private readonly List<Expression<Func<T, object>>> _expressions;
        public EqualityComparerExpand(List<Expression<Func<T, object>>> expressions)
        {
            _expressions = expressions;
        }
        public bool Equals(T x, T y)
        {
            foreach (Expression<Func<T, object>> expression in _expressions)
            {
                object xValue = expression.Compile().Invoke(x);
                object yValue = expression.Compile().Invoke(y);
                if (!xValue.Equals(yValue)) return false;
            }
            return true;
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}
