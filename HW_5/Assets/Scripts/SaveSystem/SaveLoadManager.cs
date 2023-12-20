using DI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        private GameRepository gameRepository;
        private IGameSaver[] gameSavers;
        private Context gameContext;

        [Inject]
        private void Construct(GameRepository gameRepository, IGameSaver[] gameSavers,Context gameContext)
        {
            this.gameRepository = gameRepository;
            this.gameSavers = gameSavers;
            this.gameContext = gameContext;
        }
        
        [Button]
        public void Save()
        {
            for (var i = 0; i < gameSavers.Length; i++)
            {
                IGameSaver gameSaver = gameSavers[i];
                gameSaver.SaveData(gameRepository, gameContext);
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
                gameSaver.LoadData(gameRepository, gameContext);
            }
        }
    }
}