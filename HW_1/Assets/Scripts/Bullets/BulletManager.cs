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
        [SerializeField] private GameManager gameManager;
        

        private ObjectPool<Bullet> bulletPool;

        private BulletBoundsCorrector bulletBoundsCorrector;


        private void Awake()
        {
            this.bulletBoundsCorrector = GetComponent<BulletBoundsCorrector>();

            bulletPool = new ObjectPool<Bullet>(initialCount, container, prefab, worldTransform);
            bulletPool.Initialize();
        }

        public bool SpawnBullet(out Bullet bullet)
        {
            if (!bulletPool.SpawnObject(out bullet))
            {
                return false;
            }

            bullet.SetBulletManager(this);
            bullet.SetManager(gameManager);
            bullet.Enable();
            bulletBoundsCorrector.Enable(bullet);
            return true;
        }

        public void DespawnBullet(Bullet bullet)
        {
            bullet.Disable();
            bulletBoundsCorrector.Disable(bullet);
            bulletPool.RemoveObject(bullet);
        }
    }
}