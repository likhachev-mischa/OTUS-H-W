using Sirenix.OdinInspector;

namespace Sample
{
    public class DependentUpgrade : Upgrade
    {
        public string[] DependentIds { get; private set; }

        public bool AllDependenciesReady;
        
        public DependentUpgrade(DependentUpgradeConfig config) : base(config)
        {
            DependentIds = config.dependantIds;
            AllDependenciesReady = true;
        }
        
        public override bool CanLevelUp()
        {
            return base.CanLevelUp() && AllDependenciesReady;
        }

        protected override void LevelUp(int level)
        {
            
        }
    }
}