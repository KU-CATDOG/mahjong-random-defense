using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class TripleTowerInfoJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected) => new TripleTowerInfo((MentsuInfo)selected[0],
            (MentsuInfo)selected[1], (MentsuInfo)selected[2]);

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var mentsus = candidates.Where(x => x is MentsuInfo).ToList();

            var notToitsus = mentsus.Where(x => x is not ToitsuInfo).ToList();

            var result = (from subset in MathHelper.SubSetsOf(notToitsus, 3)
                where !subset.SelectMany(x => x.Hais).GroupBy(x => x.Spec).Any(x => x.ToList().Count > 4)
                select new JoinResult(this, subset)).ToList();

            var toitsus = mentsus.Where(x => x is ToitsuInfo);
            var notToitsusTwoSubsets = MathHelper.SubSetsOf(notToitsus, 2).ToList();

            result.AddRange(
                from toitsu in toitsus
                from subset in notToitsusTwoSubsets
                select new List<TowerInfo> { toitsu, subset[0], subset[1] }
                into list
                where !list.SelectMany(x => x.Hais).GroupBy(x => x.Spec).Any(x => x.ToList().Count > 4)
                select new JoinResult(this, list));

            return result;
        }
    }
}
