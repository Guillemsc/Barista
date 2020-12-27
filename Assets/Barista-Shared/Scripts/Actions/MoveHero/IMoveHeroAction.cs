using Juce.Core.Direction;

namespace Barista.Shared.Actions
{
    public interface IMoveHeroAction
    {
        void Invoke(Direction4Axis direction);
    }
}