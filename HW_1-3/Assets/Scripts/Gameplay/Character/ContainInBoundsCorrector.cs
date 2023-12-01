using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class ContainInBoundsCorrector
    {
        [SerializeField] private float offset = 0.01f;

        private LevelBounds levelBounds;

        [Inject]
        private void Construct(LevelBounds levelBounds)
        {
            this.levelBounds = levelBounds;
        }

        public void CorrectDirection(ref Vector2 direction, in Vector2 position)
        {
            if (position.x - offset < levelBounds.LeftBorder.position.x && direction.x < 0
                || position.x + offset > levelBounds.RightBorder.position.x && direction.x > 0)
            {
                direction.x = 0;
            }
        }
    }
}