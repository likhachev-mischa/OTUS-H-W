using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IShooter
    {
        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletSystem bulletSystem;
        [SerializeField] 
        private BulletConfig bulletConfig;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private ShootComponent shootComponent;
        
        public event Action ShootEvent;
        
        private void Awake()
        {
            shootComponent = new ShootComponent(this, this.transform,
                bulletSystem, bulletConfig, false);
        }

        private void OnEnable()
        {
            shootComponent.Enable();
        }

        private void OnDisable()
        {
            shootComponent.Disable();
        }

       
        

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this._enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            /*_bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = false,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = position,
                velocity = direction * 2.0f
            });*/
        }

       
    }
}