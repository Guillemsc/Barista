using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;
using Juce.Core.Containers;
using Juce.Core.Direction;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public class HeroMovementLogic
    {
        private readonly LevelLogic levelLogic;
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly PathFactory pathfindingFactory;
        private readonly LevelState levelState;

        public HeroMovementLogic(
            LevelLogic levelLogic,
            IEventDispatcher eventDispatcher,
            EnvironmentEntityRepository environmentEntityRepository,
            PathFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.levelLogic = levelLogic;
            this.environmentEntityRepository = environmentEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public bool CanMoveHero(
            HeroEntity heroEntity,
            Direction4Axis direction
            )
        {
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

        public void MoveHero(
            HeroEntity heroEntity,
            Direction4Axis direction
            )
        {
            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition, 1);

            IReadOnlyList<Int2> path = pathfindingFactory.CreatePath(heroEntity.GridPosition, newPosition);

            if (path.Count == 0)
            {
                return;
            }

            bool isSamePosition = path[path.Count - 1] == heroEntity.GridPosition;

            if (isSamePosition)
            {
                return;
            }

            heroEntity.SetGridPosition(newPosition);

            EnvironmentEntity environmentEntity = environmentEntityRepository.Get(levelState.LoadedEnvironmentId);

            eventDispatcher.Dispatch(new HeroMovedOutEvent(
                environmentEntity,
                heroEntity,
                path
                ));

            bool couldGetItem = levelLogic.HeroGrabItemsLogic.TryGetItemAtPosition(heroEntity.GridPosition, out ItemEntity itemEntity);

            if(!couldGetItem)
            {
                return;
            }

            levelLogic.HeroGrabItemsLogic.HeroGrabsItem(heroEntity, itemEntity);
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
