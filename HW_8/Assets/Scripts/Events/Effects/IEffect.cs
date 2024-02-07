using Entities;
using Entities.Components;
using Game.EventBus;

namespace Events.Effects
{
    public interface IEffect : IEvent
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }
    }
}