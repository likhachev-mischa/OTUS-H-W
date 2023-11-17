using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IDamageable
    {
        public int Health { get; set; }
        public event Action<int> TakeDamageEvent;

    }
   
}