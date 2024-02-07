using System;
using DI;
using Game.EventBus;

namespace Handlers
{
    public abstract class BaseHandler<T> : IInitializable, IDisposable
    {
        protected EventBus EventBus;

        [Inject]
        protected void Construct(EventBus eventBus)
        {
            EventBus = eventBus;
        }
        
        void IInitializable.Initialize()
        {
            EventBus.Subscribe<T>(HandleEvent);
        }

        void IDisposable.Dispose()
        {
            EventBus.Unsubscribe<T>(HandleEvent);
        }

        protected abstract void HandleEvent(T evt);
    }
}