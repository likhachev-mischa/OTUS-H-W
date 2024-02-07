using System;
using Entities;
using Game.EventBus;

namespace Events
{
    [Serializable]
    public struct EnemySelectionEvent : IEvent
    {
        public IEntity enemy;
        public EnemySelectionEvent(IEntity enemy)
        {
            this.enemy = enemy;
        }
    }
}