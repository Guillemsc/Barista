using Juce.Core.Direction;

namespace Barista.Shared.Logic
{
    public interface IHeroMovementActions
    {
        bool CanMoveHero(Direction4Axis direction);
        bool MoveHero(Direction4Axis direction);
    }
}
