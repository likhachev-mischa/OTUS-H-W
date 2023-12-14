using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterInfo
    {
        [SerializeField] private List<CharacterStat> stats = new();
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;

        public void AddStat(CharacterStat stat)
        {
            if (!stats.Contains(stat))
            {
                stats.Add(stat);
                OnStatAdded?.Invoke(stat);
            }
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (stats.Remove(stat))
            {
                OnStatRemoved?.Invoke(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (CharacterStat stat in stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public IReadOnlyList<CharacterStat> GetStats()
        {
            return stats;
        }
    }
}