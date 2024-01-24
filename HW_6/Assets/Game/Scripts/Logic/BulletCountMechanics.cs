namespace Game
{
    public class BulletCountMechanics
    {
        private IAtomicVariable<int> bulletCount;
        private IAtomicEvent fireEvent;

        public BulletCountMechanics(IAtomicVariable<int> bulletCount, IAtomicEvent fireEvent)
        {
            this.bulletCount = bulletCount;
            this.fireEvent = fireEvent;
        }

        public void OnEnable()
        {
            fireEvent.Subscribe(OnFire);
        }
        
        public void OnDisable()
        {
            fireEvent.Unsubscribe(OnFire);
        }

        private void OnFire()
        {
            --bulletCount.Value;
        }
    }
}