using System;
using DI;
using Services;
using UI;
using UnityEngine;

namespace Installers
{
    [Serializable]
    public class ServicesInstaller : GameInstaller
    {
        [SerializeField] [Service(typeof(UIService))]
        private UIService heroListView;

        [Service(typeof(TeamService))] [Listener] [SerializeField]
        private TeamService teamService = new();

        [Service(typeof(HeroSelectionService))] [Listener]
        private HeroSelectionService heroSelectionService = new();

        [Service(typeof(ActiveHeroService))] private ActiveHeroService activeHeroService = new();

        [Service(typeof(HeroRepositoryService))]
        private HeroRepositoryService heroRepositoryService = new();

        [Service(typeof(TurnStateService))] private TurnStateService turnStateService = new();
    }
}