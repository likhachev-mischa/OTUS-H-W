using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }
        
        public event Action SpacePressedEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpacePressedEvent?.Invoke();
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.HorizontalDirection = 1;
            }
            else
            {
                this.HorizontalDirection = 0;
            }
        }
        
    }
}