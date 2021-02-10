using Barista.Client.Actions;

namespace Barista.Client.ActionsSatates
{
    public class TurnActionsState : IActionsState
    {
        private LevelActionsRepository levelActionsRepository;
        private ILevelCompletedAction levelCompletedAction;
        private IStartTurnAction startTurnAction;
        private IEndTurnAction endTurnAction;
        private IHeroMovedAction heroMovedAction;
        private IEnemyMovedAction enemyMovedAction;
        private IHeroGrabbedItemAction heroGrabbedItemAction;
        private IStartItemTargetSelection startItemTargetSelection;

        public void Init(
            LevelActionsRepository levelActionsRepository,
            ILevelCompletedAction levelCompletedAction,
            IStartTurnAction startTurnAction,
            IEndTurnAction endTurnAction,
            IHeroMovedAction heroMovedAction,
            IEnemyMovedAction enemyMovedAction,
            IHeroGrabbedItemAction heroGrabbedItemAction,
            IStartItemTargetSelection startItemTargetSelection
            )
        {
            this.levelActionsRepository = levelActionsRepository;
            this.levelCompletedAction = levelCompletedAction;
            this.startTurnAction = startTurnAction;
            this.endTurnAction = endTurnAction;
            this.heroMovedAction = heroMovedAction;
            this.enemyMovedAction = enemyMovedAction;
            this.heroGrabbedItemAction = heroGrabbedItemAction;
            this.startItemTargetSelection = startItemTargetSelection;
        }

        public void Enable()
        {
            levelActionsRepository.ClearActions();

            levelActionsRepository.AddAction(levelCompletedAction);
            levelActionsRepository.AddAction(startTurnAction);
            levelActionsRepository.AddAction(endTurnAction);
            levelActionsRepository.AddAction(heroMovedAction);
            levelActionsRepository.AddAction(enemyMovedAction);
            levelActionsRepository.AddAction(heroGrabbedItemAction);
            levelActionsRepository.AddAction(startItemTargetSelection);
        }
    }
}
