using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;
using Juce.Core.Containers;
using Juce.Core.Direction;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic.UseCases
{
    public class MoveHeroUseCase : IMoveHeroUseCase
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly PathFactory pathfindingFactory;

        public MoveHeroUseCase(
            IEventDispatcher eventDispatcher,
            PathFactory pathfindingFactory
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.pathfindingFactory = pathfindingFactory;
        }

        public bool CanMove(HeroEntity heroEntity, Direction4Axis direction)
        {
            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition, range: 1);

            IReadOnlyList<Int2> path = pathfindingFactory.CreatePath(heroEntity.GridPosition, newPosition);

            if (path.Count == 0)
            {
                return false;
            }

            bool isSamePosition = path[path.Count - 1].Equals(heroEntity.GridPosition);

            if (isSamePosition)
            {
                return false;
            }

            return true;
        }

        public void Move(HeroEntity heroEntity, Direction4Axis direction)
        {
            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition, range: 1);

            IReadOnlyList<Int2> path = pathfindingFactory.CreatePath(heroEntity.GridPosition, newPosition);

            if (path.Count == 0)
            {
                return;
            }

            bool isSamePosition = path[path.Count - 1].Equals(heroEntity.GridPosition);

            if (isSamePosition)
            {
                return;
            }

            heroEntity.SetGridPosition(newPosition);

            eventDispatcher.Dispatch(new HeroMovedOutEvent(
                heroEntity.InstanceId,
                path
                ));
        }

        private Int2 GetNewPositionFormDirection(
            Direction4Axis direction,
            Int2 position,
            int range
            )
        {
            Int2 newPosition = new Int2(position);

            switch (direction)
            {
                case Direction4Axis.Up:
                    {
                        newPosition.Y += range;
                    }
                    break;

                case Direction4Axis.Down:
                    {
                        newPosition.Y -= range;
                    }
                    break;

                case Direction4Axis.Left:
                    {
                        newPosition.X -= range;
                    }
                    break;

                case Direction4Axis.Right:
                    {
                        newPosition.X += range;
                    }
                    break;
            }

            return newPosition;
        }
    }
}
