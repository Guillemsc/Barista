using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public interface IMoveEnemyUseCase
    {
        Instruction Move(
            int enemyEntityInstanceId,
            IReadOnlyList<Int2> path
            );
    }
}
