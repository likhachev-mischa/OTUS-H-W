using Entities;
using Game.EventBus;

namespace Events
{
    public struct HeroSelectionEvent : IEvent
    {
        public IEntity hero;

        public HeroSelectionEvent(IEntity hero)
        {
            this.hero = hero;
        }
    }
}