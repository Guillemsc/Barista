using System;
using Barista.Client.Actions;
using Barista.Client.Contexts.LevelUI;
using Barista.Client.Input;
using Barista.Client.Libraries;
using Barista.Client.References.LevelUI;
using Barista.Client.Signals;
using Barista.Client.State;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Client.View.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
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

        public LevelViewEntryPoint(
            LevelViewEntryPointSettings settings,
            IEventDispatcher eventDispatcher,
            EnvironmentsLibrary environmentsLibrary,
            HeroesLibrary heroesLibrary,
            EnemiesLibrary enemiesLibrary,
            ItemsLibrary itemsLibrary
            )
        {
            this.settings = settings;
            this.eventDispatcher = eventDispatcher;
            this.environmentsLibrary = environmentsLibrary;
            this.heroesLibrary = heroesLibrary;
            this.enemiesLibrary = enemiesLibrary;
            this.itemsLibrary = itemsLibrary;
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

            LevelTimelines levelTimelines = new LevelTimelines();
            tickablesService.AddTickable(levelTimelines);
            AddCleanUpAction(() => tickablesService.RemoveTickable(levelTimelines));

            // Actions
            LevelActionsRepository levelActionsRepository = new LevelActionsRepository(
                new SetupLevelAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    heroEntityViewRepository,
                    enemyEntityViewRepository,
                    itemEntityViewRepository,
                    mainInput
                    ),

                new UnloadLevelAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    mainInput
                    ),

                new LevelCompletedAction(
                    levelTimelines
                    ),

                new StartTurnAction(
                    levelTimelines,
                    turnState
                    ),

                new EndTurnAction(
                    levelTimelines,
                    turnState
                    ),

                new HeroMovedAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    heroEntityViewRepository
                    ),

                new EnemyMovedAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    enemyEntityViewRepository
                    ),

                new HeroGrabbedItemAction(
                    levelTimelines,
                    itemEntityViewRepository,
                    levelUIReferences.ItemsViewUI
                    )
                );

            Link(
                eventDispatcher,
                levelActionsRepository,
                movementInput,
                itemViewUIClickedSignal,
                turnState
                );
        }

        private void Link(
            IEventDispatcher eventDispatcher,
            LevelActionsRepository levelActionsRepository,
            MovementInput movementInput,
            ItemViewUIClickedSignal itemViewUIClickedSignal,
            State<bool> turnState
            )
        {
            // Actions

            eventDispatcher.Subscribe((SetupLevelOutEvent ev) =>
            {
                levelActionsRepository.SetupLevelAction.Invoke(
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
                    levelActionsRepository.LevelCompletedAction.Invoke();
                }
                else
                {
                    levelActionsRepository.UnloadLevelAction.Invoke();
                }
            });

            eventDispatcher.Subscribe((StartTurnOutEvent ev) =>
            {
                levelActionsRepository.StartTurnAction.Invoke();
            });

            eventDispatcher.Subscribe((EndTurnOutEvent ev) =>
            {
                levelActionsRepository.EndTurnAction.Invoke();
            });

            eventDispatcher.Subscribe((HeroMovedOutEvent ev) =>
            {
                levelActionsRepository.HeroMovedAction.Invoke(
                    ev.EnvironmentEntity,
                    ev.HeroEntity, 
                    ev.Path
                    );
            });

            eventDispatcher.Subscribe((EnemyMovedOutEvent ev) =>
            {
                levelActionsRepository.EnemyMovedAction.Invoke(
                    ev.EnvironmentEntity,
                    ev.EnemyEntity,
                    ev.Path
                    );
            });

            eventDispatcher.Subscribe((HeroGrabbedItemOutEvent ev) =>
            {
                levelActionsRepository.HeroGrabbedItemAction.Invoke(
                    ev.HeroEntity,
                    ev.ItemEntity,
                    ev.TotalStacks
                    );
            });

            // Signals

            itemViewUIClickedSignal.Register((ItemType itemType) =>
            {
                eventDispatcher.Dispatch(new UseItemInEvent(itemType));
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