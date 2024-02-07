using DI;
using Entities;
using Events;
using Game.EventBus;
using Services;

namespace Pipeline.Tasks
{
    public class EnemySelectionTask : Task
    {
        private HeroSelectionService selectionService;
        private EventBus eventBus;

        [Inject]
        public void Construct(HeroSelectionService selectionService, EventBus eventBus)
        {
            this.selectionService = selectionService;
            this.eventBus = eventBus;
        }

        protected override void OnRun()
        {
            selectionService.OnEntitySelected += OnEnemySelected;
        }

        protected override void OnFinish()
        {
            selectionService.OnEntitySelected -= OnEnemySelected;
        }

        private void OnEnemySelected(IEntity enemy)
        {
           eventBus.RaiseEvent(new EnemySelectionEvent(enemy));
           Finish();
        }
    }
}