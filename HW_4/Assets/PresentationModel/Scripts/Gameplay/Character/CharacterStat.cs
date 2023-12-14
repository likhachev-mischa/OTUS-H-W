using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterStat
    {
        [SerializeField] [ReadOnly] private string name;
        [SerializeField] [ReadOnly] private int value;

        public event Action<CharacterStat, int> OnValueChanged;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public int Value
        {
            get { return value; }
            private set { this.value = value; }
        }

        public CharacterStat(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(this, value);
        }
    }
}