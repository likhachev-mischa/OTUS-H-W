using DI;
using Entities;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Handlers.Turn
{
    public sealed class DealDamageEffectHandler : BaseHandler<DealDamageEffect>
    {
        private TurnStateService turnStateService;
        private ActiveHeroService activeHeroService;

        [Inject]
        private void Construct(EventBus eventBus, TurnStateService turnStateService,
            ActiveHeroService activeHeroService)
        {
            base.Construct(eventBus);
            this.turnStateService = turnStateService;
            this.activeHeroService = activeHeroService;
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
                return;
            }

            health.ReceivedDamage = evt.Damage;

            Debug.LogWarning($"{evt.Source.Get<Name>().Value} is trying to deal {evt.Damage} damage to {evt.Target.entity.Get<Name>().Value}");
            
            
        }
    }
}