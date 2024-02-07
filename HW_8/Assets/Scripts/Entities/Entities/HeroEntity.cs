using Entities.Components;
using UI;
using UnityEngine;

namespace Entities.Entities
{
    public sealed class HeroEntity : Entity
    {
        [SerializeField] private HeroView heroView;
        [SerializeField] private HeroComponents components;

        private void OnEnable()
        {
            Add(new Name(components.HeroName));
            
            Add(new HeroVisual() { view = heroView });
            Add(new Target());

            Add(new Health() { Value = components.Stats.Health });
            Add(new Damage() { Value = components.Stats.Damage });

            Add(new WeaponComponent(components.WeaponEffects));
            Add(new ArmorComponent(components.ArmorEffects));
            Add(new PostAttackComponent(components.PostAttackEffects));

            InitializeVisuals();
        }

        private void InitializeVisuals()
        {
            heroView.SetStats(components.Stats.Damage + " / " + components.Stats.Health);
        }
    }
}