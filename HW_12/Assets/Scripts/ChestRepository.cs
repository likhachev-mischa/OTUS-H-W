using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class ChestRepository : IDisposable
    {
        [SerializeField] private Chest[] chests;

        private CurrencyRepository currencyRepository;
        private TimerSynchronizer timerSynchronizer;

        public ChestRepository(ChestConfig[] chestConfigs, CurrencyRepository currencyRepository,
            TimerSynchronizer timerSynchronizer)
        {
            this.currencyRepository = currencyRepository;
            this.timerSynchronizer = timerSynchronizer;

            chests = new Chest[chestConfigs.Length];
            for (var i = 0; i < chests.Length; i++)
            {
                chests[i] = new Chest(chestConfigs[i]);

                chests[i].RewardClaimed += currencyRepository.AddToCurrency;
                this.timerSynchronizer.RegisterTimer(chests[i]);
            }
        }


        public void Dispose()
        {
            for (var i = 0; i < chests.Length; i++)
            {
                chests[i].RewardClaimed -= currencyRepository.AddToCurrency;
                timerSynchronizer.UnregisterTimer(chests[i]);
                chests[i].Dispose();
            }
        }
    }
}