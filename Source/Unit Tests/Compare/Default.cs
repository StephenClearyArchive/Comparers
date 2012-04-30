﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparers;

namespace Compare_
{
    [TestClass]
    public class _Default
    {
        [TestMethod]
        public void IsEquivalentToComparerDefault()
        {
            var list1 = new[] { 3, 5, 4, 2, 6 }.ToList();
            var list2 = new[] { 3, 5, 4, 2, 6 }.ToList();
            list1.Sort();
            list2.Sort(Compare<int>.Default());
            CollectionAssert.AreEquivalent(list1, list2);
        }

        [TestMethod]
        public void UsesSequenceComparerForSequences()
        {
            var three = new[] { 3 };
            var four = new[] { 4 };
            var five = new[] { 5 };
            var list1 = new[] { three, five, four }.ToList();
            var list2 = new[] { three, five, four }.ToList();
            var comparer1 = Compare<int>.Default().Sequence();
            var comparer2 = Compare<int[]>.Default();
            list1.Sort(comparer1);
            list2.Sort(comparer2);
            CollectionAssert.AreEquivalent(list1, list2);
        }

        [TestMethod]
        public void NullIsLessThanValue()
        {
            var list = new int?[] { 3, null, 4, 2, 6 }.ToList();
            list.Sort(Compare<int?>.Default());
            CollectionAssert.AreEquivalent(list, new int?[] { null, 2, 3, 4, 6 });
        }

        [TestMethod]
        public void NullSequenceIsLessThanValuesAndEmptySequence()
        {
            var none = new int[0];
            var five = new[] { 5 };
            var list = new[] { five, none, null }.ToList();
            list.Sort(Compare<int[]>.Default());
            CollectionAssert.AreEquivalent(list, new[] { null, none, five });
        }

        [TestMethod]
        public void NullIsEqualToNull()
        {
            var comparer = Compare<int?>.Default();
            Assert.IsTrue(comparer.Compare(null, null) == 0);
            Assert.IsTrue(comparer.Equals(null, null));
        }

        [TestMethod]
        public void NullSequenceIsEqualToNullSequence()
        {
            var comparer = Compare<int[]>.Default();
            Assert.IsTrue(comparer.Compare(null, null) == 0);
            Assert.IsTrue(comparer.Equals(null, null));
        }
    }
}
