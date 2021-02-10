using Juce.Core.Containers;
using UnityEngine;

namespace Barista.Client.View.Effects.TargetSelector
{
    public interface ITargetSelectorViewFactory
    {
        TargetSelectorView Create(Int2 gridPosition);
        void Destroy(TargetSelectorView toDestroy);
    }
}