using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Barista.Shared.Factories;
using Barista.Shared.Logic;
using Juce.Core.Containers;
using Juce.Core.Direction;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Actions
{
    public class MoveHeroAction : IMoveHeroAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly TurnLogic turnLogic;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly PathfindingFactory pathfindingFactory;
        private readonly LevelState levelState;

        public MoveHeroAction(
            IEventDispatcher eventDispatcher,
            TurnLogic turnLogic,
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            PathfindingFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.turnLogic = turnLogic;
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public void Invoke(Direction4Axis direction)
        {
            EnvironmentEntity environmentEntity = environmentEntityRepository.Get(levelState.LoadedEnvironmentId);
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool canMove = HeroMovementLogic.CanMoveHero(pathfindingFactory, heroEntity, direction);

            if(!canMove)
            {
                return;
            }

            turnLogic.StartTurn();

            HeroMovementLogic.MoveHero(eventDispatcher, pathfindingFactory, environmentEntity, heroEntity, direction);

            turnLogic.TickTurn();

            turnLogic.EndTurn();
        }
    }
}