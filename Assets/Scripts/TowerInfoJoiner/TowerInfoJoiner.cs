using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public interface ITowerInfoJoiner
    {
        /// <summary>
        /// 주어진 재료를 가지고 결과물을 만들어 리턴함
        /// </summary>
        public TowerInfo Join(List<TowerInfo> selected);

        /// <summary>
        /// 리스트에 들어있는 타워들로 대상의 제작이 가능한 모든 조합을 리턴함.
        /// </summary>
        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> candidates);
    }

    public class JoinResult
    {
        private readonly ITowerInfoJoiner joiner;

        public IReadOnlyCollection<TowerInfo> Candidates { get; }

        public JoinResult(ITowerInfoJoiner j, IReadOnlyCollection<TowerInfo> candidates)
        {
            joiner = j;
            Candidates = candidates;
        }

        public TowerInfo Generate()
        {
            return joiner.Join(Candidates.ToList());
        }
    }

    public class TowerInfoJoiner
    {
        private static TowerInfoJoiner instance;

        public static TowerInfoJoiner Instance => instance ??= new TowerInfoJoiner();

        private readonly List<ITowerInfoJoiner> joiners = new();

        private TowerInfoJoiner()
        {
            joiners.Add(new ToitsuJoiner());
            joiners.Add(new ShuntsuJoiner());
            joiners.Add(new KoutsuJoiner());
            joiners.Add(new KantsuJoiner());

            joiners.Add(new TripleTowerInfoJoiner());
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> items)
        {
            return joiners.SelectMany(x => x.GetAllPossibleSets(items)).ToList();
        }
    }
}
