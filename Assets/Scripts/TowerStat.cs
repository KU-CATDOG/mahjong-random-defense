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
        private int hasCheongIlSaek = -1;
        public int HasCheongIlSaek { 
            get {
                return hasCheongIlSaek;
            }
            set {
                var roundManager = RoundManager.Inst;
                if(hasCheongIlSaek != -1) {
                    roundManager.CheongIlSaekCount[hasCheongIlSaek]--;
                    if (roundManager.CheongIlSaekCount[hasCheongIlSaek] < 0){
                        UnityEngine.Debug.Log("Warning: CheongIlSaekCount[" + hasCheongIlSaek + "] is not counted properly");
                        roundManager.CheongIlSaekCount[hasCheongIlSaek] = 0;
                    }
                }
                if(value != -1){
                    roundManager.CheongIlSaekCount[value]++;
                    hasCheongIlSaek = value;
                }
            }
        }
        public TowerStat(Tower tower, TowerInfo t) 
        {
            AttachedTower = tower;
            TowerInfo = t;
        }
        public TowerInfo TowerInfo { get; }

        public IReadOnlyDictionary<string, TowerOption> Options => options;

        public TargetTo TargetTo { get; set; } = TargetTo.Proximity;

        public int BaseAttack => TowerInfo.Hais.Count * 10;

        public float BaseAttackSpeed => .5f;

        public float AdditionalAttackMultiplier { get; private set; } = 1;

        public float BaseCritChance => 0;

        public float AdditionalAttack { get; private set; }

        public float AdditionalAttackPercent { get; private set; }

        public float AdditionalAttackSpeedMultiplier { get; private set; } = 1;

        public float AdditionalCritChance { get; private set; }


        public float AdditionalCritMultiplier { get; private set; }

        public int MaxRagePoint { get; private set; }

        public float RageAttackPercent { get; private set; }

        public float RageAttackSpeedMultiplier { get; private set; }

        public float RageCritChance { get; private set; }

        public float RageCritMultiplier { get; private set; }

        public float FinalAttack => (BaseAttack + AdditionalAttack) * (1 + (AdditionalAttackPercent + RageAttackPercent * MathF.Min(RoundManager.Inst.RagePoint, MaxRagePoint)) / 100f) *
                                    AdditionalAttackMultiplier;

        public float FinalAttackSpeed => (BaseAttackSpeed + RageAttackSpeedMultiplier * MathF.Min(RoundManager.Inst.RagePoint, MaxRagePoint)) * AdditionalAttackSpeedMultiplier;

        public float FinalCritChance => (BaseCritChance + AdditionalCritChance) * (1+(RageCritChance*MathF.Min(RoundManager.Inst.RagePoint, MaxRagePoint))/100f);

        public float FinalCritMultiplier => (BaseCritMultiplier + AdditionalCritMultiplier) * (1+(RageCritMultiplier*MathF.Min(RoundManager.Inst.RagePoint, MaxRagePoint))/100f);

        public void UpdateOptions()
        {
            if (TowerInfo == null) return;
            HasCheongIlSaek = -1;
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
            AdditionalAttack = 0;
            AdditionalAttackPercent = 0;
            AdditionalCritChance = 0;
            AdditionalCritMultiplier = 0;
            AdditionalAttackSpeedMultiplier = 1;
            AdditionalAttackMultiplier = 1;

            foreach (var o in options.Values)
            {
                switch (o)
                {
                    case TowerStatOption so:
                        AdditionalAttack += so.AdditionalAttack;
                        AdditionalAttackPercent += so.AdditionalAttackPercent;
                        AdditionalCritChance += so.AdditionalCritChance;
                        AdditionalCritMultiplier += so.AdditionalCritMultiplier;
                        AdditionalAttackSpeedMultiplier *= so.AdditionalAttackSpeedMultiplier;
                        AdditionalAttackMultiplier *= so.AdditionalAttackMultiplier;
                        TargetTo = so.TargetTo;
                        if (so.AttackBehaviour != null) AttackBehaviour = so.AttackBehaviour;
                        MaxRagePoint = so.MaxRagePoint;
                        RageAttackPercent = so.RageAttackPercent;
                        RageAttackSpeedMultiplier = so.RageAttackSpeedMultiplier;
                        RageCritChance = so.RageCritChance;
                        RageCritMultiplier = so.RageCritMultiplier;
                        if (so.Name == nameof(CheongIlSaekStatOption))
                            HasCheongIlSaek = ((int)(so.HolderStat.TowerInfo.Hais[0].Spec.HaiType) / 10) - 1;
                        break;
                    case TowerProcessAttackInfoOption oao:
                        onAttackOptions.Add(oao);
                        break;
                }
            }

            onAttackOptions.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        }

        public List<AttackInfo> ProcessAttackInfo(AttackInfo info)
        {
            var result = new List<AttackInfo> { info };

            foreach (var o in onAttackOptions) o.ProcessAttackInfo(result);
            foreach (var c in RoundManager.Inst.CheongIlSaekCount)
            {
                if(c <= 0) continue;
                foreach (var it in result)
                {
                    it.UpgradeShupaiLevel((HaiType)((c+1)*10));
                }
            }

            return result;
        }
        
    }
}
