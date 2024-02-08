using DI;
using Entities;
using Entities.Components;
using Events;
using Events.Effects;
using Events.Requests;
using Game.EventBus;
using Pipeline;
using Services;

namespace Handlers.Turn
{
    public sealed class DefenceFinishedHandler : BaseHandler<DefenceFinishedEvent>
    {
        private ActiveHeroService activeHeroService;
        private EffectStack effectStack;
        private TurnStateService turnStateService;

        [Inject]
        private void Construct(EventBus eventBus, ActiveHeroService activeHeroService, EffectStack effectStack,
            TurnStateService turnStateService)
        {
            base.Construct(eventBus);
            this.activeHeroService = activeHeroService;
            this.effectStack = effectStack;
            this.turnStateService = turnStateService;
        }

        protected override void HandleEvent(DefenceFinishedEvent evt)
        {
            IEntity entity = evt.Target.entity;

            turnStateService.State = TurnState.POST_ATTACK;

            while (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }

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
                if (effect is DealDamageEffect)
                {
                    initiateDefence = true;
                    break;
                }
            }

            if (initiateDefence)
            {
                effectStack.Push(new AttackFinishedRequest() { Source = entity, Target = newTarget });
            }
            else
            {
                effectStack.Push(new ActionFinishedRequest() { Source = entity, Target = newTarget });
            }

            if (postAttackComponent.Effects != null)
            {
                for (var i = postAttackComponent.Effects.Length - 1; i >= 0; i--)
                {
                    IEffect effect = postAttackComponent.Effects[i];
                    effect.Source = entity;
                    effect.Target = newTarget;

                    effectStack.Push(effect);
                }
            }

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}