using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM
{
    public sealed class UserPopup : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private UserView viewPrefab;
        [SerializeField] private Button levelUpButton;

        private IUserPresenter userPresenter;
        private UserView userView;

        public void Initialize(IPresenter args)
        {
            if (args is not IUserPresenter userPresenter)
            {
                throw new Exception("Expected IUserPresenter");
            }

            this.userPresenter = userPresenter;
        }

        public void Show()
        {
            gameObject.SetActive(true);

            userView = Instantiate(viewPrefab, container);
            userView.Initialize(userPresenter);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Destroy(userView);
        }
    }
}