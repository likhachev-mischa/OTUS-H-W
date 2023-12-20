
using DI;

namespace SaveSystem
{
    public interface IGameSaver
    {
        public void SaveData(GameRepository gameRepository, Context currentContext);
        public void LoadData(GameRepository gameRepository, Context currentContext);
    }
}