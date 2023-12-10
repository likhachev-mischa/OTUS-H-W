using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterLevel
    {
        public event Action OnLevelUp;
        public event Action<int> OnExperienceChanged;

        public int CurrentLevel
        {
            get { return currentLevel; }
            private set { currentLevel = value; }
        }

        public int CurrentExperience
        {
            get { return currentExperience; }
            private set { currentExperience = value; }
        }

        public int RequiredExperience
        {
            get { return 100 * (CurrentLevel + 1); }
        }

        [SerializeField] [ReadOnly] private int currentLevel = 1;
        [SerializeField] [ReadOnly] private int currentExperience;

        public void AddExperience(int range)
        {
            int xp = Math.Min(CurrentExperience + range, RequiredExperience);
            CurrentExperience = xp;
            OnExperienceChanged?.Invoke(xp);
        }

        public void LevelUp()
        {
            CurrentExperience = 0;
            CurrentLevel++;
            OnLevelUp?.Invoke();
        }

        public bool CanLevelUp()
        {
            return CurrentExperience == RequiredExperience;
        }
    }
}