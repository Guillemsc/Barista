using Juce.Core.Containers;

namespace Barista.Client.Actions
{
    public interface IItemTargetSelectedAction : IAction
    {
        void Invoke(Int2 gridPosition);
    }
}