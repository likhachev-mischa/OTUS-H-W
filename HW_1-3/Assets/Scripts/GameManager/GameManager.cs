using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public enum GameState
    {
        OFF,
        ON,
        PAUSED,
        FINISHED
    }

    public sealed class GameManager : MonoBehaviour
    {
        public GameState State { get; private set; }

        private readonly List<IGameStartListener> startListeners = new();
        private readonly List<IGameFinishListener> finishListeners = new();
        private readonly List<IGamePauseListener> pauseListeners = new();
        private readonly List<IGameResumeListener> resumeListeners = new();

        private readonly List<IGameUpdateListener> updateListeners = new();
        private readonly List<IGameFixedUpdateListener> fixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> lateUpdateListeners = new();

        private void Update()
        {
            if (this.State != GameState.ON)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.updateListeners.Count; i < count; ++i)
            {
                var listener = this.updateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.State != GameState.ON)
            {
                return;
            }

            var deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = this.fixedUpdateListeners.Count; i < count; ++i)
            {
                var listener = this.fixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.State != GameState.ON)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.lateUpdateListeners.Count; i < count; ++i)
            {
                var listener = this.lateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }

        [ContextMenu("StartGame")]
        public void StartGame()
        {
            for (int i = 0, count = this.startListeners.Count; i < count; ++i)
            {
                this.startListeners[i].OnStart();
            }

            this.State = GameState.ON;
        }

        [ContextMenu("PauseGame")]
        public void PauseGame()
        {
            for (int i = 0, count = this.pauseListeners.Count; i < count; ++i)
            {
                this.pauseListeners[i].OnPause();
            }

            this.State = GameState.PAUSED;
        }

        [ContextMenu("ResumeGame")]
        public void ResumeGame()
        {
            for (int i = 0, count = this.resumeListeners.Count; i < count; ++i)
            {
                this.resumeListeners[i].OnResume();
            }

            this.State = GameState.ON;
        }

        [ContextMenu("FinishGame")]
        public void FinishGame()
        {
            for (int i = 0, count = this.finishListeners.Count; i < count; ++i)
            {
                this.finishListeners[i].OnFinish();
            }

            this.State = GameState.FINISHED;
        }

        public void AddListeners(IEnumerable<IGameListener> listeners)
        {
            foreach (var listener in listeners)
            {
                this.AddListener(listener);
            }
        }

        public void AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }

            this.AddListeners(listener as IGameStartListener, startListeners);
            this.AddListeners(listener as IGameFinishListener, finishListeners);
            this.AddListeners(listener as IGamePauseListener, pauseListeners);
            this.AddListeners(listener as IGameResumeListener, resumeListeners);
            this.AddListeners(listener as IGameUpdateListener, updateListeners);
            this.AddListeners(listener as IGameFixedUpdateListener, fixedUpdateListeners);
            this.AddListeners(listener as IGameLateUpdateListener, lateUpdateListeners);
        }

        private void AddListeners<T>(T listener, List<T> listeners) where T : IGameListener
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

            this.RemoveListeners(listener as IGameStartListener, startListeners);
            this.RemoveListeners(listener as IGameFinishListener, finishListeners);
            this.RemoveListeners(listener as IGamePauseListener, pauseListeners);
            this.RemoveListeners(listener as IGameResumeListener, resumeListeners);
            this.RemoveListeners(listener as IGameUpdateListener, updateListeners);
            this.RemoveListeners(listener as IGameFixedUpdateListener, fixedUpdateListeners);
            this.RemoveListeners(listener as IGameLateUpdateListener, lateUpdateListeners);
        }

        private void RemoveListeners<T>(T listener, List<T> listeners) where T : IGameListener
        {
            if (listener == null)
            {
                return;
            }

            listeners.Remove(listener);
        }
    }
}