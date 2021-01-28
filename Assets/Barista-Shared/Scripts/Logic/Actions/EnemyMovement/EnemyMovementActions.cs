using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;
using Juce.Core.Containers;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public class EnemyMovementActions : IEnemyMovementActions
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly PathFactory pathfindingFactory;
        private readonly LevelState levelState;

        public EnemyMovementActions(
            IEventDispatcher eventDispatcher,
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            PathFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public void MoveEnemyTowardsHero(EnemyEntity enemyEntity, int range)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

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
