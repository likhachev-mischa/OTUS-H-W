using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent 
    {
        private Rigidbody2D rigidbody2D;
        private float speed;
        public MoveComponent(Rigidbody2D rigidbody2D, float speed)
        {
            this.rigidbody2D = rigidbody2D;
            this.speed = speed;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = this.rigidbody2D.position + vector * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }
    }
}