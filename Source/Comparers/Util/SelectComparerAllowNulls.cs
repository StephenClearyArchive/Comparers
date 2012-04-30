using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// A comparer that works by comparing the results of the specified key selector.
    /// </summary>
    /// <typeparam name="TSource">The type of key objects being compared.</typeparam>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public sealed class SelectComparerAllowNulls<T, TSource> : SourceComparerBaseAllowNulls<T, TSource>
    {
        /// <summary>
        /// The key selector.
        /// </summary>
        private readonly Func<T, TSource> selector;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectComparer&lt;T, TSource&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        public SelectComparerAllowNulls(IComparer<TSource> source, Func<T, TSource> selector)
            : base(source)
        {
            Contract.Requires(selector != null);
            this.selector = selector;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.selector != null);
        }

        /// <summary>
        /// Gets the key selector.
        /// </summary>
        public Func<T, TSource> Select
        {
            get
            {
                Contract.Ensures(Contract.Result<Func<T, TSource>>() != null);
                return this.selector;
            }
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code. This object may be <c>null</c>.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected override int DoGetHashCode(T obj)
        {
            return ComparerHelpers.GetHashCodeFromComparer(this.Source, this.selector(obj));
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare. This object may be <c>null</c>.</param>
        /// <param name="y">The second object to compare. This object may be <c>null</c>.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected override int DoCompare(T x, T y)
        {
            return this.Source.Compare(this.selector(x), this.selector(y));
        }

        /// <summary>
        /// Returns a short, human-readable description of the comparer. This is intended for debugging and not for other purposes.
        /// </summary>
        public override string ToString()
        {
            return "Select<" + typeof(TSource).Name + ">(" + this.Source + ")";
        }
    }
}
