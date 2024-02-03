using Entities;

namespace Lessons.Game.Events.Effects
{
    public interface IEffect : IEvent
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }
    }
}