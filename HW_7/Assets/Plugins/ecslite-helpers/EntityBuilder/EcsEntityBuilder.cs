namespace Leopotam.EcsLite.Helpers
{
    public readonly struct EcsEntityBuilder
    {
        private readonly int _entity;
        private readonly EcsWorld _world;

        public EcsEntityBuilder(EcsWorld world)
        {
            _entity = world.NewEntity();
            _world = world;
        }

        public EcsEntityBuilder Add<T>(T component) where T : struct
        {
            _world.GetPool<T>().Add(_entity) = component;
            return this;
        }
    }
}