using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbstractAlgebraGapPerm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraMathSet;

namespace AbstractAlgebraGroup.Tests
{
    [TestClass()]
    public class GroupTests
    {
        //FunctionIntInt ε = new FunctionIntInt((1, 1), (2, 2), (3, 3));
        //FunctionIntInt α = new FunctionIntInt((1, 1), (2, 3), (3, 2));
        //FunctionIntInt β = new FunctionIntInt((1, 3), (2, 1), (3, 2));
        //FunctionIntInt γ = new FunctionIntInt((1, 2), (2, 1), (3, 3));
        //FunctionIntInt σ = new FunctionIntInt((1, 2), (2, 3), (3, 1));
        //FunctionIntInt κ = new FunctionIntInt((1, 3), (2, 2), (3, 1));
        
        [TestMethod()] public void Group_ProperSubgroups()
        {
            var ε = new FunctionIntInt((1, 1), (2, 2), (3, 3));
            var α = new FunctionIntInt((1, 1), (2, 3), (3, 2));
            var β = new FunctionIntInt((1, 3), (2, 1), (3, 2));
            var γ = new FunctionIntInt((1, 2), (2, 1), (3, 3));
            var σ = new FunctionIntInt((1, 2), (2, 3), (3, 1));
            var κ = new FunctionIntInt((1, 3), (2, 2), (3, 1));

            string lookup(FunctionIntInt f)
            {
                var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "y"), (σ, "σ"), (κ, "k") };

                return items.First(elt => f == elt.Item1).Item2;
            }

            var S_3 = new Group<FunctionIntInt>
            {
                Identity = ε,
                Set = new MathSet<FunctionIntInt>(new[] { ε, α, β, γ, σ, κ }),
                Op = (a, b) => a.Compose(b),
                Lookup = lookup
            };

            Assert.AreEqual(
                S_3.ProperSubgroups().ConvertAll(elt => elt.Set),

                new MathSet<MathSet<FunctionIntInt>>()
                {
                    new MathSet<FunctionIntInt>(){ ε, α },
                    new MathSet<FunctionIntInt>(){ ε, γ },
                    new MathSet<FunctionIntInt>(){ ε, κ },
                    new MathSet<FunctionIntInt>(){ ε, β, σ }
                });
        }

    }
}