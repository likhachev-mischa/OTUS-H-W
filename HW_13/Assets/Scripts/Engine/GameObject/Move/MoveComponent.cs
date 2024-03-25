using System;
using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable MemberInitializerValueIgnored

namespace Game.Engine
{
    [Serializable]
    public sealed class MoveComponent : IDisposable, IFixedUpdate
    {
        public IAtomicAction<Vector3> MoveRequest => this.moveRequest;
        public IAtomicValue<bool> IsMoving => this.isMoving;
        public IAtomicValue<Vector3> MoveDirection => this.moveDirection;
        
        [SerializeField]
        private Transform transform;
        
        [SerializeField]
        private AtomicAction<Vector3> moveRequest;
        
        [SerializeField]
        private AtomicValue<float> moveSpeed = new(0);
        
        [SerializeField, ReadOnly]
        private AtomicVariable<Vector3> moveDirection = new();

        [SerializeField, ReadOnly]
        private AtomicVariable<bool> isMoving;
        
        public void Compose()
        {
            this.moveRequest.Compose(direction =>
            {
                this.moveDirection.Value = direction;
                this.isMoving.Value = true;
            });
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.isMoving.Value)
            {
                this.transform.position += this.moveSpeed.Value * deltaTime * this.moveDirection.Value;
                this.isMoving.Value = false;
            }
        }

        public void Dispose()
        {
            this.moveDirection?.Dispose();
        }
    }
}