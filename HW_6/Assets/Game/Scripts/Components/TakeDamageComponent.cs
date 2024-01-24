namespace Game
{
    public sealed class TakeDamageComponent
    {
        private readonly IAtomicAction<int> takeDamage;

        public TakeDamageComponent(IAtomicAction<int> takeDamage)
        {
            this.takeDamage = takeDamage;
        }

        public void TakeDamage(int value)
        {
            takeDamage.Invoke(value);
        }
    }
}