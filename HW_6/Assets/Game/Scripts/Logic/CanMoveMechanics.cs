namespace Game
{
    public class CanMoveMechanics
    {
        private readonly IAtomicVariable<bool> canMove;
        private readonly AtomicVariable<bool> isDead;

        public CanMoveMechanics(IAtomicVariable<bool> canMove, AtomicVariable<bool> isDead)
        {
            this.canMove = canMove;
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
            canMove.Value = !value;
        }
    }
}