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
            
            foreach (IEffect effect in weaponComponent.Effects)
            {
                effect.Source = evt.Source;
                effect.Target = evt.Source.Get<Target>();
                    
                EventBus.RaiseEvent(effect);
            }
            EventBus.RaiseEvent(new AttackFinishedRequest(evt.Source,evt.Target));
        }
    }
}