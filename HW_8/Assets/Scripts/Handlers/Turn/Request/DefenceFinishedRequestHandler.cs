using DI;
using Events;
using Events.Requests;
using Game.EventBus;

namespace Handlers.Turn
{
    public class DefenceFinishedRequestHandler : BaseHandler<DefenceFinishedRequest>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(DefenceFinishedRequest evt)
        {
            EventBus.RaiseEvent(new DefenceFinishedEvent(evt.Source, evt.Target));
        }
    }
}