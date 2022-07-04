using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public interface ITowerInfoJoiner
    {
        public TowerInfo Join(List<TowerInfo> selected);

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
        }

        public List<JoinResult> GetAllPossibleSets(List<TowerInfo> items)
        {
            return joiners.SelectMany(x => x.GetAllPossibleSets(items)).ToList();
        }
    }
}
