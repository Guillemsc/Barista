using Barista.Client.Configuration.Levels;
using Barista.Client.EntryPoints;
using Barista.Client.Utils;
using Barista.Shared.Configuration;
using Barista.Shared.EntryPoints;
using Juce.Core.Containers;
using Juce.Core.Events;
using Juce.CoreUnity.Contexts;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Contexts.Level
{
    public class LevelContext : Context
    {
        [SerializeField] private LevelContextReferences levelContextReferences = default;

        private LevelEntryPoint levelEntryPoint;
        private LevelViewEntryPoint levelViewEntryPoint;

        protected override void Init()
        {
            ContextsProvider.Register(this);
        }

        protected override void CleanUp()
        {
            CleanUpLevel();

            ContextsProvider.Unregister(this);
        }

        public void StartLevel(LevelConfiguration levelConfiguration)
        {
           LevelSetup levelSetup = GetTestSetup(levelConfiguration);

            IEventDispatcher eventDispatcher = new EventDispatcher();

            levelEntryPoint = new LevelEntryPoint(
                eventDispatcher,
                levelSetup
                );

            LevelViewEntryPointSettings settings = new LevelViewEntryPointSettings(
                true
                );

            levelViewEntryPoint = new LevelViewEntryPoint(
                settings,
                eventDispatcher,
                levelContextReferences.LevelLibrariesReferences.EnvironmentsLibrary,
                levelContextReferences.LevelLibrariesReferences.HeroesLibrary,
                levelContextReferences.LevelLibrariesReferences.EnemiesLibrary
                );

            levelViewEntryPoint.Execute();
            levelViewEntryPoint.OnFinish += OnLevelViewEntryPointFinished;

            levelEntryPoint.Execute();
        }

        private void CleanUpLevel()
        {
            if (levelViewEntryPoint == null)
            {
                return;
            }

            levelViewEntryPoint.OnFinish -= OnLevelViewEntryPointFinished;
            levelViewEntryPoint.CleanUp();
            levelViewEntryPoint = null;
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

            foreach(Vector2Int enemySpawnPosition in levelConfiguration.EnemySpawnPositions)
            {
                enemySetups.Add(new EnemySetup(
                    "test",
                    TilemapUtils.Vector2ToInt2(enemySpawnPosition)
                    ));
            }

            return new LevelSetup(
                testEnvironmentSetup,
                heroSetup,
                enemySetups
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