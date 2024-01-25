namespace Leopotam.EcsLite.Helpers
{
    public readonly struct EcsFactory<T> where T : struct
    {
        private readonly EcsWorld world;
        private readonly EcsPool<T> pool;
        
        public EcsFactory(EcsWorld world)
        {
            this.world = world;
            this.pool = this.world.GetPool<T>();
        }

        public int NewEntity(T arg)
        {
            int entity = this.world.NewEntity();
            this.pool.Add(entity) = arg;
            return entity;
        }
    }

    public readonly struct EcsFactory<T1, T2> where T1 : struct where T2 : struct
    {
        private readonly EcsWorld world;
        private readonly EcsPool<T1> pool1;
        private readonly EcsPool<T2> pool2;

        public EcsFactory(EcsWorld world)
        {
            this.world = world;
            this.pool1 = this.world.GetPool<T1>();
            this.pool2 = this.world.GetPool<T2>();
        }

        public int NewEntity(T1 arg1, T2 arg2)
        {
            int entity = this.world.NewEntity();
            this.pool1.Add(entity) = arg1;
            this.pool2.Add(entity) = arg2;
            return entity;
        }
    }
    
    public readonly struct EcsFactory<T1, T2, T3>
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        private readonly EcsWorld world;

        private readonly EcsPool<T1> pool1;
        private readonly EcsPool<T2> pool2;
        private readonly EcsPool<T3> pool3;

        public EcsFactory(EcsWorld world)
        {
            this.world = world;
            this.pool1 = this.world.GetPool<T1>();
            this.pool2 = this.world.GetPool<T2>();
            this.pool3 = world.GetPool<T3>();
        }

        public int NewEntity(T1 arg1, T2 arg2, T3 arg3)
        {
            int entity = this.world.NewEntity();
            this.pool1.Add(entity) = arg1;
            this.pool2.Add(entity) = arg2;
            this.pool3.Add(entity) = arg3;
            return entity;
        }
    }

    public readonly struct EcsFactory<T1, T2, T3, T4>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        private readonly EcsWorld world;

        private readonly EcsPool<T1> pool1;
        private readonly EcsPool<T2> pool2;
        private readonly EcsPool<T3> pool3;
        private readonly EcsPool<T4> pool4;

        public EcsFactory(EcsWorld world)
        {
            this.world = world;
            this.pool1 = this.world.GetPool<T1>();
            this.pool2 = this.world.GetPool<T2>();
            this.pool3 = this.world.GetPool<T3>();
            this.pool4 = world.GetPool<T4>();
        }

        public int NewEntity(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            int entity = this.world.NewEntity();
            this.pool1.Add(entity) = arg1;
            this.pool2.Add(entity) = arg2;
            this.pool3.Add(entity) = arg3;
            this.pool4.Add(entity) = arg4;
            return entity;
        }
    }
}