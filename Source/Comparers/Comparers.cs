using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers
{
    /// <summary>
    /// Provides sources for comparers.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public static class Comparers<T>
    {
        /// <summary>
        /// Gets the null comparer for this type, which evaluates all objects as equivalent.
        /// </summary>
        public static IComparer<T> Null
        {
            get
            {
                Contract.Ensures(Contract.Result<IComparer<T>>() != null);
                return NullComparer<T>.Instance;
            }
        }

        /// <summary>
        /// Gets the default comparer for this type.
        /// </summary>
        public static IComparer<T> Default
        {
            get
            {
                Contract.Ensures(Contract.Result<IComparer<T>>() != null);
                return DefaultComparer<T>.Instance;
            }
        }

        /// <summary>
        /// Creates a key comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <param name="selector">The key selector.</param>
        /// <param name="keyComparer">The key comparer.</param>
        /// <returns>A key comparer.</returns>
        public static IComparer<T> OrderBy<TKey>(Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<IComparer<T>>() != null);
            return Null.ThenBy(selector, keyComparer);
        }

        /// <summary>
        /// Creates an anonymous comparer.
        /// </summary>
        /// <param name="compare">A delegate which compares two objects and returns a value less than 0 if its first argument is less than its second argument, 0 if its two arguments are equal, or greater than 0 if its first argument is greater than its second argument.</param>
        /// <param name="getHashCode">A delegate which calculates a hash code for an object.</param>
        /// <returns>An anonymous comparer.</returns>
        public static IComparer<T> Anonymous(Func<T, T, int> compare, Func<T, int> getHashCode = null)
        {
            Contract.Requires(compare != null);
            Contract.Ensures(Contract.Result<IComparer<T>>() != null);
            return new AnonymousComparer<T>
            {
                Compare = compare,
                GetHashCode = getHashCode,
            };
        }
    }
}