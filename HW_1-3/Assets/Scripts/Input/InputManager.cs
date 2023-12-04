using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : IGameUpdateListener
    {
        public float MoveDirection { get; private set; }

        public event Action FireEvent;

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireEvent?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveDirection = 1;
            }
            else
            {
                MoveDirection = 0;
            }
        }
    }
}