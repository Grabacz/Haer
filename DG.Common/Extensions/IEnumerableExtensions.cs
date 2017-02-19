using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DG.Common
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> @this, Func<bool> condition, Expression<Func<T, bool>> predicate)
        {
            if (condition())
                return @this.Where(predicate.Compile());

            return @this;
        }

        public static IEnumerable<T> GetCount<T>(this IEnumerable<T> @this, out int count)
        {
            count = @this.ToList().Count();
            return @this;
        }
    }
}
