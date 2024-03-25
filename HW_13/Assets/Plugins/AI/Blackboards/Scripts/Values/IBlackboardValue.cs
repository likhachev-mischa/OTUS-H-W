namespace AIModule
{
    public interface IBlackboardValue
    {
        ushort Key { get; }
        object Value { get; }
    }
}