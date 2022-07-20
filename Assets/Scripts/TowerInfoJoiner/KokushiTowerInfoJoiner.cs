using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class KokushiTowerInfoJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected)
        {
            return new KokushiTowerInfo(selected.Cast<SingleHaiInfo>().ToList());
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var yaochu14s =
                // 요구패 14개를 모았는데
                MathHelper.SubSetsOf(candidates.Where(x => x is SingleHaiInfo s && s.Hai.Spec.IsYaochu).ToList(), 14)
                    // 그 종류가 13개여야 함
                    .Where(x => x.GroupBy(y => y.Hais[0].Spec).Count() == 13);

            var result = yaochu14s.Select(yaochu14 => new JoinResult(this, new HashSet<TowerInfo>(yaochu14))).ToList();

            return result.ToList();
        }
    }
}
