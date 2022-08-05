using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public interface ITowerInfoJoiner
    {
        /// <summary>
        ///     주어진 재료를 가지고 결과물을 만들어 리턴함
        /// </summary>
        public TowerInfo Join(List<TowerInfo> selected);

        /// <summary>
        ///     리스트에 들어있는 타워들로 대상의 제작이 가능한 모든 조합을 리턴함.
        /// </summary>
        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates);
    }

    public class JoinResult
    {
        private readonly ITowerInfoJoiner joiner;

        public JoinResult(ITowerInfoJoiner j, IReadOnlyCollection<TowerInfo> candidates)
        {
            joiner = j;
            Candidates = candidates;
        }

        public IReadOnlyCollection<TowerInfo> Candidates { get; }

        public TowerInfo Generate() => joiner.Join(Candidates.ToList());
    }

    public class TowerInfoJoiner
    {
        private static TowerInfoJoiner instance;

        private readonly List<ITowerInfoJoiner> joiners = new();

        private TowerInfoJoiner()
        {
            joiners.Add(new ToitsuJoiner());
            joiners.Add(new ShuntsuJoiner());
            joiners.Add(new KoutsuJoiner());
            joiners.Add(new KantsuJoiner());

            joiners.Add(new TripleTowerInfoJoiner());
            joiners.Add(new CompleteTowerInfoJoiner());
            joiners.Add(new ChiToiTowerInfoJoiner());
            joiners.Add(new KokushiTowerInfoJoiner());
        }

        public static TowerInfoJoiner Instance => instance ??= new TowerInfoJoiner();

        /// <summary>
        ///     현재 선택된 애들을 포함해서 만들 수 있는 결과를 전부 리턴
        /// </summary>
        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> items, List<TowerInfo> selected)
        {
            return joiners.SelectMany(x => x.GetAllPossibleSets(items))
                .Where(x => selected.All(y => x.Candidates.Contains(y))).ToList();
        }

        public bool CheckTowerJoinable(List<TowerInfo> items, TowerInfo selected, ITowerInfoJoiner joiner)
        {
            return joiner.GetAllPossibleSets(items).Count(x => x.Candidates.Contains(selected)) > 0;
        }
        public bool CheckTowerJoinable(List<TowerInfo> items, TowerInfo selected, ITowerInfoJoiner[] joiners)
        {
            return joiners.Any(x => x.GetAllPossibleSets(items).Count(x => x.Candidates.Contains(selected)) > 0);
        }
        public bool CheckTowerJoinable(List<TowerInfo> items)
        {
            return joiners.Any(x => x.GetAllPossibleSets(items).Count > 0);
        }
    }
}
