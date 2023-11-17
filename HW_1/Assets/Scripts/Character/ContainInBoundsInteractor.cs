using UnityEngine;

namespace ShootEmUp
{
    public sealed class ContainInBoundsInteractor
    {
        private readonly LevelBounds levelBounds;
        private float offset = 0.01f;

        public ContainInBoundsInteractor(LevelBounds levelBounds, Vector2 position)
        {
            this.levelBounds = levelBounds;
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