using UnityEngine;
using UnityEngine.UI;

namespace MVVM
{
    public sealed class PopupManager : MonoBehaviour
    {
        [SerializeField] private Button showButton;
        [SerializeField] private Button hideButton;
        [SerializeField] private UserPopup userPopup;
        [SerializeField] private CharacterStatsPopup characterStatsPopup;
        [SerializeField] private CharacterExperiencePopup characterExperiencePopup;

        private UserInfo userInfo;
        private UserPresenter userPresenter;

        private Character character;
        private CharacterStatsPresenter characterStatsPresenter;
        private CharacterExperiencePresenter characterExperiencePresenter;

        [Inject]
        private void Construct(User user, Character character)
        {
            userInfo = user.UserInfo;
            this.character = character;
        }

        private void Awake()
        {
            showButton.onClick.AddListener(Show);
            hideButton.onClick.AddListener(Hide);
        }

        private void Show()
        {
            showButton.gameObject.SetActive(false);

            userPresenter = new UserPresenter(userInfo);
            userPopup.Show(userPresenter);

            characterStatsPresenter = new CharacterStatsPresenter(character.CharacterInfo);
            characterStatsPopup.Show(characterStatsPresenter);

            characterExperiencePresenter = new CharacterExperiencePresenter(character.CharacterLevel);
            characterExperiencePopup.Show(characterExperiencePresenter);

            hideButton.gameObject.SetActive(true);
        }

        private void Hide()
        {
            hideButton.gameObject.SetActive(false);

            userPopup.Hide();
            characterStatsPopup.Hide();
            characterExperiencePopup.Hide();

            userPresenter.Dispose();
            characterStatsPresenter.Dispose();
            characterStatsPresenter.Dispose();

            showButton.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            userPresenter?.Dispose();
            characterStatsPresenter?.Dispose();
            characterStatsPresenter?.Dispose();

            showButton.onClick.RemoveListener(Show);
            hideButton.onClick.RemoveListener(Hide);
        }
    }
}