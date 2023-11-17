namespace ShootEmUp
{
    public static class BulletLauncherInteractor
    {
        public static void LaunchBullet(Bullet.Args args, BulletSystem bulletSystem)
        {
            Bullet bullet = bulletSystem.SpawnBullet();

            bullet.Position = args.position;
            bullet.Color = args.color;
            bullet.PhysicsLayer = args.physicsLayer;
            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.Velocity = args.velocity;
            
        }

    }
}