using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Transform leftBorder;

        [SerializeField] private Transform rightBorder;

        [SerializeField] private Transform downBorder;

        [SerializeField] private Transform topBorder;

        public Transform LeftBorder => leftBorder;

        public Transform RightBorder => rightBorder;

        public bool InBounds(Vector3 position)
        {
            float positionX = position.x;
            float positionY = position.y;
            return positionX > leftBorder.position.x
                   && positionX < rightBorder.position.x
                   && positionY > downBorder.position.y
                   && positionY < topBorder.position.y;
        }
    }
}