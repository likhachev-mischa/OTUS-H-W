using UnityEngine;

namespace Game
{
    public class RotationComponent
    {
        private readonly IAtomicAction<Vector3> rotated;

        public RotationComponent(IAtomicAction<Vector3> rotated)
        {
            this.rotated = rotated;
        }

        public void Rotate(Vector3 direction)
        {
            rotated?.Invoke(direction);
        }
    }
}