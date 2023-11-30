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


        public void AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }

            if (listener is IGameStartListener startListener)
            {
                this.startListeners.Add(startListener);
            }

            if (listener is IGameFinishListener finishListener)
            {
                this.finishListeners.Add(finishListener);
            }

            if (listener is IGamePauseListener pauseListener)
            {
                this.pauseListeners.Add(pauseListener);
            }

            if (listener is IGameResumeListener resumeListener)
            {
                this.resumeListeners.Add(resumeListener);
            }

            if (listener is IGameUpdateListener updateListener)
            {
                this.updateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }

            if (listener is IGameStartListener startListener)
            {
                this.startListeners.Remove(startListener);
            }

            if (listener is IGameFinishListener finishListener)
            {
                this.finishListeners.Remove(finishListener);
            }

            if (listener is IGamePauseListener pauseListener)
            {
                this.pauseListeners.Remove(pauseListener);
            }

            if (listener is IGameResumeListener resumeListener)
            {
                this.resumeListeners.Remove(resumeListener);
            }

            if (listener is IGameUpdateListener updateListener)
            {
                this.updateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.lateUpdateListeners.Remove(lateUpdateListener);
            }
        }
    }
}