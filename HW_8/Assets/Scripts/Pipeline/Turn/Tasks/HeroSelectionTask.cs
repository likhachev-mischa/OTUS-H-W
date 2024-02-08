using DI;
using Entities;
using Entities.Components;
using Events;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Pipeline.Tasks
{
    public sealed class HeroSelectionTask : Task
    {
        private EventBus eventBus;
        private HeroRepositoryService repositoryService;
        private TeamService teamService;


        [Inject]
        private void Construct(EventBus eventBus, HeroRepositoryService repositoryService, TeamService teamService)
        {
            this.eventBus = eventBus;
            this.repositoryService = repositoryService;
            this.teamService = teamService;
        }

        protected override void OnRun()
        {
            IEntity hero = repositoryService.GetHero();
           
            if (hero.TryGet(out Inactive inactive))
            {
                if (inactive.Value)
                {
                    if (inactive.Duration > 0)
                    {
                        --inactive.Duration;
                        if (inactive.Duration == 0)
                        {
                            inactive.Value = false;
                        }
                        Debug.LogWarning("SKIP TURN");
                        eventBus.RaiseEvent(new TurnFinishedEvent(hero));
                        eventBus.RaiseEvent(new TurnSkippedEvent(hero));
                        Finish();
                        return;
                    }

                }
               
            }
           
            eventBus.RaiseEvent(new HeroSelectionEvent(hero));
            
            Finish();
        }
    }
}