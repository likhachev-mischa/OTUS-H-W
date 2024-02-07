using DI;
using Entities;
using Events;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Pipeline.Tasks
{
    public sealed class FinishTurnTask : Task
    {
        private EventBus eventBus;
        private ActiveHeroService activeHeroService;

        [Inject]
        private void Construct(EventBus eventBus, ActiveHeroService activeHeroService)
        {
            this.eventBus = eventBus;
            this.activeHeroService = activeHeroService;
        }

        protected override void OnRun()
        {
            IEntity hero = activeHeroService.Hero;
            eventBus.RaiseEvent(new TurnFinishedEvent(hero));
            Debug.Log("Finish Turn!");
            Finish();
        }
    }
}