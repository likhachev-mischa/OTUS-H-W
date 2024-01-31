using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class ArrowInstaller : EntityInstaller
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private int damage = 1;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new ProjectileTag());
            entity.AddData(new CreatorEntity());
            
            entity.AddData(new Position { value = this.transform.position });
            entity.AddData(new Rotation { value = this.transform.rotation });
            entity.AddData(new MoveDirection { value = Vector3.forward });
            entity.AddData(new MoveState() { canMove = true });
            entity.AddData(new MoveSpeed { value = moveSpeed });
            
            entity.AddData(new Damage() { value = damage });
            entity.AddData(new Team());
            
            entity.AddData(new TransformView { value = this.transform });
        }

        protected override void Dispose(Entity entity)
        {

        }
    }
}