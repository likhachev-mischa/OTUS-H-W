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
    public class AttackFinishedHandler : BaseHandler<AttackFinishedEvent>
    {
        private TurnStateService turnStateService;
        private EffectStack effectStack;

        [Inject]
        private void Construct(EventBus eventBus, TurnStateService turnStateService, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.turnStateService = turnStateService;
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(AttackFinishedEvent evt)
        {
            IEntity target = evt.Target.entity;

            while (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }

            if (!target.TryGet(out ArmorComponent armorComponent))
            {
                return;
            }

            var newTarget = target.Get<Target>();
            newTarget.entity = evt.Source;

            if (turnStateService.State == TurnState.ATTACK)
            {
                turnStateService.State = TurnState.DEFENCE;
                effectStack.Push(new DefenceFinishedRequest() { Source = target, Target = newTarget });
            }
            else
            {
                effectStack.Push(new ActionFinishedRequest() { Source = evt.Source, Target = evt.Target });
            }

            if (armorComponent.Effects != null)
            {
                for (var i = armorComponent.Effects.Length - 1; i >= 0; i--)
                {
                    IEffect effect = armorComponent.Effects[i];

                    effect.Source = target;
                    effect.Target = newTarget;

                    if (turnStateService.State == TurnState.POST_ATTACK && effect is DealDamageEffect)
                    {
                        continue;
                    }

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