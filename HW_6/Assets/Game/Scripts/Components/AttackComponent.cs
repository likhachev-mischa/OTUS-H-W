using UnityEngine;

namespace Game
{
    public sealed class AttackComponent
    {
        public IAtomicValue<float> attackDistance;
        public IAtomicVariable<bool> canAttack;
        public IAtomicAction attackRequest;
        public IAtomicVariable<TakeDamageComponent> target;

        public AttackComponent(IAtomicValue<float> attackDistance, IAtomicVariable<bool> canAttack,
            IAtomicAction attackRequest, IAtomicVariable<TakeDamageComponent> target)
        {
            this.attackDistance = attackDistance;
            this.canAttack = canAttack;
            this.attackRequest = attackRequest;
            this.target = target;
        }

        public void Attack()
        {
            attackRequest.Invoke();
        }
    }
}