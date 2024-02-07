using DI;
using Events;
using Game.EventBus;
using Services;

namespace Handlers.Turn
{
    public class HeroSelectionHandler : BaseHandler<HeroSelectionEvent>
    {
        private ActiveHeroService activeHeroService;
        [Inject]
        private void Construct(EventBus eventBus,ActiveHeroService activeHeroService)
        {
            base.Construct(eventBus);
            this.activeHeroService = activeHeroService;
        }

        protected override void HandleEvent(HeroSelectionEvent evt)
        {
            activeHeroService.Hero = evt.hero;
        }
    }
}