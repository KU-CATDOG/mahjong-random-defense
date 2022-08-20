namespace MRD
{
    public class TrioRelic : Relic
    {
        public override string Name => "Trio";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.S;

        public override void OnBuyAction()
            => RoundManager.Inst.Grid.RemoveHaisOnTrio();

    }
}