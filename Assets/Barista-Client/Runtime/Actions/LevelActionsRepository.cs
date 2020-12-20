namespace Barista.Client.Actions
{
    public class LevelActionsRepository
    {
        public ILoadLevelAction LoadLevelAction { get; }
        public IUnloadLevelAction UnloadLevelAction { get; }
        public ILevelCompletedAction LevelCompletedAction { get; }

        public LevelActionsRepository(
            ILoadLevelAction loadLevelAction,
            IUnloadLevelAction unloadLevelAction,
            ILevelCompletedAction levelCompletedAction
            )
        {
            LoadLevelAction = loadLevelAction;
            UnloadLevelAction = unloadLevelAction;
            LevelCompletedAction = levelCompletedAction;
        }
    }
}