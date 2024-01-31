using UnityEngine;

namespace Content
{
    public class BaseVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem destroyed;
        [SerializeField] private ParticleSystem lightDamaged;
        [SerializeField] private ParticleSystem heavyDamaged;
        [SerializeField] private ParticleSystem tookDamage;

        public void OnDestroyed()
        {
            destroyed.Play();
        }

        public void OnDamageTaken()
        {
            tookDamage.Play();
        }

        public void OnLightDamaged()
        {
            if (lightDamaged.isPlaying)
            {
                return;
            }
            lightDamaged.Play();
        }

        public void OnHeavyDamaged()
        {
            if (heavyDamaged.isPlaying)
            {
                return;
            }
            heavyDamaged.Play();
        }
    }
}