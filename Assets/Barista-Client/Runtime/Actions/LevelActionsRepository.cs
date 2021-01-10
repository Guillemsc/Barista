﻿namespace Barista.Client.Actions
{
    public class LevelActionsRepository
    {
        public ISetupLevelAction SetupLevelAction { get; }
        public IUnloadLevelAction UnloadLevelAction { get; }
        public ILevelCompletedAction LevelCompletedAction { get; }
        public IStartTurnAction StartTurnAction { get; }
        public IEndTurnAction EndTurnAction { get; }
        public IHeroMovedAction HeroMovedAction { get; }
        public IEnemyMovedAction EnemyMovedAction { get; }
        public IHeroGrabbedItemAction HeroGrabbedItemAction { get; }

        public LevelActionsRepository(
            ISetupLevelAction setupLevelAction,
            IUnloadLevelAction unloadLevelAction,
            ILevelCompletedAction levelCompletedAction,
            IStartTurnAction startTurnAction,
            IEndTurnAction endTurnAction,
            IHeroMovedAction heroMovedAction,
            IEnemyMovedAction enemyMovedAction,
            IHeroGrabbedItemAction heroGrabbedItemAction
            )
        {
            SetupLevelAction = setupLevelAction;
            UnloadLevelAction = unloadLevelAction;
            LevelCompletedAction = levelCompletedAction;
            StartTurnAction = startTurnAction;
            EndTurnAction = endTurnAction;
            HeroMovedAction = heroMovedAction;
            EnemyMovedAction = enemyMovedAction;
            HeroGrabbedItemAction = heroGrabbedItemAction;
        }
    }
}