using System;

namespace Entities.Components
{
    [Serializable]
    public struct Name
    {
        public string Value { get; private set; }

        public Name(string name)
        {
            Value = name;
        }
    }
}