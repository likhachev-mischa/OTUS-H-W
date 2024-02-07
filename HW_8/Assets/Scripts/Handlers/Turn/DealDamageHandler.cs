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
    public class DealDamageHandler : BaseHandler<DealDamageEvent>
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

        protected override void HandleEvent(DealDamageEvent evt)
        {
            /*
            if (turnStateService.State == TurnState.ATTACK)
            {
                turnStateService.State = TurnState.DEFENCE;
            }
            */

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