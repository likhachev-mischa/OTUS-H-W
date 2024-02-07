using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events
{
    public struct AttackEvent : IEvent
    {
        public IEntity Source;
        public Target Target;

        public AttackEvent(IEntity source, Target target)
        {
            Source = source;
            Target = target;
        }
    }
}