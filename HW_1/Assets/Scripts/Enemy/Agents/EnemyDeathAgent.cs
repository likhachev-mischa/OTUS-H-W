using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
        [RequireComponent(typeof(DeathComponent))]
         public class EnemyDeathAgent : MonoBehaviour
         { 
             private EnemyManager enemyManager;
             private DeathComponent deathComponent;
             
             private EnemyFacade enemy;
             
             [NonSerialized]
             public HealthComponent healthComponent;


            private void Awake()
            {
                this.deathComponent = this.GetComponent<DeathComponent>();
                this.healthComponent = this.GetComponent<HealthComponent>();
                this.enemy = this.GetComponent<EnemyFacade>();
                
                this.enemyManager = FindObjectOfType<EnemyManager>();
            }

            private void OnEnable()
            {
                this.deathComponent.DeathEvent += this.OnEnemyDeath;
            }

            private void OnDisable()
            {
                this.deathComponent.DeathEvent -= this.OnEnemyDeath;
            }


            private void OnEnemyDeath()
            {
                this.enemyManager.DespawnEnemy(enemy);
            }
            
        }
    }
}