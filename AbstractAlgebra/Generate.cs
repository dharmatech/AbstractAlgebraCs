using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using AbstractAlgebraZipMany;

namespace AbstractAlgebraGenerate
{
    public static class Utils
    {
        public static IEnumerable<string> Generate(Dictionary<string, string> eqs, string s)
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

            foreach (var elt in results.Select(elt => Generate(eqs, elt)).ZipMany(elts => elts).SelectMany(elts => elts))
                yield return elt;
        }
    }
}
