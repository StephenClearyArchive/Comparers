﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comparers
{
    /// <summary>
    /// A comparer that reverses the evaluation of the specified source comparer.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public sealed class ReverseComparer<T> : Util.SourceComparerBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        public ReverseComparer(IComparer<T> source)
            : base(source)
        {
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected override int DoGetHashCode(T obj)
        {
            return Util.ComparerHelpers.GetHashCodeFromComparer(this.Source, obj);
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected override int DoCompare(T x, T y)
        {
            return this.Source.Compare(y, x);
        }

        /// <summary>
        /// Returns a short, human-readable description of the comparer. This is intended for debugging and not for other purposes.
        /// </summary>
        public override string ToString()
        {
            return "Reverse(" + this.Source + ")";
        }
    }
}
