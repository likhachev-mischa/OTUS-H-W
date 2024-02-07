namespace Entities
{
    public interface IEntity
    {
        void Add<T>(T component);
        bool TryGet<T>(out T component);
        T Get<T>();
    }
}