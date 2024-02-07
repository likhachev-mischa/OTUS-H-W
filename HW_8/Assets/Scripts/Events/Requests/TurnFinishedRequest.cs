using System;
using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events.Effects
{
    [Serializable]
    public struct TurnFinishedRequest : IEvent
    {
        public IEntity Source;

        public TurnFinishedRequest(IEntity source)
        {
            Source = source;
        }
    }
}