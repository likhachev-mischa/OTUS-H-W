using DI;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    public class DivineShieldEffectHandler : BaseHandler<DivineShieldEffect>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(DivineShieldEffect evt)
        {
            if (!evt.Source.TryGet(out DivineShieldCount divineShieldCount))
            {
                evt.Source.Add(new DivineShieldCount() {Value = evt.Amount});
                divineShieldCount = evt.Source.Get<DivineShieldCount>();
            }

            if (divineShieldCount.Value <= 0)
            {
                return;
            }

            --divineShieldCount.Value;
            var health = evt.Source.Get<Health>();
            health.ReceivedDamage = 0;
            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has blocked all damage from {evt.Target.entity.Get<Name>().Value} using Divine Shield");
        }
    }
}