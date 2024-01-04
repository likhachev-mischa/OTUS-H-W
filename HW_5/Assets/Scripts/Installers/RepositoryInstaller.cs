using DI;
using SaveSystem;

namespace Installers
{
    public class RepositoryInstaller : GameInstaller
    {
        [Service(typeof(GameRepository))] private GameRepository gameRepository = new();
    }
}