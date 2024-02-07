using System;
using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events.Requests
{
    [Serializable]
    public struct AttackFinishedRequest : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;

        public AttackFinishedRequest(IEntity source, Target target)
        {
            Source = source;
            Target = target;
        }
        
    }
}