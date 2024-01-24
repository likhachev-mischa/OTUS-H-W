namespace Game
{
    public sealed class ShootComponent
    {
        private readonly IAtomicAction fireRequest;

        public ShootComponent(IAtomicAction fireRequest)
        {
            this.fireRequest = fireRequest;
        }

        public void Shoot()
        {
            fireRequest?.Invoke();
        }
    }
}