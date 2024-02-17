using System;
using System.Collections.Generic;

namespace Sample
{
    public class DependentUpgradesController : IDisposable
    {
        private UpgradesManager upgradesManager;

        private readonly Dictionary<string, Dictionary<string, int>> dictionary = new();

        public void Construct(UpgradesManager upgradesManager)
        {
            this.upgradesManager = upgradesManager;
            this.upgradesManager.OnLevelUp += OnLevelUp;
        }

        public void Initialize()
        {
            Upgrade[] upgrades = upgradesManager.GetAllUpgrades();
            for (var index = 0; index < upgrades.Length; index++)
            {
                Upgrade upgrade = upgrades[index];
                if (upgrade is DependentUpgrade dependentUpgrade)
                {
                    dictionary[dependentUpgrade.Id] = dependentUpgrade.Dependencies;
                }
            }
            
            
            foreach ((string key, Dictionary<string, int> value) in dictionary)
            {
                var dependentUpgrade = (DependentUpgrade)upgradesManager.GetUpgrade(key);
                dependentUpgrade.AllDependenciesReady = RecalculateDependencies(dependentUpgrade);
            }
        }

        private void OnLevelUp(Upgrade upgrade)
        {
            string id = upgrade.Id;

            foreach ((string key, Dictionary<string, int> value) in dictionary)
            {
                if (value.ContainsKey(id))
                {
                    var dependentUpgrade = (DependentUpgrade)upgradesManager.GetUpgrade(key);
                    dependentUpgrade.AllDependenciesReady = RecalculateDependencies(dependentUpgrade);
                    break;
                }
            }

            if (upgrade is DependentUpgrade depUpgrade)
            {
                depUpgrade.AllDependenciesReady = RecalculateDependencies(depUpgrade);
            }
        }

        private bool RecalculateDependencies(DependentUpgrade upgrade)
        {
            Dictionary<string, int> dependencies = dictionary[upgrade.Id];

            foreach ((string id, int delta) in dependencies)
            {
                int level = upgradesManager.GetUpgrade(id).Level;

                if (level + delta < upgrade.Level)
                {
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
            upgradesManager.OnLevelUp -= OnLevelUp;
        }
    }
}