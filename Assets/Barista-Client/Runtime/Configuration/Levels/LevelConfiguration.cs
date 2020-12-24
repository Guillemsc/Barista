﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Configuration.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelConfiguration), menuName = "Barista/Configuration/" + nameof(LevelConfiguration), order = 1)]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField] public string EnvironmentTypeId { get; set; }
        [SerializeField] public List<Vector2Int> WalkabilityGrid { get; set; }
    }
}