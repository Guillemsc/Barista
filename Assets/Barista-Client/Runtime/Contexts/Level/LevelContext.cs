using Barista.Client.Configuration.Levels;
using Barista.Client.Level.EntryPoints;
using Barista.Client.Level.Logic;
using Barista.Client.Utils;
using Barista.Shared.Configuration;
using Barista.Shared.EntryPoints;
using Barista.Shared.Logic;
using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using Juce.Core.Events;
using Juce.CoreUnity.Contexts;
using Juce.CoreUnity.Service;
using Juce.CoreUnity.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Contexts.Level
{
    public class LevelContext : Context
    {
        [SerializeField] private LevelContextReferences levelContextReferences = default;

        private TickablesService tickablesService;

        private LevelLogicViewEntryPoint levelLogicViewEntryPoint;

        protected override void Init()
        {
            ContextsProvider.Register(this);

            tickablesService = ServicesProvider.GetService<TickablesService>();
        }

        protected override void CleanUp()
        {
            CleanUpLevel();

            ContextsProvider.Unregister(this);
        }

        public void StartLevel(LevelConfiguration levelConfiguration)
        {
            LevelSetup levelSetup = GetTestSetup(levelConfiguration);

            EventDispatcherAndReceiver eventDispatcher = new EventDispatcherAndReceiver();
            EventDispatcherAndReceiver eventRecevier = new EventDispatcherAndReceiver();

            LevelViewEntryPointSettings settings = new LevelViewEntryPointSettings(
                true
                );

            levelLogicViewEntryPoint = new LevelLogicViewEntryPoint(
                settings,
                eventRecevier,
                eventDispatcher,
                levelContextReferences.LevelLibrariesReferences.EnvironmentsLibrary,
                levelContextReferences.LevelLibrariesReferences.HeroesLibrary,
                levelContextReferences.LevelLibrariesReferences.EnemiesLibrary,
                levelContextReferences.LevelLibrariesReferences.ItemsLibrary,
                levelContextReferences.LevelLibrariesReferences.EffectsLibrary
                );


            LevelLogic levelLogic = new LevelLogicFactory().Create(
                eventDispatcher, 
                eventRecevier, 
                levelSetup
                );

            levelLogicViewEntryPoint.Start();

            tickablesService.AddTickable(levelLogic);
            levelLogic.Start();
        }

        private void CleanUpLevel()
        {

        }

        private void OnLevelViewEntryPointFinished(LevelViewEntryPointResult levelViewEntryPointResult)
        {
            //CleanUpLevel();

            //if (levelViewEntryPointResult.PlayAgain)
            //{
            //    StartLevel();
            //}
        }

        private LevelSetup GetTestSetup(LevelConfiguration levelConfiguration)
        {
            EnvironmentSetup testEnvironmentSetup = new EnvironmentSetup(
                levelConfiguration.EnvironmentTypeId,
                WalkabilityGridToInt2(levelConfiguration.WalkabilityGrid)
                );

            HeroSetup heroSetup = new HeroSetup(
                "test",
                TilemapUtils.Vector2ToInt2(levelConfiguration.HeroSpawnPosition)
                );

            List<EnemySetup> enemySetups = new List<EnemySetup>();

            foreach (Vector2Int enemySpawnPosition in levelConfiguration.EnemySpawnPositions)
            {
                enemySetups.Add(new EnemySetup(
                    "test",
                    TilemapUtils.Vector2ToInt2(enemySpawnPosition)
                    ));
            }

            List<ItemSetup> itemSetups = new List<ItemSetup>();

            foreach (Vector2Int itemSpawnPosition in levelConfiguration.ItemSpawnPositions)
            {
                itemSetups.Add(new ItemSetup(
                    ItemType.Sword,
                    TilemapUtils.Vector2ToInt2(itemSpawnPosition)
                    ));
            }

            return new LevelSetup(
                testEnvironmentSetup,
                heroSetup,
                enemySetups,
                itemSetups
                );
        }

        private IReadOnlyList<Int2> WalkabilityGridToInt2(IReadOnlyList<Vector2Int> walkabilityGrid)
        {
            List<Int2> ret = new List<Int2>(walkabilityGrid.Count);

            foreach(Vector2Int vector in walkabilityGrid)
            {
                ret.Add(TilemapUtils.Vector2ToInt2(vector));
            }

            return ret;
        }
    }
}