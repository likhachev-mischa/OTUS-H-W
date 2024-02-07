using System;
using Entities;
using Game.EventBus;

namespace Events
{
    [Serializable]
    public struct TurnFinishedEvent : IEvent
    {
        public IEntity hero;

        public TurnFinishedEvent(IEntity hero)
        {
            this.hero = hero;
        }
    }
}