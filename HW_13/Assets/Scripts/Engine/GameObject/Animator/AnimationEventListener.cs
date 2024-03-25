using System;
using Atomic.Behaviours;
using Atomic.Elements;

namespace Game.Engine
{
    [Serializable]
    public sealed class AnimationEventListener : IEnable, IDisable
    {
        private readonly AnimatorDispatcher animator;
        private readonly IAtomicAction action;
        private readonly string animationEvent;

        public AnimationEventListener(AnimatorDispatcher animator, string animationEvent, IAtomicAction action)
        {
            this.animator = animator;
            this.animationEvent = animationEvent;
            this.action = action;
        }

        public void Enable()
        {
            this.animator.AddListener(this.animationEvent, this.action.Invoke); 
        }

        public void Disable()
        {
            this.animator.RemoveListener(this.animationEvent, this.action.Invoke);
        }
    }
}