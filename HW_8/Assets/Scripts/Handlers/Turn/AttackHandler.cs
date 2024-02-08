using DI;
using Entities.Components;
using Events;
using Events.Effects;
using Events.Requests;
using Game.EventBus;
using Pipeline;
using Services;
using UnityEngine;

namespace Handlers.Turn
{
    public sealed class AttackHandler : BaseHandler<AttackEvent>
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

        protected override void HandleEvent(AttackEvent evt)
        {
            turnStateService.State = TurnState.ATTACK;

            if (!evt.Source.TryGet(out WeaponComponent weaponComponent))
            {
                return;
            }

            Debug.LogWarning($"{evt.Source.Get<Name>().Value} is attacking {evt.Target.entity.Get<Name>().Value}");

            effectStack.Push(new AttackFinishedRequest() { Source = evt.Source, Target = evt.Target });

            if (weaponComponent.Effects != null)
            {
                for (var i = weaponComponent.Effects.Length - 1; i >= 0; i--)
                {
                    IEffect effect = weaponComponent.Effects[i];
                    effect.Source = evt.Source;
                    effect.Target = evt.Target;

                    effectStack.Push(effect);
                }
            }

            EventBus.RaiseEvent(effectStack.Pop());
        }
    }
}