using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers.Turn
{
    public sealed class DealDamageEffectHandler : BaseHandler<DealDamageEffect>
    {
        private EffectStack effectStack;

        [Inject]
        private void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(DealDamageEffect evt)
        {
            if (evt.Damage == default)
            {
                evt.Damage = evt.Source.Get<Damage>().Value;
            }

            IEntity target = evt.Target.entity;
            if (!target.TryGet(out Health health))
            {
                if (!effectStack.IsEmpty())
                {
                    EventBus.RaiseEvent(effectStack.Pop());
                }

                return;
            }

            health.ReceivedDamage = evt.Damage;

            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} is trying to deal {evt.Damage} damage to {evt.Target.entity.Get<Name>().Value}");

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}