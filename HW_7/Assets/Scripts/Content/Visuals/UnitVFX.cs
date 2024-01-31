using UnityEngine;

namespace Content
{
    public class UnitVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem takeDamage;

        public void TakeDamage()
        {
            takeDamage.Play();
        }
    }
}