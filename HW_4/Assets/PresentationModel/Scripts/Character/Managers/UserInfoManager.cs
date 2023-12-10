using UnityEngine;

namespace MVVM
{
    public sealed class UserInfoManager : MonoBehaviour
    {
        [SerializeField] private UserInfoConfig userData;
        
        [Space] 
        [SerializeField] private string userName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;

        private User user;

        [Inject]
        private void Construct(User user)
        {
            this.user = user;
            this.user.UserInfo.ChangeName(userData.userName);
            this.user.UserInfo.ChangeDescription(userData.description);
            this.user.UserInfo.ChangeIcon(userData.icon);
        }

        public void ChangeUserInfo()
        {
            user.UserInfo.ChangeName(userData.userName);
            user.UserInfo.ChangeDescription(userData.description);
            user.UserInfo.ChangeIcon(userData.icon);
        }
        
        public void ChangeName()
        {
            user.UserInfo.ChangeName(userName);
        }

        public void ChangeDescription()
        {
            user.UserInfo.ChangeDescription(description);
        }

        public void ChangeIcon()
        {
            user.UserInfo.ChangeIcon(icon);
        }
    }
}