using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbstractAlgebraGapPerm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractAlgebraGapPerm.Tests
{
    [TestClass()]
    public class GapPermTests
    {
        [TestMethod()]
        public void GapPerm_op_equal() =>
                    Assert.IsTrue(new GapPerm("(12)") == new GapPerm("(12)"));

        [TestMethod()]
        public void GapPerm_equal() =>
            Assert.AreEqual(
                new GapPerm("(12)"),
                new GapPerm(0, 2, 1));

        [TestMethod()]
        public void GapPerm_equal_HashCode() =>
            Assert.AreEqual(
                new GapPerm("(12)").GetHashCode(),
                new GapPerm(0, 2, 1).GetHashCode());

        [TestMethod()]
        public void GapPerm_Inverse() =>
            Assert.AreEqual(
                new GapPerm("(1234)").Inverse(),
                new GapPerm("(1432)"));

        [TestMethod()]
        public void Identity_Apply() =>
            Assert.AreEqual(new GapPerm().Apply(10), 10);

        [TestMethod()]
        public void Identity_Inverse() =>
            Assert.AreEqual(new GapPerm().Inverse(), new GapPerm());

        [TestMethod()]
        public void GapPerm_Identity_Compose()
        {
            var f = new GapPerm();
            var g = new GapPerm(0, 2, 1);

            Assert.AreEqual(f.Compose(g), g);
        }

        [TestMethod()]
        public void GapPerm_Compose_Identity()
        {
            var f = new GapPerm();
            var g = new GapPerm(0, 2, 1);

            Assert.AreEqual(g.Compose(f), g);
        }

        [TestMethod()]
        public void GapPerm_Inverse_is_simplified()
        {
            Assert.AreEqual(
                new GapPerm(0, 2, 1, 3).Inverse(),
                new GapPerm(0, 2, 1));
        }

        //[TestMethod()]
        //public void GapPerm_ToDisjointCycles()
        //{
        //    Assert.AreEqual(
        //        new GapPerm("(123)(234)(456)").ToDisjointCycles(),
        //        new GapPerm("(12)(3456)"));
        //}



    }
}