using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public sealed class CharacterInstaller : EntityInstaller
    {
        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private Entity bulletPrefab;

        [SerializeField]
        private Animator animator;

        protected override void Install(Entity entity)
        {
            entity.AddData(new Position {value = this.transform.position});
            entity.AddData(new Rotation {value = this.transform.rotation});
            entity.AddData(new MoveDirection {value = Vector3.forward});
            entity.AddData(new MoveSpeed {value = 5});
            entity.AddData(new Health {value = 5});
            entity.AddData(new DamagableTag());
            
            entity.AddData(new BulletWeapon
            {
                firePoint = this.firePoint,
                bulletPrefab = this.bulletPrefab
            });


            entity.AddData(new AnimatorView {value = this.animator});
            entity.AddData(new TransformView {value = this.transform});
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}