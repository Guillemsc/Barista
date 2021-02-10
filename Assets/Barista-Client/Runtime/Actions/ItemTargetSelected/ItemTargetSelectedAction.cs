using Barista.Client.ActionsSatates;
using Barista.Client.Instructions.ActionsState;
using Barista.Client.Instructions.TargetSelector;
using Barista.Client.Timelines;
using Barista.Client.View.Effects.TargetSelector;
using Juce.Core.Containers;
using Juce.Core.Sequencing;

namespace Barista.Client.Actions
{
    public class ItemTargetSelectedAction : IItemTargetSelectedAction
    {
        private readonly LevelTimelines levelTimelines;
        private readonly IActionsState turnActionsState;
        private readonly TargetSelectorViewRepository targetSelectorViewRepository;

        public ItemTargetSelectedAction(
            LevelTimelines levelTimelines,
            IActionsState turnActionsState,
            TargetSelectorViewRepository targetSelectorViewRepository
            )
        {
            this.levelTimelines = levelTimelines;
            this.turnActionsState = turnActionsState;
            this.targetSelectorViewRepository = targetSelectorViewRepository;
        }

        public void Invoke(Int2 gridPosition)
        {
            turnActionsState.Enable();

            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new HideAllTargetSelectorsInstruction(
                targetSelectorViewRepository
                ));

            levelTimelines.MainTimeline.Play(sequence);
        }
    }
}