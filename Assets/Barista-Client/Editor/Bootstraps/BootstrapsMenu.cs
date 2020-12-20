using Barista.Client.Constants;
using Juce.CoreUnity.Scenes;
using UnityEditor;

namespace Barista.Client.Bootstraps
{
    public static class ScenesMenu
    {
        [MenuItem("Barista/Scenes/MainBootstrap", priority = 1)]
        public static void MainBootstrap()
        {
            ScenesUtils.OpenScene(ScenesConstants.MainBootstrapScene);
        }

        [MenuItem("Barista/Scenes/LevelVisualizerBootstrap", priority = 1)]
        public static void LevelVisualizerBootstrap()
        {
            ScenesUtils.OpenScene(ScenesConstants.LevelVisualizerBootstrapScene);
        }

        [MenuItem("Barista/Scenes/Services", priority = 100)]
        public static void Services()
        {
            ScenesUtils.OpenScene(ScenesConstants.ServicesScene);
        }

        [MenuItem("Barista/Scenes/Cameras", priority = 100)]
        public static void Cameras()
        {
            ScenesUtils.OpenScene(ScenesConstants.CamerasScene);
        }

        [MenuItem("Barista/Scenes/Level", priority = 100)]
        public static void Level()
        {
            ScenesUtils.OpenScene(ScenesConstants.LevelScene);
        }

        [MenuItem("Barista/Scenes/LevelUI", priority = 100)]
        public static void LevelUI()
        {
            ScenesUtils.OpenScene(ScenesConstants.LevelUIScene);
        }
    }
}