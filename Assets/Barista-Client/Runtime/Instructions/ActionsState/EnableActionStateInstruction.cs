using Barista.Client.ActionsSatates;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.ActionsState
{
    public class EnableActionStateInstruction : InstantInstruction
    {
        private readonly IActionsState actionState;

        public EnableActionStateInstruction(IActionsState actionState)
        {
            this.actionState = actionState;
        }

        protected override void OnInstantStart()
        {
            actionState.Enable();
        }
    }
}