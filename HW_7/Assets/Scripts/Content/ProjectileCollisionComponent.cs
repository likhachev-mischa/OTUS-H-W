using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    [RequireComponent(typeof(Entity))]
    public sealed class ProjectileCollisionComponent : MonoBehaviour
    {
        private Entity entity;
        
        private void Awake()
        {
            this.entity = this.GetComponent<Entity>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Entity target))
            {
                EcsAdmin.Instance.CreateEntity(EcsWorlds.Events)
                    .Add(new CollisionEnterRequest())
                    .Add(new ProjectileTag())
                    .Add(new SourceEntity {value = this.entity.Id})
                    .Add(new TargetEntity {value = target.Id})
                    .Add(new Position {value = collision.GetContact(0).point});
            }
        }
    }
}