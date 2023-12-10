using System;
using UnityEngine;

namespace MVVM
{
    public sealed class CharacterExperiencePopup : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private CharacterExperienceView experienceViewPrefab;

        private CharacterExperienceView experienceView;
        
        public void Show(IPresenter args)
        {
            if (args is not ICharacterExperiencePresenter experiencePresenter)
            {
                throw new Exception("Expected ICharacterExperiencePresenter");
            }

            gameObject.SetActive(true);
            experienceView = Instantiate(experienceViewPrefab, container);
            experienceView.Initialize(experiencePresenter);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Destroy(experienceView.gameObject);
        }
        
    }
}