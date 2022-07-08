using UnityEngine;

namespace MRD
{
    public class Tower : MonoBehaviour
    {
        public TowerInfo TowerInfo { get; private set; }
        public GridCell Pair { get; private set; }

        public XY Coordinate => Pair.Coordinate;

        /// <summary>
        /// 모든 타워가 공통으로 가지는 기본 공격력
        /// </summary>
        public float BaseAttack => TowerInfo.Hais.Count * 10;

        /// <summary>
        /// 모든 타워가 공통으로 가지는 기본 공격 속도 (1초에 이만큼 때림)
        /// </summary>
        public float BaseAttackSpeed => 1f;

        public float BaseCritChance => 0.05f;

        public float BaseCritMultiplier => 2;

        public void Init(GridCell gridCellInstance, XY coord)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);
        }
    }
}