using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public interface IItemTargetSelection
    {
        void Invoke(IReadOnlyList<Int2> avaliableTargetPositions);
    }
}