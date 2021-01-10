using Barista.Client.References.LevelUI;
using System;
using UnityEngine;

namespace Barista.Client.Contexts.LevelUI
{
    [System.Serializable]
    public class LevelUIContextReferences
    {
        [SerializeField] private LevelUICanvases levelUICanvases = default;
        [SerializeField] private LevelUIReferences levelUIViewModelsReferences = default;

        public LevelUICanvases LevelUICanvases => levelUICanvases;
        public LevelUIReferences LevelUIReferences => levelUIViewModelsReferences;
    }
}