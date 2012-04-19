using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Comparers.Util
{
    /// <summary>
    /// Provides helper methods for comparer implementations.
    /// </summary>
    public static class ComparerHelpers
    {
        /// <summary>
        /// Attempts to return a hash code for the specified object, using the specified comparer. If the comparer does not support hash codes, this method will throw an exception.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="comparer">The comparer to use to calculate a hash code.</param>
        /// <param name="obj">The object for which to return a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        public static int GetHashCodeFromComparer<T>(IComparer<T> comparer, T obj)
        {
            Contract.Requires(comparer != null);
            var equalityComparer = comparer as IEqualityComparer<T>;
            if (equalityComparer != null)
                return equalityComparer.GetHashCode(obj);
            var objectEqualityComparer = comparer as System.Collections.IEqualityComparer;
            if (objectEqualityComparer != null)
            {
                object o = obj;
                Contract.Assume(o != null);
                return objectEqualityComparer.GetHashCode(o);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts a <c>null</c> or default comparer into a default comparer that supports hash codes.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="comparer">The comparer.</param>
        /// <returns>A default comparer or <paramref name="comparer"/>.</returns>
        public static IComparer<T> NormalizeDefault<T>(IComparer<T> comparer)
        {
            Contract.Ensures(Contract.Result<IComparer<T>>() != null);
            if (comparer == null || comparer == Comparer<T>.Default)
                return DefaultComparer<T>.Instance;
            return comparer;
        }
    }
}
