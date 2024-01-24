using UnityEngine;

namespace Game
{
    public class RotationMechanics
    {
        private readonly IAtomicValue<int> speed;
        private readonly AtomicVariable<Vector3> rotationDirection;
        private readonly IAtomicEvent<Vector3> rotated;
        private readonly Transform transform;
        private readonly IAtomicVariable<bool> canMove;

        public RotationMechanics(IAtomicValue<int> speed, AtomicVariable<Vector3> rotationDirection,
            IAtomicEvent<Vector3> rotated, Transform transform, IAtomicVariable<bool> canMove)
        {
            this.speed = speed;
            this.rotationDirection = rotationDirection;
            this.rotated = rotated;
            this.transform = transform;
            this.canMove = canMove;
        }

        public void OnEnable()
        {
            rotated.Subscribe(OnRotated);
        }

        public void OnDisable()
        {
            rotated.Unsubscribe(OnRotated);
        }

        private void OnRotated(Vector3 direction)
        {
            if (!canMove.Value)
            {
                return;
            }

            float rotationSpeed = (float)speed.Value / 1000f;

            Vector3 currentRotation = transform.rotation.eulerAngles;
            Vector3 currentPosition = transform.position;

            Vector2 rotationVector = new Vector2(direction.x - currentPosition.x, direction.z - currentPosition.z)
                .normalized;

            Vector2 normalVector = new(Mathf.Sin(currentRotation.y * Mathf.Deg2Rad),
                Mathf.Cos(currentRotation.y * Mathf.Deg2Rad));

            float rotationCos = rotationVector.x * normalVector.x + rotationVector.y * normalVector.y;

            float rotationAngle = Mathf.Acos(rotationCos) * Mathf.Rad2Deg;

            float directionVectorAngle = Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg;
            directionVectorAngle = AngleToCoordinates(directionVectorAngle);

            float normalVectorAngle = Mathf.Atan2(normalVector.x, normalVector.y) * Mathf.Rad2Deg;
            normalVectorAngle = AngleToCoordinates(normalVectorAngle);

            float possibleNextNormal = normalVectorAngle - rotationAngle;

            if ((int)Mathf.Ceil(directionVectorAngle) == (int)Mathf.Ceil(possibleNextNormal) ||
                (int)Mathf.Ceil(directionVectorAngle) == (int)Mathf.Ceil(possibleNextNormal + 360f))
            {
                rotationAngle = -rotationAngle;
            }

            float newAngle = rotationAngle + currentRotation.y;

            rotationDirection.Value = new Vector3(0, newAngle, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(rotationDirection.Value),
                rotationSpeed);

            float AngleToCoordinates(float f)
            {
                if (Mathf.Sign(f) > 0)
                {
                    float temp = 180 - f;
                    f = -180 - temp;
                }

                return f;
            }
        }
    }
}