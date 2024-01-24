namespace Game
{
    public class LifeTimeMechanics
    {
        private IAtomicAction death;
        private IAtomicVariable<float> lifeTime;

        private float cachedLifeTime;
        public LifeTimeMechanics(IAtomicAction death, IAtomicVariable<float> lifeTime)
        {
            this.death = death;
            this.lifeTime = lifeTime;
            cachedLifeTime = this.lifeTime.Value;
        }

        public void OnEnable()
        {
            lifeTime.Value = cachedLifeTime;
        }

        public void Update(float deltaTime)
        {
            lifeTime.Value -= deltaTime;

            if (lifeTime.Value <= 0)
            {
                lifeTime.Value = 0;
                death.Invoke();
            }
        }
        
    }
}