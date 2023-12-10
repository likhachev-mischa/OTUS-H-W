using System;
using UnityEngine;

namespace MVVM
{
    public interface IUserPresenter : IPresenter
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }

        public event Action<string> OnNameUpdated;
        public event Action<string> OnDescriptionUpdated;
        public event Action<Sprite> OnIconUpdated;
    }
}