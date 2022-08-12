
namespace MRD
{
    public class RichiInfo
    {
        private TowerInfo towerInfo;
        public RichiState state { get; private set; }
        public float probability { get; private set; }
        public float stepSize { get; private set; } = 0.2f;

        public RichiInfo(TowerInfo towerInfo)
        {
            this.towerInfo = towerInfo;
            state = RichiState.Idle;
            probability = 0f;
        }
        public RichiState OnRoundTick()
        {
            switch(state)
            {
                case RichiState.Idle:
                    if(UnityEngine.Random.Range(0f,1f) < probability)
                    {
                        state = RichiState.Ready;
                        probability = 0.1f;
                        break;
                    }
                    probability += stepSize;
                break;
                case RichiState.Ready:
                    if(UnityEngine.Random.Range(0f,1f) < probability)
                    {
                        state = RichiState.Idle;
                        probability = 0f;
                        stepSize /= 2;
                        break;
                    }
                    probability += 0.15f;
                break;
            }
            return state;
        }
        public bool EnableRichi() 
        {
            if(RoundManager.Inst.playerHealth < 1000) return false;
            state = (state == RichiState.Ready) ? RichiState.OnRichi : state;
            RoundManager.Inst.playerHealth -= 1000;
            probability = 1f;
            return true;
        }
        public RichiState OnTsumo()
        {
            if(state != RichiState.OnRichi) return state;
            probability -= 0.1f;
            if(probability < 0f)
                state = RichiState.End;
            return state;
        }
    }
    public enum RichiState { Idle, Ready, OnRichi, End }
}