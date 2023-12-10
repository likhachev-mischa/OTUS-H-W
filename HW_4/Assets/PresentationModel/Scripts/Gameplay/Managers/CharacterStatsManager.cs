using UnityEngine;

namespace MVVM
{
    public sealed class CharacterStatsManager : MonoBehaviour
    {
        [Header("Insert Config To Change")] [SerializeField]
        private CharacterStatConfig chosenConfig;

        [Header("Value Change Delta")] [SerializeField]
        private int value;

        private Character character;
        private CharacterStatsProvider statsProvider;

        [Inject]
        private void Construct(CharacterStatsProvider statsProvider, Character character)
        {
            this.statsProvider = statsProvider;
            this.character = character;
        }

        public void AddStat()
        {
            statsProvider.AddStat(character.CharacterInfo, chosenConfig);
        }

        public void RemoveStat()
        {
            statsProvider.RemoveStat(character.CharacterInfo, chosenConfig);
        }

        public void ChangeValue()
        {
            character.CharacterInfo.GetStat(chosenConfig.Name).ChangeValue(value);
        }
    }
}