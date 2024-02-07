using Events.Effects;

namespace Entities.Components
{
    public sealed class WeaponComponent
    {
        public IEffect[] Effects { get; }

        public WeaponComponent(IEffect[] effects)
        {
            this.Effects = effects;
        }
    }
}