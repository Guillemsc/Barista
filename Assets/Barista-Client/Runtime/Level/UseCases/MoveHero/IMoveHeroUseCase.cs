using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public interface IMoveHeroUseCase
    {
        void Invoke(
            int heroEntityInstanceId,
            IReadOnlyList<Int2> path
            );
    }
}
