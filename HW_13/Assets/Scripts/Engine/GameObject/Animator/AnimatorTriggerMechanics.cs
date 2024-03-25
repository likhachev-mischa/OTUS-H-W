using Atomic.Behaviours;
using Atomic.Elements;
using UnityEngine;

namespace Game.Engine
{
    public sealed class AnimatorTriggerMechanics : IEnable, IDisable
    {
        private readonly Animator animator;
        private readonly int animHash;
        private readonly IAtomicObservable trigger;

        public AnimatorTriggerMechanics(Animator animator, int animHash, IAtomicObservable trigger)
        {
            this.animator = animator;
            this.animHash = animHash;
            this.trigger = trigger;
        }

        public void Enable()
        {
            this.trigger.Subscribe(this.OnStartGather);
        }

        public void Disable()
        {
            this.trigger.Unsubscribe(this.OnStartGather);
        }

        private void OnStartGather()
        {
            this.animator.SetTrigger(this.animHash);
        }
    }
}