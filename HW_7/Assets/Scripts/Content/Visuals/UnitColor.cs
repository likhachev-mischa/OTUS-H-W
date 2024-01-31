using Sirenix.OdinInspector;
using UnityEngine;

namespace Content
{
    public sealed class UnitColor : MonoBehaviour
    {
        public Teams Team
        {
            get => team;
            set => SetTeam(value);
        }

        private Teams team;

        [SerializeField] private Material red;
        [SerializeField] private Material blue;

        [SerializeField] private Renderer[] bodyParts;

        private Material color;

        [Button]
        public void SetTeam(Teams team)
        {
            color = team switch
            {
                Teams.RED => red,
                Teams.BLUE => blue,
                _ => blue
            };
            this.team = team;

            SetMaterial();
        }

        private void SetMaterial()
        {
            for (var i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].material = color;
            }
        }
    }
}