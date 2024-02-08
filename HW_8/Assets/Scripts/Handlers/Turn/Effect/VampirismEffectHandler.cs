using DI;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers.Turn
{
    public class VampirismEffectHandler : BaseHandler<VampirismEffect>
    {
        private EffectStack effectStack;

        [Inject]
        private void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(VampirismEffect evt)
        {
            float probability = evt.Probability;
            if (!(Random.value <= probability))
            {
                if (!effectStack.IsEmpty())
                {
                    EventBus.RaiseEvent(effectStack.Pop());
                }

                return;
            }

            int regen = evt.Target.entity.Get<Health>().ReceivedDamage;
            var health = evt.Source.Get<Health>();
            health.Value += regen;

            if (evt.SuccessEffects != null)
            {
                for (var i = evt.SuccessEffects.Length - 1; i >= 0; i--)
                {
                    IEffect effect = evt.SuccessEffects[i];
                    effect.Source = evt.Source;
                    effect.Target = evt.Target;
                    effectStack.Push(effect);
                }
            }

            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} has vampired {regen} health from {evt.Target.entity.Get<Name>().Value}");
            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}