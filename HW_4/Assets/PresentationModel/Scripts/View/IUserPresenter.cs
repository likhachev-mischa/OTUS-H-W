using UnityEngine;

namespace MVVM
{
    public interface IUserPresenter : IPresenter
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
    }
}