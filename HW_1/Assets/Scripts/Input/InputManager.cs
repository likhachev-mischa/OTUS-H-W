using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float MoveDirection { get; private set; }

        public event Action FireEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireEvent?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.MoveDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.MoveDirection = 1;
            }
            else
            {
                this.MoveDirection = 0;
            }
        }
    }
}