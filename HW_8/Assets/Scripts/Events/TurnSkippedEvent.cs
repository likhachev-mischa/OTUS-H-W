using System;
using Entities;
using Game.EventBus;

namespace Events
{
    [Serializable]
    public struct TurnSkippedEvent: IEvent
    {
        public IEntity hero;

        public TurnSkippedEvent(IEntity hero)
        {
            this.hero = hero;
        }

    }
}