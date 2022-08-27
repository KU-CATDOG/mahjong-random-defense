namespace MRD
{
    public class OneShotRelic : Relic
    {
        public override string Name => "OneShot";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.B;
    }
}