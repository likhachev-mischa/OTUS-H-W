using DI;
using GameSavers;
using SaveSystem;

namespace Installers
{
    public class GameSaversInstaller : GameInstaller
    {
        [ServiceCollection(typeof(IGameSaver[]))]
        private UnitGameSaver unitGameSaver = new();

        [ServiceCollection(typeof(IGameSaver[]))]
        private ResourceGameSaver resourceGameSaver = new();
    }
}