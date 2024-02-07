using System;
using System.Collections.Generic;
using Common;
using DI;
using Entities;
using Entities.Components;
using UI;

namespace Services
{
    public class HeroSelectionService : IInitializable, IDisposable
    {
        public Action<IEntity> OnEntitySelected;

        private TeamService teamService;
        private UIService uiService;

        [Inject]
        public void Construct(TeamService teamService, UIService uiService)
        {
            this.teamService = teamService;
            this.uiService = uiService;
        }

        void IInitializable.Initialize()
        {
            HeroListView red = uiService.GetRedPlayer();
            red.OnHeroClicked += OnEnemySelected;
            HeroListView blue = uiService.GetBluePlayer();
            blue.OnHeroClicked += OnEnemySelected;
        }

        void IDisposable.Dispose()
        {
            HeroListView red = uiService.GetRedPlayer();
            red.OnHeroClicked -= OnEnemySelected;
            HeroListView blue = uiService.GetBluePlayer();
            blue.OnHeroClicked -= OnEnemySelected;
        }

        private void OnEnemySelected(HeroView selectedView)
        {
            Teams currentTeam = teamService.GetCurrentTeam().Get<Team>().value;

            Teams enemyTeam = currentTeam == Teams.RED ? Teams.BLUE : Teams.RED;

            Entity team = teamService.GetTeam(enemyTeam);
            List<Entity> heroes = team.Get<HeroesContainer>().list;
            for (var i = 0; i < heroes.Count; i++)
            {
                HeroView view = heroes[i].Get<HeroVisual>().view;
                if (view == selectedView)
                {
                    OnEntitySelected?.Invoke(heroes[i]);
                }
            }
        }
    }
}