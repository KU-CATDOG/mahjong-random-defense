using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class KoutsuJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected)
        {
            var hais = selected.SelectMany(x => x.Hais).ToList();
            return new KoutsuInfo(hais[0], hais[1], hais[2]);
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var result = new List<JoinResult>();

            var toitsusBySpec = candidates
                .Where(x => x is ToitsuInfo)
                .GroupBy(x => x.Hais[0].Spec)
                .ToDictionary(x => x.Key, x => x.ToList());

            var singleHaisBySpec = candidates
                .Where(x => x is SingleHaiInfo)
                .GroupBy(x => x.Hais[0].Spec)
                .ToDictionary(x => x.Key, x => x.ToList());

            foreach (var (spec, singleHais) in singleHaisBySpec)
            {
                // 하나짜리 패 3개로 만들 수 있는 조합
                if (singleHais.Count >= 3)
                {
                    var threeSubsets = MathHelper.SubSetsOf(singleHais, 3);
                    result.AddRange(threeSubsets.Select(subset => new JoinResult(this, new HashSet<TowerInfo> { subset[0], subset[1], subset[2] })));
                }

                // 하나짜리 패 하나에 머리 하나를 붙여서 만들 수 있는 조합
                if (toitsusBySpec.TryGetValue(spec, out var sameSpecToitsus))
                {
                    result.AddRange(from t in sameSpecToitsus
                        from s in singleHais
                        select new JoinResult(this, new HashSet<TowerInfo> { t, s }));
                }
            }

            return result;
        }
    }
}
