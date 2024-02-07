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

        [Inject]
        private void Construct(EventBus eventBus, TurnPipeline turnPipeline, TeamService teamService)
        {
            base.Construct(eventBus);
            this.turnPipeline = turnPipeline;
            this.teamService = teamService;
        }

        protected override void HandleEvent(DeathEvent evt)
        {
            Entity[] teams = teamService.GetAllTeams();

            for (var i = 0; i < teams.Length; i++)
            {
                var list = teams[i].Get<HeroesContainer>().list;
                if (list.Count == 0)
                {
                    turnPipeline.AddTask(new FinishGameTask(turnPipeline));
                    return;
                }
            }
        }
    }
}