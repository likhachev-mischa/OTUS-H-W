using DI;
using UnityEngine;

namespace Game
{
    public sealed class Zombie : MonoBehaviour, IGameUpdateListener
    {
        [Header("Stats")] public AtomicVariable<int> Health;
        public AtomicVariable<int> MoveSpeed;
        public AtomicVariable<int> RotationSpeed;

        public AtomicVariable<float> AttackDistance;
        public AtomicVariable<int> Damage;

        [Header("Position")] public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<Vector3> RotateDirection;

        [Header("Triggers")] public AtomicVariable<bool> IsDead;
        public AtomicVariable<bool> CanMove;
        public AtomicVariable<bool> CanAttack;

        [Header("Events")] public AtomicEvent Death;
        public AtomicEvent<Vector3> Moved;
        public AtomicEvent<Vector3> Rotated;
        public AtomicEvent<MonoBehaviour> Despawn;
        public AtomicEvent<int> TakeDamage;
        public AtomicEvent AttackRequest;
        public AtomicEvent AttackEvent;

        public ZombieEntity zombieEntity;

        public AtomicVariable<TakeDamageComponent> Target;

        private BulletLauncher bulletLauncher;

        private MovementMechanics movementMechanics;
        private RotationMechanics rotationMechanics;
        private DeathMechanics deathMechanics;
        private TakeDamageMechanics takeDamageMechanics;
        private CanMoveMechanics canMoveMechanics;
        private AttackMechanics attackMechanics;
        private CanAttackMechanics canAttackMechanics;
        private CanCollideMechanics canCollideMechanics;

        public void Awake()
        {
            Transform zombieTransform = this.transform;
            movementMechanics = new MovementMechanics(MoveSpeed, MoveDirection, Moved, zombieTransform, CanMove);
            rotationMechanics =
                new RotationMechanics(RotationSpeed, RotateDirection, Rotated, zombieTransform, CanMove);
            deathMechanics = new DeathMechanics(IsDead, Death);
            takeDamageMechanics = new TakeDamageMechanics(Health, TakeDamage, Death);
            canMoveMechanics = new CanMoveMechanics(CanMove, IsDead);
            attackMechanics = new AttackMechanics(AttackEvent, Damage, Target);
            canAttackMechanics = new CanAttackMechanics(CanAttack, IsDead);
            
            canCollideMechanics = new CanCollideMechanics(gameObject.GetComponent<CapsuleCollider>(), IsDead);

            zombieEntity = this.gameObject.AddComponent<ZombieEntity>();
            zombieEntity.Initialize(this);
        }

        private void OnEnable()
        {
            movementMechanics.OnEnable();
            rotationMechanics.OnEnable();
            deathMechanics.OnEnable();
            takeDamageMechanics.OnEnable();
            canMoveMechanics.OnEnable();
            attackMechanics.OnEnable();
            canAttackMechanics.OnEnable();
            canCollideMechanics.OnEnable();
        }

        public void OnUpdate(float deltaTime)
        {
            movementMechanics.OnUpdate();
        }

        private void OnDisable()
        {
            movementMechanics.OnDisable();
            rotationMechanics.OnDisable();
            deathMechanics.OnDisable();
            takeDamageMechanics.OnDisable();
            canMoveMechanics.OnDisable();
            attackMechanics.OnDisable();
            canAttackMechanics.OnDisable();
            canCollideMechanics.OnDisable();
        }
    }
}