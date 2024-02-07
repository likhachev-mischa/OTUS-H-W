using DI;
using Entities.Components;
using Events;
using Game.EventBus;
using Services;
using UnityEngine;

namespace Handlers
{
    public class DeathHandler : BaseHandler<DeathEvent>
    {
        private HeroRepositoryService heroRepositoryService;
        [Inject]
        private void Construct(EventBus eventBus ,HeroRepositoryService heroRepositoryService)
        {
            base.Construct(eventBus);
            this.heroRepositoryService = heroRepositoryService; 
        }
        
        protected override void HandleEvent(DeathEvent evt)
        {
            Debug.LogWarning($"{evt.Target.entity.Get<Name>().Value} was killed by {evt.Source.Get<Name>().Value}");
            heroRepositoryService.RemoveHero(evt.Target.entity);
        }
    }
}