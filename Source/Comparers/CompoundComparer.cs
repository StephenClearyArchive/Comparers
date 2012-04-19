using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers
{
    /// <summary>
    /// A comparer that uses another comparer if the source comparer determines the objects are equal.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public sealed class CompoundComparer<T> : Util.SourceComparerBase<T>
    {
        /// <summary>
        /// The second comparer.
        /// </summary>
        private readonly IComparer<T> thenBy_;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompoundComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer.</param>
        /// <param name="thenBy">The second comparer.</param>
        public CompoundComparer(IComparer<T> source, IComparer<T> thenBy)
            : base(source)
        {
            this.thenBy_ = Util.ComparerHelpers.NormalizeDefault(thenBy);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.thenBy_ != null);
        }

        /// <summary>
        /// Gets the second comparer.
        /// </summary>
        public IComparer<T> ThenBy
        {
            get
            {
                Contract.Ensures(Contract.Result<IComparer<T>>() != null);
                return this.thenBy_;
            }
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
            var ret = this.Source.Compare(x, y);
            if (ret != 0)
                return ret;
            return this.ThenBy.Compare(x, y);
        }
    }
}
