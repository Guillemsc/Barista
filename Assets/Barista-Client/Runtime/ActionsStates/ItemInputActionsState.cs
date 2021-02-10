using Barista.Client.Actions;

namespace Barista.Client.ActionsSatates
{
    public class ItemInputActionsState : IActionsState
    {
        private LevelActionsRepository levelActionsRepository;
        private IItemTargetSelectedAction itemTargetSelectedAction;

        public void Init(
            LevelActionsRepository levelActionsRepository,
            IItemTargetSelectedAction itemTargetSelectedAction
            )
        {
            this.levelActionsRepository = levelActionsRepository;
            this.itemTargetSelectedAction = itemTargetSelectedAction;
        }

        public void Enable()
        {
            levelActionsRepository.ClearActions();

            levelActionsRepository.AddAction(itemTargetSelectedAction);
        }
    }
}
