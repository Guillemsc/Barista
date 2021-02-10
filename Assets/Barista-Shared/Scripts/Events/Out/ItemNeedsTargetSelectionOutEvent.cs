using Barista.Shared.Entities.Environment;
using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class ItemNeedsTargetSelectionOutEvent
    {
        public ItemType ItemType { get; }
        public IReadOnlyList<Int2> AvaliableTargetPositions { get; }

        public ItemNeedsTargetSelectionOutEvent(
            ItemType itemType,
            IReadOnlyList<Int2> avaliableTargetPositions
            )
        {
            ItemType = itemType;
            AvaliableTargetPositions = avaliableTargetPositions;
        }
    }
}
