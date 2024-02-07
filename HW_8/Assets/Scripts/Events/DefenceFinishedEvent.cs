using System;
using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events
{
    
    [Serializable]
    public struct DefenceFinishedEvent : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;

        public DefenceFinishedEvent(IEntity source, Target target)
        {
            Source = source;
            Target = target;
        }
    }
}