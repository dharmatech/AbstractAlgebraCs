using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebra;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

using static System.Console;

namespace pinter_03_D_Z2xZ2
{
    class Program
    {
        static void Main(string[] args)
        {
            var I = "I";
            var V = "V";
            var H = "H";
            var D = "D";
            
            var G = new Group<string>
            {
                Identity = I,
                Set = new[] { I, V, H, D }.ToMathSet(),
                Op = (a,b) => 
                {
                    if (a == I && b == I) return I;
                    if (a == I && b == V) return V;
                    if (a == I && b == H) return H;
                    if (a == I && b == D) return D;

                    if (a == V && b == I) return V;
                    if (a == V && b == V) return I;
                    if (a == V && b == H) return D;
                    if (a == V && b == D) return H;

                    if (a == H && b == I) return H;
                    if (a == H && b == V) return D;
                    if (a == H && b == H) return I;
                    if (a == H && b == D) return V;

                    if (a == D && b == I) return D;
                    if (a == D && b == V) return H;
                    if (a == D && b == H) return V;
                    if (a == D && b == D) return I;

                    throw new Exception();
                },
                OpString = "*"
            };

            G.ShowOperationTableColored(); WriteLine();

            // ZxZ(2,2)           

            var Z2xZ2 = new Group<(int, int)>
            {
                Identity = (0,0),
                Set = new[] { (0,0), (0,1), (1,0), (1,1) }.ToMathSet(),
                Op = (a,b) => ((a.Item1 + b.Item1) % 2, (a.Item2 + b.Item2) % 2),
                OpString = "+"
            };

            Z2xZ2.ShowOperationTableColored(); WriteLine();


            (int,int) Isomorphism_0(string a)
            {
                return new[] { (0, 0), (0, 1), (1, 0), (1, 1) }.ElementAt(new[] { I, V, H, D }.ToList().IndexOf(a));
            }


            (int, int) NonIsomorphism(string a)
            {
                return new[] { (0, 0), (0, 1), (1, 0), (1, 0) }.ElementAt(new[] { I, V, H, D }.ToList().IndexOf(a));
            }



            {
                var result = G.Set.ConvertAll(Isomorphism_0);
            }

            bool IsIsomorphism<T1,T2>(Group<T1> A, Group<T2> B, Func<T1, T2> f)
            {
                foreach (var x in A.Set)
                {
                    foreach (var y in A.Set)
                    {
                        // f(A.Op(x,y)) == B.Op(f(x), f(y))

                        if (EqualityComparer<T2>.Default.Equals(f(A.Op(x, y)), B.Op(f(x), f(y))) == false)
                        {
                            return false;
                        }           
                    }
                }

                return true;
            }

            {
                var result = IsIsomorphism(G, Z2xZ2, Isomorphism_0);
            }

            {
                var result = IsIsomorphism(G, Z2xZ2, NonIsomorphism);
            }


            void DisplayFunction<T1,T2>(Group<T1> A, Func<T1,T2> f)
            { foreach (var elt in A.Set) WriteLine("{0} -> {1}", elt, f(elt)); }


            DisplayFunction(G, Isomorphism_0); WriteLine();

            DisplayFunction(G, NonIsomorphism); WriteLine();




            
            IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> ls, int n)
            {
                if (n == 1) return ls.Select(elt => new T[] { elt });

                return
                    GetPermutations(ls, n - 1)
                    .SelectMany(
                        elt => ls.Where(item => elt.Contains(item) == false),
                        (a, b) => a.Concat(new T[] { b }));
            }


            //var injective_functions =
            //    GetPermutations(Z2xZ2.Set, Z2xZ2.Set.Count)
            //        .Select<(int,int), Func<string,(int,int)>>(seq => a => seq.ElementAt(G.Set.ToList().IndexOf(a)));

            var injective_functions =
                GetPermutations(Z2xZ2.Set, Z2xZ2.Set.Count)
                    .Select(seq =>
                    {
                        Func<string, (int, int)> f = a => seq.ElementAt(G.Set.ToList().IndexOf(a));

                        return f;
                    });

            WriteLine("injective_functions.Count: {0}\n", injective_functions.Count());

            foreach (var f in injective_functions)
            {
                DisplayFunction(G, f); WriteLine();

                WriteLine("    {0}\n", IsIsomorphism(G, Z2xZ2, f) ? "isomorphism" : "");
            }


            IEnumerable<Func<T1, T2>> GenerateInjectiveFunctions<T1,T2>(Group<T1> A, Group<T2> B)
            {
                return GetPermutations(B.Set, B.Set.Count)
                    .Select(seq =>
                    {
                        Func<T1, T2> f = a => seq.ElementAt(A.Set.ToList().IndexOf(a));

                        return f;
                    });
            }



            //bool IsIsomorphic<T1,T2>(Group<T1> A, Group<T2> B) =>
            //    GenerateInjectiveFunctions(A, B).Any(f => IsIsomorphism(A, B, f));

