using System;
using Barista.Client.Actions;
using Barista.Client.ActionsSatates;
using Barista.Client.Contexts.LevelUI;
using Barista.Client.Input;
using Barista.Client.Libraries;
using Barista.Client.References.LevelUI;
using Barista.Client.Signals;
using Barista.Client.State;
using Barista.Client.Timelines;
using Barista.Client.View.Effects.TargetSelector;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Client.View.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using Juce.Core.Direction;
using Juce.Core.EntryPoint;
using Juce.Core.Events;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Service;
using Juce.CoreUnity.Services;

namespace Barista.Client.EntryPoints
{
    public class LevelViewEntryPoint : EntryPoint<LevelViewEntryPointResult>
    {
        private readonly LevelViewEntryPointSettings settings;
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentsLibrary environmentsLibrary;
        private readonly HeroesLibrary heroesLibrary;
        private readonly EnemiesLibrary enemiesLibrary;
        private readonly ItemsLibrary itemsLibrary;
        private readonly EffectsLibrary effectsLibrary;

        public LevelViewEntryPoint(
            LevelViewEntryPointSettings settings,
            IEventDispatcher eventDispatcher,
            EnvironmentsLibrary environmentsLibrary,
            HeroesLibrary heroesLibrary,
            EnemiesLibrary enemiesLibrary,
            ItemsLibrary itemsLibrary,
            EffectsLibrary effectsLibrary
            )
        {
            this.settings = settings;
            this.eventDispatcher = eventDispatcher;
            this.environmentsLibrary = environmentsLibrary;
            this.heroesLibrary = heroesLibrary;
            this.enemiesLibrary = enemiesLibrary;
            this.itemsLibrary = itemsLibrary;
            this.effectsLibrary = effectsLibrary;
        }

        protected override void OnExecute()
        {
            // Serices
            TickablesService tickablesService = ServicesProvider.GetService<TickablesService>();

            // Contexts
            LevelUIContext levelUIContext = ContextsProvider.GetContext<LevelUIContext>();

            // State
            State<bool> turnState = new State<bool>();

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

            // Signals
            ItemViewUIClickedSignal itemViewUIClickedSignal = new ItemViewUIClickedSignal();
            AddCleanUpAction(() => itemViewUIClickedSignal.CleanUp());

            TargetSelectorSelectedSignal targetSelectorClickedSignal = new TargetSelectorSelectedSignal();
            AddCleanUpAction(() => targetSelectorClickedSignal.CleanUp());

            // Ui
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

            ITargetSelectorViewFactory targetSelectorViewFactory = new TargetSelectorViewFactory(
                effectsLibrary,
                targetSelectorClickedSignal
                );

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

            TargetSelectorViewRepository targetSelectorViewRepository = new TargetSelectorViewRepository(
                targetSelectorViewFactory
                );

            LevelTimelines levelTimelines = new LevelTimelines();
            tickablesService.AddTickable(levelTimelines);
            AddCleanUpAction(() => tickablesService.RemoveTickable(levelTimelines));

            LevelActionsRepository levelActionsRepository = new LevelActionsRepository();

            // Actions states
            InitialActionsState initialActionState = new InitialActionsState();
            TurnActionsState turnActionsState = new TurnActionsState();
            ItemInputActionsState itemInputActionState = new ItemInputActionsState();

            // Actions
            ISetupLevelAction setupLevelAction = new SetupLevelAction(
                   levelTimelines,
                   turnActionsState,
                   environmentEntityViewRepository,
                   heroEntityViewRepository,
                   enemyEntityViewRepository,
                   itemEntityViewRepository
                   );

            IUnloadLevelAction unloadLevelAction = new UnloadLevelAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    mainInput
                    );

            ILevelCompletedAction levelCompletedAction = new LevelCompletedAction(
                    levelTimelines
                    );

            IStartTurnAction startTurnAction = new StartTurnAction(
                    levelTimelines,
                    turnState
                    );

            IEndTurnAction endTurnAction = new EndTurnAction(
                    levelTimelines,
                    turnState
                    );

            IHeroMovedAction heroMovedAction = new HeroMovedAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    heroEntityViewRepository
                    );

