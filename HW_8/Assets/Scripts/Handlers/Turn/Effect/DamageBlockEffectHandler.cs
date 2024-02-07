using DI;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    public class DamageBlockEffectHandler : BaseHandler<DamageBlockEffect>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
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
                return;
            }

            IEffect[] effects = evt.SuccessEffects;
            IEffect firstEffect = evt.NextEffect;
            
            IEffect lastEffect = firstEffect;
            for (var i = 0; i < effects.Length; i++)
            {
                lastEffect.NextEffect = effects[i];
                lastEffect = lastEffect.NextEffect;
            }
            
            /*foreach (IEffect evtSuccessEffect in evt.SuccessEffects)
            {
                evtSuccessEffect.Source = evt.Source;
                evtSuccessEffect.Target = evt.Target;

                EventBus.RaiseEvent(evtSuccessEffect);
            }*/

            --shieldCount.Value;
            var health = evt.Source.Get<Health>();
            health.ReceivedDamage = 0;
            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} has blocked all damage from {evt.Target.entity.Get<Name>().Value}");
        }
    }
}