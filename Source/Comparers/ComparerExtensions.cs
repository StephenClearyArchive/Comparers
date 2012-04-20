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
            return source.ThenBy(keyComparer.Select(selector));
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
            return source.ThenBy(keyComparer.Select(selector).Reverse());
        }

        /// <summary>
        /// Implements <see cref="IComparable{T}.CompareTo"/>. Types implementing <see cref="IComparable{T}"/> should also implement <see cref="IComparable"/> and <see cref="IEquatable{T}"/>, and override <see cref="Object.Equals(Object)"/> and <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="comparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="other">The other object.</param>
        public static int ImplementCompareTo<T>(this IComparer<T> comparer, T @this, T other) where T : IComparable<T>
        {
            return comparer.Compare(@this, other);
        }

        /// <summary>
        /// Implements <see cref="IComparable.CompareTo"/>. Types implementing <see cref="IComparable"/> should also override <see cref="Object.Equals(Object)"/> and <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="comparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="obj">The other object.</param>
        public static int ImplementCompareTo<T>(this System.Collections.IComparer comparer, T @this, object obj) where T : IComparable
        {
            return comparer.Compare(@this, obj);
        }

        /// <summary>
        /// Implements <see cref="Object.GetHashCode"/>. Types overriding <see cref="Object.GetHashCode"/> should also override <see cref="Object.Equals(Object)"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="equalityComparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        public static int ImplementGetHashCode<T>(this IEqualityComparer<T> equalityComparer, T @this)
        {
            return equalityComparer.GetHashCode(@this);
        }

        /// <summary>
        /// Implements <see cref="IEquatable{T}.Equals"/>. Types implementing <see cref="IEquatable{T}"/> should also override <see cref="Object.Equals(Object)"/> and <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="equalityComparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="other">The other object.</param>
        public static bool ImplementEquals<T>(this IEqualityComparer<T> equalityComparer, T @this, T other) where T : IEquatable<T>
        {
            return equalityComparer.Equals(@this, other);
        }

        /// <summary>
        /// Implements <see cref="Object.Equals(Object)"/>. Types overriding <see cref="Object.Equals(Object)"/> should also override <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <param name="equalityComparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="obj">The other object.</param>
        public static bool ImplementEquals(this System.Collections.IEqualityComparer equalityComparer, object @this, object obj)
        {
            return equalityComparer.Equals(@this, obj);
        }
    }
}
