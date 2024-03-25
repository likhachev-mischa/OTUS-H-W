using Atomic.Behaviours;
using Atomic.Elements;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MoveAnimMechanics : IUpdate
    {
        private readonly Animator animator;
        private readonly IAtomicValue<bool> isMoving;
        private readonly int isMovingHash;

        public MoveAnimMechanics(Animator animator, int isMovingHash, IAtomicValue<bool> isMoving)
        {
            this.animator = animator;
            this.isMoving = isMoving;
            this.isMovingHash = isMovingHash;
        }

        public void OnUpdate(float deltaTime)
        {
            this.animator.SetBool(this.isMovingHash, this.isMoving.Value);
        }
    }
}