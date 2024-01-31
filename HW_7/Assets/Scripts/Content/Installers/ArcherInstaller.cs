using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class ArcherInstaller : EntityInstaller
    {
        [Header("STATS")] [SerializeField] private int health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float despawnTime;
        
        [Header("DATA")]
        [SerializeField] private UnitColor unitColor;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Entity arrowPrefab;
        [SerializeField] private UnitVFX vfx;

        private Entity entity;

        private void Awake()
        {
            entity = gameObject.GetComponent<Entity>();
        }

        public void LaunchArrow()
        {
            EcsAdmin.Instance.CreateEntity(EcsWorlds.Events)
                .Add(new SpawnRequest())
                .Add(new Position() { value = firePoint.position })
                .Add(new Rotation() { value = Quaternion.Euler(Vector3.zero) })
                .Add(new Prefab() { value = arrowPrefab })
                .Add(new CreatorEntity() { value = entity.Id });
        }

        protected override void Install(Entity entity)
        {
            entity.AddData(new UnitTag());
            entity.AddData(new RangeTag());
            entity.AddData(new DamagableTag());

            entity.AddData(new Position { value = this.transform.position });
            entity.AddData(new Rotation { value = this.transform.rotation });
            entity.AddData(new MoveDirection { value = Vector3.zero });
            entity.AddData(new MoveState() { canMove = true });
            entity.AddData(new MoveSpeed { value = moveSpeed });
            
            entity.AddData(new Health { value = health });
            entity.AddData(new Team() { value = unitColor.Team });
            entity.AddData(new AttackRange() { value = attackRange });
            entity.AddData(new AttackCooldown() { value = attackCooldown });

            entity.AddData(new DespawnTime() { value = despawnTime });

            entity.AddData(new TransformView { value = this.transform });
            entity.AddData(new ColorView() { value = unitColor });
            entity.AddData(new AnimatorView() { value = animator });
            entity.AddData(new VFXUnitView() { value = vfx });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}