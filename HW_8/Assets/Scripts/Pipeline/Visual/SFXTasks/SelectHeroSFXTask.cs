using Entities;
using Entities.Components;
using SFX;

namespace Pipeline.SFXTasks
{
    public class SelectHeroSFXTask: Task
    {
        private readonly HeroSFX sfx;
        public SelectHeroSFXTask(IEntity entity)
        {
            sfx = entity.Get<HeroSFXComponent>().sfx;
        }
        
        protected override void OnRun()
        {
            sfx.StartTurn();
            Finish();
        }
        
    }
}