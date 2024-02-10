using System.Collections.Generic;

namespace Sample
{
    public sealed class PlayerStats
    {
        private readonly Dictionary<string, int> stats = new();

        public void AddStat(string name, int value)
        {
            this.stats.Add(name, value);
        }

        public int GetStat(string name)
        {
            return this.stats[name];
        }

        public IReadOnlyDictionary<string, int> GetStats()
        {
            return this.stats;
        }

        public void RemoveStat(string name)
        {
            this.stats.Remove(name);
        }
    }
}