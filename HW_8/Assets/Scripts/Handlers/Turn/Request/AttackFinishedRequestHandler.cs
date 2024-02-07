using DI;
using Events;
using Events.Requests;
using Game.EventBus;

namespace Handlers.Turn
{
    public class AttackFinishedRequestHandler : BaseHandler<AttackFinishedRequest>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(AttackFinishedRequest evt)
        {
            EventBus.RaiseEvent(new AttackFinishedEvent(evt.Source, evt.Target));
        }
    }
}