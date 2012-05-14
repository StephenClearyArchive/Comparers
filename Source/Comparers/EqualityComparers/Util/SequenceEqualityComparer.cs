﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace EqualityComparers.Util
{
    /// <summary>
    /// A comparer that performs a lexicographical ordering on a sequence.
    /// </summary>
    /// <typeparam name="T">The type of sequence elements being compared.</typeparam>
    public sealed class SequenceEqualityComparer<T> : SourceEqualityComparerBase<IEnumerable<T>, T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceEqualityComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        public SequenceEqualityComparer(IEqualityComparer<T> source)
            : base(source, false)
        {
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected override int DoGetHashCode(IEnumerable<T> obj)
        {
            Contract.Assume(obj != null);
            int ret = 0;
            foreach (var item in obj)
                ret ^= this.Source.GetHashCode(item);
            return ret;
        }

        protected override bool DoEquals(IEnumerable<T> x, IEnumerable<T> y)
        {
            Contract.Assume(x != null);
            Contract.Assume(y != null);
            using (var xIter = x.GetEnumerator())
            using (var yIter = y.GetEnumerator())
            {
                while (true)
                {
                    if (!xIter.MoveNext())
                    {
                        if (!yIter.MoveNext())
                            return true;
                        return false;
                    }

                    if (!yIter.MoveNext())
                        return false;

                    var ret = this.Source.Equals(xIter.Current, yIter.Current);
                    if (!ret)
                        return false;
                }
            }            
        }

        /// <summary>
        /// Returns a short, human-readable description of the comparer. This is intended for debugging and not for other purposes.
        /// </summary>
        public override string ToString()
        {
            return "Sequence<" + typeof(T).Name + ">(" + this.Source + ")";
        }
    }
}