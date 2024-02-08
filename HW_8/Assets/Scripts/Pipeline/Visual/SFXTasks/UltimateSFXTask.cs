using Entities;
using Entities.Components;
using SFX;

namespace Pipeline.SFXTasks
{
    public class UltimateSFXTask : Task
    {
        private readonly HeroSFX sfx;
        public UltimateSFXTask(IEntity entity)
        {
            sfx = entity.Get<HeroSFXComponent>().sfx;
        }
        
        protected override void OnRun()
        {
            sfx.Ability();
            Finish();
        }
    }
}