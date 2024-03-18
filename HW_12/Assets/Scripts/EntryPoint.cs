using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private CurrencyRepository currencyRepository;
        [SerializeField] private ChestConfig[] chestConfigs;
        [SerializeField] private string[] currencies;

        [SerializeField] private ChestRepository chestRepository;
        [SerializeField] private SaveLoadManger saveLoadManger;

        private TimerSynchronizer timerSynchronizer = new();

        private bool isGameRunning;

        private void OnEnable()
        {
            currencyRepository = new CurrencyRepository(currencies);
            chestRepository = new ChestRepository(chestConfigs, currencyRepository, timerSynchronizer);
            saveLoadManger = new SaveLoadManger(new ISaveLoader[] { timerSynchronizer, currencyRepository });

            saveLoadManger.GameQuit += OnGameQuit;
            saveLoadManger.GameLoad += OnGameLoad;

            StartGame();
        }

        private void OnGameLoad()
        {
            isGameRunning = true;
        }

        private void OnGameQuit()
        {
            isGameRunning = false;
        }

        private void Update()
        {
            if (!isGameRunning)
            {
                return;
            }

            timerSynchronizer.UpdateTimers(Time.deltaTime);
        }

        [Button]
        public void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        private void StartGame()
        {
            saveLoadManger.LoadGame();
        }

        private void OnApplicationQuit()
        {
            saveLoadManger.QuitGame();
        }

        private void OnDestroy()
        {
            saveLoadManger.GameQuit -= OnGameQuit;
            saveLoadManger.GameLoad -= OnGameLoad;

            chestRepository.Dispose();
        }
    }
}