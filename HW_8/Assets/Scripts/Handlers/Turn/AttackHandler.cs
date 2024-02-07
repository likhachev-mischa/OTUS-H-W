using DI;
using Entities.Components;
using Events;
using Events.Effects;
using Events.Requests;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Handlers.Turn
{
    public sealed class AttackHandler : BaseHandler<AttackEvent>
    {
        private TurnStateService turnStateService;
        
        [Inject]
        private void Construct(EventBus eventBus, TurnStateService turnStateService)
        {
            base.Construct(eventBus);
            this.turnStateService = turnStateService;
        }
        
        protected override void HandleEvent(AttackEvent evt)
        {
            turnStateService.State = TurnState.ATTACK;
            if (!evt.Source.TryGet(out WeaponComponent weaponComponent))
            {
                return;
            }

            Debug.LogWarning($"{evt.Source.Get<Name>().Value} is attacking {evt.Target.entity.Get<Name>().Value}");

            IEffect[] effects = weaponComponent.Effects;
            IEffect firstEffect = effects[0];
            
            IEffect lastEffect = firstEffect;
            for (var i = 1; i < effects.Length; i++)
            {
                lastEffect.NextEffect = effects[i];
                lastEffect = lastEffect.NextEffect;
            }

            lastEffect.NextEffect = new AttackFinishedRequest() { Source = evt.Source, Target = evt.Target };
            EventBus.RaiseEvent(firstEffect);
            //EventBus.RaiseEvent(new AttackFinishedRequest() {Source = evt.Source,Target = evt.Target});
        }
    }
}