using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Comparers.Util;

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
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
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
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <returns>A comparer that works by comparing the results of the specified key selector.</returns>
        [Obsolete("You probably want to use ThenBy. If you do need Select, use SelectFrom (in the Comparers.Util namespace).")]
        public static SelectComparer<TKey, TSourceElement> Select<TKey, TSourceElement>(this IComparer<TKey> source, Func<TSourceElement, TKey> selector)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<SelectComparer<TKey, TSourceElement>>() != null);
            return source.SelectFrom(selector);
        }

        /// <summary>
        /// Returns a comparer that uses another comparer if the source comparer determines the objects are equal.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="thenBy">The comparer that is used if <paramref name="source"/> determines the objects are equal. If this is <c>null</c>, the default comparer is used.</param>
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
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <param name="keyComparer">The key comparer. Defaults to <c>null</c>. If this is <c>null</c>, the default comparer is used.</param>
        /// <returns>A comparer that uses a key comparer if the source comparer determines the objects are equal.</returns>
        public static CompoundComparer<T> ThenBy<T, TKey>(this IComparer<T> source, Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
            return source.ThenBy(keyComparer.SelectFrom(selector));
        }

        /// <summary>
        /// Returns a comparer that uses a descending key comparer if the source comparer determines the objects are equal.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <param name="keyComparer">The key comparer. The returned comparer applies this key comparer in reverse. Defaults to <c>null</c>. If this is <c>null</c>, the default comparer is used.</param>
        /// <returns>A comparer that uses a key comparer if the source comparer determines the objects are equal.</returns>
        public static CompoundComparer<T> ThenByDescending<T, TKey>(this IComparer<T> source, Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
            return source.ThenBy(keyComparer.SelectFrom(selector).Reverse());
        }
    }
}
