using DI;
using UnityEngine;

namespace Game
{
    public class CharacterAnimationDispatcher : MonoBehaviour
    {
        private IAtomicAction fireEvent;
        private IAtomicVariable<bool> canPlayAnimation;

        public void Initialize(IAtomicAction fireEvent, IAtomicVariable<bool> canPlayAnimation)
        {
            this.fireEvent = fireEvent;
            this.canPlayAnimation = canPlayAnimation;
        }

        public void ReceiveEvent(string value)
        {
            if (value == "shoot")
            {
                fireEvent.Invoke();
            }
            else if (value == "reset")
            {
                canPlayAnimation.Value = true;
            }
        }
    }
}