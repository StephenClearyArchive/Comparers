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
        /// <param name="comparer">The comparer to use to calculate a hash code. May not be <c>null</c>.</param>
        /// <param name="obj">The object for which to return a hash code. May not be <c>null</c>.</param>
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
        /// <param name="comparer">The comparer. May be <c>null</c>.</param>
        /// <returns>A default comparer or <paramref name="comparer"/>.</returns>
        public static IComparer<T> NormalizeDefault<T>(IComparer<T> comparer)
        {
            Contract.Ensures(Contract.Result<IComparer<T>>() != null);
            if (comparer == null || comparer == Comparer<T>.Default)
            {
                if (!DefaultComparer<T>.IsImplementedByType && DefaultComparer<T>.IsImplemented)
                {
                    // If T doesn't implement a default comparer but DefaultComparer does, then T must implement IEnumerable<U>.
                    // Extract the U and create a SequenceComparer<U>.
                    var enumerable = typeof(T).GetInterface("IEnumerable`1");
                    var elementTypes = enumerable.GetGenericArguments();
                    var ret = typeof(SequenceComparer<>).MakeGenericType(elementTypes)
                        .GetConstructor(new[] { typeof(IComparer<>).MakeGenericType(elementTypes) })
                        .Invoke(new object[] { null });
                    return (IComparer<T>)ret;
                }

                return DefaultComparer<T>.Instance;
            }

            return comparer;
        }
    }
}
