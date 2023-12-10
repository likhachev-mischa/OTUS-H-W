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

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public string Description
        {
            get { return description; }
            private set { description = value; }
        }

        public Sprite Icon
        {
            get { return icon; }
            private set { icon = value; }
        }

        [SerializeField] [ReadOnly] private string name;
        [SerializeField] [ReadOnly] private string description;
        [SerializeField] [ReadOnly] private Sprite icon;

        public void ChangeName(string name)
        {
            Name = name;
            OnNameChanged?.Invoke(name);
        }

        public void ChangeDescription(string description)
        {
            Description = description;
            OnDescriptionChanged?.Invoke(description);
        }

        public void ChangeIcon(Sprite icon)
        {
            Icon = icon;
            OnIconChanged?.Invoke(icon);
        }
    }
}