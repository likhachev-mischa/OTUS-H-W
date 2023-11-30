using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public interface IGameListenerProvider
    {
        IEnumerable<IGameListener> ProvideListeners();
    }
    
    public interface IServiceProvider
    {
        IEnumerable<(Type,object)> ProvideServices();
    }
    
    public interface IInjectProvider
    {
        void Inject(ServiceLocator serviceLocator);
    }
}