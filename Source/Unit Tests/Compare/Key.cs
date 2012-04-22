using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparers;

namespace Compare_
{
    [TestClass]
    public class _Key
    {
        private static readonly Person AbeAbrams = new Person { FirstName = "Abe", LastName = "Abrams" };
        private static readonly Person JackAbrams = new Person { FirstName = "Jack", LastName = "Abrams" };
        private static readonly Person WilliamAbrams = new Person { FirstName = "William", LastName = "Abrams" };
        private static readonly Person CaseyJohnson = new Person { FirstName = "Casey", LastName = "Johnson" };

        [TestMethod]
        public void OrderBySortsByKey()
        {
            var list = new List<Person> { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson };
            list.Sort(Compare.Key<Person>.OrderBy(p => p.FirstName));
            CollectionAssert.AreEquivalent(new[] { AbeAbrams, CaseyJohnson, JackAbrams, WilliamAbrams }, list);
        }

        [TestMethod]
        public void OrderByUsesKeyComparer()
        {
            var list = new List<Person> { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson };
            list.Sort(Compare.Key<Person>.OrderBy(p => p.FirstName, StringComparer.InvariantCulture.Reverse()));
            CollectionAssert.AreEquivalent(new[] { WilliamAbrams, JackAbrams, CaseyJohnson, AbeAbrams }, list);
        }

        [TestMethod]
        public void OrderByDescendingSortsByKey()
        {
            var list = new List<Person> { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson };
            list.Sort(Compare.Key<Person>.OrderByDescending(p => p.FirstName));
            CollectionAssert.AreEquivalent(new[] { WilliamAbrams, JackAbrams, CaseyJohnson, AbeAbrams }, list);
        }

        [TestMethod]
        public void OrderByDescendingUsesKeyComparer()
        {
            var list = new List<Person> { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson };
            list.Sort(Compare.Key<Person>.OrderBy(p => p.FirstName, StringComparer.InvariantCulture.Reverse()));
            CollectionAssert.AreEquivalent(new[] { AbeAbrams, CaseyJohnson, JackAbrams, WilliamAbrams }, list);
        }

        [TestMethod]
        public void ThenBySortsByKey()
        {
            var list = new List<Person> { AbeAbrams, WilliamAbrams, CaseyJohnson, JackAbrams };
            list.Sort(Compare.Key<Person>.OrderBy(p => p.LastName).ThenBy(p => p.FirstName));
            CollectionAssert.AreEquivalent(new[] { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson }, list);
        }

        [TestMethod]
        public void ThenByUsesKeyComparer()
        {
            var list = new List<Person> { AbeAbrams, WilliamAbrams, CaseyJohnson, JackAbrams };
            list.Sort(Compare.Key<Person>.OrderBy(p => p.LastName).ThenBy(p => p.FirstName, StringComparer.InvariantCulture.Reverse()));
            CollectionAssert.AreEquivalent(new[] { WilliamAbrams, JackAbrams, AbeAbrams, CaseyJohnson }, list);
        }

        [TestMethod]
        public void ThenByDescendingSortsByKey()
        {
            var list = new List<Person> { AbeAbrams, WilliamAbrams, CaseyJohnson, JackAbrams };
            list.Sort(Compare.Key<Person>.OrderBy(p => p.LastName).ThenByDescending(p => p.FirstName));
            CollectionAssert.AreEquivalent(new[] { WilliamAbrams, JackAbrams, AbeAbrams, CaseyJohnson }, list);
        }

        [TestMethod]
        public void ThenByDescendingUsesKeyComparer()
        {
            var list = new List<Person> { AbeAbrams, WilliamAbrams, CaseyJohnson, JackAbrams };
            list.Sort(Compare.Key<Person>.OrderBy(p => p.LastName).ThenByDescending(p => p.FirstName, StringComparer.InvariantCulture.Reverse()));
            CollectionAssert.AreEquivalent(new[] { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson }, list);
        }
    }
}
