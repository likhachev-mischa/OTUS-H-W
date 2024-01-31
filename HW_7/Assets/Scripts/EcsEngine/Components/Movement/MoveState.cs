using System;

namespace EcsEngine.Components
{
    [Serializable]
    public struct MoveState
    {
        public bool canMove;
        public bool isMoving;
    }
}