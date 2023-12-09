using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MVVM
{
    public sealed class UserPopupController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private UserPopup userPopup;

        //private UserPresenter userPresenter;
        [Inject]
        private void Construct(User user)
        {
            var userPresenter = new UserPresenter(user.UserInfo);
            userPopup.Initialize(userPresenter);
        }
        
        private void Awake()
        {
            button.onClick.AddListener(userPopup.Show);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(userPopup.Show);
        }
    }
}