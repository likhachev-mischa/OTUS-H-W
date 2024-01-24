namespace Game
{
    public sealed class AttackMechanics
    {
        private IAtomicEvent attack;
        private IAtomicVariable<int> damage;
        private IAtomicVariable<TakeDamageComponent> target;

        public AttackMechanics(IAtomicEvent attack, IAtomicVariable<int> damage, IAtomicVariable<TakeDamageComponent> target)
        {
            this.attack = attack;
            this.damage = damage;
            this.target = target;
        }

        public void OnEnable()
        {
            attack.Subscribe(OnAttack);
        }

        public void OnDisable()
        {
            attack.Unsubscribe(OnAttack);
        }
        
        private void OnAttack()
        {
            target.Value.TakeDamage(damage.Value);
        }
    }
}