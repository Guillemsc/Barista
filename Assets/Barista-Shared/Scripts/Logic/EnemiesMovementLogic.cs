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
        public static void MoveEnemyTowardsHero(
            IEventDispatcher eventDispatcher,
            PathfindingFactory pathfindingFactory,
            EnvironmentEntity environmentEntity,
            EnemyEntity enemyEntity,
            HeroEntity heroEntity,
            int range
            )
        {
            IReadOnlyList<Int2> path = pathfindingFactory.Create(enemyEntity.GridPosition, heroEntity.GridPosition);

            if (path.Count == 0)
            {
                return;
            }

            bool isSamePosition = path[path.Count - 1] == heroEntity.GridPosition;

            if (isSamePosition)
            {
                return;
            }

            List<Int2> rangedPath = new List<Int2>();

            for(int i = 0; i < range; ++i)
            {
                if(i >= path.Count)
                {
                    break;
                }

                rangedPath.Add(path[i]);
            }

            heroEntity.GridPosition = rangedPath[rangedPath.Count - 1];

            eventDispatcher.Dispatch(new EnemyMovedOutEvent(
                environmentEntity,
                enemyEntity,
                rangedPath
                ));
        }
    }
}
