using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletFactory : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
        public int InitialCount { get; }
        
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;

        [SerializeField]
        private BulletBoundsManager bulletBoundsManager;

        private ObjectPool<Bullet> bulletPool;

        private BulletCollisionHandler bulletCollisionHandler;
        private BulletDamageHandler bulletDamageHandler;
        
        private void Awake()
        {
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
            bulletBoundsManager.Enable(bullet);
            return true;
        }
        
        public void DespawnBullet(Bullet bullet)
        {
            bulletDamageHandler.Disable(bullet);
            bulletCollisionHandler.Disable(bullet);
            bulletBoundsManager.Disable(bullet);
            bulletPool.RemoveObject(bullet);
        }
        
       
    }
    
}
