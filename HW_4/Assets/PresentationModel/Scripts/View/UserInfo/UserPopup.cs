using System;
using UnityEngine;

namespace MVVM
{
    public sealed class UserPopup : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private UserView userViewPrefab;

        private UserView userView;

        public void Show(IPresenter args)
        {
            if (args is not IUserPresenter userPresenter)
            {
                throw new Exception("Expected IUserPresenter");
            }

            gameObject.SetActive(true);

            userView = Instantiate(userViewPrefab, container);
            userView.Initialize(userPresenter);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Destroy(userView.gameObject);
        }
    }
}