using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Comparers.Util;

namespace Comparers
{
    /// <summary>
    /// Provides sources for comparers.
    /// </summary>
    public static class Compare
    {
        /// <summary>
        /// Gets a comparer source for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="_">Parameter that is only used to derive the type <typeparamref name="T"/>.</param>
        public static CompareSource<T> Type<T>(T _ = default(T))
        {
            return new CompareSource<T>();
        }
    }
}