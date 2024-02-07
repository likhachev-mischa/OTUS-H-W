using DI;
using Entities.Components;
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
            EventBus.RaiseEvent(new VampirismEvent(evt.Source,evt.Target,evt.Probability));
        }
    }
}