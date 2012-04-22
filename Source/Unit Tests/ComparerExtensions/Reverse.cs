using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparers;

namespace ComparerExtensions_
{
    [TestClass]
    public class _Reverse
    {
        [TestMethod]
        public void SubstitutesCompareDefaultForComparerDefault()
        {
            var comparer = Comparer<int>.Default.Reverse();
            Assert.AreSame(Compare.Default<int>(), comparer.Source);

            var list = Enumerable.Range(0, 5).ToList();
            list.Sort(comparer);
            CollectionAssert.AreEquivalent(new[] { 4, 3, 2, 1, 0 }, list);
        }

        [TestMethod]
        public void SubstitutesCompareDefaultForNull()
        {
            IComparer<int> source = null;
            var comparer = source.Reverse();
            Assert.AreSame(Compare.Default<int>(), comparer.Source);

            var list = Enumerable.Range(0, 5).ToList();
            list.Sort(comparer);
            CollectionAssert.AreEquivalent(new[] { 4, 3, 2, 1, 0 }, list);
        }

        [TestMethod]
        public void ReversesComparer()
        {
            var list = Enumerable.Range(0, 5).ToList();
            list.Sort(Compare.Default<int>().Reverse());
            CollectionAssert.AreEquivalent(new[] { 4, 3, 2, 1, 0 }, list);
        }
    }
}
