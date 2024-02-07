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
            //int count = teamService.FindHeroTeam((Entity)hero).Get<HeroesContainer>().list.Count;

            /*while (hero.TryGet(out Inactive inactive) && count > 0)
            {
                --count;
                if (inactive.Value)
                {
                    if (inactive.Duration > 0)
                    {
                        --inactive.Duration;
                        hero = repositoryService.GetHero();
                    }
                    else
                    {
                        inactive.Value = false;
                    }
                }
                else
                {
                    break;
                }
            }*/

            if (hero.TryGet(out Inactive inactive))
            {
                if (inactive.Value)
                {
                    if (inactive.Duration > 0)
                    {
                        --inactive.Duration;
                    }
                    else
                    {
                        inactive.Value = false;
                    }

                    Debug.LogWarning("SKIP TURN");
                    eventBus.RaiseEvent(new TurnFinishedEvent(hero));
                    eventBus.RaiseEvent(new TurnSkippedEvent(hero));
                }
                else
                {
                    eventBus.RaiseEvent(new HeroSelectionEvent(hero));
                }
            }
            else
            {
                eventBus.RaiseEvent(new HeroSelectionEvent(hero));
            }

            /*if (count <= 0)
            {
                Debug.LogWarning("TURN FINISHED AS ALL HEROES FROZE");
                eventBus.RaiseEvent(new TurnFinishedEvent(hero));
            }*/
            /*else
            {
                eventBus.RaiseEvent(new HeroSelectionEvent(hero));
            }*/

            Finish();
        }
    }
}