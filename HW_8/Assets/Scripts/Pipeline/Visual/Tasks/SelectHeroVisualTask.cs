using Entities;
using Entities.Components;

namespace Pipeline
{
    public sealed class SelectHeroVisualTask : Task
    {
        private readonly HeroVisual heroVisual;
        private bool value;
        public SelectHeroVisualTask(IEntity entity, bool value)
        {
            heroVisual = entity.Get<HeroVisual>();
            this.value = value;
        }
        
        protected override void OnRun()
        {
            heroVisual.view.SetActive(value);
            Finish();
        }
    }
}