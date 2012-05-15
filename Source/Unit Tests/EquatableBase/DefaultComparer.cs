﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparers;
using EqualityComparers;

namespace EquatableBase_
{
    [TestClass]
    public class _DefaultComparer
    {
        private sealed class Person : EquatableBase<Person>
        {
            static Person()
            {
                DefaultComparer = EqualityCompare<Person>.EquateBy(p => p.LastName).ThenEquateBy(p => p.FirstName);
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private static readonly Person AbeAbrams = new Person { FirstName = "Abe", LastName = "Abrams" };
        private static readonly Person AbeAbrams2 = new Person { FirstName = "Abe", LastName = "Abrams" };
        private static readonly Person JackAbrams = new Person { FirstName = "Jack", LastName = "Abrams" };

        [TestMethod]
        public void ImplementsComparerDefault()
        {
            var netDefault = EqualityComparer<Person>.Default;
            Assert.IsTrue(netDefault.Equals(AbeAbrams, AbeAbrams2));
            Assert.AreEqual(netDefault.GetHashCode(AbeAbrams), netDefault.GetHashCode(AbeAbrams2));
            Assert.IsFalse(netDefault.Equals(AbeAbrams, JackAbrams));
        }
    }
}
