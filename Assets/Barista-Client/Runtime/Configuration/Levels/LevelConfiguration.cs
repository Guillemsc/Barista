using System;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Configuration.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelConfiguration), menuName = "Barista/Configuration/" + nameof(LevelConfiguration), order = 1)]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField] public string EnvironmentTypeId { get; set; }
        [SerializeField] public List<Vector2Int> WalkabilityGrid { get; set; }
        [SerializeField] public Vector2Int HeroSpawnPosition { get; set; }
        [SerializeField] public List<Vector2Int> EnemySpawnPositions { get; set; }
        [SerializeField] public List<Vector2Int> ItemSpawnPositions { get; set; }
    }
}