using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVM
{
    public sealed class CharacterStatsPresenter : ICharacterStatsPresenter,IDisposable
    {
        public List<CharacterStat> CharacterStats { get; }
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;
        public event Action<CharacterStat, int> OnStatChanged; 

        private readonly CharacterInfo characterInfo;
        public CharacterStatsPresenter(CharacterInfo characterInfo)
        {
            this.CharacterStats = characterInfo.GetStats().ToList();
            this.characterInfo = characterInfo;

            for (var index = 0; index < CharacterStats.Count; index++)
            {
                CharacterStat characterStat = CharacterStats[index];
                characterStat.OnValueChanged += OnCharacterStatChangedListener;
            }

            characterInfo.OnStatAdded += OnStatAddedListener;
            characterInfo.OnStatRemoved += OnStatRemovedListener;
        }

        private void OnStatAddedListener(CharacterStat stat)
        {
            CharacterStats.Add(stat);
            stat.OnValueChanged += OnCharacterStatChangedListener;
            OnStatAdded?.Invoke(stat);
        }

        private void OnStatRemovedListener(CharacterStat stat)
        {
            CharacterStats.Remove(stat);
            stat.OnValueChanged -= OnCharacterStatChangedListener;
            OnStatRemoved?.Invoke(stat);
        }

        private void OnCharacterStatChangedListener(CharacterStat stat,int value)
        {
            OnStatChanged?.Invoke(stat,value);
        }

        public void Dispose()
        {
            for (var index = 0; index < CharacterStats.Count; index++)
            {
                CharacterStat characterStat = CharacterStats[index];
                characterStat.OnValueChanged -= OnCharacterStatChangedListener;
            }

            characterInfo.OnStatAdded -= OnStatAddedListener;
            characterInfo.OnStatRemoved -= OnStatRemovedListener;
        }
    }
}