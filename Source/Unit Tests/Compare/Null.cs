using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparers;

namespace Compare_
{
    [TestClass]
    public class _Null
    {
        [TestMethod]
        public void ComparesUnequalElementsAsEqual()
        {
            var comparer = Compare<int>.Null();
            Assert.AreEqual(0, comparer.Compare(13, 17));
            Assert.IsTrue(comparer.Equals(19, 21));
        }

        [TestMethod]
        public void ComparesNullElementsAsEqualToValueElements()
        {
            var comparer = Compare<int?>.Null();
            Assert.AreEqual(0, comparer.Compare(null, 17));
            Assert.AreEqual(0, comparer.Compare(13, null));
            Assert.IsTrue(comparer.Equals(null, 21));
            Assert.IsTrue(comparer.Equals(19, null));
        }
    }
}
