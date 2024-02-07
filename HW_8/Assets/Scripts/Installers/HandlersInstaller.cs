using DI;
using Game.EventBus;
using Handlers;
using Handlers.Turn;

namespace Installers
{
    public class HandlersInstaller : GameInstaller
    {
        [Service(typeof(EventBus))] private EventBus eventBus = new();
        [Listener] private HeroSelectionHandler heroSelectionHandler = new();
        [Listener] private EnemySelectionHandler enemySelectionHandler = new();

        [Listener] private AttackHandler attackHandler = new();
        [Listener] private DealDamageHandler dealDamageHandler = new();
        [Listener] private ReceiveDamageHandler receiveDamageHandler = new();
        [Listener] private DeathHandler deathHandler = new();
        [Listener] private SwitchToRandomTargetHandler switchToRandomTargetHandler = new();
        [Listener] private UltimateEventHandler ultimateEventHandler = new();
        [Listener] private TeamSwitchHandler teamSwitchHandler = new();
        [Listener] private GameFinishHandler gameFinishHandler = new();

        [Listener] private TurnSkipHandler turnSkipHandler = new();

        [Listener] private AttackFinishedHandler attackFinishedHandler = new();
        [Listener] private DefenceFinishedHandler defenceFinishedHandler = new();
        [Listener] private VampirismEventHandler vampirismEventHandler = new();
        [Listener] private FreezeEnemyHandler freezeEnemyHandler = new();

        [Listener] private TurnFinishHandler turnFinishHandler = new();

        //effects
        [Listener] private DealDamageEffectHandler dealDamageEffectHandler = new();
        [Listener] private ReceiveDamageEffectHandler receiveDamageEffectHandler = new();
        [Listener] private SwitchToRandomTargetEffectHandler switchToRandomTargetEffectHandler = new();
        [Listener] private VampirismEffectHandler vampirismEffectHandler = new();
        [Listener] private DivineShieldEffectHandler divineShieldEffectHandler = new();
        [Listener] private FreezeEnemyEffectHandler freezeEnemyEffectHandler = new();
        [Listener] private UltimateEffectHandler ultimateEffectHandler = new();


        //visual
        [Listener] private HeroSelectionVisualHandler heroSelectionVisualHandler = new();
        [Listener] private HeroDeselectionVisualHandler heroDeselectionVisualHandler = new();
        [Listener] private AttackVisualHandler attackVisualHandler = new();
        [Listener] private ReceiveDamageVisualHandler receiveDamageVisualHandler = new();
        [Listener] private DeathVisualHandler deathVisualHandler = new();


        [Listener] private AttackFinishedRequestHandler attackFinishedRequestHandler = new();
        [Listener] private DefenceFinishedRequestHandler defenceFinishedRequestHandler = new();
    }
}