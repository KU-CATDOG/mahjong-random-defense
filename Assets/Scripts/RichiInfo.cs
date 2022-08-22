
namespace MRD
{
    public class RichiInfo
    {
        private TowerInfo towerInfo;
        public RichiState State { get; private set; }
        public float Probability { get; private set; }
        public int TsumoCount { get; private set; }
        public float StepSize { get; private set; } = 0.2f;
        public bool isAnimated = true;

        public RichiInfo(TowerInfo towerInfo)
        {
            this.towerInfo = towerInfo;
            State = RichiState.Idle;
            Probability = 0f;
        }
        public RichiState OnRoundTick()
        {
            switch(State)
            {
                case RichiState.Idle:
                    if(UnityEngine.Random.Range(0f,1f) < Probability)
                    {
                        State = RichiState.Ready;
                        Probability = 0.1f;
                        towerInfo.Tower.Pair.ApplyTowerImage();
                        isAnimated = true;
                        break;
                    }
                    Probability += StepSize;
                break;
                case RichiState.Ready:
                    if(UnityEngine.Random.Range(0f,1f) < Probability)
                    {
                        State = RichiState.Idle;
                        Probability = 0f;
                        StepSize /= 2;
                        towerInfo.Tower.Pair.ApplyTowerImage();
                        break;
                    }
                    Probability += 0.15f;
                break;
            }
            return State;
        }
        public bool EnableRichi() 
        {
            if(RoundManager.Inst.playerHealth < 1000) return false;
            State = (State == RichiState.Ready) ? RichiState.OnRichi : State;
            var round = RoundManager.Inst;
            round.playerHealth -= 1000;
            round.healthText.text = "" + round.playerHealth;
            TsumoCount = 10;
            towerInfo.Tower.Pair.ApplyTowerImage();
            towerInfo.Tower.Pair.UpdateRichiImage(TsumoCount);
            return true;
        }
        public RichiState OnTsumo()
        {
            if(State != RichiState.OnRichi) return State;
            TsumoCount--;
            if(TsumoCount < 0) {
                State = RichiState.End;
                towerInfo.Tower.Pair.ApplyTowerImage();
                return State;
            }
            towerInfo.Tower.Pair.UpdateRichiImage(TsumoCount);
            return State;
        }
    }
    public enum RichiState { Idle, Ready, OnRichi, End }
}