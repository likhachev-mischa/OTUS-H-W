using System.Collections.Generic;

namespace Sample
{
    public class DependentUpgrade : Upgrade
    {
        public Dictionary<string, int> Dependencies { get; private set; }
        public bool AllDependenciesReady { get; set; }


        public DependentUpgrade(DependentUpgradeConfig config) : base(config)
        {
            Dependencies = new Dictionary<string, int>(config.dependencies);
            AllDependenciesReady = true;
        }

        public override bool CanLevelUp()
        {
            return base.CanLevelUp() && AllDependenciesReady;
        }
    }
}