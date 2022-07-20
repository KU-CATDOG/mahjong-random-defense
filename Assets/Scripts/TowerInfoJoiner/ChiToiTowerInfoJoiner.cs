using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class ChiToiTowerInfoJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected)
        {
            return new ChiToiTowerInfo((ToitsuInfo)selected[0], (ToitsuInfo)selected[1], (ToitsuInfo)selected[2],
                (ToitsuInfo)selected[3], (ToitsuInfo)selected[4], (ToitsuInfo)selected[5], (ToitsuInfo)selected[6]);
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var result = from chii in MathHelper.SubSetsOf(candidates.Where(x => x is ToitsuInfo).ToList(), 7)
                where !chii.SelectMany(x => x.Hais).GroupBy(x => x.Spec).Any(x => x.Count() > 2)
                select new JoinResult(this, new HashSet<TowerInfo>(chii));

            return result.ToList();
        }
    }
}
