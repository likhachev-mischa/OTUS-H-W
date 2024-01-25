namespace Leopotam.EcsLite.Events
{
    //Add this system first:
    public sealed class EcsStartEventSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly string worldName;
        
        private EcsFilter filter;
        private EcsPool<_Enqued> requestPool;
        private EcsPool<_Event> eventPool;

        public EcsStartEventSystem(string worldName = null)
        {
            this.worldName = worldName;
        }

        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld(this.worldName);
            
            this.filter = world.Filter<_Enqued>().End();
            this.requestPool = world.GetPool<_Enqued>();
            this.eventPool = world.GetPool<_Event>();
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entitiy in this.filter)
            {
                this.requestPool.Del(entitiy);
                this.eventPool.Add(entitiy) = new _Event();
            }
        }
    }
}