using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbstractAlgebraFunctionIntInt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractAlgebraFunctionIntInt.Tests
{
    [TestClass()]
    public class FunctionIntIntTests
    {
        [TestMethod()] public void FunctionIntInt_params() => Assert.AreEqual(
            new FunctionIntInt(new[] { (1, 2), (2, 3), (3, 4), (4, 1) }),
            new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)));

        [TestMethod()] public void FunctionIntInt_out_of_order() => Assert.AreEqual(
            new FunctionIntInt(new[] { (1, 2), (2, 3), (3, 4), (4, 1) }),
            new FunctionIntInt((1, 2), (3, 4), (2, 3), (4, 1)));

        [TestMethod()] public void FunctionIntInt_not_equal() => Assert.AreNotEqual(
            new FunctionIntInt(new[] { (1, 2), (2, 3), (3, 4), (4, 1) }),
            new FunctionIntInt((1, 2), (2, 3), (3, 4)));

        [TestMethod()] public void FunctionIntInt_FromString() => Assert.AreEqual(
            new FunctionIntInt(new[] { (1, 4), (2, 3), (3, 2), (4, 1) }),
            FunctionIntInt.FromString("4321"));

        [TestMethod()] public void FunctionIntInt_InHashSet() =>
            Assert.IsTrue(
                new HashSet<FunctionIntInt>()
                {
                    new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)),
                    new FunctionIntInt((5, 6), (6, 7), (7, 8), (8, 9)),
                    new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)),
                    new FunctionIntInt((5, 6), (6, 7), (7, 8), (8, 9))
                }.Count == 2);

        [TestMethod()] public void Pinter_13_A_1()
        {
            var ε = new FunctionIntInt((1, 1), (2, 2), (3, 3));
            var α = new FunctionIntInt((1, 1), (2, 3), (3, 2));
            var β = new FunctionIntInt((1, 3), (2, 1), (3, 2));
            var γ = new FunctionIntInt((1, 2), (2, 1), (3, 3));
            var σ = new FunctionIntInt((1, 2), (2, 3), (3, 1));
            var κ = new FunctionIntInt((1, 3), (2, 2), (3, 1));

            var G = new[] { ε, α, β, γ, σ, κ };
            var H = new[] { ε, β, σ };

            var cosets = G.Select(a => H.Select(h => h.Compose(a))).Distinct(new FunctionSetComparer());

            Assert.IsTrue(
                new HashSet<IEnumerable<FunctionIntInt>>(cosets, new FunctionSetComparer())
                .SetEquals(
                    new[]
                    {
                        new[] { ε, β, σ },
                        new[] { α, κ, γ }
                    }
                ));
        }
    }

    //[TestClass()] public class GapPermTests
    //{
    //    [TestMethod()]
    //    public void GapPerm_op_equal() =>
    //        Assert.IsTrue(new GapPerm("(12)") == new GapPerm("(12)"));
        
    //    [TestMethod()]
    //    public void GapPerm_equal() =>
    //        Assert.AreEqual(
    //            new GapPerm("(12)"),
    //            new GapPerm(0, 2, 1));

    //    [TestMethod()]
    //    public void GapPerm_equal_HashCode() =>
    //        Assert.AreEqual(
    //            new GapPerm("(12)").GetHashCode(),
    //            new GapPerm(0, 2, 1).GetHashCode());

    //    [TestMethod()]
    //    public void GapPerm_Inverse() =>
    //        Assert.AreEqual(
    //            new GapPerm("(1234)").Inverse(),
    //            new GapPerm("(1432)"));
    //}
}