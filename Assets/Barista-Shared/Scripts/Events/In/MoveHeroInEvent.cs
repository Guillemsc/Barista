using Juce.Core.Direction;

namespace Barista.Shared.Events
{
    public class MoveHeroInEvent
    {
        public Direction4Axis Direction { get; }

        public MoveHeroInEvent(Direction4Axis direction)
        {
            Direction = direction;
        }
    }
}