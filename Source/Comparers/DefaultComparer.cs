using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers
{
    /// <summary>
    /// The default comparer.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public sealed class DefaultComparer<T> : Util.ComparerBase<T>
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
    }
}
