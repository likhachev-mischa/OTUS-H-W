using DI;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    public class VampirismEffectHandler : BaseHandler<VampirismEffect>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }
        
        protected override void HandleEvent(VampirismEffect evt)
        {
            float probability = evt.Probability;
            if (!(Random.value <= probability))
            {
                return;
            }

            int regen = evt.Target.entity.Get<Health>().ReceivedDamage;
            var health = evt.Source.Get<Health>();
            health.Value += regen;
            
            foreach (IEffect evtSuccessEffect in evt.SuccessEffects)
            {
                evtSuccessEffect.Source = evt.Source;
                evtSuccessEffect.Target = evt.Target;
                
                EventBus.RaiseEvent(evtSuccessEffect);
            }
            
            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has vampired {regen} health from {evt.Target.entity.Get<Name>().Value}");
        }
    }
}