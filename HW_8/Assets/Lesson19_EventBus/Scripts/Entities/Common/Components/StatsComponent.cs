using Lessons.Entities.Common.Model;

namespace Lessons.Entities.Common.Components
{
    public sealed class StatsComponent
    {
        public int Strength => _stats.strength;
        
        private readonly Stats _stats;
        
        public StatsComponent(Stats stats)
        {
            _stats = stats;
        }
    }
}