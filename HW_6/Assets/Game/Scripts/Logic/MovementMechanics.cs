using UnityEngine;

namespace Game
{
    public sealed class MovementMechanics
    {
        private readonly IAtomicValue<int> speed;
        private readonly AtomicVariable<Vector3> moveDirection;
        private readonly AtomicEvent<Vector3> moved;
        private readonly Transform transform;

        public MovementMechanics(IAtomicValue<int> speed, AtomicVariable<Vector3> moveDirection,
            AtomicEvent<Vector3> moved,Transform transform)
        {
            this.speed = speed;
            this.moveDirection = moveDirection;
            this.moved = moved;
            this.transform = transform;
        }

        public void OnEnable()
        {
            moved.Subscribe(OnMoved);
        }

        public void OnDisable()
        {
            moved.Unsubscribe(OnMoved);
        }

        private void OnMoved(Vector3 direction)
        {
            moveDirection.Value = direction;
            transform.position += moveDirection.Value * (speed.Value * Time.deltaTime);
        }
    }
}