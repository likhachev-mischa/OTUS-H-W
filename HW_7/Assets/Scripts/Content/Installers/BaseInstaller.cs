using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class BaseInstaller : EntityInstaller
    {
        [Header("STATS")] [SerializeField] private int health = 30;
        [SerializeField] private float despawnTime = 2f;
        [SerializeField] private float meleeRangeOffset = 4f;

        [Header("DATA")] [SerializeField] private BaseVFX vfx;
        [SerializeField] private Teams team;

        protected override void Install(Entity entity)
        {
            entity.AddData(new DamagableTag());
            entity.AddData(new BuildingTag());

            entity.AddData(new Position { value = this.transform.position });
            entity.AddData(new Health { value = health });
            entity.AddData(new Team() { value = team });

            entity.AddData(new HealthCondition() { maxHealth = health });
            entity.AddData(new DespawnTime() { value = despawnTime });
            entity.AddData(new MeleeAttackRangeOffset { value = meleeRangeOffset });

            entity.AddData(new VFXBuildingView() { value = vfx });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}