using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic.EnemyActions;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;
using Juce.Core.Events;
using Juce.Core.State;

namespace Barista.Shared.Logic
{
    public class LevelLogic
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly EnemyEntityRepository enemyEntityRepository;
        private readonly ILevelSetupLogicActions levelSetupLogicActions;
        private readonly IHeroMovementActions heroMovementActions;
        private readonly IEnemyMovementActions enemyMovementActions;
        private readonly IHeroGrabItemsLogicAction heroGrabItemsLogicAction;

        private StateMachine<LevelLogicState> stateMachine = new StateMachine<LevelLogicState>();

        public LevelLogic(
            IEventDispatcher eventDispatcher,
            EnemyEntityRepository enemyEntityRepository,
            ILevelSetupLogicActions levelSetupLogicActions,
            IHeroMovementActions heroMovementActions,
            IEnemyMovementActions enemyMovementActions,
            IHeroGrabItemsLogicAction heroGrabItemsLogicAction
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.enemyEntityRepository = enemyEntityRepository;
            this.levelSetupLogicActions = levelSetupLogicActions;
            this.heroMovementActions = heroMovementActions;
            this.enemyMovementActions = enemyMovementActions;
            this.heroGrabItemsLogicAction = heroGrabItemsLogicAction;

            GenerateStates();
            Link();
        }

        private void GenerateStates()
        {
            stateMachine.RegisterState(LevelLogicState.Setup, SetupState);
            stateMachine.RegisterState(LevelLogicState.Start, StartState);
            stateMachine.RegisterState(LevelLogicState.WaitingForPlayerAction, WaitingForPlayerActionState);
            stateMachine.RegisterState(LevelLogicState.StartTurn, StartTurnState);
            stateMachine.RegisterState(LevelLogicState.PerformTurn, PerformTurnState);
            stateMachine.RegisterState(LevelLogicState.EndTurn, EndTurnState);

            stateMachine.RegisterConnection(LevelLogicState.Setup, LevelLogicState.Start);
            stateMachine.RegisterConnection(LevelLogicState.Start, LevelLogicState.WaitingForPlayerAction);
            stateMachine.RegisterConnection(LevelLogicState.WaitingForPlayerAction, LevelLogicState.StartTurn);
            stateMachine.RegisterConnection(LevelLogicState.StartTurn, LevelLogicState.PerformTurn);
            stateMachine.RegisterConnection(LevelLogicState.PerformTurn, LevelLogicState.EndTurn);
            stateMachine.RegisterConnection(LevelLogicState.EndTurn, LevelLogicState.WaitingForPlayerAction);
        }

        private void Link()
        {
            eventDispatcher.Subscribe((MoveHeroInEvent ev) =>
            {
                stateMachine.Next(LevelLogicState.StartTurn);

                heroMovementActions.MoveHero(ev.Direction);

                stateMachine.Next(LevelLogicState.PerformTurn);
            });

            eventDispatcher.Subscribe((UseItemInEvent ev) =>
            {
              
            });
        }

        public void Start()
        {
            levelSetupLogicActions.Setup();

            stateMachine.Start(LevelLogicState.Start);
        }

        private void SetupState()
        {
            levelSetupLogicActions.Setup();

            stateMachine.Next(LevelLogicState.WaitingForPlayerAction);
        }

        private void StartState()
        {
            stateMachine.Next(LevelLogicState.WaitingForPlayerAction);
        }

        private void WaitingForPlayerActionState()
        {
            
        }

        private void StartTurnState()
        {
            eventDispatcher.Dispatch(new StartTurnOutEvent());
        }

        private void PerformTurnState()
        {
            PerfromHeroTurn();
            PerformEnemiesTurn();

            stateMachine.Next(LevelLogicState.EndTurn);
        }

        private void EndTurnState()
        {
            eventDispatcher.Dispatch(new EndTurnOutEvent());

            stateMachine.Next(LevelLogicState.WaitingForPlayerAction);
        }

        private void PerfromHeroTurn()
        {
            heroGrabItemsLogicAction.HeroTryGrabItem();
        }

        private void PerformEnemiesTurn()
        {
            foreach (EnemyEntity enemyEntity in enemyEntityRepository.Elements)
            {
                IEnemyAction enemyAction = enemyEntity.EnemyBrain.GenerateNextEnemyAction(enemyEntity);

                switch (enemyAction)
                {
                    case AttackEntityEnemyAction action:
                        {

                        }
                        break;

                    case MoveTowardsHeroEnemyAction action:
                        {
                            enemyMovementActions.MoveEnemyTowardsHero(enemyEntity, 1);
                        }
                        break;

                    case IdleEnemyAction action:
                        {

                        }
                        break;
                }
            }
        }
    }
}
