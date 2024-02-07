using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events
{
    public struct DealDamageEvent : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;
        public readonly int Damage;

        public DealDamageEvent(IEntity source,Target target, int damage)
        {
            Source = source;
            Target = target;
            Damage = damage;
        }
    }
}