namespace Atomic.Behaviours
{
    public interface IAtomicBehaviour
    {
        void AddLogic(ILogic target);

        void RemoveLogic(ILogic target);

        ILogic[] LogicElements();
    }
}