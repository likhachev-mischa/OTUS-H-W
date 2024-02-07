using DI;
using Entities;
using Entities.Components;
using Events;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Handlers.Turn
{
    public class ReceiveDamageHandler : BaseHandler<ReceiveDamageEvent>
    {
        private ActiveHeroService activeHeroService;
        private TurnStateService turnStateService;

        [Inject]
        private void Construct(EventBus eventBus, ActiveHeroService activeHeroService,
            TurnStateService turnStateService)
        {
            base.Construct(eventBus);
            this.activeHeroService = activeHeroService;
            this.turnStateService = turnStateService;
        }

        protected override void HandleEvent(ReceiveDamageEvent evt)
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

            /*if (turnStateService.State == TurnState.POST_ATTACK)
            {
                return;
            }

            turnStateService.State = TurnState.POST_ATTACK;*/

           

            
        }
    }
}