using Entities;
using Entities.Components;

namespace Events.Effects
{
    public struct FreezeEnemyEffect : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }

        public int Duration;
    }
}