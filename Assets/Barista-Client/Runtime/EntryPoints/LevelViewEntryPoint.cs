using System;
using Barista.Client.Actions;
using Barista.Client.Contexts.LevelUI;
using Barista.Client.Input;
using Barista.Client.Libraries;
using Barista.Client.References.LevelUI;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Shared.Events;
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

        public LevelViewEntryPoint(
            LevelViewEntryPointSettings settings,
            IEventDispatcher eventDispatcher,
            EnvironmentsLibrary environmentsLibrary,
            HeroesLibrary heroesLibrary
            )
        {
            this.settings = settings;
            this.eventDispatcher = eventDispatcher;
            this.environmentsLibrary = environmentsLibrary;
            this.heroesLibrary = heroesLibrary;
        }

        protected override void OnExecute()
        {
            // Serives
            TickablesService tickablesService = ServicesProvider.GetService<TickablesService>();

            // Contexts
            LevelUIContext levelUIContext = ContextsProvider.GetContext<LevelUIContext>();

            // Input
            MainInput mainInput = new MainInput();

            MousePositionInput mousePositionInput = new MousePositionInput(mainInput);
            AddCleanUpAction(() => mousePositionInput.CleanUp());

            NextCardInput nextCardInput = new NextCardInput(mainInput);
            AddCleanUpAction(() => nextCardInput.CleanUp());

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

            // Repositories
            EnvironmentEntityViewRepository environmentEntityViewRepository = new EnvironmentEntityViewRepository(
                environmentEntityViewFactory
                );

            HeroEntityViewRepository heroEntityViewRepository = new HeroEntityViewRepository(
                heroEntityViewFactory
                );

            LevelTimelines levelTimelines = new LevelTimelines();
            tickablesService.AddTickable(levelTimelines);
            AddCleanUpAction(() => tickablesService.RemoveTickable(levelTimelines));

            // Actions
            LevelActionsRepository levelActionsRepository = new LevelActionsRepository(
                new LoadLevelAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    heroEntityViewRepository,
                    mainInput
                    ),

                new UnloadLevelAction(
                    levelTimelines,
                    environmentEntityViewRepository,
                    mainInput
                    ),

                new LevelCompletedAction(
                    levelTimelines
                    )
                );

            Link(
                levelActionsRepository,
                levelUIViewModelsReferences,
                nextCardInput
                );
        }

        private void Link(
            LevelActionsRepository levelActionsRepository,
            LevelUIViewModelsReferences levelUIViewModelsReferences,
            NextCardInput nextCardInput
            )
        {
            eventDispatcher.Subscribe((SetupLevelOutEvent ev) =>
            {
                levelActionsRepository.LoadLevelAction.Invoke(
                    ev.EnvironmentEntity,
                    ev.HeroEntity
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
        }
    }
}