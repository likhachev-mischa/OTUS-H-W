using System;
using UnityEngine;

namespace MVVM
{
    public class UserPresenter : IUserPresenter, IDisposable
    {
        public string Name { get; private set; }
        public string Description { get; private set;}
        public Sprite Icon { get; private set;}

        private readonly UserInfo userInfo;
        
        public UserPresenter(UserInfo userInfo)
        {
            this.userInfo = userInfo;
            this.Name = userInfo.Name;
            this.Description = userInfo.Description;
            this.Icon = userInfo.Icon;

            userInfo.OnNameChanged += OnNameChanged;
            userInfo.OnDescriptionChanged += OnDescriptionChanged;
            userInfo.OnIconChanged += OnIconChanged;
        }

        private void OnNameChanged(string name)
        {
            this.Name = name;
        }
        
        private void OnDescriptionChanged(string description)
        {
            this.Description = description;
        }

        private void OnIconChanged(Sprite icon)
        {
            this.Icon = icon;
        }

        public void Dispose()
        {
            userInfo.OnNameChanged -= OnNameChanged;
            userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            userInfo.OnIconChanged -= OnIconChanged;
        }
        
    }
}