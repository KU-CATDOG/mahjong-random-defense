using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class MadiTowerStatOption : TowerStatOption
    {
        public override string Name => nameof(MadiTowerStatOption);
        public override float AdditionalCritChance => additionalCritChance;
        public override float AdditionalAttack => additionalAttack;
        public override float AdditionalAttackPercent => additionalAttackPercent;
        public override float AdditionalAttackSpeedMultiplier => additionalAttackSpeedMultiplier;

        private float additionalCritChance = 0f;
        private float additionalAttack = 0f;
        private float additionalAttackSpeedMultiplier = 1f;
        private float additionalAttackPercent = 0f;
        protected override void OnAttachOption()
        {
            if(HolderStat.TowerInfo is not MentsuInfo mentsuInfo) return;
            var hais = mentsuInfo.Hais;

            // 종류에 따른 스탯
            switch(hais[0].Spec.HaiType){
                case HaiType.Sou:
                    additionalAttackSpeedMultiplier *= 0.9f;
                    break;
                case HaiType.Wan:
                    additionalAttack -= 10f;
                    break;
                case HaiType.Kaze:
                    if(RoundManager.Inst.round.wind == hais[0].Spec.Number)
                        additionalAttackPercent += 0.5f;
                    break;
                default: // Pin, Sangen
                    break;
            }

            // 슌쯔, 커쯔, 깡쯔 스탯
            if(hais.Count == 3)
                if(hais[0] == hais[1]) // 커쯔
                    additionalAttackPercent += 0.1f;
                else // 슌쯔
                    additionalAttackSpeedMultiplier *= 1.1f;
            else if(hais.Count == 4)  // 깡쯔
                additionalAttackPercent += 0.25f;
            
            // 노두 스탯
            foreach(var hai in hais)
                if(hai.Spec.IsRoutou)
                    additionalCritChance += 0.1f;

        }
    }
    public class MadiTowerOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(MadiTowerOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if(HolderStat.TowerInfo is not MentsuInfo mentsuInfo) return;
            var hais = mentsuInfo.Hais;

            // 종류에 따른 스탯
            switch(hais[0].Spec.HaiType){
                case HaiType.Sou:
                case HaiType.Pin:
                case HaiType.Wan:
                    foreach(var info in infos){
                        info.UpdateShupaiLevel(hais[0].Spec.HaiType,1);
                    }
                    break;
                case HaiType.Sangen:
                    foreach(var info in infos)
                        info.AddOnHitOption(new ExplosiveOnHitOption(HolderStat, (float)(0.5f + HolderStat.TowerInfo.Hais.Count * 0.1f)));
                    break;
                case HaiType.Kaze:
                    if(RoundManager.Inst.round.wind == hais[0].Spec.Number)
                        foreach(var info in infos)
                            info.AddOnHitOption(new BladeOnHitOption(HolderStat));
                    break;
                default: 
                    break;
            }
        }
    }

}