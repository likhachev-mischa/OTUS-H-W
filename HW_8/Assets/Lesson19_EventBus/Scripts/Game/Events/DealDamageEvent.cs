using Entities;

namespace Lessons.Game.Events
{
    public readonly struct DealDamageEvent : IEvent
    {
        public readonly IEntity Entity;
        public readonly int Damage;

        public DealDamageEvent(IEntity entity, int damage)
        {
            Entity = entity;
            Damage = damage;
        }
    }
}