namespace Game
{
    public interface IAtomicAction
    {
        void Invoke();
    }
    
    public interface IAtomicAction<in T>
    {
        void Invoke(T value);
    }
}