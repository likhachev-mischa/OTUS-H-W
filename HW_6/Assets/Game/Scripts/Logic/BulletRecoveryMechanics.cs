namespace Game
{
    public class BulletRecoveryMechanics
    {
        private IAtomicVariable<float> cooldown;
        private IAtomicVariable<int> bulletCount;

        private float cachedCooldown;
        private int maxBullets;

        public BulletRecoveryMechanics(IAtomicVariable<float> cooldown, IAtomicVariable<int> bulletCount)
        {
            this.cooldown = cooldown;
            this.bulletCount = bulletCount;

            cachedCooldown = cooldown.Value;
            maxBullets = bulletCount.Value;
        }

        public void Update(float deltaTime)
        {
            if (bulletCount.Value >= maxBullets)
            {
                cooldown.Value = cachedCooldown;
                return;
            }
            
            cooldown.Value -= deltaTime;
            if (cooldown.Value <= 0)
            {
                ++bulletCount.Value;
                cooldown.Value = cachedCooldown;
            }
        }
    }
}