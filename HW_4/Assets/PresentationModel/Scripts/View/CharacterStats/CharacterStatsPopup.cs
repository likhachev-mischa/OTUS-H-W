using System;
using UnityEngine;

namespace MVVM
{
    public sealed class CharacterStatsPopup : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private CharacterStatsView characterStatsViewPrefab;

        private CharacterStatsView characterStatsView;

        public void Show(IPresenter args)
        {
            if (args is not ICharacterStatsPresenter characterPresenter)
            {
                throw new Exception("Expected ICharacterPresenter");
            }

            gameObject.SetActive(true);
            characterStatsView = Instantiate(characterStatsViewPrefab, container);
            characterStatsView.Initialize(characterPresenter);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Destroy(characterStatsView.gameObject);
        }
    }
}