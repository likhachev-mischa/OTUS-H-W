using DI;
using Entities.Components;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    public class VampirismEventHandler : BaseHandler<VampirismEvent>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }
        
        protected override void HandleEvent(VampirismEvent evt)
        {
            float probability = evt.Probability;
            if (!(Random.value <= probability))
            {
                return;
            }

            int regen = evt.Target.entity.Get<Health>().ReceivedDamage;
            var health = evt.Source.Get<Health>();
            health.Value += regen;
            
            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has vampired {regen} health from {evt.Target.entity.Get<Name>().Value}");
        }
    }
}