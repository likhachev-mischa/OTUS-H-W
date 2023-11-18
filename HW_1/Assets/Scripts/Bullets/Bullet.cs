using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        private bool isPlayer;
        private int damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        public bool IsPlayer
        {
            get => isPlayer;
            set => isPlayer = value;
        }

        public int Damage
        {
            get => damage;
            set => damage = value;
        }
        public Vector2 Velocity { set => rigidbody2D.velocity = value; }
        public int PhysicsLayer { set => gameObject.layer = value; }
        public Vector3 Position { set => transform.position = value; }
        public Color Color { set => spriteRenderer.color = value; }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
    
}