using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractAlgebraPowerSet
{
    public static class Extensions
    {
        // https://stackoverflow.com/a/19891897/268581

        //public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head)
        //{
        //    yield return head;
        //    foreach (var item in tail) yield return item;
        //}

        //public static IEnumerable<IEnumerable<T>> PowerSet<T>(this IEnumerable<T> items)
        //{
        //    if (items.Any() == false) yield return items;

        //    else
        //    {
        //        var head = items.First();
        //        var powerset = items.Skip(1).PowerSet().ToList();
        //        foreach (var set in powerset) yield return set.Prepend(head);
        //        foreach (var set in powerset) yield return set;
        //    }
        //}


        public static T[][] FastPowerSet<T>(T[] seq)
        {
            var powerSet = new T[1 << seq.Length][];
            powerSet[0] = new T[0]; // starting only with empty set
            for (int i = 0; i < seq.Length; i++)
            {
                var cur = seq[i];
                int count = 1 << i; // doubling list each time
                for (int j = 0; j < count; j++)
                {
                    var source = powerSet[j];
                    var destination = powerSet[count + j] = new T[source.Length + 1];
                    for (int q = 0; q < source.Length; q++)
                        destination[q] = source[q];
                    destination[source.Length] = cur;
                }
            }
            return powerSet;
        }

        public static IEnumerable<IEnumerable<T>> PowerSet<T>(this IEnumerable<T> items)
        {
            var result = FastPowerSet(items.ToArray());

            return result;
        }













    }
}
