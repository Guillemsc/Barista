using Juce.Core.Direction;

namespace Barista.Shared.Events
{
    public class MoveHeroActionInEvent
    {
        public Direction4Axis Direction { get; }

        public MoveHeroActionInEvent(Direction4Axis direction)
        {
            Direction = direction;
        }
    }
}