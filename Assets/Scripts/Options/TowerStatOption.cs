namespace MRD
{
    public abstract class TowerStatOption : TowerOption
    {
        public virtual Stat AdditionalStat => new();

        public virtual AttackBehaviour AttackBehaviour => null;

        public virtual TargetTo TargetTo => TargetTo.Proximity;

        public virtual int MaxRagePoint => 0;

        public virtual Stat RageStat => new();
    }
    public class Stat
    {
        public float DamageConstant;
        public float DamagePercent;
        public float DamageMultiplier;
        public float AttackSpeed;
        public float CritChance;
        public float CritDamage;

        public Stat(float damageConstant = 0, float damagePercent = 0, float damageMultiplier = 1, float attackSpeed = 1, float critChance = 0, float critDamage = 0)
        {
            DamageConstant = damageConstant;
            DamagePercent = damagePercent;
            DamageMultiplier = damageMultiplier;
            AttackSpeed = attackSpeed;
            CritChance = critChance;
            CritDamage = critDamage;
        }
        public static Stat DefaultStat(int haiNum) => new Stat(damageConstant: haiNum * 10f, critDamage: 2f);

        public static Stat operator +(Stat a, Stat b) => new Stat
            (
                damageConstant: a.DamageConstant + b.DamageConstant,
                damagePercent: a.DamagePercent + b.DamagePercent,
                damageMultiplier: a.DamageMultiplier * b.DamageMultiplier,
                attackSpeed: a.AttackSpeed * b.AttackSpeed,
                critChance: a.CritChance + b.CritChance,
                critDamage: a.CritDamage + b.CritDamage
            );
        public static Stat operator *(Stat a, float f) => new Stat
            (
                damageConstant: a.DamageConstant * f,
                damagePercent: a.DamagePercent * f ,
                damageMultiplier: (a.DamageMultiplier - 1) * f + 1,
                attackSpeed: (a.AttackSpeed - 1) * f + 1,
                critChance: a.CritChance + f,
                critDamage: a.CritDamage + f
            );

        public float Damage => DamageConstant * (1 + DamagePercent) * DamageMultiplier;
    }
}
