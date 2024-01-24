using System;
using DI;
using UnityEngine;

namespace Game
{
    public class CharacterVFX : MonoBehaviour, IGamePostConstructListener
    {
        [SerializeField] private ParticleSystem shootVFX;
        [SerializeField] private ParticleSystem damageVFX;
        
        private Character character;

        [Inject]
        private void Construct(Character character)
        {
            this.character = character;
        }
        
        private void OnDisable()
        {
            character.FireEvent.Unsubscribe(OnFire);
            character.TakeDamage.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int value)
        {
            damageVFX.Play();
        }

        private void OnFire()
        {
            shootVFX.transform.position = character.FirePoint.position;
            shootVFX.Play(true);
        }
        
        public void OnPostConstruct()
        {
            character.FireEvent.Subscribe(OnFire);
            character.TakeDamage.Subscribe(OnTakeDamage);
        }
    }
}