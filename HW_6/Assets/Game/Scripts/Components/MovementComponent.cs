using UnityEngine;

namespace Game
{
    public sealed class MovementComponent
    {
        private readonly IAtomicAction<Vector3> moved;

        public MovementComponent(IAtomicAction<Vector3> moved)
        {
            this.moved = moved;
        }

        public void Move(Vector3 direction)
        {
            moved?.Invoke(direction);
        }
    }
}