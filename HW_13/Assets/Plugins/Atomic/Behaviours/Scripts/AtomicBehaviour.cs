using System.Collections.Generic;
using Atomic.Objects;
using Sirenix.OdinInspector;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

namespace Atomic.Behaviours
{
    public class AtomicBehaviour : AtomicObject, IAtomicBehaviour,
        IEnable,
        IDisable,
        IUpdate,
        IFixedUpdate,
        ILateUpdate
    {
        [ShowInInspector, ReadOnly, HideInEditorMode, PropertyOrder(151)]
        public bool IsEnable => this.isEnable;

        [Title("Logic"), PropertySpace, PropertyOrder(150)]
        [ShowInInspector, HideInEditorMode]
        private readonly List<ILogic> allLogics = new();
        
        private readonly List<IEnable> enables = new();
        private readonly List<IDisable> disables = new();
        private readonly List<IUpdate> updates = new();
        private readonly List<IFixedUpdate> fixedUpdates = new();
        private readonly List<ILateUpdate> lateUpdates = new();

        private readonly List<IEnable> _enableCache = new();
        private readonly List<IDisable> _disableCache = new();
        private readonly List<IUpdate> _updateCache = new();
        private readonly List<IFixedUpdate> _fixedUpdateCache = new();
        private readonly List<ILateUpdate> _lateUpdateCache = new();

        private bool isEnable;

        public void AddLogic(ILogic target)
        {
            if (target == null)
            {
                return;
            }

            this.allLogics.Add(target);

            if (target is IEnable enable)
            {
                this.enables.Add(enable);

                if (this.isEnable)
                {
                    enable.Enable();
                }
            }

            if (target is IDisable disable)
            {
                this.disables.Add(disable);
            }

            if (target is IUpdate update)
            {
                this.updates.Add(update);
            }

            if (target is IFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Add(fixedUpdate);
            }

            if (target is ILateUpdate lateUpdate)
            {
                this.lateUpdates.Add(lateUpdate);
            }
        }

        public void RemoveLogic(ILogic target)
        {
            if (target == null)
            {
                return;
            }

            if (!this.allLogics.Remove(target))
            {
                return;
            }

            if (target is IEnable enable)
            {
                this.enables.Remove(enable);
            }

            if (target is IUpdate tickable)
            {
                this.updates.Remove(tickable);
            }

            if (target is IFixedUpdate fixedTickable)
            {
                this.fixedUpdates.Remove(fixedTickable);
            }

            if (target is ILateUpdate lateTickable)
            {
                this.lateUpdates.Remove(lateTickable);
            }

            if (target is IDisable disable)
            {
                if (this.isEnable)
                {
                    disable.Disable();
                }
            }
        }

        public ILogic[] LogicElements()
        {
            return this.allLogics.ToArray();
        }

        public int LogicElementsNonAlloc(ILogic[] results)
        {
            int i = 0;

            foreach (var element in this.allLogics)
            {
                results[i++] = element;
            }

            return i;
        }

        public IList<ILogic> LogicElementsUnsafe()
        {
            return this.allLogics;
        }

        public void Enable()
        {
            this.isEnable = true;

            if (this.enables.Count == 0)
            {
                return;
            }

            _enableCache.Clear();
            _enableCache.AddRange(this.enables);

            for (int i = 0, count = _enableCache.Count; i < count; i++)
            {
                IEnable enable = _enableCache[i];
                enable.Enable();
            }
        }

        public void Disable()
        {
            if (this.disables.Count == 0)
            {
                return;
            }

            _disableCache.Clear();
            _disableCache.AddRange(this.disables);

            for (int i = 0, count = _disableCache.Count; i < count; i++)
            {
                IDisable disable = _disableCache[i];
                disable.Disable();
            }

            this.isEnable = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (this.updates.Count == 0)
            {
                return;
            }

            _updateCache.Clear();
            _updateCache.AddRange(this.updates);

            for (int i = 0, count = _updateCache.Count; i < count; i++)
            {
                IUpdate update = _updateCache[i];
                update.OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.fixedUpdates.Count == 0)
            {
                return;
            }

            _fixedUpdateCache.Clear();
            _fixedUpdateCache.AddRange(this.fixedUpdates);

            for (int i = 0, count = _fixedUpdateCache.Count; i < count; i++)
            {
                IFixedUpdate fixedUpdate = _fixedUpdateCache[i];
                fixedUpdate.OnFixedUpdate(deltaTime);
            }
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (this.lateUpdates.Count == 0)
            {
                return;
            }

            _lateUpdateCache.Clear();
            _lateUpdateCache.AddRange(this.lateUpdates);

            for (int i = 0, count = _lateUpdateCache.Count; i < count; i++)
            {
                ILateUpdate lateUpdate = _lateUpdateCache[i];
                lateUpdate.OnLateUpdate(deltaTime);
            }
        }
    }
}