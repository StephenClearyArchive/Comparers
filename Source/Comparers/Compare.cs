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
    [Obsolete("Use the Compare<T> type instead.")]
    public static class Compare
    {
        /// <summary>
        /// Gets the null comparer for this type, which evaluates all objects as equivalent.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        public static NullComparer<T> Null<T>()
        {
            Contract.Ensures(Contract.Result<NullComparer<T>>() != null);
            Contract.Ensures(Contract.Result<NullComparer<T>>() == NullComparer<T>.Instance);
            return Compare<T>.Null();
        }

        /// <summary>
        /// Gets the default comparer for this type.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        public static DefaultComparer<T> Default<T>()
        {
            Contract.Ensures(Contract.Result<DefaultComparer<T>>() != null);
            Contract.Ensures(Contract.Result<DefaultComparer<T>>() == DefaultComparer<T>.Instance);
            return Compare<T>.Default();
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
            /// <param name="selector">The key selector. May not be <c>null</c>.</param>
            /// <param name="keyComparer">The key comparer. Defaults to <c>null</c>. If this is <c>null</c>, the default comparer is used.</param>
            /// <returns>A key comparer.</returns>
            public static CompoundComparer<T> OrderBy<TKey>(Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
            {
                Contract.Requires(selector != null);
                Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
                return Compare<T>.OrderBy(selector, keyComparer);
            }

            /// <summary>
            /// Creates a descending key comparer.
            /// </summary>
            /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
            /// <param name="selector">The key selector. May not be <c>null</c>.</param>
            /// <param name="keyComparer">The key comparer. The returned comparer applies this key comparer in reverse. Defaults to <c>null</c>. If this is <c>null</c>, the default comparer is used.</param>
            /// <returns>A key comparer.</returns>
            public static CompoundComparer<T> OrderByDescending<TKey>(Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
            {
                Contract.Requires(selector != null);
                Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
                return Compare<T>.OrderByDescending(selector, keyComparer);
            }
        }
    }

    /// <summary>
    /// Provides sources for comparers.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public static class Compare<T>
    {
        /// <summary>
        /// Gets the null comparer for this type, which evaluates all objects as equivalent.
        /// </summary>
        public static NullComparer<T> Null()
        {
            Contract.Ensures(Contract.Result<NullComparer<T>>() != null);
            Contract.Ensures(Contract.Result<NullComparer<T>>() == NullComparer<T>.Instance);
            return NullComparer<T>.Instance;
        }

        /// <summary>
        /// Gets the default comparer for this type.
        /// </summary>
        public static DefaultComparer<T> Default()
        {
            Contract.Ensures(Contract.Result<DefaultComparer<T>>() != null);
            Contract.Ensures(Contract.Result<DefaultComparer<T>>() == DefaultComparer<T>.Instance);
            return DefaultComparer<T>.Instance;
        }

        /// <summary>
        /// Creates a key comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <param name="keyComparer">The key comparer. Defaults to <c>null</c>. If this is <c>null</c>, the default comparer is used.</param>
        /// <returns>A key comparer.</returns>
        public static CompoundComparer<T> OrderBy<TKey>(Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
            return Null().ThenBy(selector, keyComparer);
        }

        /// <summary>
        /// Creates a descending key comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <param name="keyComparer">The key comparer. The returned comparer applies this key comparer in reverse. Defaults to <c>null</c>. If this is <c>null</c>, the default comparer is used.</param>
        /// <returns>A key comparer.</returns>
        public static CompoundComparer<T> OrderByDescending<TKey>(Func<T, TKey> selector, IComparer<TKey> keyComparer = null)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(Contract.Result<CompoundComparer<T>>() != null);
            return Null().ThenBy(selector, keyComparer.Reverse());
        }
    }
}