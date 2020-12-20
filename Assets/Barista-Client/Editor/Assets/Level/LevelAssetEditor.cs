using Barista.Client.Bootstraps;
using Barista.Client.Constants;
using Barista.Client.Libraries;
using Juce.CoreUnity.Assets;
using Juce.CoreUnity.Scenes;
using UnityEditor;
using UnityEngine;

namespace Barista.Client.Assets
{
    [CustomEditor(typeof(LevelAsset))]
    public class LevelAssetEditor : Editor
    {
        private LevelAsset targetData;

        private LevelVisualizerBootstrapSettings levelVisualizerBootstrapSettings;

        private void OnEnable()
        {
            targetData = (LevelAsset)target;

            GatherDependences();
        }

        public override void OnInspectorGUI()
        {
            if (!HasDependences())
            {
                return;
            }

            DrawPlaySection();

            base.DrawDefaultInspector();
        }

        private void DrawPlaySection()
        {
            if (GUILayout.Button("Play"))
            {
                LoadLevelVisualizer();
            }
        }

        private void GatherDependences()
        {
            levelVisualizerBootstrapSettings = AssetsUtils.FindAssetByType<LevelVisualizerBootstrapSettings>();
        }

        private bool HasDependences()
        {
            if (levelVisualizerBootstrapSettings == null)
            {
                return false;
            }

            return true;
        }

        private void LoadLevelVisualizer()
        {
            ScenesUtils.OpenScene(ScenesConstants.LevelVisualizerBootstrapScene);

            levelVisualizerBootstrapSettings.LevelAsset = targetData;

            EditorApplication.isPlaying = true;
        }
    }
}