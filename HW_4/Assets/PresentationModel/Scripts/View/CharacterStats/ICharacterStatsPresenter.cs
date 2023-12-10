using System;
using System.Collections.Generic;

namespace MVVM
{
    public interface ICharacterStatsPresenter : IPresenter
    {
        public List<CharacterStat> CharacterStats { get; }

        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;
        public event Action<CharacterStat, int> OnStatChanged;
    }
}