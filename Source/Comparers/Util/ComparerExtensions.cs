using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// Provides extension methods for comparers.
    /// </summary>
    public static class UtilityComparerExtensions
    {
        /// <summary>
        /// Returns a comparer that works by comparing the results of the specified key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of key objects being compared.</typeparam>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <returns>A comparer that works by comparing the results of the specified key selector.</returns>
        public static IFullComparer<T> SelectFrom<T, TSource>(this IComparer<TSource> source, Func<T, TSource> selector)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<IFullComparer<T>>() != null);
            return new SelectComparer<T, TSource>(source, selector);
        }
    }
}
