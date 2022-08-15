using System.Collections.Generic;
using System.Linq;
using System;

namespace MRD
{
    public class TowerStat
    {
        // 아무것도 안해도 모든 TowerStat이 기본적으로 가지는 옵션들
        private static readonly IReadOnlyList<string> defaultOptionNames = new[]
        {
            nameof(DoraStatOption),
        };

        private readonly List<TowerProcessAttackInfoOption> onAttackOptions = new();

        private readonly Dictionary<string, TowerOption> options = new();
        public AttackBehaviour AttackBehaviour = new BulletAttackBehaviour();

        public float BaseCritMultiplier = 2;

        public (string imageName, int priority) projectileImage = ("normal", 0);
        public Tower AttachedTower { get; }
        public TowerStat(Tower tower, TowerInfo t) 
        {
            AttachedTower = tower;
            TowerInfo = t;
            if(TowerInfo is not null) 
                TowerInfo.Tower = tower;
        }
        public TowerInfo TowerInfo { get; }

        public IReadOnlyDictionary<string, TowerOption> Options => options;

        public TargetTo TargetTo { get; set; } = TargetTo.Proximity;

        public bool InstantDeath { get; private set; } = false;

        public Stat BaseStat => new Stat(damageConstant: TowerInfo.Hais.Count * 10, attackSpeed: .5f, critDamage: 2f);

        public Stat AdditionalStat { get; private set; }

        public Stat RelicStat { get; private set; }

        public int MaxRagePoint { get; private set; }

        public Stat RageStat { get; private set;  }

        public Stat FinalStat => BaseStat + AdditionalStat + RelicStat + RageStat * MathF.Min(RoundManager.Inst.RagePoint, MaxRagePoint);

        public void UpdateOptions()
        {
            if (TowerInfo == null) return;
            var newOptions = new HashSet<string>();

            foreach (string i in defaultOptionNames) newOptions.Add(i);

            foreach (string i in TowerInfo.DefaultOptions) newOptions.Add(i);

            if (TowerInfo is YakuHolderInfo h)
            {
                h.UpdateYaku();

                foreach (string i in h.YakuList.SelectMany(x => x.OptionNames)) newOptions.Add(i);
            }

            var toRemove = new List<string>();

            // 원래 있던 옵션이 없어졌으면 Dispose 하고 제거함
            foreach (var oldOption in options.Values)
            {
                if (newOptions.Contains(oldOption.Name)) continue;

                oldOption.Dispose();
                toRemove.Add(oldOption.Name);
            }

            foreach (string r in toRemove) options.Remove(r);

            // 원래 없었는데 새로 생긴 옵션이 있으면 Attach 해줌
            foreach (var newOption in from i in newOptions
                where options.Values.All(x => x.Name != i)
                select OptionData.GetOption(i))
            {
                if (newOption == null) continue;
                if (options.ContainsKey(newOption.Name)) continue;
                newOption.AttachOption(this);

                options[newOption.Name] = newOption;
            }

            UpdateStat();
        }

        public void UpdateStat()
        { 
            if (TowerInfo == null) return;
            AdditionalStat = new();
            RageStat = new();
            RelicStat = new();
            onAttackOptions.Clear();

            foreach (var o in options.Values)
            {
                switch (o)
                {
                    case TowerStatOption so:
                        AdditionalStat += so.AdditionalStat;
                        TargetTo = so.TargetTo;
                        if (so.AttackBehaviour != null) AttackBehaviour = so.AttackBehaviour;
                        MaxRagePoint = Math.Max(MaxRagePoint, so.MaxRagePoint);
                        RageStat += so.RageStat;
                        break;
                    case TowerProcessAttackInfoOption oao:
                        onAttackOptions.Add(oao);
                        break;
                }
            }
            foreach (var relic in RoundManager.Inst.Relics) RelicStat += relic.AdditionalStat(this);

            onAttackOptions.Sort((x, y) => x.Priority.CompareTo(y.Priority));

            InstantDeath = false;
            XY[] cndLoc = new XY[]{(0,0),(-1,0),(1,0)};
            foreach(var loc in cndLoc)
            {
                var targetPos = loc + AttachedTower.Coordinate;
                if(!targetPos.isSane) continue;

                if(RoundManager.Inst.Grid.GetCell(targetPos).TowerStat.options.ContainsKey(nameof(CheongNoDuStatOption)))
                {
                    InstantDeath = true;
                    break;
                }
            }
        }

        public List<AttackInfo> ProcessAttackInfo(AttackInfo info)
        {
            var result = new List<AttackInfo> { info };

            foreach (var o in onAttackOptions) o.ProcessAttackInfo(result);

            return result;
        }
        
    }
}
