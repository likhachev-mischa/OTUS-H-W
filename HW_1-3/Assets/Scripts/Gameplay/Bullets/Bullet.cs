using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        private BulletCollisionHandler bulletCollisionHandler;
        private BulletDamageHandler bulletDamageHandler;
        public event Action<Collision2D> OnCollisionEntered;
        public event Action<Bullet> OnBulletDespawn;

        private bool isPlayer;
        private int damage;

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

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

        public Vector2 Velocity
        {
            get => rigidbody2D.velocity;
            set => rigidbody2D.velocity = value;
        }

        public int PhysicsLayer
        {
            set => gameObject.layer = value;
        }

        public Vector3 Position
        {
            set => transform.position = value;
        }

        public Color Color
        {
            set => spriteRenderer.color = value;
        }

        private void Awake()
        {
            this.bulletDamageHandler = new BulletDamageHandler(this);
            this.bulletCollisionHandler = new BulletCollisionHandler(this);
        }

        public void Enable()
        {
            this.bulletCollisionHandler.Enable();
            this.bulletDamageHandler.Enable();
        }

        public void Disable()
        {
            this.bulletCollisionHandler.Disable();
            this.bulletDamageHandler.Disable();
        }

        private Vector2 cachedVelocity;

        public void OnPause()
        {
            this.cachedVelocity = this.Velocity;
            this.Velocity = Vector2.zero;
            this.bulletCollisionHandler.Disable();
            this.bulletDamageHandler.Disable();
        }

        public void OnResume()
        {
            this.Velocity = cachedVelocity;
            this.bulletCollisionHandler.Enable();
            this.bulletDamageHandler.Enable();
        }

        public void OnFinish()
        {
            this.Velocity = Vector2.zero;
            this.bulletCollisionHandler.Disable();
            this.bulletDamageHandler.Disable();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(collision);
        }

        public void Despawn()
        {
            this.OnBulletDespawn?.Invoke(this);
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