using System;
using UnityEngine;

namespace MVVM
{
    public class UserPresenter : IUserPresenter, IDisposable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }

        private readonly UserInfo userInfo;

        public event Action<string> OnNameUpdated;
        public event Action<string> OnDescriptionUpdated;
        public event Action<Sprite> OnIconUpdated;

        public UserPresenter(UserInfo userInfo)
        {
            this.userInfo = userInfo;
            Name = userInfo.Name;
            Description = userInfo.Description;
            Icon = userInfo.Icon;

            userInfo.OnNameChanged += OnNameChanged;
            userInfo.OnDescriptionChanged += OnDescriptionChanged;
            userInfo.OnIconChanged += OnIconChanged;
        }

        private void OnNameChanged(string name)
        {
            Name = name;
            OnNameUpdated?.Invoke(name);
        }

        private void OnDescriptionChanged(string description)
        {
            Description = description;
            OnDescriptionUpdated?.Invoke(description);
        }

        private void OnIconChanged(Sprite icon)
        {
            Icon = icon;
            OnIconUpdated?.Invoke(icon);
        }

        public void Dispose()
        {
            userInfo.OnNameChanged -= OnNameChanged;
            userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            userInfo.OnIconChanged -= OnIconChanged;
        }
    }
}