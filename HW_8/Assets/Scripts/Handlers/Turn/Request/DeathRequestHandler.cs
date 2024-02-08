using DI;
using Entities.Components;
using Events;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Handlers
{
    public class DeathRequestHandler : BaseHandler<DeathRequest>
    {
        private HeroRepositoryService heroRepositoryService;

        [Inject]
        private void Construct(EventBus eventBus, HeroRepositoryService heroRepositoryService)
        {
            base.Construct(eventBus);
            this.heroRepositoryService = heroRepositoryService;
        }

        protected override void HandleEvent(DeathRequest evt)
        {
            Debug.LogWarning($"{evt.Target.entity.Get<Name>().Value} was killed by {evt.Source.Get<Name>().Value}");
            heroRepositoryService.RemoveHero(evt.Target.entity);
            EventBus.RaiseEvent(new DeathEvent(evt.Source,evt.Target));
        }
    }
}