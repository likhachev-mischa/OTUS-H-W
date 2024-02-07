using DI;
using Events;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Handlers.Turn
{
    public class TeamSwitchHandler : BaseHandler<TurnFinishedEvent>
    {
        private TeamService teamService;
        
        [Inject]
        private void Construct(EventBus eventBus,TeamService teamService)
        {
            base.Construct(eventBus);
            this.teamService = teamService;
        }
        
        protected override void HandleEvent(TurnFinishedEvent evt)
        {
            teamService.ChangeTeam();
            Debug.Log("TEAM CHANGED");
        }
    }
}