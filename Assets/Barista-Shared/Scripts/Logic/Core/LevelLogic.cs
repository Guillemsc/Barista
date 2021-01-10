using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Barista.Shared.Logic.EnemyActions;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Pathfinding;
using Juce.Core.Events;
using System;

namespace Barista.Shared.Logic
{
    public class LevelLogic
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly EnemyEntityRepository enemyEntityRepository;
        private readonly ItemEntityRepository itemEntityRepository;
        private readonly PathfindingFactory pathfindingFactory;
        private readonly ItemFactory itemFactory;
        private readonly LevelState levelState;

        public HeroMovementLogic HeroMovementLogic { get; }
        public EnemyMovementLogic EnemyMovementLogic { get; }
        public HeroGrabItemsLogic HeroGrabItemsLogic { get; }

        public LevelLogic(
            IEventDispatcher eventDispatcher,
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            EnemyEntityRepository enemyEntityRepository,
            ItemEntityRepository itemEntityRepository,
            PathfindingFactory pathfindingFactory,
            ItemFactory itemFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.enemyEntityRepository = enemyEntityRepository;
            this.pathfindingFactory = pathfindingFactory;
            this.itemFactory = itemFactory;
            this.levelState = levelState;

            HeroMovementLogic = new HeroMovementLogic(
                this,
                eventDispatcher,
                environmentEntityRepository,
                pathfindingFactory,
                levelState
                );

            EnemyMovementLogic = new EnemyMovementLogic(
                eventDispatcher,
                environmentEntityRepository,
                pathfindingFactory,
                levelState
                );

            HeroGrabItemsLogic = new HeroGrabItemsLogic(
                eventDispatcher,
                itemEntityRepository,
                itemFactory
                );
        }

        public void StartTurn()
        {
            eventDispatcher.Dispatch(new StartTurnOutEvent());
        }

        public void TickTurn()
        {
            foreach(EnemyEntity enemyEntity in enemyEntityRepository.Elements)
            {
                IEnemyAction enemyAction = enemyEntity.EnemyBrain.GenerateNextEnemyAction(enemyEntity);

                switch(enemyAction)
                {
                    case AttackEntityEnemyAction action:
                        {

                        }
                        break;

                    case MoveTowardsHeroEnemyAction action:
                        {
                            EnemyMovementLogic.MoveEnemyTowardsHero(
                                enemyEntity,
                                action.HeroEntityToReach,
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
