namespace DI
{
    public interface IGameListener
    {
    }

    public interface IGameStartListener : IGameListener
    {
        void OnStart();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinish();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}