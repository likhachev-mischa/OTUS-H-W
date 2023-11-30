using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour,
        IGameFixedUpdateListener
    {
        public event Action OnTargetReached;

        [SerializeField] private float reachOffset = 0.25f;

        private Vector2 destination;

        private MoveComponent moveComponent;

        private void Awake()
        {
            this.moveComponent = this.GetComponent<MoveComponent>();
        }

        public void Enable()
        {
            this.enabled = true;
        }

        public void Disable()
        {
            this.enabled = false;
        }

        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            var vector = this.destination - (Vector2)this.transform.position;
            if (vector.magnitude <= reachOffset)
            {
                OnTargetReached?.Invoke();
                return;
            }

            var direction = vector.normalized * deltaTime;
            this.moveComponent.Move(direction);
        }
    }
}