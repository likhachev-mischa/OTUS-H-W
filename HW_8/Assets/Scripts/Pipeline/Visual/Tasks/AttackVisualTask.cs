using Cysharp.Threading.Tasks;
using Entities;
using Entities.Components;
using UnityEngine;

namespace Pipeline
{
    public sealed class AttackVisualTask : Task
    {
        private readonly HeroVisual source;
        private readonly HeroVisual target;

        public AttackVisualTask(IEntity source, IEntity target)
        {
            this.source = source.Get<HeroVisual>();
            this.target = target.Get<HeroVisual>();
        }

        protected override void OnRun()
        {
            UniTask tcs = source.view.AnimateAttack(target.view);
            tcs.ContinueWith(Finish).Forget();
        }
    }
}