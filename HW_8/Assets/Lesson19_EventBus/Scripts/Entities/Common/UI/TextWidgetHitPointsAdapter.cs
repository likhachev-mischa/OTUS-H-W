using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Entities.Common.UI
{
    public sealed class TextWidgetHitPointsAdapter : MonoBehaviour
    {
        [SerializeField]
        private TextWidget textWidget;

        [SerializeField]
        private MonoEntity entity;

        private HitPointsComponent _hitPoints;
        private DeathComponent _death;
        
        private void Awake()
        {
            _hitPoints = entity.Get<HitPointsComponent>();
            _death = entity.Get<DeathComponent>();
        }

        private void OnEnable()
        {
            _hitPoints.OnValueChanged += OnHitPointsChanged;
            _death.OnIsDeadChanged += OnIsDeadChanged;
        }

        private void OnDisable()
        {
            _hitPoints.OnValueChanged -= OnHitPointsChanged;
            _death.OnIsDeadChanged -= OnIsDeadChanged;
        }

        private void Start()
        {
            SetHitPoints();
        }

        private void SetHitPoints()
        {
            textWidget.SetText($"{_hitPoints.Value} / {_hitPoints.MaxHitPoints}");
        }
        
        private void OnHitPointsChanged(int _)
        {
            SetHitPoints();
        }
        
        private void OnIsDeadChanged(bool value)
        {
            gameObject.SetActive(!value);
        }
    }
}