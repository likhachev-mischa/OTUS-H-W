using UnityEngine;

namespace Game
{
    public class RotationMechanics
    {
        private readonly IAtomicValue<int> speed;
        private readonly AtomicVariable<Vector3> rotationDirection;
        private readonly AtomicEvent<Vector3> rotated;
        private readonly Transform transform;

        public RotationMechanics(IAtomicValue<int> speed, AtomicVariable<Vector3> rotationDirection,
            AtomicEvent<Vector3> rotated, Transform transform)
        {
            this.speed = speed;
            this.rotationDirection = rotationDirection;
            this.rotated = rotated;
            this.transform = transform;
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

            if (Mathf.Approximately(Mathf.Ceil(directionVectorAngle), Mathf.Ceil(possibleNextNormal)) ||
                Mathf.Approximately(Mathf.Ceil(directionVectorAngle), Mathf.Ceil(possibleNextNormal + 360f)))
            {
                rotationAngle = -rotationAngle;
            }

            float newAngle = rotationAngle + currentRotation.y;

            rotationDirection.Value = new Vector3(0, newAngle, 0);

            transform.rotation = Quaternion.Lerp(Quaternion.Euler(currentRotation),
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