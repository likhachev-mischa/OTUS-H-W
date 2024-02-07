using Events.Effects;

namespace Entities.Components
{
    public sealed class PostAttackComponent
    {
        public IEffect[] Effects { get; }

        public PostAttackComponent(IEffect[]  effects)
        {
            this.Effects = effects;
        }
    }
}