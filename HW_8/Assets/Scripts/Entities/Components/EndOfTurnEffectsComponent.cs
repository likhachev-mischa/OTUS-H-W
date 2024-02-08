using System;
using Events.Effects;

namespace Entities.Components
{
    [Serializable]
    public sealed class EndOfTurnEffectsComponent
    {
        public IEffect[] Effects;

        public EndOfTurnEffectsComponent(IEffect[] effects)
        {
            Effects = effects;
        }
    }
}