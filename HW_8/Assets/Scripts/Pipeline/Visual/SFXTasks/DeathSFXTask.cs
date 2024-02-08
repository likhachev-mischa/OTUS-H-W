using Entities;
using Entities.Components;
using SFX;

namespace Pipeline.SFXTasks
{
    public class DeathSFXTask: Task
    {
        private readonly HeroSFX sfx;
        public DeathSFXTask(IEntity entity)
        {
            sfx = entity.Get<HeroSFXComponent>().sfx;
        }
        protected override void OnRun()
        {
            sfx.Death();
            Finish();
        }
    }
}