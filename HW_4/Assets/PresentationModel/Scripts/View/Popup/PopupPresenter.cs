using System;

namespace MVVM
{
    public sealed class PopupPresenter : IPopupPresenter, IDisposable
    {
        public UserPresenter UserPresenter { get; }
        public CharacterStatsPresenter StatsPresenter { get; }
        public CharacterExperiencePresenter ExperiencePresenter { get; }

        public PopupPresenter(UserInfo userInfo, CharacterInfo characterInfo, CharacterLevel characterLevel)
        {
            this.UserPresenter = new UserPresenter(userInfo);
            this.StatsPresenter = new CharacterStatsPresenter(characterInfo);
            this.ExperiencePresenter = new CharacterExperiencePresenter(characterLevel);
        }

        public void Dispose()
        {
            this.UserPresenter.Dispose();
            this.StatsPresenter.Dispose();
            this.ExperiencePresenter.Dispose();
        }
    }
}