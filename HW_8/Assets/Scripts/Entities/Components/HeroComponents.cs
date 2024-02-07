using Events.Effects;
using UnityEngine;

namespace Entities.Components
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "Configs/Game/Hero")]
    public class HeroComponents : ScriptableObject
    {
        [SerializeField] public string HeroName;
        
        [SerializeField] public Stats Stats;
        
        [SerializeReference]
        public IEffect[] WeaponEffects;
        
        [SerializeReference]
        public IEffect[] ArmorEffects;
        
        [SerializeReference]
        public IEffect[] PostAttackEffects;
        
    }
}