using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public interface IEnemyMovedAction
    {
        void Invoke(
            EnvironmentEntity environmentEntity,
            EnemyEntity enemyEntity,
            IReadOnlyList<Int2> path
            );
    }
}