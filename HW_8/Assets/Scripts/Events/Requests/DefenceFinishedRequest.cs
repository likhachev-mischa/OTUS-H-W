using System;
using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events.Requests
{
    [Serializable]
    public struct DefenceFinishedRequest : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;

        public DefenceFinishedRequest(IEntity source, Target target)
        {
            Source = source;
            Target = target;
        }
        
    }
}