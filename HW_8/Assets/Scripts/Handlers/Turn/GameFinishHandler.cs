using System.Collections.Generic;
using DI;
using Entities;
using Entities.Components;
using Events;
using Game.EventBus;
using Pipeline;
using Pipeline.Tasks;
using Services;

namespace Handlers
{
    public class GameFinishHandler : BaseHandler<DeathEvent>
    {
        private TurnPipeline turnPipeline;
        private TeamService teamService;
        private IObjectResolver objectResolver;

        [Inject]
        private void Construct(EventBus eventBus, TurnPipeline turnPipeline, TeamService teamService,
           IObjectResolver objectResolver)
        {
            base.Construct(eventBus);
            this.turnPipeline = turnPipeline;
            this.teamService = teamService;
            this.objectResolver = objectResolver;
        }

        protected override void HandleEvent(DeathEvent evt)
        {
            Entity[] teams = teamService.GetAllTeams();

            for (var i = 0; i < teams.Length; i++)
            {
                List<Entity> list = teams[i].Get<HeroesContainer>().list;
                if (list.Count == 0)
                {
                    turnPipeline.AddTask(objectResolver.CreateInstance<FinishGameTask>());
                    return;
                }
            }
        }
    }
}