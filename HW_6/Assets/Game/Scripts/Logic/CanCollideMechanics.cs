using UnityEngine;

namespace Game
{
    public class CanCollideMechanics
    {
        private readonly CapsuleCollider capsuleCollider;
        private readonly AtomicVariable<bool> isDead;

        public CanCollideMechanics(CapsuleCollider capsuleCollider, AtomicVariable<bool> isDead)
        {
            this.capsuleCollider = capsuleCollider;
            this.isDead = isDead;
        }

        public void OnEnable()
        {
            isDead.ValueChanged += CanMove;
        }

        public void OnDisable()
        {
            isDead.ValueChanged -= CanMove;
        }

        private void CanMove(bool value)
        {
            capsuleCollider.enabled = !isDead.Value;
        }
    }
}