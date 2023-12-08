using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterStat
    {
        public event Action<int> OnValueChanged;

       [ReadOnly]
        public string Name
        {
            get;
            private set;
        }

        public int Value { get; private set; }

        public CharacterStat(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public void ChangeValue(int value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }
    }
}