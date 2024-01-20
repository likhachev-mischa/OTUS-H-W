using DI;
using UnityEngine;

namespace Game
{
    public sealed class Character : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [Header("Stats")] public AtomicVariable<int> Health;
        public AtomicVariable<int> MoveSpeed;
        public AtomicVariable<int> RotationSpeed;
        public AtomicVariable<int> Damage;

        [Header("Position")] public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<Vector3> RotateDirection;

        [Header("Triggers")] public AtomicVariable<bool> IsDead;
        public AtomicVariable<bool> CanMove;

        [Header("Events")] public AtomicEvent Death;
        public AtomicEvent<Vector3> Moved;
        public AtomicEvent<Vector3> Rotated;
        public AtomicEvent FireRequest;

        public Bullet BulletPrefab;

        private MovementMechanics movementMechanics;
        private RotationMechanics rotationMechanics;

        public void OnStart()
        {
            Transform characterTransform = this.transform;
            movementMechanics = new MovementMechanics(MoveSpeed, MoveDirection, Moved, characterTransform);
            rotationMechanics = new RotationMechanics(RotationSpeed, RotateDirection, Rotated, characterTransform);

            Enable();
        }

        private void Enable()
        {
            movementMechanics.OnEnable();
            rotationMechanics.OnEnable();
        }

        private void Disable()
        {
            movementMechanics.OnDisable();
            rotationMechanics.OnDisable();
        }

        public void OnFinish()
        {
            Disable();
        }

        public void OnPause()
        {
            Disable();
        }

        public void OnResume()
        {
            Enable();
        }
    }
}