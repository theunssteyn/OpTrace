using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Csp
{
    public static class Sets
    {
        /* (A-B) U (B-A) */
        public static Dictionary<String, List<String>> GetDifferences(Dictionary<String, List<String>> A, Dictionary<String, List<String>> B)
        {
            var Left = A.Where(p => B.ContainsKey(p.Key));
            var Right = B.Where(p => A.ContainsKey(p.Key));

            return Left.Union(Right, new KeyEqualityComparer<string, List<String>>()).ToDictionary(a => a.Key, b => b.Value);
        }

        public static Dictionary<String, List<String>> RemoveSet(this Dictionary<String, List<String>> A, Dictionary<String, List<String>> B)
        {           
            return A.Where(p => !B.ContainsKey(p.Key)).ToDictionary(a => a.Key, b => b.Value);
        }

        public static bool TraceEquivalent(this Dictionary<String, List<String>> A, Dictionary<String, List<String>> B)
        {
            KeyEqualityComparer<string, List<String>> comparer = new KeyEqualityComparer<string, List<string>>();
            return A.Intersect(B, comparer).Count() == A.Union(B, comparer).Count();
        }
        //if (optInter.Count == genInter.Count && !optInter.Except(genInter).Any())
        public static bool TraceEquivalent2(this Dictionary<String, List<String>> A, Dictionary<String, List<String>> B)
        {
            KeyEqualityComparer<string, List<String>> comparer = new KeyEqualityComparer<string, List<string>>();
            return (A.Count == B.Count && !A.Except(B, comparer).Any());
        }

        private class KeyEqualityComparer<T, U> : IEqualityComparer<KeyValuePair<T, U>>
        {
            public bool Equals(KeyValuePair<T, U> x, KeyValuePair<T, U> y)
            {
                return x.Key.Equals(y.Key);
            }

            public int GetHashCode(KeyValuePair<T, U> obj)
            {
                return obj.Key.GetHashCode();
            }
        }

    }
}
