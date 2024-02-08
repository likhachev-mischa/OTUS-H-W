using System.Collections.Generic;
using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using Services;
using UnityEngine;

namespace Handlers.Turn
{
    public class SwitchToRandomTargetEffectHandler : BaseHandler<SwitchToRandomTargetEffect>
    {
        private TeamService teamService;
        private EffectStack effectStack;

        [Inject]
        public void Construct(EventBus eventBus, TeamService teamService, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.teamService = teamService;
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(SwitchToRandomTargetEffect evt)
        {
            float probability = evt.Probability;
            if (!(Random.value <= probability))
            {
                if (!effectStack.IsEmpty())
                {
                    EventBus.RaiseEvent(effectStack.Pop());
                }

                return;
            }

            Entity team;
            if (!evt.IsAimedOnAllies)
            {
                team = teamService.GetEnemyTeam();
            }
            else
            {
                team = teamService.GetCurrentTeam();
            }

            List<Entity> heroes = team.Get<HeroesContainer>().list;

            int index = Random.Range(0, heroes.Count);
            Entity newEnemy = heroes[index];

            evt.Target.entity = newEnemy;

            if (evt.SuccessEffects != null)
            {
                for (var i = evt.SuccessEffects.Length - 1; i >= 0; i--)
                {
                    IEffect effect = evt.SuccessEffects[i];
                    effect.Source = evt.Source;
                    effect.Target = evt.Target;
                    effectStack.Push(effect);
                }
            }

            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} has switched his target to {evt.Target.entity.Get<Name>().Value}");

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}