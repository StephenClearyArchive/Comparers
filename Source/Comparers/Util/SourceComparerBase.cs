using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// Common implementations for comparers that are based on a source comparer for a different type of object.
    /// </summary>
    /// <typeparam name="T">The type of objects compared by this comparer.</typeparam>
    /// <typeparam name="TSource">The type of objects compared by the source comparer.</typeparam>
    public abstract class SourceComparerBase<T, TSource> : ComparerBase<T>
    {
        /// <summary>
        /// The source comparer.
        /// </summary>
        private readonly IComparer<TSource> source_;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceComparerBase&lt;T, TSource&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        protected SourceComparerBase(IComparer<TSource> source)
        {
            this.source_ = ComparerHelpers.NormalizeDefault(source);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.source_ != null);
        }

        /// <summary>
        /// Gets the source comparer.
        /// </summary>
        public IComparer<TSource> Source
        {
            get
            {
                Contract.Ensures(Contract.Result<IComparer<TSource>>() != null);
                return this.source_;
            }
        }
    }
}
