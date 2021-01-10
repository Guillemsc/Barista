using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Barista.Shared.Logic.Pathfinding;
using Juce.Core.Containers;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public class EnemyMovementLogic
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly PathfindingFactory pathfindingFactory;
        private readonly LevelState levelState;

        public EnemyMovementLogic(
            IEventDispatcher eventDispatcher,
            EnvironmentEntityRepository environmentEntityRepository,
            PathfindingFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.environmentEntityRepository = environmentEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public void MoveEnemyTowardsHero(
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

            EnvironmentEntity environmentEntity = environmentEntityRepository.Get(levelState.LoadedEnvironmentId);

            eventDispatcher.Dispatch(new EnemyMovedOutEvent(
                environmentEntity,
                enemyEntity,
                rangedPath
                ));
        }
    }
}
