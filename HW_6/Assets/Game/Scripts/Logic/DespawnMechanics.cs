using UnityEngine;

namespace Game
{
    public sealed class DespawnMechanics
    {
        private readonly IAtomicEvent death;
        private readonly IAtomicAction<MonoBehaviour> despawn;
        private MonoBehaviour obj;

        public DespawnMechanics(IAtomicEvent death, IAtomicAction<MonoBehaviour> despawn, MonoBehaviour obj)
        {
            this.death = death;
            this.despawn = despawn;
            this.obj = obj;
        }

        public void OnEnable()
        {
            death.Subscribe(OnDespawn);
        }

        public void OnDisable()
        {
            death.Unsubscribe(OnDespawn);
        }

        private void OnDespawn()
        {
            despawn.Invoke(obj);
        }
    }
}