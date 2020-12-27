using Barista.Client.Bootstraps;
using Barista.Client.Constants;
using Barista.Client.Libraries;
using Barista.Client.Utils;
using Juce.CoreUnity.Assets;
using Juce.CoreUnity.Scenes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Barista.Client.Configuration.Levels
{
    [CustomEditor(typeof(LevelConfiguration))]
    public class LevelConfigurationEditor : Editor
    {
        private LevelConfiguration targetData;

        private LevelVisualizerBootstrapSettings levelVisualizerBootstrapSettings;
        private EnvironmentsLibrary environmentsLibrary;
        private HeroesLibrary heroesLibrary;

        private int selectedEnvironmentIndex;

        private void OnEnable()
        {
            targetData = (LevelConfiguration)target;

            GatherDependences();

            LoadData();
        }

        public override void OnInspectorGUI()
        {
            DrawEnvironmentsDropdown();
            DrawPlaySection();

            base.DrawDefaultInspector();
        }

        private void DrawPlaySection()
        {
            bool valid = true;
            List<string> errors = new List<string>();

            ValidateDependences(ref valid, ref errors);

            if (!valid)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Validation errors: ");
                foreach (string error in errors)
                {
                    EditorGUILayout.LabelField($"- {error}");
                }
            }
            else
            {
                if (GUILayout.Button("Play"))
                {
                    LoadLevelVisualizer();
                }
            }
        }

        private void GatherDependences()
        {
            levelVisualizerBootstrapSettings = AssetsUtils.FindAssetByType<LevelVisualizerBootstrapSettings>();
            environmentsLibrary = AssetsUtils.FindAssetByType<EnvironmentsLibrary>();
            heroesLibrary = AssetsUtils.FindAssetByType<HeroesLibrary>();
        }

        private void DrawEnvironmentsDropdown()
        {
            if (environmentsLibrary == null)
            {
                return;
            }

            string[] options = new string[environmentsLibrary.Items.Count];

            for (int i = 0; i < environmentsLibrary.Items.Count; ++i)
            {
                options[i] = environmentsLibrary.Items[i].TypeId;
            }

            int lastSelectedEnvironmentIndex = selectedEnvironmentIndex;

            selectedEnvironmentIndex = EditorGUILayout.Popup("Environments", selectedEnvironmentIndex, options);

            if (selectedEnvironmentIndex < environmentsLibrary.Items.Count)
            {
                if (lastSelectedEnvironmentIndex != selectedEnvironmentIndex)
                {
                    SaveData();
                }
            }
        }

        private void ValidateDependences(ref bool valid, ref List<string> errors)
        {
            if (levelVisualizerBootstrapSettings == null)
            {
                valid &= false;
                errors.Add("Could not find LevelVisualizerBootstrapSettings on the project");
                return;
            }

            if (environmentsLibrary == null)
            {
                valid &= false;
                errors.Add("Could not find EnvironmentsLibrary on the project");
                return;
            }

            if (heroesLibrary == null)
            {
                valid &= false;
                errors.Add("Could not find HeroesLibrary on the project");
                return;
            }
        }

        private void LoadLevelVisualizer()
        {
            SaveData();

            ScenesUtils.OpenScene(ScenesConstants.LevelVisualizerBootstrapScene);

            levelVisualizerBootstrapSettings.LevelConfiguration = targetData;

            EditorApplication.isPlaying = true;
        }

        private void LoadData()
        {
            if (environmentsLibrary == null)
            {
                return;
            }

            for (int i = 0; i < environmentsLibrary.Items.Count; ++i)
            {
                if (string.Equals(environmentsLibrary.Items[i].TypeId, targetData.EnvironmentTypeId))
                {
                    selectedEnvironmentIndex = i;
                }
            }
        }

        private void SaveData()
        {
            if (environmentsLibrary == null)
            {
                return;
            }

            if (selectedEnvironmentIndex < environmentsLibrary.Items.Count)
            {
                EnvironmentsLibraryItem environmentItem = environmentsLibrary.Items[selectedEnvironmentIndex];

                targetData.EnvironmentTypeId = environmentItem.TypeId;
                targetData.WalkabilityGrid = EnvironmentUtils.GenerateWalkability(environmentItem.Prefab);
                targetData.HeroSpawnPosition = EnvironmentUtils.GenerateHeroSpawnPosition(environmentItem.Prefab);
            }

            EditorUtility.SetDirty(targetData);
        }
    }
}