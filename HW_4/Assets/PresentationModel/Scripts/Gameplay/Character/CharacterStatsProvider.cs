using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterStatsProvider
    {
        [Space] [SerializeField] private CharacterStatConfig[] characterStatConfigRegistry;
        private readonly Dictionary<string, CharacterStat> characterStats = new();

        [Inject]
        private void Construct()
        {
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0, length = characterStatConfigRegistry.Length; i < length; ++i)
            {
                if (characterStats.ContainsKey(characterStatConfigRegistry[i].Name))
                {
                    Debug.LogWarning($"{characterStatConfigRegistry[i].Name} is already registered!");
                    continue;
                }

                characterStats.Add(characterStatConfigRegistry[i].Name,
                    new CharacterStat(characterStatConfigRegistry[i].Name, characterStatConfigRegistry[i].Value));
            }
        }

        public void AddStat(CharacterInfo characterInfo, CharacterStatConfig chosenConfig)
        {
            if (!characterStats.ContainsKey(chosenConfig.Name))
            {
                Debug.LogWarning($"{chosenConfig.Name} config is not registered!");
            }

            characterInfo.AddStat(characterStats[chosenConfig.Name]);
        }

        public void RemoveStat(CharacterInfo characterInfo, CharacterStatConfig chosenConfig)
        {
            if (!characterStats.ContainsKey(chosenConfig.Name))
            {
                Debug.LogWarning($"{chosenConfig.Name} config is not registered!");
            }

            characterInfo.RemoveStat(characterStats[chosenConfig.Name]);
        }
    }
}