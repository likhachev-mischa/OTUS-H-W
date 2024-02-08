using System.Collections.Generic;
using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using Services;

namespace Handlers.Turn
{
    public class MultiTargetEffectHandler : BaseHandler<MultiTargetEffect>
    {
        private EffectStack effectStack;
        private TeamService teamService;

        [Inject]
        private void Construct(EventBus eventBus, EffectStack effectStack, TeamService teamService)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
            this.teamService = teamService;
        }

        protected override void HandleEvent(MultiTargetEffect evt)
        {
            var source = (Entity)evt.Source;
            Entity[] teams = teamService.GetAllTeams();
            IEntity cachedTarget = evt.Target.entity;
            var target = source.Get<Target>();
            effectStack.Push(new ChangeTargetEffect() { Source = source, Target = target, NewTarget = cachedTarget });

            bool initiateDefence = false;
            foreach (IEffect effect in evt.Effects)
            {
                if (effect is DealDamageEffect)
                {
                    initiateDefence = true;
                    break;
                }
            }

            foreach (Entity team in teams)
            {
                List<Entity> heroes = team.Get<HeroesContainer>().list;

                foreach (Entity hero in heroes)
                {
                    if (hero == source)
                    {
                        continue;
                    }

                    IEntity enemy = hero;
                    if (evt.Effects != null)
                    {
                        ExtractDefenceEffectsToStack(enemy);

                        for (var i = evt.Effects.Length - 1; i >= 0; i--)
                        {
                            IEffect effect = evt.Effects[i];
                            effect.Source = source;
                            effect.Target = target;
                            effectStack.Push(effect);
                        }

                        effectStack.Push(new ChangeTargetEffect()
                            { Source = source, Target = target, NewTarget = enemy });
                    }
                }
            }

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }

            void ExtractDefenceEffectsToStack(IEntity enemy)
            {
                if (initiateDefence && enemy.TryGet(out ArmorComponent armorComponent))
                {
                    if (armorComponent.Effects != null)
                    {
                        for (var i = armorComponent.Effects.Length - 1; i >= 0; i--)
                        {
                            IEffect effect = armorComponent.Effects[i];

                            effect.Source = enemy;
                            effect.Target = target.entity.Get<Target>();

                            if (effect is DealDamageEffect)
                            {
                                continue;
                            }

                            effectStack.Push(effect);
                        }
                    }
                }
            }
        }
    }
}