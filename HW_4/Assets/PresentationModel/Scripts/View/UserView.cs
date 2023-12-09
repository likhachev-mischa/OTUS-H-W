using UnityEngine;

namespace MVVM
{
    public class UserView : MonoBehaviour
    {
        private string userName;
        private string description;
        private Sprite icon;

        public void Initialize(IUserPresenter userPresenter)
        {
            userName = userPresenter.Name;
            description = userPresenter.Description;
            icon = userPresenter.Icon;
        }
    }
}