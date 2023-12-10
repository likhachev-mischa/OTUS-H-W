using UnityEngine;

namespace MVVM
{
    public sealed class CharacterLevelManager : MonoBehaviour
    {
        [SerializeField] private int experience;

        private bool isAutoLevelUpEnabled;
        
        private Character character;
        private CharacterAutoLevelUpObserver autoLevelUpObserver;
        
        [Inject]
        private void Construct (Character character)
        {
            this.character = character;
            this.autoLevelUpObserver = new CharacterAutoLevelUpObserver(this.character);
        }

        public void AddExperience()
        {
            character.CharacterLevel.AddExperience(experience);
        }


    }
}