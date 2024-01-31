using System;
using EcsEngine.Components;
using EcsEngine.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace EcsEngine
{
    public sealed class EcsAdmin : MonoBehaviour
    {
        public static EcsAdmin Instance { get; private set; }

        private EcsWorld _world;
        private EcsWorld _events;
        private IEcsSystems _systems;
        private EntityManager _entityManager;

        private EntityPoolRegistry entityPoolRegistry;

        public EcsEntityBuilder CreateEntity(string worldName = null)
        {
            return new EcsEntityBuilder(_systems.GetWorld(worldName));
        }

        public EcsWorld GetWorld(string worldName = null)
        {
            return worldName switch
            {
                null => _world,
                EcsWorlds.Events => _events,
                _ => throw new Exception($"World with name {worldName} is not found!")
            };
        }

        private void Awake()
        {
            Instance = this;

            _entityManager = new EntityManager();

            _world = new EcsWorld();
            _events = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.AddWorld(_events, EcsWorlds.Events);

            entityPoolRegistry = new EntityPoolRegistry(_world);


            _systems

                //Game Logic:
               
                
                //spawn
                .Add(new ObjectSpawnPoolSystem<Team>()) //units
                .Add(new ObjectSpawnPoolSystem<CreatorEntity>()) //projectiles
                
                .Add(new DeathRequestSystem())
                .Add(new AttackRequestSystem())
                .Add(new MeleeHitRequestSystem())
                .Add(new TakeDamageRequestSystem())
                .Add(new ProjectileCollisionRequestSystem())
                
                .Add(new TargetResetOnDeathSystem())
                
                //initialization
                .Add(new ComponentResetOnSpawnSystem<Health>())
                .Add(new ProjectileMovementInitializationSystem())
                .Add(new TargetSetSystem())
                .Add(new MovementDirectionSetterSystem())
                //movement and targeting
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new TargetReachSystem())
                .Add(new MovementEnablerOnReachedSystem())
                //attack

                .Add(new AttackEventSystem())
                .Add(new CooldownSystem())
                
                //health
                
                .Add(new HealthConditionUpdateSystem())
                .Add(new HealthEmptySystem())
                //death
                
                .Add(new DeathTimerCountdownSystem())
                //after death reset
                
                .Add(new GameOverSystem())

                //Game Listeners:

                //View:
                .Add(new TransformViewSynchronizer())
                .Add(new UnitColorViewSynchronizer())
                .Add(new AnimatorMovementListener())
                
                .Add(new AnimatorAttackListener())
                .Add(new AnimatorTakeDamageListener())
                .Add(new AnimatorDeathListener())
                
                .Add(new VFXBuildingSystem())
                .Add(new VFXUnitSystem())

                //Editor:
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.Events))
#endif
                //Clean Up:
                .Add(new EntityDestructionSystem())
                .Add(new OneFrameEventSystem())
                .DelHere<DeathEvent>()
                .DelHere<SpawnEvent>()
                .DelHere<AttackEvent>();
        }

        private void Start()
        {
            _entityManager.Initialize(_world);
            _systems.Inject(_entityManager);
            _systems.Inject(entityPoolRegistry);
            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy();
                _systems = null;
            }

            // cleanup custom worlds here.

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}