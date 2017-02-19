using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DG.Common
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> source, Func<bool> condition, Expression<Func<T, bool>> predicate)
        {
            if (condition())
            {
                return source.Where(predicate.Compile());
            }

            return source;
        }
    }
}
