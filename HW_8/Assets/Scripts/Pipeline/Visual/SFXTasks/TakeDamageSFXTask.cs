using Entities;
using Entities.Components;
using SFX;

namespace Pipeline.SFXTasks
{
    public class TakeDamageSFXTask : Task
    {
        private readonly HeroSFX sfx;

        public TakeDamageSFXTask(IEntity entity)
        {
            sfx = entity.Get<HeroSFXComponent>().sfx;
        }

        protected override void OnRun()
        {
            sfx.LowHealth();
            Finish();
        }
    }
}