using System;

namespace ShootEmUp
{
    namespace Components
    {
         public interface IDamageable
        {
            public int Health { get; set; }
            public event Action<int> TakeDamageEvent;

        }
    }
   
}