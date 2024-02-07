using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events.Effects
{
    public struct FreezeEnemyEvent : IEvent
    {
        public IEntity Source;
        public Target Target;
        public int Duration;

        public FreezeEnemyEvent(IEntity source, Target target, int duration)
        {
            Source = source;
            Target = target;
            Duration = duration;
        }
    }
}