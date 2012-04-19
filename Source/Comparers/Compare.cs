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
    public static class Compare
    {
        /// <summary>
        /// Gets the null comparer for this type, which evaluates all objects as equivalent.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        public static NullComparer<T> Null<T>()
        {
            Contract.Ensures(Contract.Result<NullComparer<T>>() != null);
            return NullComparer<T>.Instance;
        }

        /// <summary>
        /// Gets the default comparer for this type.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        public static DefaultComparer<T> Default<T>()
        {
            Contract.Ensures(Contract.Result<DefaultComparer<T>>() != null);
            return DefaultComparer<T>.Instance;
        }

        /// <summary>
        /// Provides a method for creating a key comparer.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        public static class Key<T>
        {
            /// <summary>
            /// Creates a key comparer.
            /// </summary>
            /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
            /// <param name="selector">The key selector.</param>
            /// <param name="keyComparer">The key comparer.</param>
            /// <returns>A key comparer.</returns>
            public static CompoundComparer<T> OrderBy<TKey>(Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
            {
                Contract.Requires(selector != null);
                Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
                return Null<T>().ThenBy(selector, keyComparer);
            }
        }

        // TODO: string comparers, descending
    }
}