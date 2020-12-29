using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Factories;
using Juce.Core.Containers;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public static class EnemiesMovementLogic
    {
        public static void MoveEnemyTowardsEntity(
            IEventDispatcher eventDispatcher,
            PathfindingFactory pathfindingFactory,
            EnvironmentEntity environmentEntity,
            EnemyEntity enemyEntity,
            HeroEntity heroEntity,
            int range
            )
        {
            IReadOnlyList<Int2> path = pathfindingFactory.Create(enemyEntity.GridPosition, heroEntity.LastGridPosition);

            if (path.Count == 0)
            {
                return;
            }

            List<Int2> rangedPath = new List<Int2>();

            range += 1;

            for(int i = 0; i < range; ++i)
            {
                if(i >= path.Count)
                {
                    break;
                }

                rangedPath.Add(path[i]);
            }


            bool isSamePosition = rangedPath[rangedPath.Count - 1] == enemyEntity.GridPosition;

            if (isSamePosition)
            {
                return;
            }

            enemyEntity.GridPosition = rangedPath[rangedPath.Count - 1];

            eventDispatcher.Dispatch(new EnemyMovedOutEvent(
                environmentEntity,
                enemyEntity,
                rangedPath
                ));
        }
    }
}
