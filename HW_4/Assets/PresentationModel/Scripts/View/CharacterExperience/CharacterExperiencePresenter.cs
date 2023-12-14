using System;

namespace MVVM
{
    public sealed class CharacterExperiencePresenter : ICharacterExperiencePresenter, IDisposable
    {
        public int CurrentExperience { get; private set; }
        public int CurrentLevel { get; private set; }
        public bool CanLevelUp { get; private set; }
        public int RequiredExperience { get; private set; }
        public string ExperienceText { get; private set; }
        public string LevelText { get; private set; }
        public event Action<int> OnExperienceChanged;
        public event Action OnNewLevelReached;

        private readonly CharacterLevel characterLevel;

        public CharacterExperiencePresenter(CharacterLevel characterLevel)
        {
            this.characterLevel = characterLevel;
            CurrentExperience = this.characterLevel.CurrentExperience;
            CurrentLevel = this.characterLevel.CurrentLevel;
            RequiredExperience = this.characterLevel.RequiredExperience;
            CanLevelUp = this.characterLevel.CanLevelUp();
            UpdateText();
            characterLevel.OnExperienceChanged += OnExperienceChangedListener;
        }

        private void OnExperienceChangedListener(int value)
        {
            CurrentExperience = value;
            UpdateText();
            OnExperienceChanged?.Invoke(value);
            if (characterLevel.CanLevelUp())
            {
                OnNewLevelReached?.Invoke();
            }
        }

        public void LevelUp()
        {
            characterLevel.LevelUp();
            CanLevelUp = characterLevel.CanLevelUp();
            RequiredExperience = characterLevel.RequiredExperience;
            CurrentLevel = characterLevel.CurrentLevel;
            CurrentExperience = characterLevel.CurrentExperience;
            UpdateText();
            OnExperienceChanged?.Invoke(characterLevel.CurrentExperience);
        }

        private void UpdateText()
        {
            LevelText = $"Level : {CurrentLevel}";
            ExperienceText = $"{CurrentExperience}/{RequiredExperience}";
        }

        public void Dispose()
        {
            characterLevel.OnExperienceChanged -= OnExperienceChangedListener;
        }
    }
}