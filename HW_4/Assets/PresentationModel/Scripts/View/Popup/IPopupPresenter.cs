namespace MVVM
{
    public interface IPopupPresenter : IPresenter
    {
        public UserPresenter UserPresenter { get; }
        public CharacterStatsPresenter StatsPresenter { get; }
        public CharacterExperiencePresenter ExperiencePresenter { get; }
    }
}