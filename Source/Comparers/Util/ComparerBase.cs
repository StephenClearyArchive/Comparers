using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// Common implementations for comparers.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public abstract class ComparerBase<T> : IFullComparer<T>
    {
        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code. This object is not <c>null</c>.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected abstract int DoGetHashCode(T obj);

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare. This object is not <c>null</c>.</param>
        /// <param name="y">The second object to compare. This object is not <c>null</c>.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected abstract int DoCompare(T x, T y);

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
            return (this as IComparer<T>).Compare((T)x, (T)y) == 0;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        int System.Collections.IEqualityComparer.GetHashCode(object obj)
        {
            if (obj == null)
                return 0;
            Contract.Assume(obj is T);
            return (this as IEqualityComparer<T>).GetHashCode((T)obj);
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        int System.Collections.IComparer.Compare(object x, object y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                return -1;
            }
            else if (y == null)
            {
                return 1;
            }

            Contract.Assume(x is T);
            Contract.Assume(y is T);
            return (this as IComparer<T>).Compare((T)x, (T)y);
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether they are equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="x"/> is equal to <paramref name="y"/>; otherwise <c>false</c>.</returns>
        public bool Equals(T x, T y)
        {
            return (this as IComparer<T>).Compare(x, y) == 0;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        public int GetHashCode(T obj)
        {
            if (obj == null)
                return 0;
            return this.DoGetHashCode(obj);
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        public int Compare(T x, T y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                return -1;
            }
            else if (y == null)
            {
                return 1;
            }

            return this.DoCompare(x, y);
        }
    }
}
