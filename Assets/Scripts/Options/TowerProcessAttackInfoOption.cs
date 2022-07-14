using System.Collections.Generic;

namespace MRD
{
    public abstract class TowerProcessAttackInfoOption : TowerOption
    {
        public virtual int Priority { get; }

        // 이 함수들을 모아서 순회하며 AttackInfo를 처리한다.
        public abstract void ProcessAttackInfo(List<AttackInfo> infos);

        // 이 옵션은 공격할 때만 참조할 옵션이므로 붙고 떨어질 때 아무것도 하면 안됨
        protected sealed override void OnAttachOption()
        {
        }

        public sealed override void Dispose()
        {
        }
    }
}
