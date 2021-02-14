using System;
using Barista.Client.Actions;
using Barista.Client.Contexts.LevelUI;
using Barista.Client.Level.Input;
using Barista.Client.Level.Logic;
using Barista.Client.Level.Logic.Timelines;
using Barista.Client.Level.UseCases;
using Barista.Client.Libraries;
using Barista.Client.References.LevelUI;
using Barista.Client.Signals;
using Barista.Client.State;
using Barista.Client.View.Effects.TargetSelector;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Client.View.Entities.Item;
using Juce.Core.EntryPoint;
using Juce.Core.Events;
using Juce.Core.Sequencing;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Service;
using Juce.CoreUnity.Services;

namespace Barista.Client.Level.EntryPoints
{
    public class LevelLogicViewEntryPoint : EntryPoint<int>
    {
        private readonly LevelViewEntryPointSettings settings;
        private readonly IEventDispatcher eventDispatcher;
        private readonly IEventReceiver eventReceiver;
        private readonly EnvironmentsLibrary environmentsLibrary;
        private readonly HeroesLibrary heroesLibrary;
        private readonly EnemiesLibrary enemiesLibrary;
        private readonly ItemsLibrary itemsLibrary;
        private readonly EffectsLibrary effectsLibrary;

        public LevelLogicViewEntryPoint(
            LevelViewEntryPointSettings settings,
            IEventDispatcher eventDispatcher,
            IEventReceiver eventReceiver,
            EnvironmentsLibrary environmentsLibrary,
            HeroesLibrary heroesLibrary,
            EnemiesLibrary enemiesLibrary,
            ItemsLibrary itemsLibrary,
            EffectsLibrary effectsLibrary
            )
        {
            this.settings = settings;
            this.eventDispatcher = eventDispatcher;
            this.eventReceiver = eventReceiver;
            this.environmentsLibrary = environmentsLibrary;
            this.heroesLibrary = heroesLibrary;
            this.enemiesLibrary = enemiesLibrary;
            this.itemsLibrary = itemsLibrary;
            this.effectsLibrary = effectsLibrary;
        }

