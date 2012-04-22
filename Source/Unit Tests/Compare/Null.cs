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
        public void ComparesAllElementsAsEqual()
        {
            var comparer = Compare.Null<int>();
            Assert.AreEqual(0, comparer.Compare(13, 17));
            Assert.IsTrue(comparer.Equals(19, 21));
        }
    }
}
