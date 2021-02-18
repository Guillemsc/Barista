using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class StartHeroItemTargetSelectionOutEvent
    {
        public ItemType ItemType { get; }
        public ItemTargetType ItemTargetType { get; }
        public IReadOnlyList<Int2> AvaliableTargetPositions { get; }

        public StartHeroItemTargetSelectionOutEvent(
            ItemType itemType,
            ItemTargetType itemTargetType,
            IReadOnlyList<Int2> avaliableTargetPositions
            )
        {
            ItemType = itemType;
            ItemTargetType = itemTargetType;
            AvaliableTargetPositions = avaliableTargetPositions;
        }
    }
}
