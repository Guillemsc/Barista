using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Barista.Shared.Factories;
using Juce.Core.Containers;
using Juce.Core.Direction;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Actions
{
    public class MoveHeroAction : IMoveHeroAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly PathfindingFactory pathfindingFactory;
        private readonly LevelState levelState;

        public MoveHeroAction(
            IEventDispatcher eventDispatcher,
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            PathfindingFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public void Invoke(Direction4Axis direction)
        {
            EnvironmentEntity environmentEntity = environmentEntityRepository.Get(levelState.LoadedEnvironmentId);
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            Int2 newPosition = GetNewPositionFormDirection(direction, heroEntity.GridPosition);

            IReadOnlyList<Int2> path = pathfindingFactory.Create(heroEntity.GridPosition, newPosition);

            if(path.Count == 0)
            {
                return;
            }

            bool isSamePosition = path[path.Count - 1] == heroEntity.GridPosition;

            if(isSamePosition)
            {
                return;
            }

            heroEntity.GridPosition = newPosition;

            eventDispatcher.Dispatch(new StartTurnOutEvent());

            eventDispatcher.Dispatch(new HeroMovedOutEvent(
                environmentEntity,
                heroEntity,
                path
                ));

            eventDispatcher.Dispatch(new EndTurnOutEvent());
        }

        private Int2 GetNewPositionFormDirection(Direction4Axis direction, Int2 position)
        {
            Int2 newPosition = new Int2(position);

            switch(direction)
            {
                case Direction4Axis.Up:
                    {
                        newPosition.Y += 1;
                    }
                    break;

                case Direction4Axis.Down:
                    {
                        newPosition.Y -= 1;
                    }
                    break;

                case Direction4Axis.Left:
                    {
                        newPosition.X -= 1;
                    }
                    break;

                case Direction4Axis.Right:
                    {
                        newPosition.X += 1;
                    }
                    break;
            }

            return newPosition;
        }
    }
}