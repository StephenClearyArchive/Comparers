using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers
{
    /// <summary>
    /// A comparer that works by comparing the results of the specified key selector.
    /// </summary>
    /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
    /// <typeparam name="TSourceElement">The type of objects being compared.</typeparam>
    public sealed class SelectComparer<TKey, TSourceElement> : Util.SourceComparerBase<TKey, TSourceElement>
    {
        /// <summary>
        /// The key selector.
        /// </summary>
        private readonly Func<TSourceElement, TKey> selector;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectComparer&lt;TKey, TSourceElement&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        public SelectComparer(IComparer<TKey> source, Func<TSourceElement, TKey> selector)
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
        public Func<TSourceElement, TKey> Select
        {
            get
            {
                Contract.Ensures(Contract.Result<Func<TSourceElement, TKey>>() != null);
                return this.selector;
            }
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected override int DoGetHashCode(TSourceElement obj)
        {
            return Util.ComparerHelpers.GetHashCodeFromComparer(this.Source, this.selector(obj));
        }

        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected override int DoCompare(TSourceElement x, TSourceElement y)
        {
            return this.Source.Compare(this.selector(x), this.selector(y));
        }
    }
}
