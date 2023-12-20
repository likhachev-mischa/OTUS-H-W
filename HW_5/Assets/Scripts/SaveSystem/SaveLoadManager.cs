using DI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaveSystem
{
    //TODO вернуть один контекст и сервис локатор и продумать другое решение
    public class SaveLoadManager : MonoBehaviour
    {
        private GameRepository gameRepository;
        private ServiceLocator serviceLocator;
        private IGameSaver[] gameSavers;

        [Inject]
        private void Construct(GameRepository gameRepository, IGameSaver[] gameSavers,ServiceLocator serviceLocator)
        {
            this.gameRepository = gameRepository;
            this.gameSavers = gameSavers;
        }
        
        [Button]
        public void Save()
        {
            for (var i = 0; i < gameSavers.Length; i++)
            {
                IGameSaver gameSaver = gameSavers[i];
                    //gameSaver.SaveData(gameRepository, serviceLocator);
            }

            gameRepository.SetState();
        }

        [Button]
        public void Load()
        {
            gameRepository.GetState();

            for (var i = 0; i < gameSavers.Length; i++)
            {
                IGameSaver gameSaver = gameSavers[i];
                //gameSaver.LoadData(gameRepository, serviceLocator);
            }
        }
    }
}