            bool IsIsomorphic<T1, T2>(Group<T1> A, Group<T2> B) =>
                A.Set.Count == B.Set.Count
                &&
                GenerateInjectiveFunctions(A, B).Any(f => IsIsomorphism(A, B, f));


            {
                var result = IsIsomorphic(G, Z2xZ2);
            }

            {
                var result = IsIsomorphic(Z2xZ2, G);
            }



            bool Divisible(int a, int b) => a % b == 0;

            bool Prime(int n) =>
                Enumerable.Range(1, n / 2).Skip(1).Any(elt => Divisible(n, elt)) == false;



            bool IsSquare(int n)
            {
                int Square(int a) => a * a;

                return n == Square(Convert.ToInt32(Math.Sqrt(n)));
            }

            
            bool IsSquareOfPrime(int n)
            {
                return
                    IsSquare(n)
                    &&
                    Prime(Convert.ToInt32(Math.Sqrt(n)));
            }

            {
                foreach (var i in Enumerable.Range(1, 30))
                    WriteLine("{0} : {1}", i, IsSquareOfPrime(i));
            }

            WriteLine();

            int IntSqrt(int n)
            {
                var result = Convert.ToInt32(Math.Sqrt(n));

                if (result * result == n) return result;

                throw new Exception();
            }

            //bool IsSquareOfPrime(int n)
            //{
            //    int Square(int a) => a * a;

            //    return n == Square(Convert.ToInt32(Math.Sqrt(n)));
            //}

            {
                // var result_a = IsSquareOfPrime()
            }



            Group<int> Z(int n) =>
                new Group<int>
                {
                    Identity = 0,
                    Set = Enumerable.Range(0, n).ToMathSet(),
                    Op = (a, b) => (a + b) % n,
                    OpString = "+"
                };


            //Group<(int, int)> ZxZ(int a, int b)
            //{
            //    var set = new List<(int, int)>();

            //    foreach (var i in Enumerable.Range(0, a))
            //        foreach (var j in Enumerable.Range(0,b))
            //            set.Add((i, j));

            //    return
            //        new Group<(int, int)>
            //        {
            //            Identity = (0, 0),
            //            Set = set.ToMathSet(),
            //            Op = (x,y) => ((x.Item1 + y.Item1) % a, (x.Item2 + y.Item2) % b),
            //            OpString = "+"
            //        };
            //}


            // Za X Zb
            // integers modulo a   X   integers modulo b

            Group<(int, int)> ZxZ(int a, int b) =>
                new Group<(int, int)>
                {
                    Identity = (0, 0),
                    Set = 
                        Enumerable.Range(0, a).SelectMany(i => 
                            Enumerable.Range(0,b).Select(j => 
                                (i,j))).ToMathSet(),
                    Op = (x, y) => ((x.Item1 + y.Item1) % a, (x.Item2 + y.Item2) % b),
                    OpString = "+"
                };
            


            //{
            //    var result =
            //        Enumerable.Range(0, 5)
            //            .Select(i => 
            //                Enumerable.Range(0, 3)
            //                    .Select(j => (i, j))
            //                    .ToList())
            //            .ToList();
            //}

            {
                var result =
                    Enumerable.Range(0, 5)
                        .SelectMany(i =>
                            Enumerable.Range(0, 3)
                                .Select(j => (i, j))
                                .ToList())
                        .ToList();
            }


            // IsomorphicImage(G)
            //
            // if G is size 4, this may return:
            //    Group<int>
            //    Group<(int,int)>
            //
            // however, the return type must be known at compile time
            //
            // and which one to use is being discovered at runtime
            //
            // is there a language where this can be executed at compile time?


            // Group<T2> IsomorphicImage<T1,T2>(Group<T1> A)

            string IsomorphicImage<T1>(Group<T1> A)
            {
                // IF     group size is p² where p is a prime number
                // THEN   G ≅ ℤp²   or   G ≅ ℤp x ℤp



                if (IsSquareOfPrime(A.Set.Count))
                {
                    var n = IntSqrt(A.Set.Count);

                    // Z(n)

                    // ZxZ(n,n)

                    // if (IsIsomorphic(A, Z(n))) return Z(n);

                    if (IsIsomorphic(A, Z(n * n))) return String.Format("Z{0}", n * n);

                    if (IsIsomorphic(A, ZxZ(n, n))) return String.Format("Z{0}xZ{0}", n);
                }

                // var n = Math.Sqrt(A.Set.Count);

                

                //Convert.ToInt32(Math.Sqrt(A.Set.Count))

                //if (Prime(Convert.ToInt32(Math.Sqrt(A.Set.Count)))



                //if (A.Set.Count == 4)
                //{
                    
                //}

                throw new Exception();
            }

            WriteLine("G is isomorphic to {0}", IsomorphicImage(G));

            {
                var result = IsIsomorphic(Z(3), Z(2));
            }
        }
    }
}
