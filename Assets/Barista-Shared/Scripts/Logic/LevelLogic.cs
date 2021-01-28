using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Events;
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
        private readonly ILevelSetupLogicActions levelSetupLogicActions;
        private readonly IHeroMovementActions heroMovementActions;

        private StateMachine<LevelLogicState> stateMachine = new StateMachine<LevelLogicState>();

        public HeroMovementLogic HeroMovementLogic { get; }
        public EnemyMovementLogic EnemyMovementLogic { get; }
        public HeroGrabItemsLogic HeroGrabItemsLogic { get; }

        public LevelLogic(
            IEventDispatcher eventDispatcher,
            ILevelSetupLogicActions levelSetupLogicActions,
            IHeroMovementActions heroMovementActions
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.levelSetupLogicActions = levelSetupLogicActions;
            this.heroMovementActions = heroMovementActions;

            GenerateStates();
            Link();
        }

        private void GenerateStates()
        {
            stateMachine.RegisterState(LevelLogicState.Setup, SetupState);
            stateMachine.RegisterState(LevelLogicState.Start, StartState);
            stateMachine.RegisterState(LevelLogicState.WaitingForPlayerAction, WaitingForPlayerActionState);

            stateMachine.RegisterConnection(LevelLogicState.Setup, LevelLogicState.Start);
            stateMachine.RegisterConnection(LevelLogicState.Start, LevelLogicState.WaitingForPlayerAction);
        }

        private void Link()
        {
            eventDispatcher.Subscribe((MoveHeroInEvent ev) =>
            {
                heroMovementActions.MoveHero(ev.Direction);
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

        //public void StartTurn()
        //{
        //    eventDispatcher.Dispatch(new StartTurnOutEvent());
        //}

        //public void TickTurn()
        //{
        //    foreach(EnemyEntity enemyEntity in enemyEntityRepository.Elements)
        //    {
        //        IEnemyAction enemyAction = enemyEntity.EnemyBrain.GenerateNextEnemyAction(enemyEntity);

        //        switch(enemyAction)
        //        {
        //            case AttackEntityEnemyAction action:
        //                {

        //                }
        //                break;

        //            case MoveTowardsHeroEnemyAction action:
        //                {
        //                    EnemyMovementLogic.MoveEnemyTowardsHero(
        //                        enemyEntity,
        //                        action.HeroEntityToReach,
        //                        1
        //                        );
        //                }
        //                break;

        //            case IdleEnemyAction action:
        //                {

        //                }
        //                break;
        //        }
        //    }
        //}

        //public void EndTurn()
        //{
        //    eventDispatcher.Dispatch(new EndTurnOutEvent());
        //}
    }
}
