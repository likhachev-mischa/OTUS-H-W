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
    public class AttackFinishedHandler : BaseHandler<AttackFinishedEvent>
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

        protected override void HandleEvent(AttackFinishedEvent evt)
        {
            IEntity target = evt.Target.entity;

            if (!target.TryGet(out ArmorComponent armorComponent))
            {
                return;
            }

            var newTarget = target.Get<Target>();
            newTarget.entity = evt.Source;
            foreach (IEffect effect in armorComponent.Effects)
            {
                effect.Source = target;
                effect.Target = newTarget;

                if (turnStateService.State != TurnState.ATTACK && effect is DealDamageEffect)
                {
                    continue;
                }

                EventBus.RaiseEvent(effect);
            }

            if (turnStateService.State == TurnState.ATTACK)
            {
                turnStateService.State = TurnState.DEFENCE;
                EventBus.RaiseEvent(new DefenceFinishedRequest(target, newTarget));
            }
            else
            {
                EventBus.RaiseEvent(new TurnFinishedRequest(evt.Source));
            }
        }
    }
}