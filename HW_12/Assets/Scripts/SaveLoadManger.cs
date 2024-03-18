using System;
using Sirenix.OdinInspector;

namespace Game
{
    [Serializable]
    public sealed class SaveLoadManger
    {
        private ISaveLoader[] saveLoaders;
        public event Action GameQuit;
        public event Action GameLoad;

        public SaveLoadManger(ISaveLoader[] saveLoaders)
        {
            this.saveLoaders = saveLoaders;
        }

        [Button]
        public void QuitGame()
        {
            for (var i = 0; i < saveLoaders.Length; i++)
            {
                saveLoaders[i].OnSaveGame();
            }
            GameQuit?.Invoke();
        }

        [Button]
        public void LoadGame()
        {
            for (var i = 0; i < saveLoaders.Length; i++)
            {
                saveLoaders[i].OnLoadGame();
            }
            GameLoad?.Invoke();
        }
    }
}