using System;
using ShootEmUp.Components;
using ShootEmUp.Enemy;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyAttackAgent),typeof(EnemyMoveAgent),typeof(EnemyDeathAgent))]
    public class EnemyFacade : MonoBehaviour
    {
        [NonSerialized]
        public EnemyAttackAgent enemyAttackAgent;
        [NonSerialized]
        public EnemyMoveAgent enemyMoveAgent;
        [NonSerialized]
        public EnemyDeathAgent enemyDeathAgent;
        

        private int initialHealth;
        private void Awake()
        {
            this.enemyAttackAgent = this.GetComponent<EnemyAttackAgent>();
            this.enemyMoveAgent = this.GetComponent<EnemyMoveAgent>();
            this.enemyDeathAgent = this.GetComponent<EnemyDeathAgent>();
        }
        
    }
}