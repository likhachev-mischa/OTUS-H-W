using Events.Effects;

namespace Entities.Components
{
    public sealed class ArmorComponent
    {
        public IEffect[] Effects { get; }

        public ArmorComponent(IEffect[] effects)
        {
            this.Effects = effects;
        }
    }
}