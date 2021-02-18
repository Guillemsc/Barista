using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public interface IMoveEnemyUseCase
    {
        void Invoke(
            int enemyEntityInstanceId,
            IReadOnlyList<Int2> path
            );
    }
}
