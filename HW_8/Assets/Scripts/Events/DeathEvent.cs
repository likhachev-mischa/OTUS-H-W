using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events
{
    public struct DeathEvent : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;
        public DeathEvent(IEntity source,Target target)
        {
            Source = source;
            Target = target;
        }
    }
}