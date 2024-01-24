namespace Game
{
    public class CanShootMechanics
    {
        private IAtomicVariable<bool> isDead;
        private IAtomicVariable<bool> canShoot;
        private IAtomicVariable<int> bulletCount;

        public CanShootMechanics(IAtomicVariable<bool> isDead, IAtomicVariable<bool> canShoot, IAtomicVariable<int> bulletCount)
        {
            this.isDead = isDead;
            this.canShoot = canShoot;
            this.bulletCount = bulletCount;
        }

        public void OnEnable()
        {
            isDead.ValueChanged += OnDeath;
            bulletCount.ValueChanged += OnBulletShot;
        }

        public void OnDisable()
        {
            isDead.ValueChanged -= OnDeath;
            bulletCount.ValueChanged -= OnBulletShot;
        }

        private void OnDeath(bool value)
        {
            canShoot.Value = !value;
        }

        private void OnBulletShot(int bullets)
        {
            if (bullets <= 0)
            {
                canShoot.Value = false;
            }
            else
            {
                canShoot.Value = true;
            }
        }
    }
    
}