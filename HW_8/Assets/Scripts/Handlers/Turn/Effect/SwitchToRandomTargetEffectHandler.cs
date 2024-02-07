using System.Collections.Generic;
using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Handlers.Turn
{
    public class SwitchToRandomTargetEffectHandler :BaseHandler<SwitchToRandomTargetEffect>
    {
        private TeamService teamService;
        
        [Inject]
        public void Construct(EventBus eventBus, TeamService teamService)
        {
            base.Construct(eventBus);
            this.teamService = teamService;
        }

        protected override void HandleEvent(SwitchToRandomTargetEffect evt)
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
            foreach (IEffect evtSuccessEffect in evt.SuccessEffects)
            {
                evtSuccessEffect.Source = evt.Source;
                evtSuccessEffect.Target = evt.Target;
                
                EventBus.RaiseEvent(evtSuccessEffect);
            }
            
            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has switched his target to {evt.Target.entity.Get<Name>().Value}");
        }
    }
   
}