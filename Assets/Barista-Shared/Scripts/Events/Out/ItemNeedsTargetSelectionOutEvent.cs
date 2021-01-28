using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class ItemNeedsTargetSelectionOutEvent
    {
        public IReadOnlyList<Int2> AvaliableTargetPositions { get; }

        public ItemNeedsTargetSelectionOutEvent(IReadOnlyList<Int2> avaliableTargetPositions)
        {
            AvaliableTargetPositions = avaliableTargetPositions;
        }
    }
}
