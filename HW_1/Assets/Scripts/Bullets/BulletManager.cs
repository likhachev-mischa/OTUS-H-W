using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(BulletBoundsCorrector))]
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform worldTransform;

        private ObjectPool<Bullet> bulletPool;

        private BulletBoundsCorrector bulletBoundsCorrector;
        private BulletCollisionHandler bulletCollisionHandler;
        private BulletDamageHandler bulletDamageHandler;

        private void Awake()
        {
            this.bulletBoundsCorrector = GetComponent<BulletBoundsCorrector>();

            bulletPool = new ObjectPool<Bullet>(initialCount, container, prefab, worldTransform);
            bulletPool.Initialize();
            bulletCollisionHandler = new BulletCollisionHandler(this);
            bulletDamageHandler = new BulletDamageHandler();
        }

        public bool SpawnBullet(out Bullet bullet)
        {
            if (!bulletPool.SpawnObject(out bullet))
            {
                return false;
            }

            bulletDamageHandler.Enable(bullet);
            bulletCollisionHandler.Enable(bullet);
            bulletBoundsCorrector.Enable(bullet);
            return true;
        }

        public void DespawnBullet(Bullet bullet)
        {
            bulletDamageHandler.Disable(bullet);
            bulletCollisionHandler.Disable(bullet);
            bulletBoundsCorrector.Disable(bullet);
            bulletPool.RemoveObject(bullet);
        }
    }
}