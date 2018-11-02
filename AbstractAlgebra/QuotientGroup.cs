using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCoset;

namespace AbstractAlgebraQuotientGroup
{
    public static class Extensions
    {
        //public static Group<Coset<T>> QuotientGroup<T>(this Group<T> G, Group<T> H)
        //{
        //    var set = G.Set
        //        .Select(elt => new Coset<T> { Group = H, Element = elt, Name = "H" })
        //        .GroupBy(elt => elt.ToRightCoset())
        //        .Select(elt => elt.OrderBy(coset => G.Lookup(coset.Element)).First())
        //        .ToMathSet();

        //    return new Group<Coset<T>>
        //    {
        //        Identity = new Coset<T> { Group = H, Element = G.Identity, Name = "H" },
        //        Set = set,
        //        Op = (a, b) => set.First(elt => elt.ToRightCoset() == a.Combine(b).ToRightCoset())
        //    };
        //}

        //public static Group<Coset<T>> QuotientGroupOrderBy<T>(this Group<T> G, Group<T> H, Func<Coset<T>, string> Selector)
        //{
        //    // Func<Coset<T>, string> selector = coset => G.Lookup(coset.Element);

        //    var set = G.Set
        //        .Select(elt => new Coset<T> { Group = H, Element = elt, Name = "H" })
        //        .GroupBy(elt => elt.ToRightCoset())
        //        .Select(elt => elt.OrderBy(Selector).First())
        //        .ToMathSet();

        //    return new Group<Coset<T>>
        //    {
        //        Identity = new Coset<T> { Group = H, Element = G.Identity, Name = "H" },
        //        Set = set,
        //        Op = (a, b) => set.First(elt => elt.ToRightCoset() == a.Combine(b).ToRightCoset())
        //    };
        //}

        public static Group<Coset<T>> QuotientGroup<T>(this Group<T> G, Group<T> H, Func<Coset<T>, string> Selector, string Name)
        {
            var set = G.Set
                .Select(elt => new Coset<T> { Group = H, Element = elt, Name = Name })
                .GroupBy(elt => elt.ToRightCoset())
                .Select(elt => elt.OrderBy(Selector).First())
                .ToMathSet();

            // var set_ = G.CosetGrouping(H, "H").Select(elt => elt.OrderBy(Selector).First()).ToMathSet()

            return new Group<Coset<T>>
            {
                Identity = new Coset<T> { Group = H, Element = G.Identity, Name = Name },
                Set = set,
                Op = (a, b) => set.First(elt => elt.ToRightCoset() == a.Combine(b).ToRightCoset())
            };
        }

        public static Group<Coset<T>> QuotientGroup<T>(this Group<T> G, Group<T> H, string Name) =>
            G.QuotientGroup(H, coset => G.Lookup(coset.Element), Name);




        public static Group<Coset<T>> QuotientGroup<T>(this Group<T> G, Group<T> H, Func<Coset<T>, string> Selector)
        {
            var set = G.Set
                .Select(elt => new Coset<T> { Group = H, Element = elt, Name = "H" })
                .GroupBy(elt => elt.ToRightCoset())
                .Select(elt => elt.OrderBy(Selector).First())
                .ToMathSet();

            // var set_ = G.CosetGrouping(H, "H").Select(elt => elt.OrderBy(Selector).First()).ToMathSet()

            return new Group<Coset<T>>
            {
                Identity = new Coset<T> { Group = H, Element = G.Identity, Name = "H" },
                Set = set,
                Op = (a, b) => set.First(elt => elt.ToRightCoset() == a.Combine(b).ToRightCoset())
            };
        }

        public static Group<Coset<T>> QuotientGroup<T>(this Group<T> G, Group<T> H) =>
            G.QuotientGroup(H, coset => G.Lookup(coset.Element));
    }
}
