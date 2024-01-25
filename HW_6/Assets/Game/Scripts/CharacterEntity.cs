using DI;
using UnityEngine;

namespace Game
{
    public sealed class CharacterEntity : Entity
    {
        private Character character;

        private void Awake()
        {
            character = this.gameObject.GetComponent<Character>();
            
            Add(new MovementComponent(character.Moved,character.transform));
            Add(new RotationComponent(character.Rotated));
            Add(new ShootComponent(character.FireRequest));
            Add(new TakeDamageComponent(character.TakeDamage));
        }
    }
}