using Barista.Shared.Entities.Environment;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public interface IStartItemTargetSelection : IAction
    {
        void Invoke(
            IReadOnlyList<Int2> avaliableTargetPositions
            );
    }
}