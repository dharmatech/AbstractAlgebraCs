using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCartesianProduct;

using static AbstractAlgebraStandardGroupS3.Utils;

using static System.Console;

namespace pinter_14_E_6_S3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
                        
            S3.ShowOperationTableColored();

            WriteLine("subgroups       : {0}", S3.ProperSubgroups());
            WriteLine("normal subgroups: {0}", S3.NormalProperSubgroups());

            {
                // var H = new[] { ε, β, σ }.ToMathSet();

                var H = new Group<GapPerm>
                {
                    Identity = ε,
                    Set = new MathSet<GapPerm>(new[] { ε, β, σ }),
                    Op = (a, b) => a.Compose(b),
                    Lookup = S3.Lookup
                };

                // var result = S3.Set.Select(a => H.RightCoset(a)).ToMathSet();

                // var result = S3.Set.Select(a => H.RightCoset(a) == H.LeftCoset(a) ? H.RightCoset(a) : new MathSet<GapPerm>()).ToMathSet();

                var S = S3.Set
                    .Select(a => H.RightCoset(a) == H.LeftCoset(a) ? H.RightCoset(a) : new MathSet<GapPerm>())
                    .Aggregate((a, b) => a.Union(b).ToMathSet())
                    .ConvertAll(S3.Lookup);

                WriteLine("H (normal subgroup of S): {0}", H.Set.ConvertAll(S3.Lookup));
                WriteLine("S (subgroup of G)       : {0}", S);
            }

            {
                foreach (var H in S3.ProperSubgroups())
                {
                    var S = S3.Set
                        .Select(a => H.RightCoset(a) == H.LeftCoset(a) ? H.RightCoset(a) : new MathSet<GapPerm>())
                        .Aggregate((a, b) => a.Union(b).ToMathSet())
                        .ConvertAll(S3.Lookup);

                    WriteLine("H (normal subgroup of S): {0}", H.Set.ConvertAll(S3.Lookup));
                    WriteLine("S (subgroup of G)       : {0}", S);
                }

                //foreach (var H_ in S3.ProperSubgroups())
                //{
                //    var H = new Group<GapPerm>
                //    {
                //        Identity = ε,
                //        Set = new MathSet<GapPerm>(new[] { ε, β, σ }),
                //        Op = (a, b) => a.Compose(b),
                //        Lookup = lookup
                //    };
                //}
            }

        }
    }
}
