using System;
using UnityEngine;

namespace Lessons.Game
{
    public sealed class KeyboardInput : MonoBehaviour
    {
        public event Action<Vector2Int> OnMove;
        
        private void Update()
        {
            Vector2Int movement = Vector2Int.zero;
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                movement.y += 1;
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                movement.y -= 1;
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                movement.x += 1;
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                movement.x -= 1;
            }
            
            if (movement != Vector2Int.zero)
            {
                OnMove?.Invoke(movement);
            }
        }
    }
}