using System;
using Content;

namespace EcsEngine.Components
{
    [Serializable]
    public struct HealthCondition
    {
        public int maxHealth;
        public HealthState state;
    }
}