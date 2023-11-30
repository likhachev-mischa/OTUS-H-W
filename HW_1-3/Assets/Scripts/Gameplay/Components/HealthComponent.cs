using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int health;
        public event Action TakeDamageEvent;

        public int Health
        {
            get => health;
            set
            {
                health = value;
                TakeDamageEvent?.Invoke();
            }
        }
    }
}