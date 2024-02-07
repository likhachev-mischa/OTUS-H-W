using DI;
using Events;
using Events.Effects;
using Game.EventBus;

namespace Handlers.Turn
{
    public class SwitchToRandomTargetEffectHandler :BaseHandler<SwitchToRandomTargetEffect>
    {
        [Inject]
        public new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(SwitchToRandomTargetEffect evt)
        {
            EventBus.RaiseEvent(new SwitchToRandomTargetEvent(evt.Source,evt.Target,evt.Probability));
        }
    }
   
}