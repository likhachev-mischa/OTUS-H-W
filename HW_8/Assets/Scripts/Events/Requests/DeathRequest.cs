using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events
{
    public struct DeathRequest : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;
        public DeathRequest(IEntity source,Target target)
        {
            Source = source;
            Target = target;
        }
    }
}