using DI;
using Entities;
using Entities.Components;
using Events;
using Game.EventBus;
using Services;

namespace Handlers.Turn
{
    public class EnemySelectionHandler : BaseHandler<EnemySelectionEvent>
    {
        private ActiveHeroService activeHeroService;

        [Inject]
        private void Construct(EventBus eventBus, ActiveHeroService activeHeroService)
        {
            base.Construct(eventBus);
            this.activeHeroService = activeHeroService;
        }

        protected override void HandleEvent(EnemySelectionEvent evt)
        {
            IEntity hero = activeHeroService.Hero;
            var heroTarget = hero.Get<Target>();
            heroTarget.entity = evt.enemy;
            EventBus.RaiseEvent(new AttackEvent(hero, heroTarget));
        }
    }
}