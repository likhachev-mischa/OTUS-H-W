using System.Collections.Generic;
using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Events.Requests;
using Game.EventBus;
using Pipeline;
using Services;

namespace Handlers.Turn
{
    public class EndOfTurnEffectsHandler : BaseHandler<ActionFinishedRequest>
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

        protected override void HandleEvent(ActionFinishedRequest evt)
        {
            effectStack.Clear();

            Entity team = teamService.GetCurrentTeam();

            List<Entity> heroes = team.Get<HeroesContainer>().list;

            effectStack.Push(new TurnFinishedRequest() { Source = evt.Source, Target = evt.Target });

            foreach (Entity entity in heroes)
            {
                if (entity.TryGet(out EndOfTurnEffectsComponent component))
                {
                    if (component.Effects != null)
                    {
                        for (var i = component.Effects.Length - 1; i >= 0; i--)
                        {
                            IEffect effect = component.Effects[i];
                            effect.Source = entity;
                            effect.Target = entity.Get<Target>();
                            effectStack.Push(effect);
                        }
                    }
                }
            }

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}