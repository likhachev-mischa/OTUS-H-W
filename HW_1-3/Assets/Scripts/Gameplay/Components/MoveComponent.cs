using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour
    {
        private new Rigidbody2D rigidbody2D;

        [SerializeField] private float speed;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 vector)
        {
            Vector2 nextPosition = rigidbody2D.position + vector * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}