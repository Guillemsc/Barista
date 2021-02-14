using Barista.Shared.Entities.Hero;
using Juce.Core.Direction;

namespace Barista.Shared.Logic.UseCases
{
    public interface IMoveHeroUseCase
    {
        bool CanMove(HeroEntity heroEntity, Direction4Axis direction);
        void Move(HeroEntity heroEntity, Direction4Axis direction);
    }
}
