using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class ToitsuJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected)
        {
            return new ToitsuInfo(selected[0].Hais[0], selected[1].Hais[0]);
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            return candidates
                .Where(x => x is SingleHaiInfo s && !s.Hai.IsFuroHai)
                .GroupBy(x => x.Hais[0].Spec)
                .Select(x => x.ToList())
                .Where(x => x.Count >= 2)
                .SelectMany(l => SubsetGenerator.SubSetsOf(l, 2),
                    (_, subset) => new JoinResult(this, new HashSet<TowerInfo> { subset[0], subset[1] })).ToList();
        }
    }
}
