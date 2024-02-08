using DI;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers.Turn
{
    public class DamageBlockEffectHandler : BaseHandler<DamageBlockEffect>
    {
        private EffectStack effectStack;

        [Inject]
        private void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(DamageBlockEffect evt)
        {
            if (!evt.Source.TryGet(out ShieldCount shieldCount))
            {
                evt.Source.Add(new ShieldCount() { Value = evt.Amount });
                shieldCount = evt.Source.Get<ShieldCount>();
            }

            if (!evt.IsInfinite && shieldCount.Value <= 0)
            {
                if (!effectStack.IsEmpty())
                {
                    EventBus.RaiseEvent(effectStack.Pop());
                }

                return;
            }

            --shieldCount.Value;
            var health = evt.Source.Get<Health>();
            health.ReceivedDamage = 0;
            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} has blocked all damage from {evt.Target.entity.Get<Name>().Value}");

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

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}