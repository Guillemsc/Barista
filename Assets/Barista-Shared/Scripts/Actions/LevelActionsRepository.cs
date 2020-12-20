namespace Barista.Shared.Actions
{
    public class LevelActionsRepository
    {
        public ISetupLevelAction SetupLevelAction { get; }
        public ILevelLostAction LevelLostAction { get; }
        public ILevelCompletedAction LevelCompletedAction { get; }

        public LevelActionsRepository(
            ISetupLevelAction setupLevelAction,
            ILevelLostAction levelLostAction,
            ILevelCompletedAction levelCompletedAction
            )
        {
            SetupLevelAction = setupLevelAction;
            LevelLostAction = levelLostAction;
            LevelCompletedAction = levelCompletedAction;
        }
    }
}