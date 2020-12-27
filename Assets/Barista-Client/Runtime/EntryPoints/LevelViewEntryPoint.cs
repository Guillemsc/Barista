﻿using System;
using Barista.Client.Actions;
using Barista.Client.Contexts.LevelUI;
using Barista.Client.Input;
using Barista.Client.Libraries;
using Barista.Client.References.LevelUI;
using Barista.Client.State;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Shared.Events;
using Juce.Core.Direction;
using Juce.Core.EntryPoint;
using Juce.Core.Events;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Service;
using Juce.CoreUnity.Services;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Barista.Client.EntryPoints
{
    public class LevelViewEntryPoint : EntryPoint<LevelViewEntryPointResult>
    {
        private readonly LevelViewEntryPointSettings settings;
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentsLibrary environmentsLibrary;
        private readonly HeroesLibrary heroesLibrary;
        private readonly EnemiesLibrary enemiesLibrary;

        public LevelViewEntryPoint(
            LevelViewEntryPointSettings settings,
            IEventDispatcher eventDispatcher,
            EnvironmentsLibrary environmentsLibrary,
            HeroesLibrary heroesLibrary,
            EnemiesLibrary enemiesLibrary
            )
        {
            this.settings = settings;
            this.eventDispatcher = eventDispatcher;
            this.environmentsLibrary = environmentsLibrary;
            this.heroesLibrary = heroesLibrary;
            this.enemiesLibrary = enemiesLibrary;
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
            LevelUIViewModelsReferences levelUIViewModelsReferences = levelUIContextReferences.LevelUIViewModelsReferences;

            // Signals

            // Ui

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
                    )
                );

            Link(
                eventDispatcher,
                levelActionsRepository,
                movementInput,
                turnState
                );
        }

        private void Link(
            IEventDispatcher eventDispatcher,
            LevelActionsRepository levelActionsRepository,
            MovementInput movementInput,
            State<bool> turnState
            )
        {
            // Actions

            eventDispatcher.Subscribe((SetupLevelOutEvent ev) =>
            {
                levelActionsRepository.SetupLevelAction.Invoke(
                    ev.EnvironmentEntity,
                    ev.HeroEntity,
                    ev.EnemyEntities
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