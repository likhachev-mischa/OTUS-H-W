using System;
using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events
{
    [Serializable]
    public struct UltimateEvent : IEvent
    {
        public IEntity Source;
        public Target Target;

        public UltimateEvent(IEntity source, Target target)
        {
            Source = source;
            Target = target;
        }
    }
}