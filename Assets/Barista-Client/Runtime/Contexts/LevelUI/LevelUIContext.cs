using Juce.CoreUnity.Contexts;
using System;
using UnityEngine;

namespace Barista.Client.Contexts.LevelUI
{
    public class LevelUIContext : Context
    {
        [SerializeField] private LevelUIContextReferences levelUIContextReferences = default;

        public LevelUIContextReferences LevelUIContextReferences => levelUIContextReferences;

        protected override void Init()
        {
            ContextsProvider.Register(this);
        }

        protected override void CleanUp()
        {
            ContextsProvider.Unregister(this);
        }
    }
}