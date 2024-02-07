using Entities;
using Entities.Components;

namespace Pipeline
{
    public class DeathVisualTask : Task
    {
        private readonly HeroVisual heroVisual;
        public DeathVisualTask(IEntity entity)
        {
            heroVisual = entity.Get<HeroVisual>();
        }
        
        protected override void OnRun()
        {
            heroVisual.view.gameObject.SetActive(false);
            Finish();
        }
    }
}