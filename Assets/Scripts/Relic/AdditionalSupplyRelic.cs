namespace MRD
{
    public class AdditionalSupplyRelic : Relic
    {
        public override string Name => "AdditionalSupply";
        public override int MaxAmount => 2;
        public override RelicRank Rank => RelicRank.A;
        public override void OnBuyAction()
        
            => RoundManager.Inst.Grid.SetUICells(furoLimit: RoundManager.Inst.Grid.gridFuroLimit+1 ,doLock: false);
        

    }
}