using System;
using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class ShuntsuJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected) =>
            new ShuntsuInfo(selected[0].Hais[0], selected[1].Hais[0], selected[2].Hais[0]);

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var result = new List<JoinResult>();

            var lists = candidates.Where(x =>
            {
                if (x is not SingleHaiInfo hai) return false;

                var h = hai.Hai.Spec;

                return !h.IsJi;
            }).GroupBy(x => x.Hais[0].Spec.HaiType).ToDictionary(x => x.Key, x => x.ToList());

            foreach (var infosByType in lists.Values)
            {
                var infosBySpec = infosByType.GroupBy(x => x.Hais[0].Spec).Select(x => x.ToList()).ToList();

                if (infosBySpec.Count < 3) continue;

                var threeSubsets = MathHelper.SubSetsOf(infosBySpec, 3);

                foreach (var subset in threeSubsets)
                {
                    var firsts = subset[0];
                    var seconds = subset[1];
                    var thirds = subset[2];

                    if (!IsConsecutive(firsts[0].Hais[0].Spec.Number, seconds[0].Hais[0].Spec.Number, thirds[0].Hais[0].Spec.Number)) continue;

                    result.AddRange(from f in firsts
                        from s in seconds
                        from t in thirds
                        select new JoinResult(this, new HashSet<TowerInfo> { f, s, t }));
                }
            }

            return result;
        }

        private static bool IsConsecutive(int a, int b, int c)
        {
            int min = Math.Min(a, Math.Min(b, c));
            int max = Math.Max(a, Math.Max(b, c));
            return max - min == 2 && a != b && a != c && b != c;
        }
    }
}
