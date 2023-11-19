using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class ContainInBoundsController : MonoBehaviour
    {
        private  LevelBounds levelBounds;
        
        [SerializeField]
        private float offset = 0.01f;

        private void Awake()
        {
            this.levelBounds = FindObjectOfType<LevelBounds>();
        }

        public void CorrectDirection(ref Vector2 direction,in Vector2 position)
        {
            if (position.x - offset < levelBounds.LeftBorder.position.x && direction.x < 0
                || position.x + offset > levelBounds.RightBorder.position.x && direction.x > 0)
            {
                direction.x = 0;
            }
            
        }
    }
}