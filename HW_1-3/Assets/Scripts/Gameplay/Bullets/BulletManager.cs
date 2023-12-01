using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private GameObject prefab;
        [SerializeField] private ServiceLocator serviceLocator;
        
        
        private Transform container;
        private Transform worldTransform;
        private ObjectPool<Bullet> bulletPool;
        private BulletBoundsCorrector bulletBoundsCorrector;

        [Inject]
        private void Construct(World world, BulletBoundsCorrector bulletBoundsCorrector)
        {
            this.worldTransform = world.transform;
            this.bulletBoundsCorrector = bulletBoundsCorrector;
        }

        private void Awake()
        {
            var pool = new GameObject("Bullet Pool");
            pool.transform.SetParent(this.transform);
            pool.SetActive(false);
            this.container = pool.transform;
            bulletPool = new ObjectPool<Bullet>(initialCount, container, prefab,
                worldTransform, serviceLocator);
            bulletPool.Initialize();
        }

        public bool SpawnBullet(out Bullet bullet)
        {
            if (!bulletPool.SpawnObject(out bullet))
            {
                return false;
            }
            
            bullet.Enable();
            bullet.OnBulletDespawn += DespawnBullet;
            bulletBoundsCorrector.Enable(bullet);
            bulletBoundsCorrector.OnBulletDespawn += DespawnBullet;
            return true;
        }

        public void DespawnBullet(Bullet bullet)
        {
            bullet.Disable();
            bullet.OnBulletDespawn -= DespawnBullet;
            bulletBoundsCorrector.Disable(bullet);
            bulletBoundsCorrector.OnBulletDespawn -= DespawnBullet;
            bulletPool.RemoveObject(bullet);
        }

        public void OnPause()
        {
            for (var index = 0; index < this.bulletPool.ActiveObjects.Count; index++)
            {
                this.bulletPool.ActiveObjects[index].OnPause();
            }
        }

        public void OnResume()
        {
            for (var index = 0; index < this.bulletPool.ActiveObjects.Count; index++)
            {
                this.bulletPool.ActiveObjects[index].OnResume();
            }
        }

        public void OnFinish()
        {
            for (var index = 0; index < this.bulletPool.ActiveObjects.Count; index++)
            {
                this.bulletPool.ActiveObjects[index].OnFinish();
            }
        }
    }
}