using UnityEngine;

namespace Game
{
    public sealed class MovementComponent
    {
        private readonly IAtomicAction<Vector3> moved;
        public Transform Transform { get; }

        public MovementComponent(IAtomicAction<Vector3> moved, Transform transform)
        {
            this.moved = moved;
            this.Transform = transform;
        }

        public void Move(Vector3 direction)
        {
            moved?.Invoke(direction);
        }
    }
}