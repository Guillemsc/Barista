using Barista.Shared.Logic.States;
using Juce.Core.Events;
using Juce.Core.State;
using Juce.Core.Tickable;

namespace Barista.Shared.Logic
{
    public class LevelLogic : ITickable
    {
        public IEventReceiver EventReceiver { get; }
        public LevelLogicUseCasesRepository UseCases { get; }
        public StateMachine<LevelLogicState> StateMachine { get; }

        public ExpectingItemTargetBoard ExpectingItemTargetBoard { get; }

        public LevelLogic(
            IEventReceiver eventReceiver,
            LevelLogicUseCasesRepository useCases
            )
        {
            EventReceiver = eventReceiver;
            UseCases = useCases;

            StateMachine = new StateMachine<LevelLogicState>();

            ExpectingItemTargetBoard = new ExpectingItemTargetBoard();

            GenerateStates();
        }

        private void GenerateStates()
        {
            // States
            StateMachine.RegisterState(LevelLogicState.Setup, new SetupState(this));
            StateMachine.RegisterState(LevelLogicState.Start, new StartState(this));
            StateMachine.RegisterState(LevelLogicState.WaitingForHeroAction, new WaitingForHeroActionState(this));
            StateMachine.RegisterState(LevelLogicState.WaitingForHeroActionTarget, new WaitingForHeroItemTargetState(this));
            StateMachine.RegisterState(LevelLogicState.TurnStart, new TurnStartState(this));
            StateMachine.RegisterState(LevelLogicState.HeroTurn, new HeroTurnState(this));
            StateMachine.RegisterState(LevelLogicState.EnemiesTurn, new EnemiesTurnState(this));
            StateMachine.RegisterState(LevelLogicState.TurnEnd, new TurnEndState(this));

            // Connections
            StateMachine.RegisterConnection(LevelLogicState.Setup, LevelLogicState.Start);

            StateMachine.RegisterConnection(LevelLogicState.Start, LevelLogicState.WaitingForHeroAction);

            StateMachine.RegisterConnection(LevelLogicState.WaitingForHeroAction, LevelLogicState.TurnStart);
            StateMachine.RegisterConnection(LevelLogicState.WaitingForHeroAction, LevelLogicState.WaitingForHeroActionTarget);

            StateMachine.RegisterConnection(LevelLogicState.WaitingForHeroActionTarget, LevelLogicState.TurnStart);
            StateMachine.RegisterConnection(LevelLogicState.WaitingForHeroActionTarget, LevelLogicState.WaitingForHeroAction);

            StateMachine.RegisterConnection(LevelLogicState.TurnStart, LevelLogicState.HeroTurn);

            StateMachine.RegisterConnection(LevelLogicState.HeroTurn, LevelLogicState.EnemiesTurn);

            StateMachine.RegisterConnection(LevelLogicState.EnemiesTurn, LevelLogicState.TurnEnd);

            StateMachine.RegisterConnection(LevelLogicState.TurnEnd, LevelLogicState.WaitingForHeroAction);
        }

        public void Start()
        {
            StateMachine.Start(LevelLogicState.Setup);
        }

        public void Tick()
        {
            DequeueAllEvents();
        }

        private void DequeueAllEvents()
        {
            while(EventReceiver.EventQueueCount > 0)
            {
                EventReceiver.DequeNext();
            }
        }

        //private void Link()
        //{
        //    eventDispatcher.Subscribe((MoveHeroInEvent ev) =>
        //    {
        //        stateMachine.Next(LevelLogicState.StartTurn);

        //        heroMovementActions.MoveHero(ev.Direction);

        //        stateMachine.Next(LevelLogicState.PerformTurn);
        //    });

        //    eventDispatcher.Subscribe((UseItemInEvent ev) =>
        //    {
        //        bool canUse = CanUseHeroItem(ev.ItemType, out bool needsTarget);

        //        if(!canUse)
        //        {
        //            return;
        //        }

        //        if (needsTarget)
        //        {
        //            GatherItemTarget(ev.ItemType);
        //        }
        //    });

        //    eventDispatcher.Subscribe((ItemTargetSelectedInEvent ev) =>
        //    {
        //        stateMachine.Next(LevelLogicState.StartTurn);

        //        heroItemEffectLogicAction.ApplyItemEffect(ItemType.Sword, ev.GridPosition);

        //        stateMachine.Next(LevelLogicState.PerformTurn);
        //    });
        //}

        //public void Start()
        //{
        //    levelSetupLogicActions.Setup();

        //    stateMachine.Start(LevelLogicState.Start);
        //}

        //private void SetupState()
        //{
        //    levelSetupLogicActions.Setup();

        //    stateMachine.Next(LevelLogicState.WaitingForPlayerAction);
        //}

        //private void StartState()
        //{
        //    stateMachine.Next(LevelLogicState.WaitingForPlayerAction);
        //}

        //private void WaitingForPlayerActionState()
        //{

        //}

        //private void StartTurnState()
        //{
        //    eventDispatcher.Dispatch(new StartTurnOutEvent());
        //}

        //private void PerformTurnState()
        //{
        //    PerfromHeroTurn();
        //    PerformEnemiesTurn();

        //    stateMachine.Next(LevelLogicState.EndTurn);
        //}

        //private void EndTurnState()
        //{
        //    eventDispatcher.Dispatch(new EndTurnOutEvent());

        //    stateMachine.Next(LevelLogicState.WaitingForPlayerAction);
        //}

        //private bool CanUseHeroItem(ItemType itemType, out bool needsTarget)
        //{
        //    needsTarget = heroItemEffectLogicAction.ItemEffectNeedsTarget(itemType);

        //    if (needsTarget)
        //    {
        //        IReadOnlyList<Int2> targets = heroItemEffectLogicAction.GetItemAvaliableTargets(itemType);

        //        if (targets.Count > 0)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //private void GatherItemTarget(ItemType itemType)
        //{
        //    bool needsTarget = heroItemEffectLogicAction.ItemEffectNeedsTarget(itemType);

        //    if (needsTarget)
        //    {
        //        IReadOnlyList<Int2> targets = heroItemEffectLogicAction.GetItemAvaliableTargets(itemType);

        //        if (targets.Count > 0)
        //        {
        //            eventDispatcher.Dispatch(new ItemNeedsTargetSelectionOutEvent(itemType, targets));
        //        }
        //    }
        //}

        //private void PerfromHeroTurn()
        //{
        //    heroGrabItemsLogicAction.HeroTryGrabItem();
        //}

        //private void PerformEnemiesTurn()
        //{
        //    foreach (EnemyEntity enemyEntity in enemyEntityRepository.Elements)
        //    {
        //        IEnemyAction enemyAction = enemyEntity.EnemyBrain.GenerateNextEnemyAction(enemyEntity);

        //        switch (enemyAction)
        //        {
        //            case AttackEntityEnemyAction action:
        //                {

        //                }
        //                break;

        //            case MoveTowardsHeroEnemyAction action:
        //                {
        //                    enemyMovementActions.MoveEnemyTowardsHero(enemyEntity, 1);
        //                }
        //                break;

        //            case IdleEnemyAction action:
        //                {

        //                }
        //                break;
        //        }
        //    }
        //}
    }
}
