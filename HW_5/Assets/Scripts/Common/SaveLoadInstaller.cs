using DI;
using SaveSystem;

namespace Common
{
    public class SaveLoadInstaller : GameInstaller
    {
        [Service(typeof(GameRepository))]
        private GameRepository gameRepository = new();
    }
}