        protected override void OnStart()
        {
            // Contexts
            LevelUIContext levelUIContext = ContextsProvider.GetContext<LevelUIContext>();

            // Secrives
            TickablesService tickablesService = ServicesProvider.GetService<TickablesService>();

            // State
            State<TurnState> turnState = new State<TurnState>();
            State<ExpectingHeroActionState> expectingHeroActionState = new State<ExpectingHeroActionState>();

            // Input
            MainInput mainInput = new MainInput();
            mainInput.Enable();

            MousePositionInput mousePositionInput = new MousePositionInput(mainInput);
            AddCleanUpAction(() => mousePositionInput.CleanUp());

            MovementInput movementInput = new MovementInput(mainInput);
            AddCleanUpAction(() => movementInput.CleanUp());

            // Ui references
            LevelUIContextReferences levelUIContextReferences = levelUIContext.LevelUIContextReferences;
            LevelUICanvases levelUICanvases = levelUIContextReferences.LevelUICanvases;
            LevelUIReferences levelUIReferences = levelUIContextReferences.LevelUIReferences;

            //Signals
            ItemViewUIClickedSignal itemViewUIClickedSignal = new ItemViewUIClickedSignal();
            AddCleanUpAction(() => itemViewUIClickedSignal.CleanUp());

            //TargetSelectorSelectedSignal targetSelectorClickedSignal = new TargetSelectorSelectedSignal();
            //AddCleanUpAction(() => targetSelectorClickedSignal.CleanUp());

            levelUIReferences.ItemsViewUI.Init(
                itemsLibrary,
                levelUICanvases.MainCanvas,
                itemViewUIClickedSignal
                );

            // Factories
            IEnvironmentEntityViewFactory environmentEntityViewFactory = new EnvironmentEntityViewFactory(
                environmentsLibrary
                );

            IHeroEntityViewFactory heroEntityViewFactory = new HeroEntityViewFactory(
                heroesLibrary
                );

            IEnemyEntityViewFactory enemyEntityViewFactory = new EnemyEntityViewFactory(
                enemiesLibrary
                );

            IItemEntityViewFactory itemEntityViewFactory = new ItemEntityViewFactory(
                itemsLibrary
                );

            //ITargetSelectorViewFactory targetSelectorViewFactory = new TargetSelectorViewFactory(
            //    effectsLibrary,
            //    targetSelectorClickedSignal
            //    );

            // Repositories
            EnvironmentEntityViewRepository environmentEntityViewRepository = new EnvironmentEntityViewRepository(
                environmentEntityViewFactory
                );

            HeroEntityViewRepository heroEntityViewRepository = new HeroEntityViewRepository(
                heroEntityViewFactory
                );

            EnemyEntityViewRepository enemyEntityViewRepository = new EnemyEntityViewRepository(
                enemyEntityViewFactory
                );

            ItemEntityViewRepository itemEntityViewRepository = new ItemEntityViewRepository(
                itemEntityViewFactory
                );

            //TargetSelectorViewRepository targetSelectorViewRepository = new TargetSelectorViewRepository(
            //    targetSelectorViewFactory
            //    );

            TimelinesPlayer timelinesPlayer = new TimelinesPlayer();
            tickablesService.AddTickable(timelinesPlayer);
            AddCleanUpAction(() => tickablesService.RemoveTickable(timelinesPlayer));

            LevelLogicViewTimelines levelLogicViewTimelines = new LevelLogicViewTimelines(
                timelinesPlayer.AddTimeline(),
                timelinesPlayer.AddTimeline()
                );

            LevelActionsRepository levelActionsRepository = new LevelActionsRepository();

            // Use Cases
            IMovementInputPerformedUseCase movementInputPerformedUseCase = new MovementInputPerformedUseCase(
                eventDispatcher,
                expectingHeroActionState
                );

            IItemUIClickedUseCase itemUIClickedUseCase = new ItemUIClickedUseCase(
                eventDispatcher,
                expectingHeroActionState
                );

            ISetupLevelUseCase setupLevelUseCase = new SetupLevelUseCase(
                environmentEntityViewRepository,
                heroEntityViewRepository,
                enemyEntityViewRepository,
                itemEntityViewRepository
                );

            IExpectingHeroActionChangedUseCase expectingHeroActionChangedUseCase = new ExpectingHeroActionChangedUseCase(
                expectingHeroActionState
                );

            IMoveHeroUseCase moveHeroUseCase = new MoveHeroUseCase(
                environmentEntityViewRepository,
                heroEntityViewRepository
                );

            IMoveEnemyUseCase moveEnemyUseCase = new MoveEnemyUseCase(
                environmentEntityViewRepository,
                enemyEntityViewRepository
                );

            IHeroGrabbedItemUseCase heroGrabbedItemUseCase = new HeroGrabbedItemUseCase(
                itemEntityViewRepository,
                levelUIReferences.ItemsViewUI
                );

            LevelLogicViewUseCasesRepository levelLogicViewUseCasesRepository = new LevelLogicViewUseCasesRepository(
                movementInputPerformedUseCase,
                itemUIClickedUseCase,
                setupLevelUseCase,
                expectingHeroActionChangedUseCase,
                moveHeroUseCase,
                moveEnemyUseCase,
                heroGrabbedItemUseCase
                );

            LevelLogicView levelViewLogic = new LevelLogicView(
                eventReceiver,
                levelLogicViewUseCasesRepository,
                levelLogicViewTimelines,
                movementInput,
                itemViewUIClickedSignal
                );

            tickablesService.AddTickable(levelViewLogic);

            levelViewLogic.Start();

            // Actions states
            //InitialActionsState initialActionState = new InitialActionsState();
            //TurnActionsState turnActionsState = new TurnActionsState();
            //ItemInputActionsState itemInputActionState = new ItemInputActionsState();

            // Actions
            //ISetupLevelAction setupLevelAction = new SetupLevelAction(
            //       levelTimelines,
            //       turnActionsState,
            //       environmentEntityViewRepository,
            //       heroEntityViewRepository,
            //       enemyEntityViewRepository,
            //       itemEntityViewRepository
            //       );

            //IUnloadLevelAction unloadLevelAction = new UnloadLevelAction(
            //        levelTimelines,
            //        environmentEntityViewRepository,
            //        mainInput
            //        );

            //ILevelCompletedAction levelCompletedAction = new LevelCompletedAction(
            //        levelTimelines
            //        );

            //IStartTurnAction startTurnAction = new StartTurnAction(
            //        levelTimelines,
            //        turnState
            //        );

            //IEndTurnAction endTurnAction = new EndTurnAction(
            //        levelTimelines,
            //        turnState
            //        );

            //IHeroMovedAction heroMovedAction = new HeroMovedAction(
            //        levelTimelines,
            //        environmentEntityViewRepository,
            //        heroEntityViewRepository
            //        );

            //IEnemyMovedAction enemyMovedAction = new EnemyMovedAction(
            //        levelTimelines,
            //        environmentEntityViewRepository,
            //        enemyEntityViewRepository
            //        );

            //IHeroGrabbedItemAction heroGrabbedItemAction = new HeroGrabbedItemAction(
            //        levelTimelines,
            //        itemEntityViewRepository,
            //        levelUIReferences.ItemsViewUI
            //        );

            //IStartItemTargetSelection itemTargetSelection = new StartItemTargetSelection(
            //    levelTimelines,
            //    itemInputActionState,
            //    environmentEntityViewRepository,
            //    targetSelectorViewRepository
            //    );

            //IItemTargetSelectedAction itemTargetSelectedAction = new ItemTargetSelectedAction(
            //    levelTimelines,
            //    turnActionsState,
            //    targetSelectorViewRepository
            //    );

            //IEnemyEntityKilledAction enemyEntityKilledAction = new EnemyEntityKilledAction(
            //    enemyEntityViewRepository
            //    );

            //initialActionState.Init(
            //    levelActionsRepository,
            //    setupLevelAction
            //    );

            //turnActionsState.Init(
            //    levelActionsRepository,
            //    levelCompletedAction,
            //    startTurnAction,
            //    endTurnAction,
            //    heroMovedAction,
            //    enemyMovedAction,
            //    heroGrabbedItemAction,
            //    itemTargetSelection,
            //    enemyEntityKilledAction
            //    );

            //itemInputActionState.Init(
            //    levelActionsRepository,
            //    itemTargetSelectedAction
            //    );

            //Link(
            //    eventDispatcher,
            //    levelActionsRepository,
            //    movementInput,
            //    itemViewUIClickedSignal,
            //    targetSelectorClickedSignal,
            //    turnState
            //    );
        }

