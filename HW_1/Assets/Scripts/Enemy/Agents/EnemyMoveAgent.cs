using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
         public interface IDependsOnReachedTarget
        {
            public void OnTargetReached();
        }

        public sealed class EnemyMoveAgent : EnemyAgent, IDependsOnReachedTarget
        {
            private bool isReached;
            public event Action ReachedEvent;

            [SerializeField] private float speed = 5.0f;
            [SerializeField] private float reachOffset = 0.25f;

            private Vector2 destination;

            private MoveComponent moveComponent;

            private void Awake()
            {
                moveComponent = new MoveComponent(this.GetComponent<Rigidbody2D>(), speed);
            }

            public void OnTargetReached()
            {
                this.enabled = false;
            }

            public void SetDestination(Vector2 endPoint)
            {
                this.destination = endPoint;
                this.isReached = false;
            }

            private void FixedUpdate()
            {
                if (this.isReached)
                {
                    return;
                }

                var vector = this.destination - (Vector2)this.transform.position;
                if (vector.magnitude <= reachOffset)
                {
                    this.isReached = true;
                    ReachedEvent?.Invoke();
                    return;
                }

                var direction = vector.normalized * Time.fixedDeltaTime;
                this.moveComponent.MoveByRigidbodyVelocity(direction);
            }
        }
    }
}