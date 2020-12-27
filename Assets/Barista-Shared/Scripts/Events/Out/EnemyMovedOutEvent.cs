using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class EnemyMovedOutEvent
    {
        public EnvironmentEntity EnvironmentEntity { get; }
        public EnemyEntity EnemyEntity { get; }
        public IReadOnlyList<Int2> Path { get; }

        public EnemyMovedOutEvent(EnvironmentEntity environmentEntity, EnemyEntity enemyEntity, IReadOnlyList<Int2> path)
        {
            EnvironmentEntity = environmentEntity;
            EnemyEntity = enemyEntity;
            Path = path;
        }
    }
}