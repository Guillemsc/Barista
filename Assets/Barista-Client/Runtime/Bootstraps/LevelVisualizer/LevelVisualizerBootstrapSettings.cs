using Barista.Client.Configuration.Levels;
using UnityEngine;

namespace Barista.Client.Bootstraps
{
    [CreateAssetMenu(fileName = nameof(LevelVisualizerBootstrapSettings), menuName = "Barista/Bootstrap/" + nameof(LevelVisualizerBootstrapSettings), order = 1)]
    public class LevelVisualizerBootstrapSettings : ScriptableObject
    {
        [SerializeField] private LevelConfiguration levelConfiguration = default;

        public LevelConfiguration LevelConfiguration { get => levelConfiguration; set => levelConfiguration = value; }
    }
}