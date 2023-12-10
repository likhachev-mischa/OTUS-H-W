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
            get => currentLevel;
            private set => currentLevel = value;
        }

        public int CurrentExperience
        {
            get => currentExperience;
            private set => currentExperience = value;
        }

        public int RequiredExperience
        {
            get { return 100 * (this.CurrentLevel + 1); }
        }

        [SerializeField] [ReadOnly] private int currentLevel = 1;
        [SerializeField] [ReadOnly] private int currentExperience;

        public void AddExperience(int range)
        {
            var xp = Math.Min(this.CurrentExperience + range, this.RequiredExperience);
            this.CurrentExperience = xp;
            this.OnExperienceChanged?.Invoke(xp);
        }

        public void LevelUp()
        {
            this.CurrentExperience = 0;
            this.CurrentLevel++;
            this.OnLevelUp?.Invoke();
        }

        public bool CanLevelUp()
        {
            return this.CurrentExperience == this.RequiredExperience;
        }
    }
}