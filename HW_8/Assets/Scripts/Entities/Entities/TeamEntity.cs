using System.Collections.Generic;
using Common;
using Entities.Components;
using UnityEngine;

namespace Entities.Entities
{
    public class TeamEntity : Entity
    {
        [SerializeField] private List<Entity> heroes;
        [SerializeField] private Teams team;

        private void OnEnable()
        {
            Add(new HeroesContainer() { list = heroes });
            Add(new Team() { value = team });
        }
    }
}