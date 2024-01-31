using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public sealed class SwordsmanInstaller : EntityInstaller
    {
        [Header("STATS")] [SerializeField] private int health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int damage;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float despawnTime;
        
        [Header("DATA")]
        [SerializeField] private UnitColor unitColor;
        [SerializeField] private Animator animator;
        [SerializeField] private UnitVFX vfx;

        private Entity entity;

        private void Awake()
        {
            entity = gameObject.GetComponent<Entity>();
        }
        public void Hit()
        {
            if (!entity.TryGetData(out TargetEntity targetEntity))
            {
                return;
            }

            EcsAdmin.Instance.CreateEntity(EcsWorlds.Events)
                .Add(new MeleeHitRequest())
                .Add(new SourceEntity { value = this.entity.Id })
                .Add(new TargetEntity { value = targetEntity.value });
        }

        protected override void Install(Entity entity)
        {
            entity.AddData(new UnitTag());
            entity.AddData(new MeleeTag());
            entity.AddData(new DamagableTag());

            entity.AddData(new Position { value = this.transform.position });
            entity.AddData(new Rotation { value = this.transform.rotation });
            entity.AddData(new MoveDirection { value = Vector3.zero });
            entity.AddData(new MoveState() { canMove = true });
            entity.AddData(new MoveSpeed { value = moveSpeed });
            
            entity.AddData(new Health { value = health });
            entity.AddData(new Team() { value = unitColor.Team });
            entity.AddData(new Damage() { value = damage });
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