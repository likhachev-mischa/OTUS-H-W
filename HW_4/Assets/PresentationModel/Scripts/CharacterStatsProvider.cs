using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MVVM
{
    public sealed class CharacterStatsProvider : MonoBehaviour
    {
        [SerializeField] private Character character;

        [SerializeField] private CharacterStatConfig[] characterStatConfigRegistry;
        private readonly Dictionary<string, CharacterStat> characterStats = new();

        [Header("Insert Config To Add/Remove To Character")]
        [SerializeField] private CharacterStatConfig chosenConfig;
        

        private void Awake()
        {
            for (int i = 0, length = characterStatConfigRegistry.Length; i < length; ++i)
            {
                if (characterStats.ContainsKey(characterStatConfigRegistry[i].Name))
                {
                    Debug.LogWarning($"{characterStatConfigRegistry[i].Name} is already registered!");
                    return;
                }
                
                characterStats.Add(characterStatConfigRegistry[i].Name,
                    new CharacterStat(characterStatConfigRegistry[i].Name, characterStatConfigRegistry[i].Value));
            }
        }
        
        public void AddStat()
        {
            if (!characterStats.ContainsKey(chosenConfig.Name))
            {
                Debug.LogWarning($"{chosenConfig.Name} config is not registered!");
            }
            character.CharacterInfo.AddStat(characterStats[chosenConfig.Name]);
        }

        public void RemoveStat()
        {
            if (!characterStats.ContainsKey(chosenConfig.Name))
            {
                Debug.LogWarning($"{chosenConfig.Name} config is not registered!");
            }
            character.CharacterInfo.RemoveStat(characterStats[chosenConfig.Name]);
        }
        

    }
}