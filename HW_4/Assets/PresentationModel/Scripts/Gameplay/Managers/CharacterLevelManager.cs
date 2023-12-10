using UnityEngine;

namespace MVVM
{
    public sealed class CharacterLevelManager : MonoBehaviour
    {
        [SerializeField] private int experience;

        private bool isAutoLevelUpEnabled;

        private Character character;

        [Inject]
        private void Construct(Character character)
        {
            this.character = character;
        }

        public void AddExperience()
        {
            character.CharacterLevel.AddExperience(experience);
        }
    }
}