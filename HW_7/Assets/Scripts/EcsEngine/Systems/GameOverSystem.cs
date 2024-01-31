using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class GameOverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent, BuildingTag, Team>> filter;

        private readonly EcsFilterInject<Inc<Team>,Exc<Inactive>> enemies;
        private readonly EcsFilterInject<Inc<MoveSpeed>, Exc<Inactive>> units;
        private readonly EcsPoolInject<Health> healthPool;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                Teams loser = filter.Pools.Inc3.Get(entity).value;

                Teams winner = loser == Teams.RED? Teams.BLUE : Teams.RED;
                
                Debug.Log($"GAME OVER {winner} TEAM WON!");
                
                foreach (int enemy in enemies.Value)
                {
                    Teams enemyTeam = enemies.Pools.Inc1.Get(enemy).value;
                    if (enemyTeam != loser)
                    {
                        continue;
                    }

                    if (healthPool.Value.Has(enemy))
                    {
                        ref int health =ref healthPool.Value.Get(enemy).value;
                        health = 0;
                    }
                    
                }
                foreach (int unit in units.Value)
                {
                    ref float speed = ref units.Pools.Inc1.Get(unit).value;
                    speed = 0;
                }
                
            }

            
        }
    }
}