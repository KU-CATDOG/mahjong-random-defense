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

            var koutsusBtSpec = candidates
                .Where(x => x is KoutsuInfo)
                .GroupBy(x => x.Hais[0].Spec)
                .ToDictionary(x => x.Key, x => x.ToList());

            var singleHaisBySpec = candidates
                .Where(x => x is SingleHaiInfo)
                .GroupBy(x => x.Hais[0].Spec)
                .ToDictionary(x => x.Key, x => x.ToList());

            foreach (var (spec, singleHais) in singleHaisBySpec)
            {
                // 하나짜리 패 하나에 커쯔 하나를 붙여서 만들 수 있는 조합
                if (koutsusBtSpec.TryGetValue(spec, out var sameSpecKoutsus))
                {
                    result.AddRange(from t in sameSpecKoutsus
                        from s in singleHais
                        select new JoinResult(this, new HashSet<TowerInfo> { t, s }));
                }
            }

            return result;
        }
    }
}
