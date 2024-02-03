using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Entities.Player
{
    [RequireComponent(typeof(PlayerModel))]
    [DefaultExecutionOrder(-100)]
    public sealed class PlayerEntity : MonoEntityBase
    {
        private void Awake()
        {
            PlayerModel model = GetComponent<PlayerModel>();
            Add(new PositionComponent(model.position.transform));
            Add(new CoordinatesComponent(model.position.coordinates));
            Add(new StatsComponent(model.stats));
            Add(new TransformComponent(transform));
            Add(new WeaponComponent(model.attack.weapon));
        }
    }
}