using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ZombieSystem : IGameUpdateListener,IGameFinishListener
    {
        [SerializeField] private Zombie zombiePrefab;
        [SerializeField] private int zombieAmount = 15;

        public AtomicEvent<Zombie> OnSpawned;
        public AtomicEvent<Zombie> OnDespawned;
        public AtomicEvent OnZombieDeath;
        
        private ObjectPool<Zombie> zombiePool;

        public List<Zombie> Zombies
        {
            get => zombiePool.ActiveObjects;
        }

        private Transform target;

        [Inject]
        private void Construct(Character character)
        {
            target = character.transform;

            zombiePool = new ObjectPool<Zombie>(zombiePrefab, zombieAmount);
        }

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < Zombies.Count; i++)
            {
                Zombies[i].OnUpdate(deltaTime);
            }
        }

        public Zombie SpawnZombie()
        {
            Zombie zombie = zombiePool.GetObject();
            zombie.IsDead.Value = false;
            
            zombie.Despawn.Subscribe(DespawnZombie);
            zombie.Death.Subscribe(OnDeath);
            
            OnSpawned.Invoke(zombie);
            return zombie;
        }

        public void DespawnZombie(MonoBehaviour obj)
        {
            var zombie = (Zombie)obj;
            
            zombie.Despawn.Unsubscribe(DespawnZombie);
            zombie.Death.Unsubscribe(OnDeath);
            
            zombiePool.RemoveObject(zombie);
            OnDespawned.Invoke(zombie);
        }

        private void OnDeath()
        {
            OnZombieDeath.Invoke();
        }
        public void OnFinish()
        {
            zombiePool.Clear();
        }
    }
}