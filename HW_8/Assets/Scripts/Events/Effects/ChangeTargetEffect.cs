using Entities;
using Entities.Components;

namespace Events.Effects
{
    public class ChangeTargetEffect : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }
        public IEntity NewTarget { get; set; }
    }
}