using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;

namespace Game.Engine
{
    [Serializable, InlineProperty]
    public sealed class ExtractResourceAction : IAtomicAction<IAtomicObject>
    {
        private ResourceStorage myStorage;
        private IAtomicValue<int> extractCount;

        public void Compose(ResourceStorage myStorage, IAtomicValue<int> extractCount)
        {
            this.myStorage = myStorage;
            this.extractCount = extractCount;
        }

        [Button]
        public void Invoke(IAtomicObject target)
        {
            if (target.TryGet(ObjectAPI.ResourceStorage, out ResourceStorage otherStorage) &&
                otherStorage.ExtractResources(this.extractCount.Value))
            {
                this.myStorage.PutResources(this.extractCount.Value);
            }
        }
    }
}