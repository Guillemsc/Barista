using Juce.Core.Containers;
using Juce.Core.Direction;

namespace Barista.Shared.Events
{
    public class ItemTargetSelectedInEvent
    {
        public Int2 GridPosition { get; }

        public ItemTargetSelectedInEvent(Int2 gridPosition)
        {
            GridPosition = gridPosition;
        }
    }
}