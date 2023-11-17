using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterHealthSystem : MonoBehaviour,IKillable
    {
        public event Action<int> TakeDamageEvent;

        [SerializeField]
        private int health;

        /*[ContextMenu("takedamage")] 
        private void DebugDamage()
        {
            Health -= 1;
            Debug.Log(health);
        }*/
        
        public int Health
        {
            get => health;
            set
            {
                health = value;
                TakeDamageEvent?.Invoke(value);
            }
            
        }

        private DeathComponent deathComponent;

        private void Awake()
        {
            deathComponent = new DeathComponent(this);
        }

        private void OnEnable()
        {
            deathComponent.Enable();
        }

        private void OnDisable()
        {
            deathComponent.Disable();
        }


        public void Death()
        {
            GameManager.FinishGame();
        }
    }
}