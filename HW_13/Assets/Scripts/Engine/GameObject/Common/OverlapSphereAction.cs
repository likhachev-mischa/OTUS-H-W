using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class OverlapSphereAction : IAtomicAction
    {
        private static readonly Collider[] buffer = new Collider[32];

        private IAtomicValue<Vector3> hitCenter;
        private IAtomicValue<float> hitRadius;
        private IAtomicValue<LayerMask> layerMask;

        private IAtomicFunction<IAtomicObject, bool> hitCondition;
        private IAtomicAction<IAtomicObject> hitAction;

        public void Compose(
            IAtomicValue<Vector3> hitPoint,
            IAtomicValue<float> hitRadius,
            IAtomicValue<LayerMask> layerMask, 
            IAtomicFunction<IAtomicObject, bool> hitCondition,
            IAtomicAction<IAtomicObject> hitAction
        )
        {
            this.hitCenter = hitPoint;
            this.hitRadius = hitRadius;
            this.layerMask = layerMask;
            this.hitCondition = hitCondition;
            this.hitAction = hitAction;
        }

        [Button]
        public void Invoke()
        {
            Vector3 hitPosition = this.hitCenter.Value;
            float hitRadius = this.hitRadius.Value;
            
            int size = Physics.OverlapSphereNonAlloc(hitPosition, hitRadius, buffer, this.layerMask.Value);

            for (int i = 0; i < size; i++)
            {
                Collider collider = buffer[i];
                if (collider.TryGetComponent(out IAtomicObject target) && this.hitCondition.Invoke(target))
                {
                    this.hitAction.Invoke(target);
                    return;
                }
            }
        }
    }
}