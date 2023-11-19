using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
        [RequireComponent(typeof(MoveComponent))]
        public sealed class EnemyMoveAgent : MonoBehaviour
        {
            public event Action<EnemyFacade> TargetReachedEvent;
            
            [SerializeField] private float reachOffset = 0.25f;

            private Vector2 destination;

            private MoveComponent moveComponent;
            private EnemyFacade enemy;

            private void Awake()
            {
                this.moveComponent = this.GetComponent<MoveComponent>();
                this.enemy = this.GetComponent<EnemyFacade>();
            }

            public void SetDestination(Vector2 endPoint)
            {
                this.destination = endPoint;
            }

            private void FixedUpdate()
            {
                var vector = this.destination - (Vector2)this.transform.position;
                if (vector.magnitude <= reachOffset)
                {
                    TargetReachedEvent?.Invoke(enemy);
                    return;
                }

                var direction = vector.normalized * Time.fixedDeltaTime;
                this.moveComponent.Move(direction);
            }
        }
    }
}