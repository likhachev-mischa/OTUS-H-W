using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(menuName = "Create CharacterStatsConfig", fileName = "CharacterStatsConfig", order = 0)]
    public sealed class CharacterStatsConfig : ScriptableObject
    {
        [SerializeField] public Stat[] stats;
    }
}