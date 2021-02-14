using Juce.Core.Direction;

namespace Barista.Client.Level.UseCases
{
    public interface IMovementInputPerformedUseCase
    {
        void Invoke(Direction4Axis direction);
    }
}
