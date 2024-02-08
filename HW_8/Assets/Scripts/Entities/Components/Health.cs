using System;

namespace Entities.Components
{
    [Serializable]
    public sealed class Health
    {
        public int Value;
        public int ReceivedDamage;

        public int InitialValue;
    }
}