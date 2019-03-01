using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;

using AbstractAlgebraPowerSet;
using AbstractAlgebraCartesianProduct;

using static System.Console;

namespace AbstractAlgebraGroup
{
    public class Group<T>
    {
        public MathSet<T> Set;

        public Func<T, T, T> Op;

        public T Identity;

        public Func<T, string> Lookup = elt => elt.ToString();

        public string OpString;

        // ----------------------------------------------------------------------

        public override string ToString() => Set.ConvertAll(a => Lookup(a)).ToString();


        public T Inverse(T a) => Set.First(elt => EqualityComparer<T>.Default.Equals(Op(a, elt), Identity));

        public int Order(T a)
        {
            var n = 1;

            while(true)
            {
                if (EqualityComparer<T>.Default.Equals(OpN(a, n), Identity)) return n;

                n++;
            }
        }
        
        public Group<T> Subgroup(IEnumerable<T> elts) =>
            new Group<T>
            {
                Identity = Identity,
                Set = elts.ToMathSet(),
                Op = Op,
                Lookup = Lookup,
                OpString = OpString
            };
        
        //public MathSet<Group<T>> Subgroups()
        //{
        //    var subsets = Set
        //        .PowerSet()
        //        .OrderBy(elt => elt.Count())
        //        .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(Op(elt.ElementAt(0), elt.ElementAt(1)))).All(elt => elt))
        //        .Where(set => set.Count() > 0)
        //        .Select(set => set.ToMathSet())
        //        .ToMathSet();

        //    return subsets.ConvertAll(subset => new Group<T> { Set = subset, Op = Op, Identity = Identity, Lookup = Lookup });
        //}
        
        public MathSet<Group<T>> Subgroups()
        {
            bool is_subgroup(IEnumerable<T> set)
            {
                foreach (var a in set)
                    foreach (var b in set)
                        // if (set.Contains(a.Compose(b)) == false) return false;
                        if (set.Contains(Op(a,b)) == false) return false;

                return true;
            }

            int NumberOfSetBits(int i)
            {
                i = i - ((i >> 1) & 0x55555555);
                i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
                return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
            }

            List<List<T>> power_set_divisible_size<T>(IEnumerable<T> set_)
            {
                var set = set_.ToList();

                var power_set_size = Math.Pow(2, set.Count());

                var subsets = new List<List<T>>();

                for (var counter = 1; counter < power_set_size; counter++)
                {
                    if (set.Count % NumberOfSetBits(counter) == 0)
                    {
                        var subset = new List<T>();

                        for (var i = 0; i < set.Count(); i++)
                            if ((counter & (1 << i)) != 0)
                                subset.Add(set[i]);

                        subsets.Add(subset);
                    }
                }

                return subsets;
            }

            {
                var subsets = power_set_divisible_size(Set)
                                .AsParallel()
                                .Where(set => set.Contains(Identity))
                                .Where(is_subgroup)
                                .Select(elt => elt.ToMathSet())
                                .ToMathSet();

                return subsets.ConvertAll(subset => new Group<T>
                {
                    Set = subset,
                    Op = Op,
                    Identity = Identity,
                    Lookup = Lookup,
                    OpString = OpString
                });
            }
        }
        
        public MathSet<Group<T>> ProperSubgroups()
        {
            return Subgroups()
                .Where(G => G.Set.Count() > 1)
                .Where(G => G.Set.Count() < Set.Count())
                .ToMathSet();
        }

        public bool ClosedConjugates(Group<T> G) =>
            Set.All(a => G.Conjugates(a).IsSubsetOf(Set));

        public MathSet<Group<T>> NormalSubgroups() =>
            // Subgroups().Where(H => ClosedConjugates(this)).ToMathSet();
            Subgroups().Where(H => H.ClosedConjugates(this)).ToMathSet();

        public MathSet<Group<T>> NormalProperSubgroups() =>
            ProperSubgroups().Where(H => H.ClosedConjugates(this)).ToMathSet();

        // IsSubgroup(IEnumerable<T> elts)

        // Subgroup(IEnumerable<T> elts)    constructs a subgroup instance


