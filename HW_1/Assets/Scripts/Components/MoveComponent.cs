using System;
using UnityEngine;

namespace ShootEmUp
{
    namespace Components
    {
         [RequireComponent(typeof(Rigidbody2D))]
         public sealed class MoveComponent : MonoBehaviour
        {
            private new Rigidbody2D rigidbody2D;
            
            [SerializeField]
            private float speed;

            private void Awake()
            {
                this.rigidbody2D = this.GetComponent<Rigidbody2D>();
            }
            
            public void Move(Vector2 vector)
            {
                var nextPosition = this.rigidbody2D.position + vector * this.speed;
                this.rigidbody2D.MovePosition(nextPosition);
            }
        }
    }
}