namespace MRD
{
    public class FastExpandRelic : Relic
    {
        public override string Name => "FastExpand";
        public override int MaxAmount => 2;
        public override RelicRank Rank => RelicRank.C;
        public override void OnBuyAction() {
            var round = RoundManager.Inst;
            round.Grid.RefreshLockedCellsImage();
        }
        
    }
}