using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Comparers
{
    /// <summary>
    /// The default comparer.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public sealed class DefaultComparer<T> : Util.ComparerBase<T>, IEqualityComparer<T>, System.Collections.IEqualityComparer
    {
        private DefaultComparer()
        {
            // This constructor does nothing; it only exists to be inaccessible.
        }

        static DefaultComparer()
        {
            // This type constructor does nothing; it only exists to make static field initialization deterministic.
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected override int DoGetHashCode(T obj)
        {
            return EqualityComparer<T>.Default.GetHashCode(obj);
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected override int DoCompare(T x, T y)
        {
            return Comparer<T>.Default.Compare(x, y);
        }

        private static readonly DefaultComparer<T> instance = new DefaultComparer<T>();

        /// <summary>
        /// Gets the default comparer for this type.
        /// </summary>
        public static DefaultComparer<T> Instance
        {
            get
            {
                Contract.Ensures(Contract.Result<DefaultComparer<T>>() != null);
                return instance;
            }
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether they are equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="x"/> is equal to <paramref name="y"/>; otherwise <c>false</c>.</returns>
        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return EqualityComparer<T>.Default.GetHashCode(obj);
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether they are equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="x"/> is equal to <paramref name="y"/>; otherwise <c>false</c>.</returns>
        bool System.Collections.IEqualityComparer.Equals(object x, object y)
        {
            if (x == null || y == null)
                return x == y;
            Contract.Assume(x is T);
            Contract.Assume(y is T);
            return (this as IEqualityComparer<T>).Equals((T)x, (T)y);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        int System.Collections.IEqualityComparer.GetHashCode(object obj)
        {
            Contract.Assume(obj is T);
            return (this as IEqualityComparer<T>).GetHashCode((T)obj);
        }

        /// <summary>
        /// Returns a short, human-readable description of the comparer. This is intended for debugging and not for other purposes.
        /// </summary>
        public override string ToString()
        {
            var typeofT = typeof(T);
            string comparableBaseString = null;
            try
            {
                Type comparableBase = typeof(ComparableBase<>).MakeGenericType(typeofT);
                var property = comparableBase.GetProperty("DefaultComparer", BindingFlags.Static | BindingFlags.Public);
                var value = property.GetValue(null, null);
                comparableBaseString = value.ToString();
            }
            catch
            {
            }

            if (comparableBaseString != null)
                return "Default(" + comparableBaseString + ")";
            if (typeofT.GetInterface("IComparable`1") != null)
                return "Default(" + typeofT.Name + ": IComparable<T>)";
            if (typeofT.GetInterface("IComparable") != null)
                return "Default(" + typeofT.Name + ": IComparable)";
            return "Default(" + typeofT.Name + ": undefined)";
        }
    }
}
