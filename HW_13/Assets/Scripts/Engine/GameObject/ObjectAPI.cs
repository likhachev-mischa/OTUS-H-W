using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace Game.Engine
{
    public static class ObjectAPI
    {
        [Contract(typeof(Transform))]
        public const string Transform = nameof(Transform);

        /// <summary>
        ///     <para>Checks an object is active on scene or not.</para>>
        /// </summary>
        [Contract(typeof(IAtomicValue<bool>))]
        public const string IsActive = nameof(IsActive);
        
        /// <summary>
        ///     <para>Move an object towards direction one frame.</para>>
        /// </summary>
        [Contract(typeof(IAtomicAction<Vector3>))]
        public const string MoveStepRequest = nameof(MoveStepRequest);

        /// <summary>
        ///     <para>Makes a request for resource gatheing.</para>>
        /// </summary>
        [Contract(typeof(IAtomicAction<Vector3>))]
        public const string GatherRequest = nameof(GatherRequest);

        /// <summary>
        ///     <para>Checks a resource gathering process</para>>
        /// </summary>
        [Contract(typeof(IAtomicValue<bool>))]
        public const string IsGathering = nameof(IsGathering);
        
        /// <summary>
        ///     <para>Returns a resource storage of an object.</para>>
        /// </summary>
        [Contract(typeof(ResourceStorage))]
        public const string ResourceStorage = nameof(ResourceStorage);
        
        /// <summary>
        ///     <para>Current look direction.</para>>
        /// </summary>
        [Contract(typeof(IAtomicVariable<Vector3>))]
        public const string LookDirection = nameof(LookDirection);
    }
}