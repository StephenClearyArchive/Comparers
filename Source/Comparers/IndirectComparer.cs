using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers
{
    /// <summary>
    /// A comparer that works by treating list indexes as though they were list elements.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public sealed class IndirectComparer<T> : Util.SourceComparerBase<T, int>
    {
        /// <summary>
        /// The list of source elements.
        /// </summary>
        private readonly IList<T> list;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="list">The list of source elements. May not be <c>null</c>.</param>
        public IndirectComparer(IComparer<T> source, IList<T> list)
            : base(source)
        {
            Contract.Requires(list != null);
            this.list = list;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.list != null);
        }

        /// <summary>
        /// Gets the list of source elements.
        /// </summary>
        public IList<T> List
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<T>>() != null);
                return this.list;
            }
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected override int DoGetHashCode(int obj)
        {
            Contract.Assume(obj >= 0 && obj < this.list.Count);
            return Util.ComparerHelpers.GetHashCodeFromComparer(this.Source, this.list[obj]);
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected override int DoCompare(int x, int y)
        {
            Contract.Assume(x >= 0 && x < this.list.Count);
            Contract.Assume(y >= 0 && y < this.list.Count);
            return this.Source.Compare(this.list[x], this.list[y]);
        }
    }
}