        //private void Link(
        //    IEventDispatcher eventDispatcher,
        //    LevelActionsRepository levelActionsRepository,
        //    MovementInput movementInput,
        //    ItemViewUIClickedSignal itemViewUIClickedSignal,
        //    TargetSelectorSelectedSignal targetSelectorClickedSignal,
        //    State<bool> turnState
        //    )
        //{
        //    // Actions
        //    eventDispatcher.Subscribe((SetupLevelOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out ISetupLevelAction action);

        //        if(!found)
        //        {
        //            return;
        //        }

        //        action.Invoke(
        //            ev.EnvironmentEntity,
        //            ev.HeroEntity,
        //            ev.EnemyEntities,
        //            ev.ItemEntities
        //            );
        //    });

        //    eventDispatcher.Subscribe((LevelCompletedOutEvent ev) =>
        //    {
        //        if (!settings.IsVisualizer)
        //        {
        //            bool found = levelActionsRepository.TryGetAction(out ILevelCompletedAction action);

        //            if (!found)
        //            {
        //                return;
        //            }

        //            action.Invoke();
        //        }
        //        else
        //        {
        //            bool found = levelActionsRepository.TryGetAction(out IUnloadLevelAction action);

        //            if (!found)
        //            {
        //                return;
        //            }

        //            action.Invoke();
        //        }
        //    });

        //    eventDispatcher.Subscribe((StartTurnOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IStartTurnAction action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke();
        //    });

        //    eventDispatcher.Subscribe((EndTurnOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IEndTurnAction action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke();
        //    });

        //    eventDispatcher.Subscribe((HeroMovedOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IHeroMovedAction action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke(
        //            ev.HeroEntity, 
        //            ev.Path
        //            );
        //    });

        //    eventDispatcher.Subscribe((EnemyMovedOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IEnemyMovedAction action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke(
        //            ev.EnemyEntity,
        //            ev.Path
        //            );
        //    });

        //    eventDispatcher.Subscribe((HeroGrabbedItemOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IHeroGrabbedItemAction action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke(
        //            ev.HeroEntity,
        //            ev.ItemEntity,
        //            ev.TotalStacks
        //            );
        //    });

        //    eventDispatcher.Subscribe((ItemNeedsTargetSelectionOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IStartItemTargetSelection action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke(
        //            ev.AvaliableTargetPositions
        //            );
        //    });

        //    eventDispatcher.Subscribe((EnemyEntityKilledOutEvent ev) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IEnemyEntityKilledAction action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke(
        //            ev.EnemyEntity
        //            );
        //    });

        //    // Signals

        //    itemViewUIClickedSignal.Register((ItemType itemType) =>
        //    {
        //        eventDispatcher.Dispatch(new UseItemInEvent(itemType));
        //    });

        //    targetSelectorClickedSignal.Register((Int2 gridPosition) =>
        //    {
        //        bool found = levelActionsRepository.TryGetAction(out IItemTargetSelectedAction action);

        //        if (!found)
        //        {
        //            return;
        //        }

        //        action.Invoke(gridPosition);

        //        eventDispatcher.Dispatch(new ItemTargetSelectedInEvent(gridPosition));
        //    });

        //    // Input

        //    movementInput.OnPerformed += (Direction4Axis direction) =>
        //    {
        //        if(turnState.Value)
        //        {
        //            return;
        //        }

        //        eventDispatcher.Dispatch(new MoveHeroInEvent(direction));
        //    };
        //}
    }
}