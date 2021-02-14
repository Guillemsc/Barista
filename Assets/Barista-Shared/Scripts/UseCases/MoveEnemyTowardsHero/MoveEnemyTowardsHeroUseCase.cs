using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Logic.Pathfinding;
using Juce.Core.Containers;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic.UseCases
{
    public class MoveEnemyTowardsHeroUseCase : IMoveEnemyTowardsHeroUseCase
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly PathFactory pathfindingFactory;

        public MoveEnemyTowardsHeroUseCase(
            IEventDispatcher eventDispatcher,
            PathFactory pathfindingFactory
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.pathfindingFactory = pathfindingFactory;
        }

        public bool CanMove(EnemyEntity enemyEntity, HeroEntity heroEntity, int range)
        {
            return false;
        }

        public void Move(EnemyEntity enemyEntity, HeroEntity heroEntity, int range)
        {
            IReadOnlyList<Int2> path = pathfindingFactory.CreatePath(enemyEntity.GridPosition, heroEntity.LastGridPosition);

            if (path.Count == 0)
            {
                return;
            }

            List<Int2> rangedPath = new List<Int2>();

            range += 1;

            for (int i = 0; i < range; ++i)
            {
                if (i >= path.Count)
                {
                    break;
                }

                rangedPath.Add(path[i]);
            }


            bool isSamePosition = rangedPath[rangedPath.Count - 1].Equals(enemyEntity.GridPosition);

            if (isSamePosition)
            {
                return;
            }

            enemyEntity.GridPosition = rangedPath[rangedPath.Count - 1];

            eventDispatcher.Dispatch(new EnemyMovedOutEvent(
                enemyEntity.InstanceId,
                rangedPath
                ));
        }
    }
}
