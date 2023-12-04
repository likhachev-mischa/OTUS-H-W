using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent :
        MonoBehaviour,
        IGameFixedUpdateListener
    {
        public event Action OnTargetReached;

        [SerializeField] private float reachOffset = 0.25f;

        private Vector2 destination;

        private MoveComponent moveComponent;

        private void Awake()
        {
            moveComponent = GetComponent<MoveComponent>();
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }

        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            Vector2 vector = destination - (Vector2)transform.position;
            if (vector.magnitude <= reachOffset)
            {
                OnTargetReached?.Invoke();
                return;
            }

            Vector2 direction = vector.normalized * deltaTime;
            moveComponent.Move(direction);
        }
    }
}