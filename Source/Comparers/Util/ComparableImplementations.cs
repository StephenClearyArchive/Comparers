using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// Provides implementations for comparison, equality, and hash code methods.
    /// </summary>
    public static class ComparableImplementations
    {
        /// <summary>
        /// Implements <see cref="IComparable{T}.CompareTo"/>. Types implementing <see cref="IComparable{T}"/> should also implement <see cref="IComparable"/> and <see cref="IEquatable{T}"/>, and override <see cref="Object.Equals(Object)"/> and <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="comparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="other">The other object.</param>
        public static int ImplementCompareTo<T>(IComparer<T> comparer, T @this, T other) where T : IComparable<T>
        {
            Contract.Requires(comparer != null);
            return comparer.Compare(@this, other);
        }

        /// <summary>
        /// Implements <see cref="IComparable.CompareTo"/>. Types implementing <see cref="IComparable"/> should also override <see cref="Object.Equals(Object)"/> and <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="obj">The other object.</param>
        public static int ImplementCompareTo(System.Collections.IComparer comparer, IComparable @this, object obj)
        {
            Contract.Requires(comparer != null);
            Contract.Requires(@this != null);
            return comparer.Compare(@this, obj);
        }

        /// <summary>
        /// Implements <see cref="Object.GetHashCode"/>. Types overriding <see cref="Object.GetHashCode"/> should also override <see cref="Object.Equals(Object)"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="equalityComparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        public static int ImplementGetHashCode<T>(IEqualityComparer<T> equalityComparer, T @this)
        {
            Contract.Requires(equalityComparer != null);
            return equalityComparer.GetHashCode(@this);
        }

        /// <summary>
        /// Implements <see cref="IEquatable{T}.Equals"/>. Types implementing <see cref="IEquatable{T}"/> should also override <see cref="Object.Equals(Object)"/> and <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="equalityComparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="other">The other object.</param>
        public static bool ImplementEquals<T>(IEqualityComparer<T> equalityComparer, T @this, T other) where T : IEquatable<T>
        {
            Contract.Requires(equalityComparer != null);
            return equalityComparer.Equals(@this, other);
        }

        /// <summary>
        /// Implements <see cref="Object.Equals(Object)"/>. Types overriding <see cref="Object.Equals(Object)"/> should also override <see cref="Object.GetHashCode"/>.
        /// </summary>
        /// <param name="equalityComparer">The comparer.</param>
        /// <param name="this">The object doing the implementing.</param>
        /// <param name="obj">The other object.</param>
        public static bool ImplementEquals(System.Collections.IEqualityComparer equalityComparer, object @this, object obj)
        {
            Contract.Requires(equalityComparer != null);
            Contract.Requires(@this != null);
            return equalityComparer.Equals(@this, obj);
        }
    }
}
