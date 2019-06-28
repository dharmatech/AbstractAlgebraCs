using System;
using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;

using AbstractAlgebraZipMany;

using static System.Console;

namespace pinter_5_F_1
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

    class Program
    {
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
                        
            foreach (var elt in results.Select(elt => generate(eqs, elt)).ZipMany(elts => elts).SelectMany(elts => elts))
                yield return elt;
        }

        static void table(List<string> G, Dictionary<string, string> eqs)
        {
            Console.Write("{0,-5}|", ""); foreach (var y in G) Console.Write($"{y,-5}|"); Console.WriteLine();

            Console.Write("-----|"); foreach (var y in G) Console.Write($"-----|"); Console.WriteLine();

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
            Dictionary<string, string> string_to_dictionary(string s) =>
                s
                    .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                    .Chunk(2)
                    .ToDictionary(eq => eq.ToList()[0], eq => eq.ToList()[1]);

            {
                WriteLine("Pinter 5.F.1");

                var G = new List<string>() { "e", "a", "b", "bb", "bbb", "ab", "abb", "abbb" };

                var eqs = new Dictionary<string, string>
                { { "e", "" }, { "aaaa", "e" }, { "aa", "bb" }, { "ba", "abbb" } };

                table(G, eqs); WriteLine();
            }

            {
                // Pinter 5.F.2

                WriteLine("Pinter 5.F.2");

                var G = new List<string>() { "e", "a", "b", "bb", "bbb", "ab", "abb", "abbb" };

                var eqs = new Dictionary<string, string>
                { { "e", "" }, { "aa", "e" }, { "bbbb", "e" }, { "ba", "abbb" } };

                table(G, eqs); WriteLine();
            }

            {
                // Pinter 5.F.3

                WriteLine("Pinter 5.F.3");

                var G = "e a b bb bbb ab abb abbb".Split(null).ToList();

                var eqs = new Dictionary<string, string>
                { { "e", "" }, { "aaaa", "e" }, { "aa", "bb" }, { "ba", "abbb" } };

                table(G, eqs); WriteLine();
            }

            //{
            //    // Takes a while to run

            //    WriteLine("Pinter 5.F.4");

            //    var G = "e a b c ab bc ac abc".Split(null).ToList();

            //    var eqs = new Dictionary<string, string>
            //    {
            //        { "e", "" }, { "aa", "bb" }, { "bb", "cc" }, { "cc", "e" },
            //        { "ab", "ba" }, { "ac", "ca" }, { "bc", "cb" }
            //    };

            //    table(G, eqs); WriteLine();
            //}

            {
                WriteLine("Pinter 5.G.4");

                var G = "e b bb bbb a ab abb abbb".Split(null).ToList();

                var eqs = string_to_dictionary("aa e   bbbb e   ba abbb"); eqs["e"] = "";

                table(G, eqs); WriteLine();
            }

            {
                WriteLine("Pinter 5.G.5");

                var G = "e b bb bbb a ab abb abbb".Split(null).ToList();

                var eqs = string_to_dictionary("bbbb e   ba ab   aa e"); eqs["e"] = "";

                table(G, eqs); WriteLine();
            }

            {
                WriteLine("Pinter 5.G.6");

                var G = "e b bb a ab abb ba bab babb bba bbab bbabb".Split(null).ToList();

                var eqs = string_to_dictionary("bbb e   aa e   aba bbabb"); eqs["e"] = "";

                table(G, eqs); WriteLine();
            }
        }
    }
}
