using System;
using System.Collections.Generic;

namespace DI
{
    public interface IGameListenerProvider
    {
        IEnumerable<IGameListener> ProvideListeners();
    }

    public interface IServiceProvider
    {
        IEnumerable<(Type, object)> ProvideServices();

        IEnumerable<Type> ProvideServiceCollection();
    }

    public interface IInjectProvider
    {
        void Inject(ServiceLocator serviceLocator);
    }

    public interface IGameInstallerProvider
    {
        IEnumerable<GameInstaller> ProvideInstallers();
    }
}