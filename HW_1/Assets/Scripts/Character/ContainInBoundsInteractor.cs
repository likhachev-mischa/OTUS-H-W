using UnityEngine;

namespace ShootEmUp
{
    public class ContainInBoundsInteractor
    {
        private readonly LevelBounds levelBounds;

        public ContainInBoundsInteractor(LevelBounds levelBounds, Vector2 position)
        {
            this.levelBounds = levelBounds;
        }
        public void CorrectDirection(ref Vector2 direction,in Vector2 position)
        {
            if (position.x < levelBounds.LeftBorder.position.x && direction.x < 0
                || position.x > levelBounds.RightBorder.position.x && direction.x > 0)
            {
                direction.x = 0;
            }
            
        }
    }
}