using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killsText;
        [SerializeField] private TextMeshProUGUI bulletsText;
        [SerializeField] private TextMeshProUGUI healthText;
        
        public void UpdateKills(string str)
        {
            killsText.text = str;
        }

        public void UpdateBullets(string str)
        {
            bulletsText.text = str;
        }

        public void UpdateHealth(string str)
        {
            healthText.text = str;
        }
    }
}