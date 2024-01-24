using DI;
using UnityEngine;

namespace Game.UI
{
    public class CharacterAdapter : MonoBehaviour
    {
        [SerializeField] private CharacterView characterView;

        private Character character;
        private KillCounter killCounter;

        private int maxBullets;

        [Inject]
        private void Construct(Character character, KillCounter killCounter)
        {
            this.character = character;
            this.killCounter = killCounter;

            character.Health.ValueChanged += UpdateHealth;
            character.BulletCount.ValueChanged += UpdateBullets;
            killCounter.OnKillsUpdated += UpdateKills;

            maxBullets = character.BulletCount.Value;

            UpdateHealth(character.Health.Value);
            UpdateKills(killCounter.Kills);
            UpdateBullets(character.BulletCount.Value);
        }

        private void UpdateHealth(int health)
        {
            string str = "Health: " + health;
            characterView.UpdateHealth(str);
        }

        private void UpdateKills(int kills)
        {
            string str = "Kills: " + kills;
            characterView.UpdateKills(str);
        }

        private void UpdateBullets(int bullets)
        {
            string str = "Bullets: " + bullets + "/" + maxBullets;
            characterView.UpdateBullets(str);
        }

        private void OnDisable()
        {
            character.Health.ValueChanged -= UpdateHealth;
            character.BulletCount.ValueChanged -= UpdateBullets;
            killCounter.OnKillsUpdated -= UpdateKills;
        }
    }
}