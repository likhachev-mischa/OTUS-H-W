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
            IEntity target = evt.Target.entity;

            if (target.TryGet(out Inactive inactive))
            {
                inactive.Value = true;
                inactive.Duration = evt.Duration;
            }
            else
            {
                target.Add(new Inactive(){Value = true,Duration = evt.Duration}); 
            }
            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has frozen {evt.Target.entity.Get<Name>().Value} for {evt.Duration} turns");
        }
    }
}