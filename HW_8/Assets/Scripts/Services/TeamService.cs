using System;
using System.Collections.Generic;
using Common;
using DI;
using UnityEngine;
using Entities;
using Entities.Components;

namespace Services
{
    [Serializable]
    public class TeamService : ILateLoadListener
    {
        [SerializeField] private Entity[] teams;
        [SerializeField] private Teams currentTeam;

        public void OnLateLoad()
        {
            for (var i = 0; i < teams.Length; i++)
            {
                if (!teams[i].TryGet(out Team _))
                {
                    throw new Exception("Entity in Teams has no TeamComponent!");
                }
            }
        }

        public void ChangeTeam(Teams newTeam)
        {
            currentTeam = newTeam;
        }

        public void ChangeTeam()
        {
            currentTeam = currentTeam switch
            {
                Teams.RED => Teams.BLUE,
                Teams.BLUE => Teams.RED,
                _ => currentTeam
            };
        }

        public Entity GetCurrentTeam()
        {
            return GetTeam(currentTeam);
        }

        public Entity GetEnemyTeam()
        {
            Teams team = currentTeam == Teams.RED ? Teams.BLUE : Teams.RED;
            return GetTeam(team);
        }

        public Entity GetTeam(Teams team)
        {
            for (var i = 0; i < teams.Length; i++)
            {
                var teamComponent = teams[i].Get<Team>();
                if (teamComponent.value == team)
                {
                    return teams[i];
                }
            }

            throw new Exception($"Team with {team} value was not found!");
        }

        public Entity[] GetAllTeams()
        {
            return teams;
        }

        public Entity FindHeroTeam(Entity hero)
        {
            for (var index = 0; index < teams.Length; index++)
            {
                Entity team = teams[index];
                List<Entity> list = team.Get<HeroesContainer>().list;
                for (var i = 0; i < list.Count; i++)
                {
                    if (list[i] == hero)
                    {
                        return team;
                    }
                }
            }

            throw new Exception("Hero's team was not found!");
        }
    }
}