
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCosetGrouping;

using static System.Console;

namespace AbstractAlgebraShowCosets
{
    public static class Extensions
    {
        public static void ShowCoset<T>(this Group<T> G, Group<T> H_)
        {
            var H = new Group<T>
            {
                Set = H_.Set,
                Op = H_.Op,
                Identity = H_.Identity,
                Lookup = G.Lookup,
                OpString = H_.OpString
            };


            WriteLine("H = {0}   order: {1}   index: {2}", H, H.Set.Count, G.Set.Count / H.Set.Count);

            WriteLine("    cosets:");

            foreach (var elt in G.CosetGrouping(H, "H"))
                WriteLine("    each of these: {0}   are equal to:   {1}",
                    elt.ToMathSet(),
                    elt.Key.ConvertAll(G.Lookup));

            WriteLine();
        }

        public static void ShowCosets<T>(this Group<T> G)
        {
            WriteLine("subgroups:");

            foreach (var elt in G.Subgroups()) WriteLine("    {0}", elt);

            WriteLine();

            WriteLine("proper subgroups:");

            foreach (var elt in G.ProperSubgroups()) WriteLine("    {0}", elt);

            WriteLine();

            foreach (var H in G.ProperSubgroups())
            {
                WriteLine("H = {0}   order: {1}   index: {2}", H, H.Set.Count, G.Set.Count / H.Set.Count);

                WriteLine("    cosets:");

                foreach (var elt in G.CosetGrouping(H, "H"))
                    WriteLine("    each of these: {0}   are equal to:   {1}",
                        elt.ToMathSet(),
                        elt.Key.ConvertAll(G.Lookup));

                WriteLine();
            }
        }
    }
}
