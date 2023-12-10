using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM
{
    public class UserView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userName;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image icon;

        private IUserPresenter userPresenter;

        public void Initialize(IUserPresenter userPresenter)
        {
            userName.text = userPresenter.Name;
            description.text = userPresenter.Description;
            icon.sprite = userPresenter.Icon;
            this.userPresenter = userPresenter;

            userPresenter.OnNameUpdated += OnNameChanged;
            userPresenter.OnDescriptionUpdated += OnDescriptionChanged;
            userPresenter.OnIconUpdated += OnIconChanged;
        }

        private void OnNameChanged(string name)
        {
            userName.text = name;
        }

        private void OnDescriptionChanged(string description)
        {
            this.description.text = description;
        }

        private void OnIconChanged(Sprite icon)
        {
            this.icon.sprite = icon;
        }

        private void OnDestroy()
        {
            userPresenter.OnNameUpdated -= OnNameChanged;
            userPresenter.OnDescriptionUpdated -= OnDescriptionChanged;
            userPresenter.OnIconUpdated -= OnIconChanged;
        }
    }
}