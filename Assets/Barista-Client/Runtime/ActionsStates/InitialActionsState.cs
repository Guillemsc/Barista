using Barista.Client.Actions;

namespace Barista.Client.ActionsSatates
{
    public class InitialActionsState : IActionsState
    {
        private LevelActionsRepository levelActionsRepository;
        private ISetupLevelAction setupLevelAction;

        public void Init(
            LevelActionsRepository levelActionsRepositor,
            ISetupLevelAction setupLevelAction
            )
        {
            this.levelActionsRepository = levelActionsRepositor;
            this.setupLevelAction = setupLevelAction;
        }

        public void Enable()
        {
            levelActionsRepository.ClearActions();

            levelActionsRepository.AddAction(setupLevelAction);
        }
    }
}
