using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.EventBus
{
    public sealed class EventBus
    {
        private readonly Dictionary<Type, IEventHandlerCollection> _handlers = new();

        private readonly Queue<IEvent> _queue = new();

        private bool _isRunning;
        
        public void Subscribe<T>(Action<T> handler)
        {
            Type eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
            {
                _handlers.Add(eventType, new EventHandlerCollection<T>());
            }
            
            _handlers[eventType].Subscribe(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            Type eventType = typeof(T);

            if (_handlers.TryGetValue(eventType, out IEventHandlerCollection handlers))
            {
                handlers.Unsubscribe(handler);
            }
        }

        public void RaiseEvent<T>(T evt) where T : IEvent
        {
            if (_isRunning)
            {
                _queue.Enqueue(evt);
                return;
            }
            
            _isRunning = true;
            
            Type eventType = evt.GetType();
            Debug.Log(eventType);

            if (!_handlers.TryGetValue(eventType, out var handlers))
            {
                Debug.Log($"No subscribers found in: {eventType}");
                _isRunning = false;
                return;
            }

            handlers.RaiseEvent(evt);

            _isRunning = false;

            if (_queue.TryDequeue(out var result))
            {
                RaiseEvent(result);
            }
        }
        
        private interface IEventHandlerCollection
        {
            public void Subscribe(Delegate handler);
            
            public void Unsubscribe(Delegate handler);
            
            public void RaiseEvent<T>(T evt);
        }

        private sealed class EventHandlerCollection<T> : IEventHandlerCollection
        {
            private readonly List<Delegate> _handlers = new();

            private int _currentIndex = -1;

            public void Subscribe(Delegate handler)
            {
                _handlers.Add(handler);
            }

            public void Unsubscribe(Delegate handler)
            {
                int index = _handlers.IndexOf(handler);
                _handlers.RemoveAt(index);

                if (index <= _currentIndex)
                {
                    _currentIndex--;
                }
            }

            public void RaiseEvent<TEvent>(TEvent evt)
            {
                if (evt is not T concreteEvent)
                {
                    return;
                }
                
                for (_currentIndex = 0; _currentIndex < _handlers.Count; _currentIndex++)
                {
                    Action<T> handler = (Action<T>)_handlers[_currentIndex];
                    handler.Invoke(concreteEvent);
                }

                _currentIndex = -1;
            }
        }
    }
}