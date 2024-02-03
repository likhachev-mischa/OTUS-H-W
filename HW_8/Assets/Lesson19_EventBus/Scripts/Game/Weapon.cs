using Lessons.Game.Events.Effects;
using UnityEngine;

namespace Lessons.Game
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Lession19/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeReference]
        public IEffect[] Effects;
    }
}