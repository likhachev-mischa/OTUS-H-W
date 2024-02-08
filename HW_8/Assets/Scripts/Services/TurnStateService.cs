namespace Services
{
    public enum TurnState
    {
        ATTACK,
        DEFENCE,
        POST_ATTACK
    }

    public sealed class TurnStateService
    {
        public TurnState State { get; set; }
    }
}