namespace MRD
{
    public class DoraRelic : Relic
    {
        public bool buyDora = false;
        public override string Name => "Dora";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.B;
        public override void OnBuyAction()
        {
            RoundManager.Inst.Grid.doraList.AddDora();
            RoundManager.Inst.Grid.doraList.SetDoraButton();
        }
    }
}