using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Comparers;
using EqualityComparers.Util;

namespace EqualityComparers
{
    /// <summary>
    /// Provides sources for equality comparers.
    /// </summary>
    public static class EqualityCompare
    {
        /// <summary>
        /// Gets an equality comparer source for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="_">Parameter that is only used to derive the type <typeparamref name="T"/>.</param>
        public static EqualityCompareSource<T> Type<T>(T _ = default(T))
        {
            return new EqualityCompareSource<T>();
        }
    }
}