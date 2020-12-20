using Barista.Client.Assets;
using Barista.Client.EntryPoints;
using Barista.Shared.Configuration;
using Barista.Shared.EntryPoints;
using Juce.Core.Ammount;
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

        public void StartLevel(LevelAsset levelAsset)
        {
            LevelConfiguration levelConfiguration = GetTestConfiguration(levelAsset);

            IEventDispatcher eventDispatcher = new EventDispatcher();

            levelEntryPoint = new LevelEntryPoint(
                eventDispatcher,
                levelConfiguration
                );

            LevelViewEntryPointSettings settings = new LevelViewEntryPointSettings(
                true
                );

            levelViewEntryPoint = new LevelViewEntryPoint(
                settings,
                eventDispatcher,
                levelContextReferences.LevelLibrariesReferences.EnvironmentsLibrary,
                levelContextReferences.LevelLibrariesReferences.HeroesLibrary
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

        private LevelConfiguration GetTestConfiguration(LevelAsset levelAsset)
        {
            EnvironmentConfiguration testEnvironmentConfiguration = new EnvironmentConfiguration(
                levelAsset.EnvironmentTypeId
                );

            return new LevelConfiguration(
                testEnvironmentConfiguration
                );
        }
    }
}