using System;
using DI;
using UnityEngine;

namespace Game
{
    public sealed class Bullet : MonoBehaviour, IGameUpdateListener
    {
        public AtomicVariable<int> Speed;
        public AtomicVariable<float> LifeTime;
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicEvent<Vector3> Moved;
        public AtomicVariable<int> Damage;
        public AtomicVariable<bool> canMove;

        public AtomicVariable<bool> isDead;

        public AtomicEvent Death;
        public AtomicEvent<MonoBehaviour> Despawn;

        private MovementMechanics movementMechanics;
        private DeathMechanics deathMechanics;
        private DespawnMechanics despawnMechanics;

        private LifeTimeMechanics lifeTimeMechanics;
        private BulletCollisionMechanics bulletCollisionMechanics;

        private void Awake()
        {
            movementMechanics = new MovementMechanics(Speed, MoveDirection, Moved, transform, canMove);
            deathMechanics = new DeathMechanics(isDead, Death);
            despawnMechanics = new DespawnMechanics(Death, Despawn, this);
            lifeTimeMechanics = new LifeTimeMechanics(Death, LifeTime);
            bulletCollisionMechanics = new BulletCollisionMechanics(Damage, Death);
        }

        private void OnEnable()
        {
            movementMechanics.OnEnable();
            deathMechanics.OnEnable();
            despawnMechanics.OnEnable();
            lifeTimeMechanics.OnEnable();
        }

        public void OnUpdate(float deltaTime)
        {
            movementMechanics.OnUpdate();
            lifeTimeMechanics.Update(deltaTime);
        }

        private void OnDisable()
        {
            movementMechanics.OnDisable();
            deathMechanics.OnDisable();
            despawnMechanics.OnDisable();
        }

        private void OnTriggerEnter(Collider other)
        {
            bulletCollisionMechanics.OnTriggerEnter(other);
        }
    }
}