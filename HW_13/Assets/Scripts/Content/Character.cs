using System;
using AIModule;
using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Engine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sample
{
    [Is(ObjectType.ResourceCollector)]
    public sealed class Character : AtomicObject
    {
        ///Interface 
        [Get(ObjectAPI.Transform)]
        public Transform Transform => this.transform;

        [Get(ObjectAPI.MoveStepRequest)]
        public IAtomicAction<Vector3> MoveStepRequest => this.core.moveComponent.MoveRequest;
        
        [Get(ObjectAPI.LookDirection)]
        public IAtomicVariable<Vector3> LookDirection => this.core.lookComponent.LookDirection;

        [Get(ObjectAPI.ResourceStorage)]
        public ResourceStorage ResourceBag => this.core.resourceStorage;
        
        [Get(ObjectAPI.GatherRequest)]
        public IAtomicAction GatherRequest => this.core.gatherComponent.Request;

        [Get(ObjectAPI.IsGathering)]
        public IAtomicValue<bool> IsGathering => this.view.isGathering;

        ///Core:
        public Character_Core core;
        public Character_View view;
        public Character_AI ai;

        public override void Compose()
        {
            this.core.Compose();
            this.view.Compose(this.core);
            this.ai.Compose(this);
            base.Compose();
        }

        private void Awake()
        {
            this.Compose();
        }

        private void OnEnable()
        {
            this.view.Enable();
        }

        private void OnDisable()
        {
            this.view.Disable();
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            this.core.OnFixedUpdate(deltaTime);
            this.ai.OnFixedUpdate(deltaTime);
        }

        private void LateUpdate()
        {
            this.view.OnLateUpdate(Time.deltaTime);
        }
    }

    [Serializable]
    public sealed class Character_Core : IFixedUpdate
    {
        [Space]
        public MoveComponent moveComponent;

        [Space]
        public LookComponent lookComponent;

        [Space]
        public ActionComponent gatherComponent;

        [Space]
        [FormerlySerializedAs("resouceStorage")]
        public ResourceStorage resourceStorage;

        [Space]
        public Axe axe;

        [SerializeField]
        private ExtractResourceAction extractResourceAction;
        
        public void Compose()
        {
            this.moveComponent.Compose();

            this.gatherComponent.Let(it =>
            {
                it.Condition.Append(new AtomicFunction<bool>(this.resourceStorage.IsNotFull));
                it.Compose();
            });

            this.axe.HitEvent.Subscribe(this.extractResourceAction);

            this.extractResourceAction.Compose(this.resourceStorage, 1.AsValue());
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.moveComponent.IsMoving.Value)
            {
                this.lookComponent.LookDirection.Value = this.moveComponent.MoveDirection.Value;
            }

            this.moveComponent.OnFixedUpdate(deltaTime);
            this.lookComponent.OnFixedUpdate(deltaTime);
        }
    }

    [Serializable]
    public sealed class Character_View : IEnable, IDisable, ILateUpdate
    {
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop");
        private const string ChopAnimEvent = "chop";

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorDispatcher dispatcher;

        private MoveAnimMechanics moveAnimMechanics;
        private AnimatorTriggerMechanics chopAnimatorTriggerMechanics;
        private AnimationEventListener chopAnimListener;
        
        public AtomicFunction<bool> isGathering;

        public void Compose(Character_Core core)
        {
            this.moveAnimMechanics = new MoveAnimMechanics(
                this.animator, IsMovingHash, core.moveComponent.IsMoving
            );
            this.chopAnimatorTriggerMechanics = new AnimatorTriggerMechanics(
                this.animator, ChopAnimHash, core.gatherComponent.Event
            );
            this.chopAnimListener = new AnimationEventListener(
                this.dispatcher, ChopAnimEvent, core.axe.HitAction
            );

            this.isGathering.Compose(() => this.animator.GetBehaviour<ChopSMBehaviour>().IsChopping);
        }

        public void Enable()
        {
            this.chopAnimListener.Enable();
            this.chopAnimatorTriggerMechanics.Enable();
        }

        public void Disable()
        {
            this.chopAnimatorTriggerMechanics.Disable();
            this.chopAnimListener.Disable();
        }

        public void OnLateUpdate(float deltaTime)
        {
            this.moveAnimMechanics.OnUpdate(deltaTime);
        }
    }

    [Serializable]
    public sealed class Character_AI : IFixedUpdate, IEnable, IDisable
    {
        [SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private AIBehaviour behaviour;

        public void Compose(AtomicObject obj)
        {
            this.blackboard.SetObject(BlackboardAPI.Character, obj);
        }

        public void Enable()
        {
            this.behaviour.OnStart();
        }

        public void Disable()
        {
            behaviour.OnStop();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.behaviour.OnUpdate(deltaTime);
        }
    }
}