using System;
using DI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [Serializable]
    public sealed class ZombieSpawner : IGameUpdateListener, IGamePostConstructListener
    {
        [SerializeField] private float spawnInterval = 2f;

        private ZombieSystem zombieSystem;
        private float cachedTime;
        private ZombieSpawns zombieSpawns;

        private Transform[] spawns;

        [Inject]
        private void Construct(ZombieSystem zombieSystem, ZombieSpawns zombieSpawns)
        {
            this.zombieSystem = zombieSystem;
            this.zombieSpawns = zombieSpawns;
        }

        public void OnPostConstruct()
        {
            cachedTime = spawnInterval;
            spawns = zombieSpawns.Positions;
        }

        public void OnUpdate(float deltaTime)
        {
            spawnInterval -= deltaTime;

            if (spawnInterval <= 0)
            {
                spawnInterval = cachedTime;
                Zombie zombie = zombieSystem.SpawnZombie();
                int index = Random.Range(1, spawns.Length);
                //Debug.Log($"{index}/{spawns.Length}");
                zombie.transform.position = spawns[index].position;
            }
        }
    }
}