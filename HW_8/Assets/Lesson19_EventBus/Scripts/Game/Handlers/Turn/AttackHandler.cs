using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Turn
{
    [UsedImplicitly]
    public sealed class AttackHandler : BaseHandler<AttackEvent>
    {
        public AttackHandler(EventBus eventBus) : base(eventBus)
        {
            
        }

        protected override void HandleEvent(AttackEvent evt)
        {
            if (!evt.Entity.TryGet(out WeaponComponent weaponComponent))
                return;
            
            foreach (IEffect effect in weaponComponent.Value.Effects)
            {
                effect.Source = evt.Entity;
                effect.Target = evt.Target;
                    
                EventBus.RaiseEvent(effect);
            }
        }
    }
}