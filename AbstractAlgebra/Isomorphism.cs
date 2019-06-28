using System;
using System.Collections.Generic;
using System.Linq;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGetPermutations;

using static AbstractAlgebraStandardGroupZ.Utils;
using static AbstractAlgebraStandardGroupZxZ.Utils;
using static AbstractAlgebraStandardGroupZxZxZ.Utils;
using static AbstractAlgebraStandardGroupD4.Utils;

using static AbstractAlgebraGenerate.Utils;
using static AbstractAlgebraIsomorphism.Utils;

namespace AbstractAlgebraIsomorphism
{
    static class Utils
    {
        static bool Divisible(int a, int b) => a % b == 0;

        static bool Prime(int n) =>
            Enumerable.Range(1, n / 2).Skip(1).Any(elt => Divisible(n, elt)) == false;

        static bool IsSquare(int n)
        {
            int Square(int a) => a * a;

            return n == Square(Convert.ToInt32(Math.Sqrt(n)));
        }
        
        public static bool IsSquareOfPrime(int n)
        {
            return
                IsSquare(n)
                &&
                Prime(Convert.ToInt32(Math.Sqrt(n)));
        }

        public static int IntSqrt(int n)
        {
            var result = Convert.ToInt32(Math.Sqrt(n));

            if (result * result == n) return result;

            throw new Exception();
        }
    }

    public static class Extensions
    {
        public static IEnumerable<Func<T1, T2>> GenerateInjectiveFunctions<T1, T2>(Group<T1> A, Group<T2> B)
        {
            return B.Set.GetPermutations(B.Set.Count)
                .Select(seq =>
                {
                    Func<T1, T2> f = a => seq.ElementAt(A.Set.ToList().IndexOf(a));

                    return f;
                });
        }
                
        public static bool IsIsomorphism<T1, T2>(Group<T1> A, Group<T2> B, Func<T1, T2> f)
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

        public static bool IsIsomorphic<T1, T2>(this Group<T1> A, Group<T2> B) =>
            A.Set.Count == B.Set.Count
            &&
            GenerateInjectiveFunctions(A, B).Any(f => IsIsomorphism(A, B, f));
        
        public static string IsomorphicImage<T>(this Group<T> A)

        {
            if (A.Set.Count == 2) return "S2";

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


            // See Pinter 13.H: Survey of All Eight-Element Groups

            if (A.Set.Count == 8)
            {
                if (A.IsIsomorphic(Z(8))) return "Z8";

                if (A.IsIsomorphic(ZxZxZ(2, 2, 2))) return "Z2xZ2xZ2";
                                
                if (A.IsIsomorphic(ZxZ(4, 2))) return "Z4xZ2";

                if (A.IsIsomorphic(D4)) return "D4";

                //{
                //    var eqs = new Dictionary<string, string>
                //    {
                //        { "e", "" },
                //        { "aaaa", "e" },    // a^4 = e
                //        { "bbbb", "e" },    // b^4 = e
                //        { "aa", "bb" },     // a^2 = b^2
                //        { "ba", "aaab" }    // ba = a^3 b
                //    };

                //    var G = new Group<string>
                //    {
                //        Identity = "e",
                //        Set = new MathSet<string>(new[] { "e", "a", "aa", "aaa", "b", "ab", "aab", "aaab" })
                //    };

                //    G.Op = (a, b) => Generate(eqs, a + b).First(elt => G.Set.Contains(elt));

                //    if (A.IsIsomorphic(G)) return "Q";
                //}

                return "Q";
            }

            throw new Exception();
        }
    }
}
