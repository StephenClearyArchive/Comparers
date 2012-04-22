using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparers;

namespace ComparerExtensions_
{
    [TestClass]
    public class _Select
    {
        private static List<Person> GetPeople()
        {
            return new List<Person>
            {
                new Person { Priority = 3 },
                new Person { Priority = 4 },
                new Person { Priority = 2 },
                new Person { Priority = 5 },
            };
        }

        [TestMethod]
        public void SubstitutesCompareDefaultForComparerDefault()
        {
            var comparer = Comparer<int>.Default.Select((Person p) => p.Priority);
            Assert.AreSame(Compare.Default<int>(), comparer.Source);

            var list = GetPeople();
            list.Sort(comparer);
            CollectionAssert.AreEquivalent(new[] { 2, 3, 4, 5 }, list.Select(x => x.Priority).ToList());
        }

        [TestMethod]
        public void SubstitutesCompareDefaultForNull()
        {
            IComparer<int> source = null;
            var comparer = source.Select((Person p) => p.Priority);
            Assert.AreSame(Compare.Default<int>(), comparer.Source);

            var list = GetPeople();
            list.Sort(comparer);
            CollectionAssert.AreEquivalent(new[] { 2, 3, 4, 5 }, list.Select(x => x.Priority).ToList());
        }

        [TestMethod]
        public void SortsByKey()
        {
            var list = GetPeople();
            list.Sort(Compare.Default<int>().Select((Person p) => p.Priority));
            CollectionAssert.AreEquivalent(new[] { 2, 3, 4, 5 }, list.Select(x => x.Priority).ToList());
        }
    }
}
