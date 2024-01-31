using System;

namespace EcsEngine.Components
{
    [Serializable]
    public struct AttackCooldown
    {
        public float value;
        public float currentTimer;
        public bool onCooldown;
    }
}