using DI;
using Events;
using Events.Requests;
using Game.EventBus;

namespace Handlers.Turn
{
    public class TurnFinishRequestHandler : BaseHandler<TurnFinishedRequest>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(TurnFinishedRequest evt)
        {
            EventBus.RaiseEvent(new TurnFinishedEvent(evt.Source));
        }
    }
}