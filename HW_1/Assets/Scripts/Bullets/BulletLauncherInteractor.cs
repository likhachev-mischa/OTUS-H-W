namespace ShootEmUp
{
    public static class BulletLauncherInteractor
    {
        public static void LaunchBullet(Bullet.Args args, BulletFactory bulletFactory)
        {
            if(!bulletFactory.SpawnBullet(out var bullet))
            {
                    return;
            }
            bullet.Position = args.position;
            bullet.Color = args.color;
            bullet.PhysicsLayer = args.physicsLayer;
            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.Velocity = args.velocity;
            
        }

    }
}