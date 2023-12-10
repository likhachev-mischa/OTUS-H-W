namespace MVVM
{
    public sealed class CharacterAutoLevelUpObserver
    {
        private readonly Character character;

        public CharacterAutoLevelUpObserver(Character character)
        {
            this.character = character;
        }

        public void Enable()
        {
            character.CharacterLevel.OnExperienceChanged += OnXPChanged;
        }

        public void Disable()
        {
            character.CharacterLevel.OnExperienceChanged -= OnXPChanged;
        }

        private void OnXPChanged(int _)
        {
            if (character.CharacterLevel.CanLevelUp())
            {
                character.CharacterLevel.LevelUp();
            }
        }
    }
}