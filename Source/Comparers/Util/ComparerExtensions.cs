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
    public static class ComparerExtensions
    {
        /// <summary>
        /// Returns a comparer that works by comparing the results of the specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <typeparam name="TSourceElement">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <returns>A comparer that works by comparing the results of the specified key selector.</returns>
        public static SelectComparer<TKey, TSourceElement> SelectFrom<TKey, TSourceElement>(this IComparer<TKey> source, Func<TSourceElement, TKey> selector)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<SelectComparer<TKey, TSourceElement>>() != null);
            return new SelectComparer<TKey, TSourceElement>(source, selector);
        }
    }
}
