using Barista.Client.Libraries;
using Barista.Client.Signals;
using Juce.Core.Containers;
using UnityEngine;

namespace Barista.Client.View.Effects.TargetSelector
{
    public class TargetSelectorViewFactory : ITargetSelectorViewFactory
    {
        private readonly EffectsLibrary effectsLibrary;
        private readonly TargetSelectorSelectedSignal targetSelectorClickedSignal;

        public TargetSelectorViewFactory(
            EffectsLibrary effectsLibrary,
            TargetSelectorSelectedSignal targetSelectorClickedSignal
            )
        {
            this.effectsLibrary = effectsLibrary;
            this.targetSelectorClickedSignal = targetSelectorClickedSignal;
        }

        public TargetSelectorView Create(Int2 gridPosition)
        {
            TargetSelectorView targetSelectorView = effectsLibrary.TargetSelectorViewPrefab.InstantiateGameObjectAndGet();

            targetSelectorView.Init(targetSelectorClickedSignal, gridPosition);

            return targetSelectorView;
        }

        public void Destroy(TargetSelectorView toDestroy)
        {
            toDestroy.DestroyGameObject();
        }
    }
}