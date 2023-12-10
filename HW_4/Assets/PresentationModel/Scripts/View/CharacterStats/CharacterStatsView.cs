using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MVVM
{
    public sealed class CharacterStatsView : MonoBehaviour
    {
        [SerializeField] private Transform statsContainer;
        [SerializeField] private TextMeshProUGUI characterStatTextPrefab;

        private ICharacterStatsPresenter characterStatsPresenter;
        private Dictionary<CharacterStat, TextMeshProUGUI> statDisplays = new();

        public void Initialize(ICharacterStatsPresenter characterStatsPresenter)
        {
            this.characterStatsPresenter = characterStatsPresenter;

            characterStatsPresenter.OnStatAdded += OnStatAdded;
            characterStatsPresenter.OnStatRemoved += OnStatRemoved;
            characterStatsPresenter.OnStatChanged += OnStatChanged;


            for (var index = 0; index < characterStatsPresenter.CharacterStats.Count; index++)
            {
                CharacterStat characterStat = characterStatsPresenter.CharacterStats[index];
                SpawnText(characterStat);
            }
        }

        private void OnStatAdded(CharacterStat stat)
        {
            SpawnText(stat);
        }

        private void OnStatRemoved(CharacterStat stat)
        {
            Destroy(statDisplays[stat].gameObject);
            statDisplays.Remove(stat);
        }

        private void OnStatChanged(CharacterStat stat, int value)
        {
            statDisplays[stat].text = $"{stat.Name} : {value}";
        }

        private void SpawnText(CharacterStat characterStat)
        {
            TextMeshProUGUI text = Instantiate(characterStatTextPrefab, statsContainer);
            text.text = $"{characterStat.Name} : {characterStat.Value}";
            statDisplays.Add(characterStat, text);
        }

        private void OnDestroy()
        {
            statDisplays.Clear();
            characterStatsPresenter.OnStatAdded -= OnStatAdded;
            characterStatsPresenter.OnStatRemoved -= OnStatRemoved;
            characterStatsPresenter.OnStatChanged -= OnStatChanged;
        }
    }
}