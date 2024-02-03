using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Turn
{
    [UsedImplicitly]
    public sealed class DealDamageHandler : BaseHandler<DealDamageEvent>
    {
        public DealDamageHandler(EventBus eventBus) : base(eventBus)
        {
            
        }

        protected override void HandleEvent(DealDamageEvent evt)
        {
            if (!evt.Entity.TryGet(out HitPointsComponent hitPointsComponent))
            {
                return;
            }

            hitPointsComponent.Value -= evt.Damage;

            if (hitPointsComponent.Value <= 0)
            {
                EventBus.RaiseEvent(new DestroyEvent(evt.Entity));
            }
        }
    }
}