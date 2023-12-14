using UnityEngine;
using UnityEngine.UI;

namespace MVVM
{
    public sealed class PopupManager : MonoBehaviour
    {
        [SerializeField] private Button showButton;
        [SerializeField] private Button hideButton;
        [SerializeField] private PopupView popup;

        private UserInfo userInfo;
        private Character character;

        private PopupPresenter popupPresenter;

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

            popupPresenter = new PopupPresenter(userInfo, 
                character.CharacterInfo, character.CharacterLevel);
            popup.Show(popupPresenter);

            hideButton.gameObject.SetActive(true);
        }

        private void Hide()
        {
            hideButton.gameObject.SetActive(false);
            
            popup.Hide();
            popupPresenter.Dispose();

            showButton.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            popupPresenter.Dispose();

            showButton.onClick.RemoveListener(Show);
            hideButton.onClick.RemoveListener(Hide);
        }
    }
}