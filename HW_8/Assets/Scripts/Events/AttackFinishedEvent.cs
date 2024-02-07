using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events
{
    public struct AttackFinishedEvent : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;

        public AttackFinishedEvent(IEntity source, Target target)
        {
            Source = source;
            Target = target;
        }
    }
}