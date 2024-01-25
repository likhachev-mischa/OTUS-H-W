using System;
using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    [RequireComponent(typeof(Entity))]
    public sealed class BulletCollisionComponent : MonoBehaviour
    {
        private Entity entity;
        
        private void Awake()
        {
            this.entity = this.GetComponent<Entity>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("ON COLLISION ENTER", this);

            if (collision.gameObject.TryGetComponent(out Entity target))
            {
                Debug.Log($"ON COLLISION ENTER {target.Id}", this);

                EcsAdmin.Instance.CreateEntity(EcsWorlds.Events)
                    .Add(new CollisionEnterRequest())
                    .Add(new BulletTag())
                    .Add(new SourceEntity {value = this.entity.Id})
                    .Add(new TargetEntity {value = target.Id})
                    .Add(new Position {value = collision.GetContact(0).point});
            }
        }
    }
}