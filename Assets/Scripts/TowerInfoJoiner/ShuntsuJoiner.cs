using System;
using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class ShuntsuJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected)
        {
            return new ShuntsuInfo(selected[0].Hais[0], selected[1].Hais[0], selected[2].Hais[0]);
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var result = new List<JoinResult>();

            var lists = candidates.Where(x =>
            {
                if (x is not SingleHaiInfo hai) return false;

                var h = hai.Hai.Spec;

                return h.HaiType is not (HaiType.Kaze or HaiType.Sangen);
            }).GroupBy(x => x.Hais[0].Spec.HaiType).ToDictionary(x => x.Key, x => x.ToList());

            foreach (var infosByType in lists.Values)
            {
                if (infosByType.Count < 3) continue;

                var threeSubsets = SubsetGenerator.SubSetsOf(infosByType, 3);

                foreach (var subset in threeSubsets)
                {
                    var f = subset[0];
                    var s = subset[1];
                    var t = subset[2];

                    if (!IsConsecutive(f.Hais[0].Spec.Number, s.Hais[0].Spec.Number, t.Hais[0].Spec.Number)) continue;

                    result.Add(new JoinResult(this, new HashSet<TowerInfo> { f, s, t }));
                }
            }

            return result;
        }

        private static bool IsConsecutive(int a, int b, int c) {
            int min = Math.Min(a, Math.Min(b, c));
            int max = Math.Max(a, Math.Max(b, c));
            return max - min == 2 && a != b && a != c && b != c;
        }
    }
}
