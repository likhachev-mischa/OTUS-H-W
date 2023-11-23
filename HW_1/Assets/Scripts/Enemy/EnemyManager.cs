﻿using UnityEngine;

namespace ShootEmUp
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private int initialCount = 7;

        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform worldTransform;

        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject character;

        private ObjectPool<Enemy> enemyPool;

        private void Awake()
        {
            enemyPool = new ObjectPool<Enemy>(initialCount, container, prefab, worldTransform);
            enemyPool.Initialize();
        }

        public void SpawnEnemy()
        {
            if (!enemyPool.SpawnObject(out var enemy))
            {
                return;
            }

            enemy.SetPosition(this.enemyPositions.RandomSpawnPosition().position);
            enemy.OnStart();
            enemy.SetDestination(this.enemyPositions.RandomAttackPosition().position);
            enemy.SetTarget(this.character);
        }

        public void DespawnEnemy(Enemy enemy)
        {
            enemy.OnFinish();
            enemyPool.RemoveObject(enemy);
        }
    }
}