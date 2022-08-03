using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class CompleteTowerInfoJoiner : ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected) => new CompleteTowerInfo((TripleTowerInfo)selected[0],
            (MentsuInfo)selected[1], (MentsuInfo)selected[2]);

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates)
        {
            var notToitsus = new List<TowerInfo>();
            var triplesWithToitsu = new List<TowerInfo>();

            var toitsus = new List<TowerInfo>();
            var triplesWithoutToitsu = new List<TowerInfo>();

            foreach (var i in candidates)
            {
                switch (i)
                {
                    case MentsuInfo:
                        (i is ToitsuInfo ? toitsus : notToitsus).Add(i);
                        break;
                    case TripleTowerInfo t:
                        (t.MentsuInfos.Any(x => x is ToitsuInfo) ? triplesWithToitsu : triplesWithoutToitsu).Add(i);
                        break;
                }
            }

            var result =
                (from toitsu in triplesWithToitsu
                    from subset in MathHelper.SubSetsOf(notToitsus, 2)
                    select new List<TowerInfo> { toitsu, subset[0], subset[1] }
                    into list
                    where !list.SelectMany(x => x.Hais).GroupBy(x => x.Spec).Any(x => x.ToList().Count > 4)
                    select new JoinResult(this, list)).ToList();

            result.AddRange(
                from toitsu in toitsus
                from noToitsuTriple in triplesWithoutToitsu
                from noToitsu in notToitsus
                select new List<TowerInfo> { noToitsuTriple, toitsu, noToitsu }
                into list
                where !list.SelectMany(x => x.Hais).GroupBy(x => x.Spec).Any(x => x.ToList().Count > 4)
                select new JoinResult(this, list));

            return result;
        }
    }
}
