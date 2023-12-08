using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class UserInfo
    {
        public event Action<string> OnNameChanged;
        public event Action<string> OnDescriptionChanged;
        public event Action<Sprite> OnIconChanged; 
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public Sprite Icon { get; private set; }
        
        public void ChangeName(string name)
        {
            this.Name = name;
            this.OnNameChanged?.Invoke(name);
        }
        
        public void ChangeDescription(string description)
        {
            this.Description = description;
            this.OnDescriptionChanged?.Invoke(description);
        }
        
        public void ChangeIcon(Sprite icon)
        {
            this.Icon = icon;
            this.OnIconChanged?.Invoke(icon);
        }
    }
}