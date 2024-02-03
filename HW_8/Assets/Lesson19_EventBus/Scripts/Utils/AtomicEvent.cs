using System;
using System.Collections.Generic;

namespace Lessons.Utils
{
    public sealed class AtomicEvent
    {
        private readonly List<Action> _actions = new();
        private int _currentIndex;
        
        public static AtomicEvent operator+(AtomicEvent atomicEvent, Action action)
        {
            atomicEvent._actions.Add(action);
            return atomicEvent;
        }

        public static AtomicEvent operator-(AtomicEvent atomicEvent, Action action)
        {
            var index = atomicEvent._actions.IndexOf(action);
            atomicEvent._actions.RemoveAt(index);

            if (index <= atomicEvent._currentIndex)
            {
                --atomicEvent._currentIndex;    
            }
            
            return atomicEvent;
        }

        public void Invoke()
        {
            for (_currentIndex = 0; _currentIndex < _actions.Count; ++_currentIndex)
            {
                var action = _actions[_currentIndex];
                action.Invoke();
            }

            _currentIndex = -1;
        }
    }

    public sealed class AtomicEvent<T>
    {
        private readonly List<Action<T>> _actions = new();
        private int _currentIndex;
        
        public static AtomicEvent<T> operator+(AtomicEvent<T> atomicEvent, Action<T> action)
        {
            atomicEvent._actions.Add(action);
            return atomicEvent;
        }

        public static AtomicEvent<T> operator-(AtomicEvent<T> atomicEvent, Action<T> action)
        {
            var index = atomicEvent._actions.IndexOf(action);
            atomicEvent._actions.RemoveAt(index);

            if (index <= atomicEvent._currentIndex)
            {
                --atomicEvent._currentIndex;    
            }
            
            return atomicEvent;
        }

        public void Invoke(T arg)
        {
            for (_currentIndex = 0; _currentIndex < _actions.Count; ++_currentIndex)
            {
                var action = _actions[_currentIndex];
                action.Invoke(arg);
            }
        }
    }
}