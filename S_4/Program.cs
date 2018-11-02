using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using AbstractAlgebraMathSet;

using AbstractAlgebra.Program;

using AbstractAlgebraFunctionIntInt;

using static System.Console;

using static AbstractAlgebra.Program.Utils;
using static AbstractAlgebraSymmetricGroup.Utils;
using static AbstractAlgebra.alternating_group.Utils;

// using AbstractAlgebra.permutation;

using AbstractAlgebraCycles;

namespace S_4
{
    class Program
    {
        static void Main(string[] args)
        {

            WriteLine("---------- 13.A.5 ----------"); WriteLine();

            {            
                var G = SymmetricGroup(4);

                var H = AlternatingGroup(4);

                string lookup(FunctionIntInt f) => G.ToList().FindIndex(elt => elt.Equals(f)).ToString();

                WriteLine("S_4:");

                foreach (var elt in G) WriteLine("{0,-4} {1}", lookup(elt), elt); WriteLine();

                show_operation_table_colored(G, (a, b) => a.Compose(b), lookup);
                                
                WriteLine();

                WriteLine("H = {0}", H.ConvertAll(lookup));

                WriteLine("order H = {0}", H.Count);
                WriteLine("index H = {0}", G.Count / H.Count);
                WriteLine();

                foreach (var a in G)
                    WriteLine("H{0,2} = {1}",
                        lookup(a),
                        H.ConvertAll(h => lookup(h.Compose(a))));

                WriteLine(); WriteLine("cosets:");

                foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                    WriteLine(coset.ConvertAll(f => lookup(f)));

                WriteLine();
            }

            {
                var S_4 = SymmetricGroup(4);

                // var result = new Cycles(S_4.ConvertAll(set => set.to_disjoint_cycles().Select(elt => new Cycle(elt))).ToList());
                
                var result = S_4.ConvertAll(set => new Cycles(set.to_disjoint_cycles_alt().Select(elt => new Cycle(elt))));

                var result_b = S_4.ConvertAll(set => set.to_disjoint_cycles_alt());
            }

            // represent group as set of generators

            // gap > GeneratorsOfGroup(SymmetricGroup(4));
            // [ (1, 2, 3, 4), (1, 2)]


            {
                // consider: https://math.stackexchange.com/questions/76176/enumerating-all-subgroups-of-the-symmetric-group

                // consider: https://math.stackexchange.com/questions/1569349/how-to-find-all-subgroups-of-a-group-in-gap

                // subgroups

                var G = SymmetricGroup(4);

                string lookup(FunctionIntInt f) => G.ToList().FindIndex(elt => elt.Equals(f)).ToString();
                
                WriteLine("subgroups:");

                //{
                //    var result = G.PowerSet();
                //}


                //var subgroups = G.PowerSet().OrderBy(elt => elt.Count())
                //    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                //    .Where(set => set.Count() > 0);

                //var subgroups = G.PowerSet()//.OrderBy(elt => elt.Count())
                //    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                //    .Where(set => set.Count() > 0);

                var identity = G.ElementAt(0);

                //var result_a = G.PowerSet().Count();

                // var result_b = G.PowerSet().Take(1).Where(set => set.Contains(identity));

                // var result_c = G.PowerSet().Where(set => set.Contains(identity)).Take(10).Select(elt => elt.ToList()).ToList();

                // var result_d = G.PowerSet().Where(set => set.Contains(identity)).Select(elt => elt.ToList()).ToList();

                // var result_e = G.PowerSet().Where(set => set.ToMathSet().Contains(identity)).Select(elt => elt.ToList()).ToList();

                //var result_f = G

                //    .PowerSet()

                //    .Where(set => set.ToMathSet().Contains(identity))

                //    .OrderBy(elt => elt.Count())

                //    ;

                //.Select(elt => elt.ToList()).ToList();


                //var subgroups = G

                //    .PowerSet()

                //    .Where(set => set.ToMathSet().Contains(G.ElementAt(0)))

                //    .OrderBy(elt => elt.Count())
                //    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                //    .Where(set => set.Count() > 0);


                //{
                //    var subgroups_b = G.AsParallel()

                //    .PowerSet()

                //    .Where(set => set.ToMathSet().Contains(G.ElementAt(0)))

                //    .OrderBy(elt => elt.Count())
                //    .Where(set => cartesian_product(set, set).Select(elt => set.Contains(elt.Item1.Compose(elt.Item2))).All(elt => elt))
                //    .Where(set => set.Count() > 0);

                //    var result = subgroups_b.Count();

                //}


                //{
                //    var id = G.ElementAt(0);

                //    var result = G.PowerSet().Count();

                //    var result_b = G.PowerSet().Where(set => set.Contains(id)).Count();
                //}


                //IEnumerable<(FunctionIntInt,FunctionIntInt)> cartesian_product(IEnumerable<FunctionIntInt> a, IEnumerable<FunctionIntInt> b)
                //{
                //    foreach (var x in a) foreach (var y in b) yield return (x, y);
                //}

                //{
                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = G.AsParallel().PowerSet().Count();

                //    stopwatch.Stop();

                //    WriteLine(stopwatch.Elapsed);
                //}

                //{
                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = G.AsParallel().PowerSet()
                //        .Where(set => set.Contains(G.ElementAt(0)))
                //        .Count();

                //    stopwatch.Stop();

                //    WriteLine(stopwatch.Elapsed); // 00:02:23.0190329
                //}


                //{
                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = G.AsParallel()
                //        .PowerSet()
                //        .Where(set => set.Contains(G.ElementAt(0)))
                //        .OrderBy(elt => elt.Count())
                //        .Count();

                //    stopwatch.Stop();

                //    WriteLine(stopwatch.Elapsed); // 00:04:01.2354253
                //}


                bool is_subgroup(IEnumerable<FunctionIntInt> set)
                {
                    foreach (var a in set)
                        foreach (var b in set)
                            if (set.Contains(a.Compose(b)) == false) return false;

                    return true;

                }

                //{
                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = G.AsParallel()

                //        .PowerSet()

                //        .Where(set => set.Contains(G.ElementAt(0)))

                //        .Where(is_subgroup)

                //        .Count();

                //    WriteLine("count: {0}", result);

                //    stopwatch.Stop();

                //    WriteLine(stopwatch.Elapsed); // 00:20:11.3889431
                //}


                bool divides_evenly(int a, int b) => a % b == 0;

                //{
                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = G.AsParallel()

                //        .PowerSet()

                //        // .Where(set => G.Count() % set.Count() == 0)

                //        // .Where(set => set.Count() > 0)

                //        .Where(set => set.Count() > 0 ? G.Count() % set.Count() == 0 : true)

                //        // .Where(set => set.Contains(G.ElementAt(0)))

                //        // .Where(is_subgroup)

                //        .Count();

                //    WriteLine("count: {0}", result);

                //    stopwatch.Stop();
                //                                  // count: 3587174
                //    WriteLine(stopwatch.Elapsed); // 00:08:03.1519598
                //}




                //{
                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = G.AsParallel()

                //        .PowerSet()

                //        // .Where(set => G.Count() % set.Count() == 0)
                //        .Where(set => set.Count() > 0)

                //        .Where(set => set.Contains(G.ElementAt(0)))

                //        .Where(set => set.Count() > 0 ? G.Count() % set.Count() == 0 : true)

                //        // .Where(is_subgroup)

                //        .Count();

                //    WriteLine("count: {0}", result);

                //    stopwatch.Stop();

                //    WriteLine(stopwatch.Elapsed);

                //    // count: 1632933
                //    // 00:07:43.2021730
                //}





                //{
                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = G.AsParallel()

                //        .PowerSet()

                //        .Where(set => set.Count() > 0)

                //        .Where(set => set.Contains(G.ElementAt(0)))

                //        .Where(set => G.Count() % set.Count() == 0)

                //        .Where(is_subgroup)

                //        .Count();

                //    stopwatch.Stop();

                //    WriteLine("count: {0}   elapsed: {1}", result, stopwatch.Elapsed);

                //    // count: 30   elapsed: 00:09:04.9828447
                //}

                // power set - bit values
                //
                // https://www.geeksforgeeks.org/power-set/

                // consider - other versions of power set:
                //
                // https://stackoverflow.com/questions/19890781/creating-a-power-set-of-a-sequence

                //{
                //    List<List<FunctionIntInt>> power_set(IEnumerable<FunctionIntInt> set_)
                //    {
                //        var set = set_.ToList();

                //        var power_set_size = Math.Pow(2, set.Count());

                //        var subsets = new List<List<FunctionIntInt>>();

                //        for (var counter = 0; counter < power_set_size; counter++)
                //        {
                //            var subset = new List<FunctionIntInt>();

                //            for (var i = 0; i < set.Count(); i++)
                //                if ((counter & (1 << i)) != 0)
                //                    subset.Add(set[i]);

                //            subsets.Add(subset);
                //        }

                //        return subsets;
                //    }

                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = power_set(G);

                //    stopwatch.Stop();

                //    WriteLine("count: {0}   elapsed: {1}", result.Count(), stopwatch.Elapsed);  // count: 16777216   elapsed: 00:00:37.3039173
                //}




                //{
                //    int NumberOfSetBits(int i)
                //    {
                //        i = i - ((i >> 1) & 0x55555555);
                //        i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
                //        return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
                //    }

                //    List<List<FunctionIntInt>> power_set_divisible_size(IEnumerable<FunctionIntInt> set_)
                //    {
                //        var set = set_.ToList();

                //        var power_set_size = Math.Pow(2, set.Count());

                //        var subsets = new List<List<FunctionIntInt>>();

                //        for (var counter = 1; counter < power_set_size; counter++)
                //        {
                //            // var bit_count = NumberOfSetBits(counter);

                //            if (set.Count % NumberOfSetBits(counter) == 0)
                //            {
                //                var subset = new List<FunctionIntInt>();

                //                for (var i = 0; i < set.Count(); i++)
                //                    if ((counter & (1 << i)) != 0)
                //                        subset.Add(set[i]);

                //                subsets.Add(subset);
                //            }
                //        }

                //        return subsets;
                //    }

                //    var stopwatch = new Stopwatch();

                //    stopwatch.Start();

                //    var result = power_set_divisible_size(G);

                //    stopwatch.Stop();

                //    WriteLine("count: {0}   elapsed: {1}", result.Count(), stopwatch.Elapsed);  // count: 3587174   elapsed: 00:00:08.6522378
                //}

                {
                    var elts = new[] { 1, 2, 3, 4 };

                    // variations - 2 at a time

                    var result = new[] { elts, elts }.CartesianProduct().Select(elt => elt.ToList()).ToList();
                }

                {
                    var elts = new[] { 1, 2, 3, 4 };

                    // variations - 3 at a time

                    var result = new[] { elts, elts, elts }.CartesianProduct().Select(elt => elt.ToList()).ToList();
                }

                {

                    var S4_subgroups = new MathSet<MathSet<FunctionIntInt>>(
                        new[]
                        {
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 2), (3, 4), (4, 3)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 2), (4, 4)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 4), (4, 2)), new FunctionIntInt((1, 1), (2, 4), (3, 2), (4, 3)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 2), (3, 4), (4, 3)), new FunctionIntInt((1, 1), (2, 3), (3, 2), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 4), (4, 2)), new FunctionIntInt((1, 1), (2, 4), (3, 2), (4, 3)), new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 3), (4, 4)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 2), (3, 4), (4, 3)), new FunctionIntInt((1, 2), (2, 1), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 3), (3, 1), (4, 4)), new FunctionIntInt((1, 3), (2, 1), (3, 2), (4, 4)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 2), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 3), (3, 1), (4, 4)), new FunctionIntInt((1, 3), (2, 1), (3, 2), (4, 4)), new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)), new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)), new FunctionIntInt((1, 4), (2, 1), (3, 2), (4, 3)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 4), (3, 3), (4, 1)), new FunctionIntInt((1, 4), (2, 1), (3, 3), (4, 2)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 3), (2, 2), (3, 4), (4, 1)), new FunctionIntInt((1, 4), (2, 2), (3, 1), (4, 3)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 4), (2, 2), (3, 3), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)), new FunctionIntInt((1, 2), (2, 1), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 4), (3, 3), (4, 1)), new FunctionIntInt((1, 4), (2, 1), (3, 3), (4, 2)), new FunctionIntInt((1, 4), (2, 2), (3, 3), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 2), (3, 4), (4, 3)), new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)), new FunctionIntInt((1, 3), (2, 2), (3, 4), (4, 1)), new FunctionIntInt((1, 4), (2, 2), (3, 1), (4, 3)), new FunctionIntInt((1, 4), (2, 2), (3, 3), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)), new FunctionIntInt((1, 3), (2, 4), (3, 2), (4, 1)), new FunctionIntInt((1, 4), (2, 3), (3, 1), (4, 2)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 4), (3, 1), (4, 3)), new FunctionIntInt((1, 3), (2, 1), (3, 4), (4, 2)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)), new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)), new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)), new FunctionIntInt((1, 4), (2, 1), (3, 2), (4, 3)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 4), (4, 2)), new FunctionIntInt((1, 1), (2, 4), (3, 2), (4, 3)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)), new FunctionIntInt((1, 2), (2, 3), (3, 1), (4, 4)), new FunctionIntInt((1, 2), (2, 4), (3, 3), (4, 1)), new FunctionIntInt((1, 3), (2, 1), (3, 2), (4, 4)), new FunctionIntInt((1, 3), (2, 2), (3, 4), (4, 1)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)), new FunctionIntInt((1, 4), (2, 1), (3, 3), (4, 2)), new FunctionIntInt((1, 4), (2, 2), (3, 1), (4, 3)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 2), (4, 4)), new FunctionIntInt((1, 4), (2, 2), (3, 3), (4, 1)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 2), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)), new FunctionIntInt((1, 2), (2, 4), (3, 1), (4, 3)), new FunctionIntInt((1, 3), (2, 1), (3, 4), (4, 2)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)), new FunctionIntInt((1, 4), (2, 2), (3, 3), (4, 1)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 2), (3, 4), (4, 3)), new FunctionIntInt((1, 2), (2, 1), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)), new FunctionIntInt((1, 3), (2, 4), (3, 2), (4, 1)), new FunctionIntInt((1, 4), (2, 3), (3, 1), (4, 2)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) }),
                            new MathSet<FunctionIntInt>(new []{ new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)), new FunctionIntInt((1, 1), (2, 2), (3, 4), (4, 3)), new FunctionIntInt((1, 1), (2, 3), (3, 2), (4, 4)), new FunctionIntInt((1, 1), (2, 3), (3, 4), (4, 2)), new FunctionIntInt((1, 1), (2, 4), (3, 2), (4, 3)), new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)), new FunctionIntInt((1, 2), (2, 1), (3, 3), (4, 4)), new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)), new FunctionIntInt((1, 2), (2, 3), (3, 1), (4, 4)), new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)), new FunctionIntInt((1, 2), (2, 4), (3, 1), (4, 3)), new FunctionIntInt((1, 2), (2, 4), (3, 3), (4, 1)), new FunctionIntInt((1, 3), (2, 1), (3, 2), (4, 4)), new FunctionIntInt((1, 3), (2, 1), (3, 4), (4, 2)), new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)), new FunctionIntInt((1, 3), (2, 2), (3, 4), (4, 1)), new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)), new FunctionIntInt((1, 3), (2, 4), (3, 2), (4, 1)), new FunctionIntInt((1, 4), (2, 1), (3, 2), (4, 3)), new FunctionIntInt((1, 4), (2, 1), (3, 3), (4, 2)), new FunctionIntInt((1, 4), (2, 2), (3, 1), (4, 3)), new FunctionIntInt((1, 4), (2, 2), (3, 3), (4, 1)), new FunctionIntInt((1, 4), (2, 3), (3, 1), (4, 2)), new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)) })
                        }
                    );

                    

                }


                {
                    WriteLine(new FunctionIntInt((1, 1), (2, 2)) == new FunctionIntInt((2, 2), (1, 1)));

                    WriteLine(new FunctionIntInt((1, 1), (2, 2)).GetHashCode());
                    WriteLine(new FunctionIntInt((2, 2), (1, 1)).GetHashCode());
                }

                {
                    // https://stackoverflow.com/questions/12171584/what-is-the-fastest-way-to-count-set-bits-in-uint32-in-c-sharp

                    int NumberOfSetBits(int i)
                    {
                        i = i - ((i >> 1) & 0x55555555);
                        i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
                        return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
                    }

                    List<List<FunctionIntInt>> power_set_divisible_size(IEnumerable<FunctionIntInt> set_)
                    {
                        var set = set_.ToList();

                        var power_set_size = Math.Pow(2, set.Count());

                        var subsets = new List<List<FunctionIntInt>>();

                        for (var counter = 1; counter < power_set_size; counter++)
                        {
                            if (set.Count % NumberOfSetBits(counter) == 0)
                            {
                                var subset = new List<FunctionIntInt>();

                                for (var i = 0; i < set.Count(); i++)
                                    if ((counter & (1 << i)) != 0)
                                        subset.Add(set[i]);

                                subsets.Add(subset);
                            }
                        }

                        return subsets;
                    }

                    var stopwatch = new Stopwatch();

                    stopwatch.Start();

                    var ident = G.ElementAt(0);
                    
                    var result =
                        power_set_divisible_size(G)
                        .AsParallel()
                        .Where(set => set.Contains(ident))
                        .Where(is_subgroup)
                        // .FindAll(set => set.Contains(ident))
                        // .FindAll(is_subgroup)
                        // .Count()
                        .ToList()
                        ;

                    stopwatch.Stop();

                    foreach (var elt in result)
                        WriteLine(elt.ToMathSet());

                    //WriteLine("count: {0}   elapsed: {1}", result, stopwatch.Elapsed); // count: 30   elapsed: 00:01:03.9496887
                }


                var subgroups = G.AsParallel()

                    .PowerSet()

                    .Where(set => set.ToMathSet().Contains(G.ElementAt(0)))

                    .OrderBy(elt => elt.Count())
                    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                    .Where(set => set.Count() > 0);

                // specialized version of CartesianProduct for two sequences
                //     result is sequence of two-elements tuples
                //         (a,b) (b,c) (c,d) ...


                foreach (var subgroup in subgroups)
                    WriteLine(subgroup.Select(elt => lookup(elt)).ToMathSet());

                WriteLine(); WriteLine("proper subgroups:");

                var proper_subgroups = subgroups.Where(subgroup => subgroup.Count() > 1).Where(subgroup => subgroup.Count() < G.Count());

                foreach (var subgroup in proper_subgroups)
                    WriteLine(subgroup.Select(elt => lookup(elt)).ToMathSet());

                WriteLine();

                {
                    foreach (var H in proper_subgroups.Select(elt => elt.ToMathSet()))
                    {
                        WriteLine("------------------------------");

                        WriteLine("H = {0}", H.ConvertAll(lookup));

                        WriteLine(); WriteLine("cosets:");

                        foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                            WriteLine(coset.ConvertAll(f => lookup(f)));

                        WriteLine();

                        {
                            var ls = G.Select(a => (a, H.ConvertAll(h => h.Compose(a))));

                            foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                            {
                                foreach (var a in ls.Where(elt => coset == elt.Item2).Select(elt => elt.a))
                                    Write("{0} {1} = ", H.ConvertAll(lookup), lookup(a));

                                WriteLine(coset.ConvertAll(lookup));
                            }
                        }
                    }
                }
            }
        }
    }
}
