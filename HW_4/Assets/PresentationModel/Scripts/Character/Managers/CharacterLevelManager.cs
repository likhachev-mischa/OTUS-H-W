using UnityEngine;

namespace MVVM
{
    public sealed class CharacterLevelManager : MonoBehaviour
    {
        [SerializeField] private int experience;

        public bool IsAutoLevelUpEnabled
        {
            get { return isAutoLevelUpEnabled; }
            set
            {
                if (isAutoLevelUpEnabled == value)
                {
                    return;
                }
                
                isAutoLevelUpEnabled = value;
                if (isAutoLevelUpEnabled)
                {
                    EnableAutoLevelUp();
                }
                else
                {
                    DisableAutoLevelUp();
                }
                
            }
        }
        
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

        public void ForceLevelUp()
        {
            character.CharacterLevel.ForceLevelUp();
        }

        private void EnableAutoLevelUp()
        {
            autoLevelUpObserver.Enable();
        }

        private void DisableAutoLevelUp()
        {
            autoLevelUpObserver.Disable();
        }
    }
}