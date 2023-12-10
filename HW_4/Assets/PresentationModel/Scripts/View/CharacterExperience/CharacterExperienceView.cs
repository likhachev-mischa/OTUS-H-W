using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM
{
    public sealed class CharacterExperienceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private Image sliderBackground;
        [SerializeField] private Sprite experienceNotFullBackground;
        [SerializeField] private Sprite experienceFullBackground;

        [SerializeField] private Button leveUpButton;
        [SerializeField] private Image buttonBackground;
        [SerializeField] private Sprite buttonDisabledBackground;
        [SerializeField] private Sprite buttonEnabledBackground;

        private ICharacterExperiencePresenter experiencePresenter;

        public void Initialize(ICharacterExperiencePresenter experiencePresenter)
        {
            this.experiencePresenter = experiencePresenter;
            experienceText.text = $"{experiencePresenter.CurrentExperience}/{experiencePresenter.RequiredExperience}";
            levelText.text = $"Level : {this.experiencePresenter.CurrentLevel}";
            if (this.experiencePresenter.CanLevelUp)
            {
                EnableView();
            }
            else
            {
                DisableView();
            }

            this.experiencePresenter.OnExperienceChanged += OnExperienceChanged;
            this.experiencePresenter.OnNewLevelReached += OnNewLevelReached;

            leveUpButton.onClick.AddListener(OnLevelUp);
        }

        private void DisableView()
        {
            sliderBackground.sprite = experienceNotFullBackground;
            buttonBackground.sprite = buttonDisabledBackground;
            leveUpButton.interactable = false;
        }

        private void EnableView()
        {
            sliderBackground.sprite = experienceFullBackground;
            buttonBackground.sprite = buttonEnabledBackground;
            leveUpButton.interactable = true;
        }

        private void OnExperienceChanged(int value)
        {
            experienceText.text = $"{value}/{experiencePresenter.RequiredExperience}";
        }

        private void OnNewLevelReached()
        {
            EnableView();
        }

        private void OnLevelUp()
        {
            DisableView();
            experiencePresenter.LevelUp();
            levelText.text = $"Level : {this.experiencePresenter.CurrentLevel}";
        }

        private void OnDestroy()
        {
            leveUpButton.onClick.RemoveListener(OnLevelUp);
            experiencePresenter.OnExperienceChanged -= OnExperienceChanged;
            experiencePresenter.OnNewLevelReached -= OnNewLevelReached;
        }
    }
}