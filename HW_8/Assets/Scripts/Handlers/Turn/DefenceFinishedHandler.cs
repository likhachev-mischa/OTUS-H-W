using DI;
using Entities;
using Entities.Components;
using Events;
using Events.Effects;
using Events.Requests;
using Game.EventBus;
using Services;

namespace Handlers.Turn
{
    public sealed class DefenceFinishedHandler : BaseHandler<DefenceFinishedEvent>
    {
        private ActiveHeroService activeHeroService;
        [Inject]
        private void Construct(EventBus eventBus, ActiveHeroService activeHeroService)
        {
            base.Construct(eventBus);
            this.activeHeroService = activeHeroService;
        }
        
        protected override void HandleEvent(DefenceFinishedEvent evt)
        {
            IEntity entity = evt.Target.entity;
            if (entity != activeHeroService.Hero)
            {
                return;
            }

            if (!entity.TryGet(out PostAttackComponent postAttackComponent))
            {
                return;
            }

            var newTarget = entity.Get<Target>();
            newTarget.entity = evt.Source;

            bool initiateDefence = false;
            foreach (IEffect effect in postAttackComponent.Effects)
            {
                effect.Source = entity;
                effect.Target = newTarget;

                if (!initiateDefence && effect is DealDamageEffect)
                {
                    initiateDefence = true;
                }
                
                EventBus.RaiseEvent(effect);
            }

            if (initiateDefence)
            {
                EventBus.RaiseEvent(new AttackFinishedRequest(){Source = entity,Target = newTarget});
            }
            else
            {
                EventBus.RaiseEvent(new TurnFinishedRequest(entity));
            }
        }
    }
}