using System;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button button;

        [SerializeField] private TextMeshProUGUI menuText;
        [SerializeField] private Color startTextColor;
        [SerializeField] private Color endTextColor;

        public event Action ButtonPressed;

        private Tween tween;

        public void Initialize()
        {
            button.onClick.AddListener(OnButtonPressed);

            Tween rotationTween = Tween.ShakeLocalRotation(menuText.gameObject.transform,
                strength: new Vector3(0, 0, 16),
                int.MaxValue, frequency: 0.05f);

            tween = Tween.Custom(startTextColor, endTextColor, duration: 10,
                onValueChange: newVal => menuText.color = newVal,
                cycles: int.MaxValue, cycleMode: CycleMode.Rewind);
        }

        public void Disable()
        {
            button.onClick.RemoveListener(OnButtonPressed);
            tween.Stop();
        }

        private void OnButtonPressed()
        {
            ButtonPressed?.Invoke();
        }
    }
}