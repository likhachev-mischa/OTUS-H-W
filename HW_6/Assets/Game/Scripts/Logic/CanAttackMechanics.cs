namespace Game
{
    public class CanAttackMechanics
    {
        private readonly IAtomicVariable<bool> canAttack;
        private readonly AtomicVariable<bool> isDead;

        public CanAttackMechanics(IAtomicVariable<bool> canAttack, AtomicVariable<bool> isDead)
        {
            this.canAttack = canAttack;
            this.isDead = isDead;
        }

        public void OnEnable()
        {
            isDead.ValueChanged += CanMove;
        }

        public void OnDisable()
        {
            isDead.ValueChanged -= CanMove;
        }

        private void CanMove(bool value)
        {
            canAttack.Value = !value;
        }
    }
}