using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class ActionComponent
    {
        public IAtomicAction Request => this.request;
        
        public IAtomicExpression<bool> Condition => this.condition;
        
        public IAtomicObservable Event => this.@event;

        [SerializeField]
        private AtomicAction request;

        [SerializeField]
        private AndExpression condition;

        [SerializeField]
        private AtomicEvent @event;

        public void Compose()
        {
            this.request.Compose(() =>
            {
                if (this.condition.Invoke())
                {
                    this.@event?.Invoke();
                }
            });
        }
    }
}