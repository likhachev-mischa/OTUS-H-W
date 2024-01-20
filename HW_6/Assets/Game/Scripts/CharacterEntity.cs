using DI;
using UnityEngine;

namespace Game
{
    public sealed class CharacterEntity : Entity, IGamePostConstructListener
    {
        private Character character;

        [Inject]
        private void Construct(Character character)
        {
            this.character = character;
        }

        public void OnPostConstruct()
        {
            Add(new MovementComponent(character.Moved));
            Add(new RotationComponent(character.Rotated));
        }
    }
}