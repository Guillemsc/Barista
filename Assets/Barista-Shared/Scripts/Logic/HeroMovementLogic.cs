using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Factories;
using Juce.Core.Containers;
using Juce.Core.Direction;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public static class HeroMovementLogic
    {
        public static bool CanMoveHero(
            PathfindingFactory pathfindingFactory,
            HeroEntity heroEntity,
            Direction4Axis direction
            )
        {
            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition, 1);

            IReadOnlyList<Int2> path = pathfindingFactory.Create(heroEntity.GridPosition, newPosition);

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

        public static void MoveHero(
            IEventDispatcher eventDispatcher,
            PathfindingFactory pathfindingFactory,
            EnvironmentEntity environmentEntity,
            HeroEntity heroEntity,
            Direction4Axis direction
            )
        {
            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition, 1);

            IReadOnlyList<Int2> path = pathfindingFactory.Create(heroEntity.GridPosition, newPosition);

            if (path.Count == 0)
            {
                return;
            }

            bool isSamePosition = path[path.Count - 1] == heroEntity.GridPosition;

            if (isSamePosition)
            {
                return;
            }

            heroEntity.GridPosition = newPosition;

            eventDispatcher.Dispatch(new HeroMovedOutEvent(
                environmentEntity,
                heroEntity,
                path
                ));
        }

        private static Int2 GetNewPositionFormDirection(
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
