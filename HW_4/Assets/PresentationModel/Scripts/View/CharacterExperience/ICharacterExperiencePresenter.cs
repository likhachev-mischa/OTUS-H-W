using System;

namespace MVVM
{
    public interface ICharacterExperiencePresenter : IPresenter
    {
        public int CurrentExperience { get; }
        public int CurrentLevel { get; }
        public bool CanLevelUp { get; }
        public int RequiredExperience { get; }
        public event Action<int> OnExperienceChanged;
        public event Action OnNewLevelReached;

        public void LevelUp();
    }
}