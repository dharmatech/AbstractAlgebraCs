using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using AbstractAlgebraZipMany;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

using static System.Console;

namespace pinter_13_I_conjugate_elements_group_class
{
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

        static void Main(string[] args)
        {
            {
                var eqs = new Dictionary<string, string>
                { { "e", "" }, { "aa", "e" }, { "bbb", "e" }, { "ba", "abb" } };
                
                var G = new Group<string>
                {
                    Identity = "e",
                    Set = new MathSet<string>(new[] { "e", "a", "b", "bb", "ab", "abb" })
                };

                G.Op = (a, b) => generate(eqs, a + b).First(elt => G.Set.Contains(elt));

                G.ShowOperationTable(); WriteLine();

                foreach (var item in G.Set) WriteLine("({0})^-1 -> {1}", item, G.Inverse(item)); WriteLine();

                WriteLine("conjugacy classes (partition of group):");

                WriteLine(G.Set.Select(a => G.ConjugacyClass(a)).ToMathSet()); WriteLine();

                foreach (var a in G.Set) WriteLine("conjugacy class of {0,4}: {1}", a, G.ConjugacyClass(a)); WriteLine();

                foreach (var a in G.Set) WriteLine("centralizer of {0,4}: {1}", a, G.Centralizer(a)); WriteLine();

                G.ShowConjugates();

                G.ShowCentralizers();
            }
        }
    }
}
