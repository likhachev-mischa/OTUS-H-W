using DI;
using UnityEngine;

namespace Game
{
    public class ZombieVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem damageVFX;

        [SerializeField] private Zombie zombie;
        
        public void OnEnable()
        {
            zombie.TakeDamage.Subscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int value)
        {
            damageVFX.Play();
        }

        private void OnDisable()
        {

            zombie.TakeDamage.Unsubscribe(OnTakeDamage);
        }
       
    }
}