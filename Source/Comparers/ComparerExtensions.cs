using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers
{
    /// <summary>
    /// Provides extension methods for comparers.
    /// </summary>
    public static class ComparerExtensions
    {
        /// <summary>
        /// Returns a comparer that reverses the evaluation of the specified source comparer.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer.</param>
        /// <returns>A comparer that reverses the evaluation of the specified source comparer.</returns>
        public static ReverseComparer<T> Reverse<T>(this IComparer<T> source)
        {
            Contract.Ensures(Contract.Result<ReverseComparer<T>>() != null);
            return new ReverseComparer<T>(source);
        }

        /// <summary>
        /// Returns a comparer that works by comparing the results of the specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <typeparam name="TSourceElement">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer.</param>
        /// <param name="selector">The key selector.</param>
        /// <returns>A comparer that works by comparing the results of the specified key selector.</returns>
        public static SelectComparer<TKey, TSourceElement> Select<TKey, TSourceElement>(this IComparer<TKey> source, Func<TSourceElement, TKey> selector)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<SelectComparer<TKey, TSourceElement>>() != null);
            return new SelectComparer<TKey, TSourceElement>(source, selector);
        }

        /// <summary>
        /// Returns a comparer that uses another comparer if the source comparer determines the objects are equal.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer.</param>
        /// <param name="thenBy">The comparer that is used if <paramref name="source"/> determines the objects are equal.</param>
        /// <returns>A comparer that uses another comparer if the source comparer determines the objects are equal.</returns>
        public static CompoundComparer<T> ThenBy<T>(this IComparer<T> source, IComparer<T> thenBy)
        {
            Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
            return new CompoundComparer<T>(source, thenBy);
        }

        /// <summary>
        /// Returns a comparer that uses a key comparer if the source comparer determines the objects are equal.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <param name="source">The source comparer.</param>
        /// <param name="selector">The key selector.</param>
        /// <param name="keyComparer">The key comparer.</param>
        /// <returns>A comparer that uses a key comparer if the source comparer determines the objects are equal.</returns>
        public static CompoundComparer<T> ThenBy<T, TKey>(this IComparer<T> source, Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
            return source.ThenBy(keyComparer.Select(selector));
        }
    }
}
