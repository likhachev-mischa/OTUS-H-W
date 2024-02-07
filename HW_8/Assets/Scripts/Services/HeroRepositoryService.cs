using System;
using System.Collections.Generic;
using DI;
using Entities;
using Entities.Components;
using UnityEngine;

namespace Services
{
    public sealed class HeroRepositoryService
    {
        private TeamService teamService;

        private Dictionary<Entity, int> indexes;

        [Inject]
        private void Construct(TeamService teamService)
        {
            this.teamService = teamService;
            indexes = new Dictionary<Entity, int>();
        }
        
        public IEntity GetHero()
        {
            Entity team = teamService.GetCurrentTeam();

            if (!team.TryGet(out HeroesContainer heroes))
            {
                throw new Exception($"TEAM {team} HAS NO HEROES!");
            }

            if (!indexes.ContainsKey(team))
            {
                indexes[team] = 0;
            }
            else
            {
                ++indexes[team];
                if (indexes[team] >= heroes.list.Count)
                {
                    indexes[team] = 0;
                }
            }

            int index = indexes[team];

            IEntity hero = heroes.list[index];
           
            return hero;
        }

        public void RemoveHero(IEntity hero)
        {
            var castedHero = (Entity)hero;
            Entity team = teamService.FindHeroTeam(castedHero);
            List<Entity> list = team.Get<HeroesContainer>().list;
            int heroIndex = list.IndexOf(castedHero);
            list.Remove(castedHero);

            if (!indexes.ContainsKey(team))
            {
                indexes[team] = 0;
            }
            
            if (indexes[team] >= heroIndex && indexes[team] >= 0)
            {
                --indexes[team];
            }
        }
        
    }
}