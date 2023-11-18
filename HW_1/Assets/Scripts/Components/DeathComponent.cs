namespace ShootEmUp
{
    namespace Components
    {
         public interface IKillable : IDamageable
        {
            public void Death();
        }

        public sealed class DeathComponent
        {
            private IKillable killable;

            public DeathComponent(IKillable killable)
            {
                this.killable = killable;
            }

            public void Enable()
            {
                killable.TakeDamageEvent += OnTakeDamage;
            }

            public void Disable()
            {
                killable.TakeDamageEvent -= OnTakeDamage;
            }

            private void OnTakeDamage(int damage)
            {
                if (killable.Health <= 0)
                {
                    killable.Death();
                }
            }
        }
    }
}