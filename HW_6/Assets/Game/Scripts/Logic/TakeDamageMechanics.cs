using UnityEngine;

namespace Game
{
    public sealed class TakeDamageMechanics
    {
        private readonly IAtomicVariable<int> hitPoints;
        private readonly IAtomicEvent<int> takeDamage;
        private readonly IAtomicAction death;

        public TakeDamageMechanics(IAtomicVariable<int> hitPoints, IAtomicEvent<int> takeDamage, IAtomicAction death)
        {
            this.hitPoints = hitPoints;
            this.takeDamage = takeDamage;
            this.death = death;
        }

        public void OnEnable()
        {
            takeDamage.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            takeDamage.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int damage)
        {
            var hitPoint = hitPoints.Value - damage;
            hitPoints.Value = Mathf.Max(0, hitPoint);

            if (hitPoints.Value == 0)
            {
                death?.Invoke();
            }
        }
    }
}