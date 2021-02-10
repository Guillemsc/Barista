using Barista.Client.Actions;

namespace Barista.Client.ActionsSatates
{
    public class ItemInputActionsState : IActionsState
    {
        private LevelActionsRepository levelActionsRepository;

        public void Init(
            LevelActionsRepository levelActionsRepository
            )
        {
            this.levelActionsRepository = levelActionsRepository;
        }

        public void Enable()
        {
            levelActionsRepository.ClearActions();
        }
    }
}
