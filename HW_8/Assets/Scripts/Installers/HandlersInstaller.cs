using DI;
using Game.EventBus;
using Handlers;
using Handlers.SFX;
using Handlers.Turn;

namespace Installers
{
    public class HandlersInstaller : GameInstaller
    {
        [Service(typeof(EventBus))] private EventBus eventBus = new();
        [Listener] private HeroSelectionHandler heroSelectionHandler = new();
        [Listener] private EnemySelectionHandler enemySelectionHandler = new();

        [Listener] private AttackHandler attackHandler = new();
        [Listener] private DeathRequestHandler deathRequestHandler = new();
        [Listener] private TeamSwitchHandler teamSwitchHandler = new();
        [Listener] private GameFinishHandler gameFinishHandler = new();
        [Listener] private TurnSkipHandler turnSkipHandler = new();
        [Listener] private AttackFinishedHandler attackFinishedHandler = new();
        [Listener] private DefenceFinishedHandler defenceFinishedHandler = new();

        [Listener] private TurnFinishRequestHandler turnFinishRequestHandler = new();
        [Listener] private AttackFinishedRequestHandler attackFinishedRequestHandler = new();
        [Listener] private DefenceFinishedRequestHandler defenceFinishedRequestHandler = new();

        //effects
        [Listener] private DealDamageEffectHandler dealDamageEffectHandler = new();
        [Listener] private ReceiveDamageEffectHandler receiveDamageEffectHandler = new();
        [Listener] private SwitchToRandomTargetEffectHandler switchToRandomTargetEffectHandler = new();
        [Listener] private VampirismEffectHandler vampirismEffectHandler = new();
        [Listener] private DamageBlockEffectHandler damageBlockEffectHandler = new();
        [Listener] private FreezeEnemyEffectHandler freezeEnemyEffectHandler = new();
        [Listener] private UltimateEffectHandler ultimateEffectHandler = new();
        [Listener] private EndOfTurnEffectsHandler endOfTurnEffectsHandler = new();
        [Listener] private HealEffectHandler healEffectHandler = new();
        [Listener] private MultiTargetEffectHandler multiTargetEffectHandler = new();
        [Listener] private ChangeTargetEffectHandler changeTargetEffectHandler = new();

        //visual
        [Listener] private HeroSelectionVisualHandler heroSelectionVisualHandler = new();
        [Listener] private HeroDeselectionVisualHandler heroDeselectionVisualHandler = new();
        [Listener] private AttackVisualHandler attackVisualHandler = new();
        [Listener] private ReceiveDamageVisualHandler receiveDamageVisualHandler = new();
        [Listener] private DeathVisualHandler deathVisualHandler = new();

        //sfx
        [Listener] private HeroSelectionVFXHandler heroSelectionVFXHandler = new();
        [Listener] private UltimateSFXHandler ultimateSfxHandler = new();
        [Listener] private TakeDamageSFXHandler takeDamageSfxHandler = new();
        [Listener] private DeathSFXHandler deathSfxHandler = new();
    }
}