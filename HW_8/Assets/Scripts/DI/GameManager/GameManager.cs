using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public enum GameState
    {
        OFF,
        LOADING,
        ON,
        PAUSED,
        FINISHED
    }

    public sealed class GameManager : MonoBehaviour
    {
        public GameState State { get; private set; }

        private readonly List<IInitializable> postConstructListeners = new();
        private readonly List<ILateLoadListener> lateLoadListeners = new();
        private readonly List<IGameStartListener> startListeners = new();
        private readonly List<IGameFinishListener> finishListeners = new();
        private readonly List<IGamePauseListener> pauseListeners = new();
        private readonly List<IGameResumeListener> resumeListeners = new();

        private readonly List<IGameUpdateListener> updateListeners = new();
        private readonly List<IGameFixedUpdateListener> fixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> lateUpdateListeners = new();

        private readonly List<IDisposable> cleanupList = new();

        private void Update()
        {
            if (State != GameState.ON)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            for (int i = 0, count = updateListeners.Count; i < count; ++i)
            {
                IGameUpdateListener listener = updateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (State != GameState.ON)
            {
                return;
            }

            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = fixedUpdateListeners.Count; i < count; ++i)
            {
                IGameFixedUpdateListener listener = fixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (State != GameState.ON)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            for (int i = 0, count = lateUpdateListeners.Count; i < count; ++i)
            {
                IGameLateUpdateListener listener = lateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }

        public void Initialize()
        {
            for (int i = 0, count = postConstructListeners.Count; i < count; i++)
            {
                postConstructListeners[i].Initialize();
            }

            State = GameState.LOADING;
        }

        public void LateLoad()
        {
            for (int i = 0, count = lateLoadListeners.Count; i < count; i++)
            {
                lateLoadListeners[i].OnLateLoad();
            }

            State = GameState.LOADING;
        }

        [ContextMenu("StartGame")]
        public void StartGame()
        {
            for (int i = 0, count = startListeners.Count; i < count; ++i)
            {
                startListeners[i].OnStart();
            }

            State = GameState.ON;
        }

        [ContextMenu("PauseGame")]
        public void PauseGame()
        {
            for (int i = 0, count = pauseListeners.Count; i < count; ++i)
            {
                pauseListeners[i].OnPause();
            }

            State = GameState.PAUSED;
        }

        [ContextMenu("ResumeGame")]
        public void ResumeGame()
        {
            for (int i = 0, count = resumeListeners.Count; i < count; ++i)
            {
                resumeListeners[i].OnResume();
            }

            State = GameState.ON;
        }

        [ContextMenu("FinishGame")]
        public void FinishGame()
        {
            for (int i = 0, count = finishListeners.Count; i < count; ++i)
            {
                finishListeners[i].OnFinish();
            }
            
            for (var i = 0; i < cleanupList.Count; i++)
            {
                cleanupList[i].Dispose();
            }

            State = GameState.FINISHED;
        }

        public void AddListeners(IEnumerable<IGameListener> listeners)
        {
            foreach (IGameListener listener in listeners)
            {
                AddListener(listener);
            }
        }

        public void RemoveListeners(IEnumerable<IGameListener> listeners)
        {
            foreach (IGameListener listener in listeners)
            {
                RemoveListener(listener);
            }
        }

        public void AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }

            AddListeners(listener as IInitializable, postConstructListeners);
            AddListeners(listener as ILateLoadListener, lateLoadListeners);
            AddListeners(listener as IGameStartListener, startListeners);
            AddListeners(listener as IGameFinishListener, finishListeners);
            AddListeners(listener as IGamePauseListener, pauseListeners);
            AddListeners(listener as IGameResumeListener, resumeListeners);
            AddListeners(listener as IGameUpdateListener, updateListeners);
            AddListeners(listener as IGameFixedUpdateListener, fixedUpdateListeners);
            AddListeners(listener as IGameLateUpdateListener, lateUpdateListeners);
            
            AddListeners(listener as IDisposable, cleanupList);
        }

        private void AddListeners<T>(T listener, List<T> listeners)
        {
            if (listener == null)
            {
                return;
            }

            listeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }

            RemoveListeners(listener as IInitializable, postConstructListeners);
            RemoveListeners(listener as ILateLoadListener, lateLoadListeners);
            RemoveListeners(listener as IGameStartListener, startListeners);
            RemoveListeners(listener as IGameFinishListener, finishListeners);
            RemoveListeners(listener as IGamePauseListener, pauseListeners);
            RemoveListeners(listener as IGameResumeListener, resumeListeners);
            RemoveListeners(listener as IGameUpdateListener, updateListeners);
            RemoveListeners(listener as IGameFixedUpdateListener, fixedUpdateListeners);
            RemoveListeners(listener as IGameLateUpdateListener, lateUpdateListeners);
            
            RemoveListeners(listener as IDisposable, cleanupList);
        }

        private void RemoveListeners<T>(T listener, List<T> listeners)
        {
            if (listener == null)
            {
                return;
            }

            listeners.Remove(listener);
        }

        private void OnDestroy()
        {
            for (var i = 0; i < cleanupList.Count; i++)
            {
                cleanupList[i].Dispose();
            }
        }
    }
}