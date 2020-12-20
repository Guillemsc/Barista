using Barista.Client.Assets;
using System;
using UnityEngine;

namespace Barista.Client.Bootstraps
{
    [CreateAssetMenu(fileName = nameof(LevelVisualizerBootstrapSettings), menuName = "Barista/Bootstrap/" + nameof(LevelVisualizerBootstrapSettings), order = 1)]
    public class LevelVisualizerBootstrapSettings : ScriptableObject
    {
        [SerializeField] private LevelAsset levelAsset = default;

        public LevelAsset LevelAsset { get => levelAsset; set => levelAsset = value; }
    }
}