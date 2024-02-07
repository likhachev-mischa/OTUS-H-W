using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    public class FreezeEnemyEffectHandler : BaseHandler<FreezeEnemyEffect>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }
        
        protected override void HandleEvent(FreezeEnemyEffect evt)
        {
            EventBus.RaiseEvent(new FreezeEnemyEvent(evt.Source, evt.Target, evt.Duration));
        }
    }
}