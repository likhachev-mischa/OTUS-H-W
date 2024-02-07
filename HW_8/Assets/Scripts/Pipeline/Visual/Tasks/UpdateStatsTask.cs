using Entities;
using Entities.Components;
using UI;

namespace Pipeline
{
    public class UpdateStatsTask : Task
    {
        private IEntity hero;

        public UpdateStatsTask(IEntity hero)
        {
            this.hero = hero;
        }

        protected override void OnRun()
        {
            HeroView view= hero.Get<HeroVisual>().view;
            int health = hero.Get<Health>().Value;
            int damage = hero.Get<Damage>().Value;
            
            view.SetStats(damage + " / " + health);
            Finish();
        }
    }
}