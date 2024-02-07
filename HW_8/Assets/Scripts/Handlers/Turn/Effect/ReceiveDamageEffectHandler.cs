using DI;
using Entities;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    public class ReceiveDamageEffectHandler : BaseHandler<ReceiveDamageEffect>
    {
        [Inject]
        public new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(ReceiveDamageEffect evt)
        {
            IEntity target = evt.Target.entity;
            var health = evt.Source.Get<Health>();
            int damage = health.ReceivedDamage;
            health.Value = Mathf.Max(0, health.Value - damage);

            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has received {damage} damage from {evt.Target.entity.Get<Name>().Value}");
            health.ReceivedDamage = 0;
            
            if (health.Value <= 0)
            {
                EventBus.RaiseEvent(new DeathEvent(target, target.Get<Target>()));
            }

        }
    }
}