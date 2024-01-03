using Common.GameSavers;
using DI;
using SaveSystem;

namespace Common
{
    public class GameSaversInstaller : GameInstaller
    {
        [ServiceCollection(typeof(IGameSaver[]))]
        private UnitGameSaver unitGameSaver = new();
    }
}