        public T Conjugate(T a, T x) => Op(x, Op(a, Inverse(x)));

        public MathSet<T> ConjugacyClass(T a) => Set.ConvertAll(x => Conjugate(a, x));

        public MathSet<T> Conjugates(T a) => Set.ConvertAll(x => Conjugate(a, x));


        

        public bool Commute(T a, T b) => EqualityComparer<T>.Default.Equals(Op(a, b), Op(b, a));
                
        public Func<T, bool> CommutesWith(T a) => b => Commute(a, b);

        public MathSet<T> Center() => Set.Where(a => Set.All(CommutesWith(a))).ToMathSet();



        public MathSet<T> Centralizer(T a) =>
            Set.Where(x => EqualityComparer<T>.Default.Equals(Op(a, x), Op(x, a))).ToMathSet();

        public T Op_(params T[] elts) => elts.Aggregate(Op);

        public T OpN(T a, int n)
        {
            return Enumerable.Range(1, n).Select(i => a).Aggregate(Op);
        }


        public MathSet<T> RightCoset(T elt) => Set.ConvertAll(item => Op(item, elt));

        public MathSet<T> LeftCoset(T elt) => Set.ConvertAll(item => Op(elt, item));


        public void ShowOperationTable()
        {
            var width = Set.Select(elt => Lookup(elt).Count()).Max();

            foreach (var x in Set)
            {
                foreach (var y in Set) Write("{0} ", Lookup(Op(x, y)).PadLeft(width));

                WriteLine();
            }
        }

        public void ShowOperationTableColored()
        {
            WriteLine("operation table:\n");

            var width = Set.Select(elt => Lookup(elt).Count()).Max();

            var color_thunks = new List<Action>()
                {
                    () => { ForegroundColor = ConsoleColor.White; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Red; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Yellow; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Green; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Blue; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Cyan; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Magenta; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkRed; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkBlue; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkGreen; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkMagenta; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkYellow; BackgroundColor = ConsoleColor.Black; }
                };

            void pick_color(T elt)
            {
                var i = Set.ToList().IndexOf(elt);

                color_thunks[i % color_thunks.Count()]();
            }


            Write("{0}|", "".PadLeft(width));

            foreach (var elt in Set) { pick_color(elt); Write("{0} ", Lookup(elt).PadLeft(width)); }

            WriteLine();


            color_thunks[0](); WriteLine(new String('-', (Set.Count() + 1) * (width + 1)));

            foreach (var x in Set)
            {
                pick_color(x); Write("{0}", Lookup(x).PadLeft(width)); color_thunks[0](); Write("|");

                foreach (var y in Set)
                {
                    var elt = Op(x, y);

                    pick_color(elt);

                    Write("{0} ", Lookup(elt).PadLeft(width));
                }

                WriteLine();
            }

            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
        }

        public void ShowInverses()
        {
            foreach (var item in Set) WriteLine("{0,2} inverse: {1}", Lookup(item), Lookup(Inverse(item)));
            WriteLine();
        }

        public void ShowConjugates()
        {
            foreach (var a in Set)
            {
                foreach (var x in Set)
                    WriteLine("{0} conjugate with {1} : {2} : {3}",
                        Lookup(a),
                        Lookup(x),
                        Lookup(x) + Lookup(a) + Lookup(Inverse(x)),
                        Lookup(Conjugate(a, x)));

                WriteLine();
            }
        }

        public void ShowCentralizers()
        {
            foreach (var a in Set)
            {
                WriteLine("centralizer of {0}: {1}\n", Lookup(a), Centralizer(a).ConvertAll(Lookup));

                WriteLine("  cosets:");

                foreach (var x in Set)
                    WriteLine("    {0} {1,4} -> {2}",
                        Centralizer(a).ConvertAll(Lookup),
                        Lookup(x),
                        Centralizer(a).ConvertAll(elt => Lookup(Op(elt, x))));

                WriteLine();

                WriteLine("  cosets: {0}\n", Set.ConvertAll(elt => Centralizer(a).ConvertAll(item => Lookup(Op(item, elt)))));
            }
        }




    }
}
