using UnityEngine;

namespace MVVM
{
    public sealed class User : MonoBehaviour
    {
        public UserInfo UserInfo
        {
            get { return userInfo; }
        }

        [SerializeField] private UserInfo userInfo = new();
    }
}