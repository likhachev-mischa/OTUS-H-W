using System;

namespace MVVM
{
    public interface ICharacterExperiencePresenter : IPresenter
    {
        public int CurrentExperience { get; }
        public int CurrentLevel { get; }
        public bool CanLevelUp { get; }
        public int RequiredExperience { get; }
        public string ExperienceText { get; }
        public string LevelText { get; }
        public event Action<int> OnExperienceChanged;
        public event Action OnNewLevelReached;

        public void LevelUp();
    }
}