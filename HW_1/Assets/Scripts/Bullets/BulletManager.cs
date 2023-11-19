using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    [RequireComponent(typeof(BulletBoundsController))]
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform worldTransform;
        
        private BulletBoundsController bulletBoundsController;

        private ObjectPool<Bullet> bulletPool;

        private BulletCollisionHandler bulletCollisionHandler;
        private BulletDamageHandler bulletDamageHandler;
        
        private void Awake()
        {
            this.bulletBoundsController = GetComponent<BulletBoundsController>();
            
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
            bulletBoundsController.Enable(bullet);
            return true;
        }
        
        public void DespawnBullet(Bullet bullet)
        {
            bulletDamageHandler.Disable(bullet);
            bulletCollisionHandler.Disable(bullet);
            bulletBoundsController.Disable(bullet);
            bulletPool.RemoveObject(bullet);
        }
        
       
    }
    
}
