using UnityEngine;
using Random = UnityEngine.Random;

namespace SFX
{
    public class HeroSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip[] startTurn;
        [SerializeField] private AudioClip lowHealth;
        [SerializeField] private AudioClip ability;
        [SerializeField] private AudioClip death;

        private AudioPlayer audioPlayer;

        private void Start()
        {
            this.audioPlayer = AudioPlayer.Instance;
        }

        public void StartTurn()
        {
            int index = Random.Range(0, startTurn.Length);
            audioPlayer.PlaySound(startTurn[index]);
        }

        public void LowHealth()
        {
            audioPlayer.PlaySound(lowHealth);
        }

        public void Ability()
        {
            audioPlayer.PlaySound(ability);
        }

        public void Death()
        {
            audioPlayer.PlaySound(death);
        }
        
        
    }
}