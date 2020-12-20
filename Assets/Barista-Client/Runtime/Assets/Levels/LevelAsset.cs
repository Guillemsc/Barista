using System;
using UnityEngine;

namespace Barista.Client.Assets
{
    [CreateAssetMenu(fileName = nameof(LevelAsset), menuName = "Barista/Assets/" + nameof(LevelAsset), order = 1)]
    public class LevelAsset : ScriptableObject
    {
        [SerializeField] private string environmentTypeId = default;

        public string EnvironmentTypeId => environmentTypeId;
    }
}