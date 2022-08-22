using System.Collections.Generic;

namespace MRD
{
    public class GukSaMuSangStatOption : TowerStatOption
    {
        public override string Name => nameof(GukSaMuSangStatOption);
        public override Stat AdditionalStat => new(damageMultiplier: 5f, attackSpeed: 0.5f, critChance: 1f);

        public override TargetTo TargetTo => TargetTo.GukSa;
    }

    public class GukSaMuSangOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(GukSaMuSangOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            foreach (var i in infos)
            {
                if(i is not BulletInfo) continue;
                i.SetImage(AttackImage.GukSaMuSang, 5);
                i.UpdateShupaiLevel(HaiType.Sou, 2);
                
            }
        }
    }
    public class GukSaMuSangImageOption : TowerImageOption
    {
        public override string Name => nameof(GukSaMuSangImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (50, 1) };
    }
}
