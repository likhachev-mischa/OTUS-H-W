using System.Collections.Generic;
using DI;
using Entities;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Handlers.Turn
{
    public class SwitchToRandomTargetHandler :BaseHandler<SwitchToRandomTargetEvent>
    {
        private TeamService teamService;
        
        [Inject]
        public void Construct(EventBus eventBus, TeamService teamService)
        {
            base.Construct(eventBus);
            this.teamService = teamService;
        }

        protected override void HandleEvent(SwitchToRandomTargetEvent evt)
        {
            float probability = evt.Probability;
            if (!(Random.value <= probability))
            {
                return;
            }

            Entity enemyTeam = teamService.GetEnemyTeam();
            List<Entity> heroes = enemyTeam.Get<HeroesContainer>().list;

            int index = Random.Range(0, heroes.Count);
            Entity newEnemy = heroes[index];

            evt.Target.entity = newEnemy;
            
            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has switched his target to {evt.Target.entity.Get<Name>().Value}");
        }
    }
   
}