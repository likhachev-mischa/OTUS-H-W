using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager :
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        private int initialCount;
        private GameObject prefab;

        private Transform worldTransform;
        private BulletBoundsCorrector bulletBoundsCorrector;

        private ObjectPool<Bullet> bulletPool;

        [Inject]
        private void Construct(World world, BulletBoundsCorrector bulletBoundsCorrector,
            BulletManagerConfig config)
        {
            worldTransform = world.transform;
            this.bulletBoundsCorrector = bulletBoundsCorrector;
            initialCount = config.initialCount;
            prefab = config.prefab;

            CreatePool();
        }

        private void CreatePool()
        {
            var pool = new GameObject("Bullet Pool");
            //pool.transform.SetParent(this.transform);
            pool.SetActive(false);
            Transform container = pool.transform;
            bulletPool = new ObjectPool<Bullet>(initialCount, container,
                prefab, worldTransform);
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
            for (var index = 0; index < bulletPool.ActiveObjects.Count; index++)
            {
                bulletPool.ActiveObjects[index].OnPause();
            }
        }

        public void OnResume()
        {
            for (var index = 0; index < bulletPool.ActiveObjects.Count; index++)
            {
                bulletPool.ActiveObjects[index].OnResume();
            }
        }

        public void OnFinish()
        {
            for (var index = 0; index < bulletPool.ActiveObjects.Count; index++)
            {
                bulletPool.ActiveObjects[index].OnFinish();
            }
        }
    }
}