            IEnemyMovedAction enemyMovedAction = new EnemyMovedAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    enemyEntityViewRepository
                    );

            IHeroGrabbedItemAction heroGrabbedItemAction = new HeroGrabbedItemAction(
                    levelTimelines,
                    itemEntityViewRepository,
                    levelUIReferences.ItemsViewUI
                    );

            IStartItemTargetSelection itemTargetSelection = new StartItemTargetSelection(
                levelTimelines,
                itemInputActionState,
                environmentEntityViewRepository,
                targetSelectorViewRepository
                );

            IItemTargetSelectedAction itemTargetSelectedAction = new ItemTargetSelectedAction(
                levelTimelines,
                turnActionsState,
                targetSelectorViewRepository
                );

            initialActionState.Init(
                levelActionsRepository,
                setupLevelAction
                );

            turnActionsState.Init(
                levelActionsRepository,
                levelCompletedAction,
                startTurnAction,
                endTurnAction,
                heroMovedAction,
                enemyMovedAction,
                heroGrabbedItemAction,
                itemTargetSelection
                );

            itemInputActionState.Init(
                levelActionsRepository,
                itemTargetSelectedAction
                );

            Link(
                eventDispatcher,
                levelActionsRepository,
                movementInput,
                itemViewUIClickedSignal,
                targetSelectorClickedSignal,
                turnState
                );

            initialActionState.Enable();
        }

        private void Link(
            IEventDispatcher eventDispatcher,
            LevelActionsRepository levelActionsRepository,
            MovementInput movementInput,
            ItemViewUIClickedSignal itemViewUIClickedSignal,
            TargetSelectorSelectedSignal targetSelectorClickedSignal,
            State<bool> turnState
            )
        {
            // Actions
            eventDispatcher.Subscribe((SetupLevelOutEvent ev) =>
            {
                bool found = levelActionsRepository.TryGetAction(out ISetupLevelAction action);

                if(!found)
                {
                    return;
                }

                action.Invoke(
                    ev.EnvironmentEntity,
                    ev.HeroEntity,
                    ev.EnemyEntities,
                    ev.ItemEntities
                    );
            });

            eventDispatcher.Subscribe((LevelCompletedOutEvent ev) =>
            {
                if (!settings.IsVisualizer)
                {
                    bool found = levelActionsRepository.TryGetAction(out ILevelCompletedAction action);

                    if (!found)
                    {
                        return;
                    }

                    action.Invoke();
                }
                else
                {
                    bool found = levelActionsRepository.TryGetAction(out IUnloadLevelAction action);

                    if (!found)
                    {
                        return;
                    }

                    action.Invoke();
                }
            });

            eventDispatcher.Subscribe((StartTurnOutEvent ev) =>
            {
                bool found = levelActionsRepository.TryGetAction(out IStartTurnAction action);

                if (!found)
                {
                    return;
                }

                action.Invoke();
            });

            eventDispatcher.Subscribe((EndTurnOutEvent ev) =>
            {
                bool found = levelActionsRepository.TryGetAction(out IEndTurnAction action);

                if (!found)
                {
                    return;
                }

                action.Invoke();
            });

            eventDispatcher.Subscribe((HeroMovedOutEvent ev) =>
            {
                bool found = levelActionsRepository.TryGetAction(out IHeroMovedAction action);

                if (!found)
                {
                    return;
                }

                action.Invoke(
                    ev.EnvironmentEntity,
                    ev.HeroEntity, 
                    ev.Path
                    );
            });

            eventDispatcher.Subscribe((EnemyMovedOutEvent ev) =>
            {
                bool found = levelActionsRepository.TryGetAction(out IEnemyMovedAction action);

                if (!found)
                {
                    return;
                }

                action.Invoke(
                    ev.EnvironmentEntity,
                    ev.EnemyEntity,
                    ev.Path
                    );
            });

            eventDispatcher.Subscribe((HeroGrabbedItemOutEvent ev) =>
            {
                bool found = levelActionsRepository.TryGetAction(out IHeroGrabbedItemAction action);

                if (!found)
                {
                    return;
                }

                action.Invoke(
                    ev.HeroEntity,
                    ev.ItemEntity,
                    ev.TotalStacks
                    );
            });

            eventDispatcher.Subscribe((ItemNeedsTargetSelectionOutEvent ev) =>
            {
                bool found = levelActionsRepository.TryGetAction(out IStartItemTargetSelection action);

                if (!found)
                {
                    return;
                }

                action.Invoke(
                    ev.AvaliableTargetPositions
                    );
            });

            // Signals

            itemViewUIClickedSignal.Register((ItemType itemType) =>
            {
                eventDispatcher.Dispatch(new UseItemInEvent(itemType));
            });

            targetSelectorClickedSignal.Register((Int2 gridPosition) =>
            {
                bool found = levelActionsRepository.TryGetAction(out IItemTargetSelectedAction action);

                if (!found)
                {
                    return;
                }

                action.Invoke(gridPosition);

                eventDispatcher.Dispatch(new ItemTargetSelectedInEvent(gridPosition));
            });

            // Input

            movementInput.OnPerformed += (Direction4Axis direction) =>
            {
                if(turnState.Value)
                {
                    return;
                }

                eventDispatcher.Dispatch(new MoveHeroInEvent(direction));
            };
        }
    }
}