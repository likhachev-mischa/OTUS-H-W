using System;
using DI;
using LoadSystem;
using UnityEngine;

namespace Game.UI
{
    public sealed class MainMenuAdapter : MonoBehaviour
    {
        [SerializeField] private MainMenuView mainMenuView;

        private ApplicationLoader applicationLoader;
        
        [Inject]
        private void Construct(ApplicationLoader applicationLoader)
        {
            this.applicationLoader = applicationLoader;
        }

        private void OnEnable()
        {
            mainMenuView.Initialize();
            mainMenuView.ButtonPressed += LoadApplication;
        }

        private void OnDestroy()
        {
            mainMenuView.ButtonPressed -= LoadApplication;
            mainMenuView.Disable();
        }

        private void LoadApplication()
        {
            applicationLoader.LoadSceneAsync().Forget();
        }
        
    }
}