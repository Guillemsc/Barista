using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public interface IMoveHeroUseCase
    {
        Instruction Move(
            int heroEntityInstanceId,
            IReadOnlyList<Int2> path
            );
    }
}
