using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;
using Juce.Core.Containers;
using Juce.Core.Direction;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public class HeroMovementActions : IHeroMovementActions
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly PathFactory pathfindingFactory;
        private readonly LevelState levelState;

        public HeroMovementActions(
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

        public bool CanMoveHero(Direction4Axis direction)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition, 1);

            IReadOnlyList<Int2> path = pathfindingFactory.CreatePath(heroEntity.GridPosition, newPosition);

            if (path.Count == 0)
            {
                return false;
            }

            bool isSamePosition = path[path.Count - 1] == heroEntity.GridPosition;

            if (isSamePosition)
            {
                return false;
            }

            return true;
        }

        public bool MoveHero(Direction4Axis direction)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool canMove = CanMoveHero(direction);

            if (!canMove)
            {
                return false;
            }

            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition, 1);

            IReadOnlyList<Int2> path = pathfindingFactory.CreatePath(heroEntity.GridPosition, newPosition);

            if (path.Count == 0)
            {
                return false;
            }

            bool isSamePosition = path[path.Count - 1] == heroEntity.GridPosition;

            if (isSamePosition)
            {
                return false;
            }

            heroEntity.SetGridPosition(newPosition);

            EnvironmentEntity environmentEntity = environmentEntityRepository.Get(levelState.LoadedEnvironmentId);

            eventDispatcher.Dispatch(new HeroMovedOutEvent(
                environmentEntity,
                heroEntity,
                path
                ));

            return true;
        }

        private Int2 GetNewPositionFormDirection(
           Direction4Axis direction,
           Int2 position,
           int distance
           )
        {
            Int2 newPosition = new Int2(position);

            switch (direction)
            {
                case Direction4Axis.Up:
                    {
                        newPosition.Y += distance;
                    }
                    break;

                case Direction4Axis.Down:
                    {
                        newPosition.Y -= distance;
                    }
                    break;

                case Direction4Axis.Left:
                    {
                        newPosition.X -= distance;
                    }
                    break;

                case Direction4Axis.Right:
                    {
                        newPosition.X += distance;
                    }
                    break;
            }

            return newPosition;
        }
    }
}
