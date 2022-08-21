using System.Collections.Generic;

namespace MRD
{
    public class GuRyeonBoDeungStatOption : TowerStatOption
    {
        public override string Name => nameof(GuRyeonBoDeungStatOption);

        public override Stat AdditionalStat => new
    (
            damageMultiplier: 3f,
            attackSpeed: .2f,
            critChance: .4f,
            critDamage: .5f
    );
        public override AttackBehaviour AttackBehaviour => new MinitowerAttackBehaviour();
    }

    public class GuRyeonBoDeungOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(GuRyeonBoDeungOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
    public class GuRyeonBoDeungImageOption : TowerImageOption
    {
        public override string Name => nameof(GuRyeonBoDeungImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (HolderStat.TowerInfo.Hais[0].Spec.HaiType switch
        {
            HaiType.Wan => 47,
            HaiType.Sou => 48,
            HaiType.Pin => 49,
            _ => 47,
        }, 1) };
    }
}
