using DI;
using Newtonsoft.Json;

namespace SaveSystem
{
    public abstract class GameSaver<TData, TService> : IGameSaver where TService : class
    {
        public void SaveData(GameRepository gameRepository, Context currentContext)
        {
            var service = currentContext.GetService<TService>();
            TData data = ConvertToData(service);
            gameRepository.SetData(data);
        }

        public void LoadData(GameRepository gameRepository, Context currentContext)
        {
            var service = currentContext.GetService<TService>();
            if (gameRepository.TryGetData(out TData data))
            {
                SetupData(data, service);
            }
            else
            {
                SetupDefaultData(service);
            }
        }

        protected abstract TData ConvertToData(TService service);
        protected abstract void SetupData(TData data, TService service);

        protected virtual void SetupDefaultData(TService service)
        {
        }
    }
}