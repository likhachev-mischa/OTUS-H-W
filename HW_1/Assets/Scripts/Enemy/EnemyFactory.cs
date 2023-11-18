using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
         public class EnemyFactory : MonoBehaviour
        {
            [SerializeField] private int initialCount = 7;
            public int InitialCount { get; }

            [SerializeField] private Transform container;
            [SerializeField] private EnemyProvider prefab;
            [SerializeField] private Transform worldTransform;

            [SerializeField] private EnemyPositions enemyPositions;
            [SerializeField] private GameObject character;

            private ObjectPool<EnemyProvider> enemyPool;

            private BulletCollisionHandler bulletCollisionHandler;
            private BulletDamageHandler bulletDamageHandler;

            private EnemyInstaller enemyInstaller;

            private void Awake()
            {
                enemyInstaller = new EnemyInstaller(this);
                enemyPool = new ObjectPool<EnemyProvider>(initialCount, container, prefab, worldTransform);
                enemyPool.Initialize();
            }

            public bool SpawnEnemy(out EnemyProvider enemyProvider)
            {
                if (!enemyPool.SpawnObject(out enemyProvider))
                {
                    return false;
                }

                var spawnPosition = this.enemyPositions.RandomSpawnPosition();
                enemyProvider.transform.position = spawnPosition.position;

                EnemyInstaller.SetUpMovement(enemyProvider, enemyPositions);
                EnemyInstaller.SetUpAttack(enemyProvider, character);
                enemyInstaller.SetUpHealth(enemyProvider);

                return true;
            }
            
            public void DespawnEnemy(EnemyProvider enemyProvider)
            {
                EnemyInstaller.DisableMovement(enemyProvider);
                enemyInstaller.DisableHealth(enemyProvider);
                enemyPool.RemoveObject(enemyProvider);
            }

            private class EnemyInstaller
            {
                private EnemyFactory enemyFactory;

                public EnemyInstaller(EnemyFactory enemyFactory)
                {
                    this.enemyFactory = enemyFactory;
                }

                public static void SetUpMovement(EnemyProvider enemyProvider, EnemyPositions enemyPositions)
                {
                    var attackPosition = enemyPositions.RandomAttackPosition();
                    for (int index = 0; index < enemyProvider.Agents.Length; ++index)
                    {
                        var agent = enemyProvider.Agents[index];
                        if (agent is EnemyMoveAgent moveAgent)
                        {
                            moveAgent.SetDestination(attackPosition.position);
                            moveAgent.enabled = true;
                            SubscribeDependersOnMove(moveAgent, enemyProvider);
                            return;

                        }
                    }

                }

                public static void SetUpAttack(EnemyProvider enemyProvider, GameObject target)
                {
                    for (int index = 0; index < enemyProvider.Agents.Length; ++index)
                    {
                        var agent = enemyProvider.Agents[index];
                        if (agent is EnemyAttackAgent attackAgent)
                        {
                            attackAgent.enabled = false;
                            attackAgent.SetTarget(target);
                        }
                    }
                }

                public void SetUpHealth(EnemyProvider enemyProvider)
                {
                    for (int index = 0; index < enemyProvider.Agents.Length; ++index)
                    {
                        var agent = enemyProvider.Agents[index];
                        if (agent is EnemyHealthAgent enemyHealthAgent)
                        {
                            enemyHealthAgent.DeathEvent += enemyFactory.DespawnEnemy;
                        }
                    }
                }


                public void DisableHealth(EnemyProvider enemyProvider)
                {
                    for (int index = 0; index < enemyProvider.Agents.Length; ++index)
                    {
                        var agent = enemyProvider.Agents[index];
                        if (agent is EnemyHealthAgent enemyHealthAgent)
                        {
                            enemyHealthAgent.DeathEvent -= enemyFactory.DespawnEnemy;
                        }
                    }
                }

                public static void DisableMovement(EnemyProvider enemyProvider)
                {
                    for (int index = 0; index < enemyProvider.Agents.Length; ++index)
                    {
                        var agent = enemyProvider.Agents[index];
                        if (agent is EnemyMoveAgent moveAgent)
                        {
                            UnsubscribeDependersOnMove(moveAgent, enemyProvider);
                            return;
                        }
                    }
                }

                private static void SubscribeDependersOnMove(EnemyMoveAgent enemyMoveAgent, EnemyProvider enemyProvider)
                {
                    for (int index = 0; index < enemyProvider.Agents.Length; ++index)
                    {
                        var agent = enemyProvider.Agents[index];
                        if (agent is IDependsOnReachedTarget dependsOnReachedTarget)
                        {
                            enemyMoveAgent.ReachedEvent += dependsOnReachedTarget.OnTargetReached;
                        }

                    }
                }

                private static void UnsubscribeDependersOnMove(EnemyMoveAgent enemyMoveAgent,
                    EnemyProvider enemyProvider)
                {
                    for (int index = 0; index < enemyProvider.Agents.Length; ++index)
                    {
                        var agent = enemyProvider.Agents[index];
                        if (agent is IDependsOnReachedTarget dependsOnReachedTarget)
                        {
                            enemyMoveAgent.ReachedEvent -= dependsOnReachedTarget.OnTargetReached;
                        }

                    }
                }
            }

        }
    }
}