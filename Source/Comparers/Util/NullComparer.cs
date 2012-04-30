﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// The null comparer.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public sealed class NullComparer<T> : ComparerBase<T>
    {
        private NullComparer()
        {
            // This constructor does nothing; it only exists to be inaccessible.
        }

        static NullComparer()
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
            return 0;
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected override int DoCompare(T x, T y)
        {
            return 0;
        }

        private static readonly NullComparer<T> instance = new NullComparer<T>();

        /// <summary>
        /// Gets the null comparer for this type.
        /// </summary>
        public static NullComparer<T> Instance
        {
            get
            {
                Contract.Ensures(Contract.Result<NullComparer<T>>() != null);
                return instance;
            }
        }

        /// <summary>
        /// Returns a short, human-readable description of the comparer. This is intended for debugging and not for other purposes.
        /// </summary>
        public override string ToString()
        {
            return "Null";
        }
    }
}