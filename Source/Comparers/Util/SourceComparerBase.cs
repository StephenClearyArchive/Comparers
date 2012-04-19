using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// Common implementations for comparers that are based on a source comparer.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    public abstract class SourceComparerBase<T> : ComparerBase<T>
    {
        /// <summary>
        /// The source comparer.
        /// </summary>
        private readonly IComparer<T> source_;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceComparerBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer.</param>
        protected SourceComparerBase(IComparer<T> source)
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
        public IComparer<T> Source
        {
            get
            {
                Contract.Ensures(Contract.Result<IComparer<T>>() != null);
                return this.source_;
            }
        }
    }

    /// <summary>
    /// Common implementations for comparers that are based on a source comparer for a different type of object.
    /// </summary>
    /// <typeparam name="TKey">The type of objects compared by the source comparer.</typeparam>
    /// <typeparam name="TSourceElement">The type of objects compared by this comparer.</typeparam>
    public abstract class SourceComparerBase<TKey, TSourceElement> : ComparerBase<TSourceElement>
    {
        /// <summary>
        /// The source comparer.
        /// </summary>
        private readonly IComparer<TKey> source_;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceComparerBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer.</param>
        protected SourceComparerBase(IComparer<TKey> source)
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
        public IComparer<TKey> Source
        {
            get
            {
                Contract.Ensures(Contract.Result<IComparer<TKey>>() != null);
                return this.source_;
            }
        }
    }
}
