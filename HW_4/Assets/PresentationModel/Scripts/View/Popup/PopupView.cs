using System;
using UnityEngine;

namespace MVVM
{
    public sealed class PopupView : MonoBehaviour
    {
        [SerializeField] private UserPopup userPopup;
        [SerializeField] private CharacterStatsPopup characterStatsPopup;
        [SerializeField] private CharacterExperiencePopup characterExperiencePopup;
        
        public void Show(IPresenter args)
        {
            if (args is not IPopupPresenter popupPresenter)
            {
                throw new Exception("Expected IPopupPresenter");
            }
                
            userPopup.Show(popupPresenter.UserPresenter);
            characterExperiencePopup.Show(popupPresenter.ExperiencePresenter);
            characterStatsPopup.Show(popupPresenter.StatsPresenter);
        }

        public void Hide()
        {
            userPopup.Hide();
            characterStatsPopup.Hide();
            characterExperiencePopup.Hide();
        }
    }
}