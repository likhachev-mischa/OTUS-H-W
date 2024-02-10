using System;
using System.Collections.Generic;

namespace Sample
{
    public class DependentUpgradesController : IDisposable
    {
        private UpgradesManager upgradesManager;

        private readonly Dictionary<string, string[]> dictionary = new();

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
                    dictionary[dependentUpgrade.Id] = dependentUpgrade.DependentIds;
                }
            }
        }

        private void OnLevelUp(Upgrade upgrade)
        {
            string id = upgrade.Id;

            foreach ((string key, string[] value) in dictionary)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (string.Equals(id, value[i]))
                    {
                        var dependentUpgrade = (DependentUpgrade)upgradesManager.GetUpgrade(key);
                        dependentUpgrade.AllDependenciesReady = RecalculateDependencies(dependentUpgrade);
                        break;
                    }
                }
            }

            if (upgrade is DependentUpgrade depUpgrade)
            {
                depUpgrade.AllDependenciesReady = RecalculateDependencies(depUpgrade);
            }
        }

        private bool RecalculateDependencies(DependentUpgrade upgrade)
        {
            string[] dependencies = dictionary[upgrade.Id];
            for (var i = 0; i < dependencies.Length; i++)
            {
                int level = upgradesManager.GetUpgrade(dependencies[i]).Level;

                if (level < upgrade.Level)
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