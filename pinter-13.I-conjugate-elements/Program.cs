using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

using AbstractAlgebraMathSet;

using static System.Console;

namespace pinter_13.I_conjugate_elements
{
    public static class Extensions
    {
        // https://stackoverflow.com/a/6362642/268581

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }
    }



    //class Group<T>
    //{
    //    public MathSet<T> G;
                
    //    public Func<T, T, T> Op;
    //}
    

    class Program
    {
        static Dictionary<string, string> string_to_dictionary(string s) =>
            s
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                .Chunk(2)
                .ToDictionary(eq => eq.ToList()[0], eq => eq.ToList()[1]);


        public static IEnumerable<TResult> ZipMany<TSource, TResult>(
            IEnumerable<IEnumerable<TSource>> source,
            Func<IEnumerable<TSource>, TResult> selector)
        {
            // ToList is necessary to avoid deferred execution
            var enumerators = source.Select(seq => seq.GetEnumerator()).ToList();
            try
            {
                while (true)
                {
                    foreach (var e in enumerators)
                    {
                        bool b = e.MoveNext();
                        if (!b) yield break;
                    }
                    // Again, ToList (or ToArray) is necessary to avoid deferred execution
                    yield return selector(enumerators.Select(e => e.Current).ToList());
                }
            }
            finally
            {
                foreach (var e in enumerators)
                    e.Dispose();
            }
        }

        static IEnumerable<string> generate(Dictionary<string, string> eqs, string s)
        {
            var results = new List<string>();

            foreach (var elt in eqs)
            {
                if (new Regex(elt.Key).IsMatch(s))
                    results.Add(new Regex(elt.Key).Replace(s, elt.Value, 1));

                if (new Regex(elt.Value).IsMatch(s))
                    results.Add(new Regex(elt.Value).Replace(s, elt.Key, 1));
            }

            foreach (var result in results) yield return result;

            foreach (var elt in ZipMany(results.Select(elt => generate(eqs, elt)), elts => elts).SelectMany(elts => elts))
                yield return elt;
        }

        static void table(List<string> G, Dictionary<string, string> eqs)
        {
            Console.Write("     |"); foreach (var y in G) Console.Write($"{y,-5}|"); Console.WriteLine();

            Console.Write("-----|"); foreach (var y in G) Console.Write("-----|"); Console.WriteLine();

            foreach (var x in G)
            {
                Console.Write($"{x,5}|");

                foreach (var y in G)
                {
                    var result = generate(eqs, x + y).First(elt => G.Contains(elt));

                    Console.Write($"{result,-5}|");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            string simplify(List<string> G, Dictionary<string, string> eqs, string a) => 
                generate(eqs, a).First(elt => G.Contains(elt));
            
            string op(List<string> G, Dictionary<string, string> eqs, string a, string b) =>
                generate(eqs, a + b).First(elt => G.Contains(elt));
            
            string inverse(List<string> G, Dictionary<string, string> eqs, string a) =>
                G.First(elt => op(G, eqs, a, elt) == "e");
            
            string conjugate(List<string> G, Dictionary<string, string> eqs, string a, string x) =>
                simplify(G, eqs, x + a + inverse(G, eqs, x));

            // op(op(x, a), inverse(x))
            //
            // op_multi(x, a, inverse(x))
            
            MathSet<string> conjugacy_class(List<string> G, Dictionary<string, string> eqs, string a) =>
                G.Select(x => conjugate(G, eqs, a, x)).ToMathSet();

            MathSet<string> centralizer(List<string> G, Dictionary<string, string> eqs, string a)
            {
                return G.Where(x => simplify(G, eqs, a + x) == simplify(G, eqs, x + a)).ToMathSet();
            }


            // new Group(set, op)
            // new Group(set, eqs)   set with defining equations

            // Pinter 5.F.1

            // 13.I - Conjugate Elements 

            {
                var G = new List<string>() { "e", "a", "b", "bb", "ab", "abb" };

                var eqs = new Dictionary<string, string>
                { { "e", "" }, { "aa", "e" }, { "bbb", "e" }, { "ba", "abb" } };

                table(G, eqs); WriteLine();

                foreach (var elt in G) WriteLine("{0,4} inverse: {1}", elt, inverse(G, eqs, elt)); WriteLine();
                
                WriteLine("conjugacy classes (partition of group):");

                WriteLine(G.Select(a => conjugacy_class(G, eqs, a)).ToMathSet()); WriteLine();
                
                foreach (var a in G) WriteLine("conjugacy class of {0,4}: {1}", a, conjugacy_class(G, eqs, a)); WriteLine();

                foreach (var a in G) WriteLine("centralizer of {0,4}: {1}", a, centralizer(G, eqs, a)); WriteLine();
                
                foreach (var a in G)
                {
                    foreach (var x in G)
                        WriteLine($"{a,4} conjugate with {x,4} : {x + a + inverse(G, eqs, x),9} : {conjugate(G, eqs, a, x)}");

                    WriteLine();
                }
                
                foreach (var a in G)
                {
                    WriteLine("centralizer of {0}: {1}\n", a, centralizer(G, eqs, a));

                    WriteLine("cosets:");

                    foreach (var x in G)
                        WriteLine("{0} {1,4} -> {2}", centralizer(G, eqs, a), x, centralizer(G, eqs, a).ConvertAll(elt => simplify(G, eqs, elt + x)));

                    WriteLine();

                    WriteLine("cosets: {0}\n", G.ToMathSet().ConvertAll(elt => centralizer(G, eqs, a).ConvertAll(item => simplify(G, eqs, item + elt))));
                }
            }
            
            // Pinter 5.F.2

            //var G = new List<string>() { "e", "a", "b", "bb", "bbb", "ab", "abb", "abbb" };

            //var eqs = new Dictionary<string, string>
            //{ { "e", "" }, { "aa", "e" }, { "bbbb", "e" }, { "ba", "abbb" } };

            //table(G, eqs);


            // var G = new List<string>() { "e", "a", "b", "bb", "bbb", "ab", "abb", "abbb" };


            //var G = "e a b bb bbb ab abb abbb".Split(null).ToList();

            //var eqs = string_to_dictionary("aaaa e   aa bb   ba abbb");   eqs["e"] = "";

            //table(G, eqs);



            // pinter 5.G.4

            //var G = "e b bb bbb a ab abb abbb".Split(null).ToList();

            //var eqs = string_to_dictionary("aa e   bbbb e   ba abbb"); eqs["e"] = "";

            //table(G, eqs);



            // pinter 5.G.5

            //var G = "e b bb bbb a ab abb abbb".Split(null).ToList();

            //var eqs = string_to_dictionary("bbbb e   ba ab   aa e"); eqs["e"] = "";

            //table(G, eqs);



            // pinter 5.G.6

            //var G = "e b bb a ab abb ba bab babb bba bbab bbabb".Split(null).ToList();

            //var eqs = string_to_dictionary("bbb e   aa e   aba bbabb"); eqs["e"] = "";

            //table(G, eqs);


            //var G = "e b bb a ab abb ba bab babb aba abab ababb".Split(null).ToList();

            //var eqs = string_to_dictionary("bbb e   bba abab   baba abb   aa e"); eqs["e"] = "";

            //table(G, eqs);





        }
    }
}
