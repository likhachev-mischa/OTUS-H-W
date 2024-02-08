using DI;
using Entities;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers.Turn
{
    public class ReceiveDamageEffectHandler : BaseHandler<ReceiveDamageEffect>
    {
        private EffectStack effectStack;

        [Inject]
        public void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(ReceiveDamageEffect evt)
        {
            IEntity target = evt.Target.entity;
            var health = evt.Source.Get<Health>();
            int damage = health.ReceivedDamage;
            health.Value = Mathf.Max(0, health.Value - damage);

            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} has received {damage} damage from {evt.Target.entity.Get<Name>().Value}");

            if (health.Value <= 0)
            {
                EventBus.RaiseEvent(new DeathRequest(target, target.Get<Target>()));
            }

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}