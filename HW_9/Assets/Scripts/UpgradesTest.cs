using System;
using System.Collections.Generic;
using Game.Gameplay.Player;
using Sample;
using UnityEngine;

namespace DefaultNamespace
{
    public class UpgradesTest : MonoBehaviour
    {
        [SerializeField] private UpgradesManager upgradesManager = new();
        [SerializeField] private MoneyStorage moneyStorage = new();
        [SerializeField] private UpgradeCatalog upgradeCatalog;
        
        private readonly DependentUpgradesController dependentUpgradesController = new();

        private void OnEnable()
        {
            upgradesManager.Construct(moneyStorage);
            UpgradeConfig[] configs = upgradeCatalog.GetAllUpgrades();
            var upgrades = new Upgrade[configs.Length];

            for (var i = 0; i < configs.Length; i++)
            {
                upgrades[i] = configs[i].InstantiateUpgrade();
            }

            upgradesManager.Setup(upgrades);

            dependentUpgradesController.Construct(upgradesManager);
            dependentUpgradesController.Initialize();
        }

        private void OnDestroy()
        {
            dependentUpgradesController.Dispose();
        }
    }
}