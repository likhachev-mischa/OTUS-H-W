namespace Game
{
    public sealed class DeathMechanics
    {
        private readonly IAtomicVariable<bool> isDead;
        private readonly IAtomicEvent death;

        public DeathMechanics(IAtomicVariable<bool> isDead, IAtomicEvent death)
        {
            this.isDead = isDead;
            this.death = death;
        }

        public void OnEnable()
        {
            death.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            death.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            if (isDead.Value)
            {
                return;    
            }
            
            isDead.Value = true;
        }
    }
}