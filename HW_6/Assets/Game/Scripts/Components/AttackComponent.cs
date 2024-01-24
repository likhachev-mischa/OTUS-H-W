using UnityEngine;

namespace Game
{
    public sealed class AttackComponent
    {
        public IAtomicValue<float> attackDistance;
        private IAtomicVariable<bool> canAttack;
        private IAtomicAction attackRequest;
        private IAtomicVariable<TakeDamageComponent> target;

        public AttackComponent(IAtomicValue<float> attackDistance, IAtomicVariable<bool> canAttack,
            IAtomicAction attackRequest, IAtomicVariable<TakeDamageComponent> target)
        {
            this.attackDistance = attackDistance;
            this.canAttack = canAttack;
            this.attackRequest = attackRequest;
            this.target = target;
        }

        public void Attack(Entity target)
        {
            if (!canAttack.Value)
            {
                return;
            }

            if (target.TryGet(out TakeDamageComponent takeDamageComponent))
            {
                this.target.Value = takeDamageComponent;
                attackRequest.Invoke();
            }
        }
    }
}