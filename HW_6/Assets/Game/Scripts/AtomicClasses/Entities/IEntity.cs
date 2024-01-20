namespace Game
{
    public interface IEntity
    {
        void Add<T>(T component);

        T Get<T>();
        bool TryGet<T>(out T component);
    }
}