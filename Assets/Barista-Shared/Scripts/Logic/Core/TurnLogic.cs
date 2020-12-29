using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Barista.Shared.Factories;
using Barista.Shared.Logic.EnemyActions;
using Juce.Core.Events;
using System;

namespace Barista.Shared.Logic
{
    public class TurnLogic
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly EnemyEntityRepository enemyEntityRepository;
        private readonly PathfindingFactory pathfindingFactory;
        private readonly LevelState levelState;

        public TurnLogic(
            IEventDispatcher eventDispatcher,
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            EnemyEntityRepository enemyEntityRepository,
            PathfindingFactory pathfindingFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.enemyEntityRepository = enemyEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.levelState = levelState;
        }

        public void StartTurn()
        {
            eventDispatcher.Dispatch(new StartTurnOutEvent());
        }

        public void TickTurn()
        {
            EnvironmentEntity environmentEntity = environmentEntityRepository.Get(levelState.LoadedEnvironmentId);
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            foreach(EnemyEntity enemyEntity in enemyEntityRepository.Elements)
            {
                IEnemyAction enemyAction = enemyEntity.EnemyBrain.GenerateNextEnemyAction(enemyEntity);

                switch(enemyAction)
                {
                    case AttackEntityEnemyAction action:
                        {

                        }
                        break;

                    case MoveTowardsEntityEnemyAction action:
                        {
                            EnemiesMovementLogic.MoveEnemyTowardsEntity(
                                eventDispatcher,
                                pathfindingFactory,
                                environmentEntity,
                                enemyEntity,
                                heroEntity,
                                1
                                );
                        }
                        break;

                    case IdleEnemyAction action:
                        {

                        }
                        break;
                }
            }
        }

        public void EndTurn()
        {
            eventDispatcher.Dispatch(new EndTurnOutEvent());
        }
    }
}
