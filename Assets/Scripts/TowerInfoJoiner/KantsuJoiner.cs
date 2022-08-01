using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class KantsuJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected)
        {
            var hais = selected.SelectMany(x => x.Hais).ToList();
            return new KantsuInfo(hais[0], hais[1], hais[2], hais[3]);
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var result = new List<JoinResult>();

            var toitsusBySpec = candidates
                .Where(x => x is ToitsuInfo)
                .GroupBy(x => x.Hais[0].Spec)
                .ToDictionary(x => x.Key, x => x.Cast<ToitsuInfo>().ToList());

            var koutsusBySpec = candidates
                .Where(x => x is KoutsuInfo)
                .GroupBy(x => x.Hais[0].Spec)
                .ToDictionary(x => x.Key, x => x.Cast<KoutsuInfo>().ToList());

            var singleHaisBySpec = candidates
                .Where(x => x is SingleHaiInfo)
                .GroupBy(x => x.Hais[0].Spec)
                .ToDictionary(x => x.Key, x => x.Cast<SingleHaiInfo>().ToList());

            foreach (var (spec, singleHais) in singleHaisBySpec)
            {
                // 하나짜리 패 4개로 만들 수 있는 조합
                if (singleHais.Count >= 4)
                {
                    var fourSubsets = MathHelper.SubSetsOf(singleHais, 4);
                    result.AddRange(fourSubsets.Select(x =>
                        new JoinResult(this, new HashSet<TowerInfo> { x[0], x[1], x[2], x[3] })));
                }

                // 머리 두개로 만들 수 있는 조합
                if (toitsusBySpec.TryGetValue(spec, out var sameSpecToitsus) && sameSpecToitsus.Count >= 2)
                {
                    var twoToitsuSubset = MathHelper.SubSetsOf(sameSpecToitsus, 2);
                    result.AddRange(twoToitsuSubset.Select(x =>
                        new JoinResult(this, new HashSet<TowerInfo> { x[0], x[1] })));
                }

                // 하나짜리 패 하나에 커쯔 하나를 붙여서 만들 수 있는 조합
                if (koutsusBySpec.TryGetValue(spec, out var sameSpecKoutsus))
                    result.AddRange(from t in sameSpecKoutsus
                        from s in singleHais
                        // 커쯔가 멘젠이면 상관없는데 비멘젠 커쯔에는 후로패를 못 붙인다.
                        where t.IsMenzen || !t.IsMenzen && !s.Hai.IsFuroHai
                        select new JoinResult(this, new HashSet<TowerInfo> { t, s }));
            }

            return result;
        }
    }
}
