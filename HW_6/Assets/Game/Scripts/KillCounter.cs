using System;
using DI;

namespace Game
{
    public class KillCounter : IGamePostConstructListener, IGameFinishListener
    {
        public int Kills { get; private set; }

        public Action<int> OnKillsUpdated;

        private ZombieSystem zombieSystem;

        [Inject]
        public void Construct(ZombieSystem zombieSystem)
        {
            this.zombieSystem = zombieSystem;
            Kills = 0;
        }

        public void OnPostConstruct()
        {
            zombieSystem.OnZombieDeath.Subscribe(UpdateKills);
        }

        public void OnFinish()
        {
            zombieSystem.OnZombieDeath.Unsubscribe(UpdateKills);
        }

        private void UpdateKills()
        {
            Kills++;
            OnKillsUpdated.Invoke(Kills);
        }
    }
}