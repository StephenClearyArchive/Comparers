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
        /// Gets a key comparer.
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
    }
}