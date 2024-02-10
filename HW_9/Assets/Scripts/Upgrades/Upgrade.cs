using System;
using Sirenix.OdinInspector;
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

namespace Sample
{
    public abstract class Upgrade
    {
        public event Action<int> OnLevelUp;

        [ShowInInspector, ReadOnly]
        public string Id => this.config.id;

        [ShowInInspector, ReadOnly]
        public int Level => this.currentLevel;

        [ShowInInspector, ReadOnly]
        public int MaxLevel => this.config.maxLevel;

        public bool IsMaxLevel => this.currentLevel == this.config.maxLevel;

        [ShowInInspector, ReadOnly]
        public float Progress => (float) this.currentLevel / this.config.maxLevel;

        [ShowInInspector, ReadOnly]
        public int NextPrice => this.config.GetPrice(this.Level + 1);

        private readonly UpgradeConfig config;

        private int currentLevel;

        protected Upgrade(UpgradeConfig config)
        {
            this.config = config;
            this.currentLevel = 1;
        }

        public void SetupLevel(int level)
        {
            this.currentLevel = level;
        }

        public void LevelUp()
        {
            if (this.Level >= this.MaxLevel)
            {
                throw new Exception($"Can not increment level for upgrade {this.config.id}!");
            }

            var nextLevel = this.Level + 1;
            this.currentLevel = nextLevel;
            this.LevelUp(nextLevel);
            this.OnLevelUp?.Invoke(nextLevel);
        }

        protected abstract void LevelUp(int level);
    }
}