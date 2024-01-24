using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class ZombiesController : IGamePostConstructListener, IGameUpdateListener, IGameFinishListener
    {
        private List<Entity> zombieEntities = new();
        private Entity target;

        private IAtomicEvent<Zombie> onSpawned;
        private IAtomicEvent<Zombie> onDespawned;

        [Inject]
        private void Construct(CharacterEntity characterEntity, ZombieSystem zombieSystem)
        {
            target = characterEntity;

            onSpawned = zombieSystem.OnSpawned;
            onDespawned = zombieSystem.OnDespawned;
        }

        public void OnPostConstruct()
        {
            onSpawned.Subscribe(OnZombieSpawned);
            onDespawned.Subscribe(OnZombieDespawned);
        }

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < zombieEntities.Count; i++)
            {
                var rotationComponent = zombieEntities[i].Get<RotationComponent>();
                rotationComponent.Rotate(target.transform.position);

                var movementComponent = zombieEntities[i].Get<MovementComponent>();
                var attackComponent = zombieEntities[i].Get<AttackComponent>();

                Vector3 toTarget = (target.transform.position - movementComponent.Transform.position);

                if (toTarget.magnitude <= attackComponent.attackDistance.Value)
                {
                    attackComponent.Attack(target);
                    movementComponent.Move(Vector3.zero);
                    
                }
                else
                {
                    movementComponent.Move(toTarget.normalized);
                }
            }
        }

        private void OnZombieSpawned(Zombie zombie)
        {
            zombieEntities.Add(zombie.zombieEntity);
        }

        private void OnZombieDespawned(Zombie zombie)
        {
            zombieEntities.Remove(zombie.zombieEntity);
        }

        public void OnFinish()
        {
            onSpawned.Unsubscribe(OnZombieSpawned);
            onDespawned.Unsubscribe(OnZombieDespawned);
        }
    }
}